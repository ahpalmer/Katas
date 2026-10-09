These katas focus on the presentation and infrastructure layers of clean coding. This will help fill some gaps in my knowledge with respect to building an end-to-end application

My suggestion: do TypeScript + React for these katas. Since you’ve already got Blazor going, treat it as a side project where you reuse the same concepts, and you’ll quickly see which ideas are universal and which are framework-specific.
20 morning katas: Presentation + Infrastructure
Each is sized for roughly 30–90 minutes, with a “break it” step like your original list.

Presentation (TypeScript + React)
1.	Hello, typed component. Scaffold a Vite + React + TS app. Build a component with typed props, then pass the wrong prop type and read the compiler error.
2.	State and lists. Build a todo list with useState. Break it by mutating state directly and watch the UI fail to update.
3.	Fetch and render. Call a public API from a useEffect. Break it by navigating away mid-request, and fix the race with an abort controller.
4.	Typed API boundary. Write a typed fetch wrapper. Break it by returning a field the type doesn’t expect, then add runtime validation (e.g., Zod) and see the difference.
5.	Forms and validation. Build a form with client-side validation and error states. Break it with weird input: empty, huge, emoji, pasted HTML.
6.	Routing. Build a three-page app with React Router, including a 404 page. Break it by deep-linking and refreshing on a nested route.
7.	Loading, error, and empty states. Take kata 3’s UI and make all three states real. Throttle the network in dev tools to see them.
8.	Context vs. prop drilling. Share a theme or user across five levels of components. Count the re-renders each approach causes.
9.	Auth flow UI. Build a login page that calls your own Azure Function and stores a token. Break it by letting the token expire mid-session.
10.	Component tests. Write tests with Vitest and React Testing Library. Break a component deliberately and confirm a test catches it.

Hosting and Infrastructure
11.	Deploy a static site. Put your React build on Azure Static Web Apps. Break it with a bad route config and see what the host returns.
12.	Stand up a database. Run Postgres or SQL Server locally in Docker and create tables by hand. Break it by violating a constraint on purpose.
13.	Provision Cosmos DB. Use the emulator or a free-tier account to create a container and choose a partition key. Break it with a bad partition key and compare RU costs.
14.	Infrastructure as code. Write a Bicep file for a storage account plus a Function App. Break it with a naming conflict or a missing dependency, and redeploy to see idempotency.
15.	Docker a service. Containerize a small API with a Dockerfile. Break it by forgetting to expose a port or set an environment variable.
16.	CORS and HTTPS. Make your React app on one origin call your Function on another. Cause the CORS error on purpose, then fix it properly rather than with *.
17.	CI/CD pipeline. Build a pipeline that tests and deploys your app on push. Break it with a failing test and confirm nothing ships.
18.	Secrets and config. Move secrets into Key Vault with managed identity and remove them from source. Break it by revoking the identity’s access.
19.	Migrations. Create a schema migration for a small database and then roll it back. Break it with a migration that can’t be reversed, and see why that matters.
20.	Observability. Add structured logs and a trace to your app plus Function, then query them in Application Insights with KQL. Break something and find it from the logs alone.

Weekend capstone: combine them into one app. A React + TS front end on Static Web Apps calls your Functions, which store data in a database you provisioned with Bicep, deployed through a pipeline with secrets in Key Vault. That one project touches all four layers end to end.

Doing katas 12–14 early would help most with your database gap. They’re the closest to the ETag work you’ve already done.

