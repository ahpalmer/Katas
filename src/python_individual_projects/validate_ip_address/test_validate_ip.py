import validate_ip

def test_validate_ip():
    ip = "100.1.23.69"
    assert validate_ip.validate_ip(ip) == True
    assert validate_ip.validate_ip("1.1.1.1") == True
    assert validate_ip.validate_ip("255.23.255.1") == True

def test_validate_ip_bad_ip():
    assert validate_ip.validate_ip("01.101.1.1") == False
    assert validate_ip.validate_ip("256.1.1.1") == False
    assert validate_ip.validate_ip("200.13.3.23.50") == False