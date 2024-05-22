def domain_name(url):
    if url.startswith("http://"):
        url = url[7:]
    elif url.startswith("https://"):
        url = url[8:]

    if url.startswith("www."):
        url = url[4:]
    
    cutoff_location = url.find(".")
    url = url[:cutoff_location]
    print(url)
    return url

domain_name("http://www.google.com")