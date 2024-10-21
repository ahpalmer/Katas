import divisors_script as div

def test_divisors():
    assert div.divisors(15) == [3, 5]
    assert div.divisors(253) == [11, 23]
    assert div.divisors(24) == [2, 3, 4, 6, 8, 12]    
    assert div.divisors(25) == [5]
    assert div.divisors(13) == "13 is prime"
    assert div.divisors(3) == "3 is prime"
