What is a container? What is an image? Where can I look at these files on my computer? I want a technical description of what these things are. If you can remove a stopped container and it retains the data, then where is the data? What is the point of a container if it doesn't control the existence of data?
Let me try to explain first: a container is specific to docker, and it represents a tiny, local VM running on linux (not exactly a VM, more like a VM-lite). However, the data exists on the local machine in a different place, maybe in a volume file that stays dormant until the container is spun up. In order to access the database, you spin up the tiny, local VM that contains the mongo database or the postgres database, and then once it's spun up docker automatically mounts that volume to the VM which allows you to access the data

Answer from ChatGPT:
Thought for 3s
I need to explain the correct Linux processes and not just focus on the Docker Desktop VM, which is shared with the Windows host. Paths for ext4 sparse vhdx files can vary, so I should inspect the Docker info and the volume inspect paths. I'm pondering on finding the user’s actual machine location to discover the default AppData for Docker on WSL and understanding the limited exact directories through a method like getchild. Let’s explore those specifics!

Shell Locate Docker Linux storage and PostgreSQL volume backing disk 4 lines…
docker info --format 'OS: {{.OperatingSystem}}; Kernel: {{.KernelVersion}}; Docker root: {{.DockerRootDir}}; Storage driver: {{.Driver}}'; docker volume inspect lss2foreignke…

Read compose.local.yaml 21 lines read

Task complete 
Your model is close. The key correction is that the container isn’t a tiny VM—it’s an isolated process, or group of processes. On your Windows machine, Docker runs those processes inside a shared Linux environment provided by WSL 2.

Image: the packaged software

An image is a read-only filesystem template plus configuration describing how to run it.

Your  postgres:17  image contains PostgreSQL executables, supporting Linux libraries, startup scripts, and default configuration. It does not contain the users and orders we inserted later.

Technically, images consist of filesystem layers plus metadata. Docker downloads and stores those layers locally, and multiple containers can share them. The image itself doesn’t run; Docker creates a container from it.

Container: the isolated running environment

A container combines:

 - The image’s read-only filesystem layers.
 - A private writable filesystem layer.
 - Configuration such as environment variables, networking, and volume mounts.
 - Processes when the container is running.

Linux namespaces isolate things such as process IDs, networking, and filesystem mounts. Control groups, or cgroups, track and can limit resources such as CPU and memory.

Unlike a VM, a Linux container does not boot its own kernel. Containers share the Linux host’s kernel. Containers also aren’t exclusive to Docker; other container runtimes use these mechanisms.

A stopped container still has its configuration and writable layer, but its processes aren’t running. Removing it deletes that container’s writable layer and configuration.

Volume: separately managed persistent storage

A named volume is a Docker-managed directory with a lifecycle separate from a container. It is not another VM, and it doesn’t run anything.

Your Compose file mounts that storage at PostgreSQL’s data directory:

 volumes:
   - postgres-data:/var/lib/postgresql/data

Inside the container, PostgreSQL accesses  /var/lib/postgresql/data  as an ordinary directory. But writes there go to the volume, not to the container’s private writable layer.

That directory contains PostgreSQL’s actual database files: table and index storage, write-ahead logs, and other database state. These aren’t simply SQL scripts or JSON documents.

Your “Docker mounts the storage so the database can access it” explanation is correct. The adjustment is that Docker mounts it into the container’s filesystem view—not into a separate VM belonging to that container.

Where this is on your computer

I checked your setup. The storage arrangement is:

 Windows computer
   Docker Desktop's WSL 2 Linux environment
     PostgreSQL container: isolated database processes
       /var/lib/postgresql/data
         mounted from the named volume
     Named volume: lss2foreignkey_postgres-data
       persistent PostgreSQL files

Docker reports the volume directory inside Linux as:

 /var/lib/docker/volumes/lss2foreignkey_postgres-data/_data

That is not a normal Windows Explorer path. On your machine, Docker Desktop’s data disk exists at:

 C:\Users\ahpal\AppData\Local\Docker\wsl\disk\docker_data.vhdx

The  .vhdx  is a virtual disk containing a Linux filesystem, not a PostgreSQL-only file. Docker stores managed data inside that Linux storage rather than exposing each database file as an ordinary Windows file.

For supported inspection, open Docker Desktop → Volumes →  lss2foreignkey_postgres-data . Its data/file browser, if available in your version, lets you inspect the contents. Use Images to inspect the downloaded images. Don’t edit the database files or the virtual disk directly; query PostgreSQL or use its backup tools instead.

Why have a container if the data exists separately?

The container controls the execution environment; the volume controls persistent storage. Separating them lets you replace the database software’s container without throwing away the database.

┌────────────────────────────────────────────┬─────────────────────────────┬────────────────────────────────────────┐
│ Action                                     │ Container                   │ Your volume’s data                     │
├────────────────────────────────────────────┼─────────────────────────────┼────────────────────────────────────────┤
│ Stop container                             │ Retained, processes stopped │ Retained                               │
├────────────────────────────────────────────┼─────────────────────────────┼────────────────────────────────────────┤
│ Remove container                           │ Removed                     │ Retained                               │
├────────────────────────────────────────────┼─────────────────────────────┼────────────────────────────────────────┤
│ Create a replacement using the same volume │ New container               │ Existing data becomes accessible again │
├────────────────────────────────────────────┼─────────────────────────────┼────────────────────────────────────────┤
│ Delete the volume                          │ Separate operation          │ Database files deleted                 │
└────────────────────────────────────────────┴─────────────────────────────┴────────────────────────────────────────┘

A container can control the lifetime of data: files written only into its private writable layer disappear when you remove it. We deliberately put PostgreSQL’s data in a volume to avoid that.

So your refined mental model is: the image supplies the software, the container runs it in isolation, and the volume holds the database files independently of that particular container.