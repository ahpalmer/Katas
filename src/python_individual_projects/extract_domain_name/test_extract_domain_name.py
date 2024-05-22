import extract_domain_name

def test_extract_domain_name():
    url = "http://www.google.com"
    assert extract_domain_name.domain_name(url) == "google"
    assert extract_domain_name.domain_name("http://www.yahoo.com") == "yahoo"
    assert extract_domain_name.domain_name("http://www.facebook.com") == "facebook"

def test_extract_domain_n_nameodewars():
    assert extract_domain_name.domain_name("http://google.co.jp") == "google"
    assert extract_domain_name.domain_name("http://google.com") == "google"
    assert extract_domain_name.domain_name("https://123.net") == "123"
    assert extract_domain_name.domain_name("https://hyphen-site.org") == "hyphen-site"
    assert extract_domain_name.domain_name("http://codewars.com") == "codewars"
    assert extract_domain_name.domain_name("www.xakep.ru") == "xakep"
    assert extract_domain_name.domain_name("https://youtube.com") == "youtube"
    assert extract_domain_name.domain_name("http://www.codewars.com/kata/") == "codewars"
    assert extract_domain_name.domain_name("icann.org") == "icann"