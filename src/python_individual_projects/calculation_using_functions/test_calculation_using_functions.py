import calculation_using_functions.calculation_using_functions as cuf

def test_isFrameworkWorking():
    assert cuf.plus(1, 2) == 3

def test_first():
    assert cuf.how_do_delegates() == 8

def test_numbers():
    assert cuf.one(cuf.plus(cuf.one)) == 2

def test_add():
    assert cuf.five(cuf.plus(cuf.five)) == 10
    assert cuf.zero(cuf.plus(cuf.six)) == 6
    assert cuf.one(cuf.plus(cuf.two)) == 3

def test_subtract():
    assert cuf.five(cuf.subtract(cuf.five)) == 0
    assert cuf.zero(cuf.subtract(cuf.six)) == -6
    assert cuf.seven(cuf.subtract(cuf.two)) == 5
    assert cuf.nine(cuf.subtract(cuf.eight)) == 1

def test_multiply():
    assert cuf.five(cuf.multiply(cuf.five)) == 25
    assert cuf.zero(cuf.multiply(cuf.six)) == 0
    assert cuf.one(cuf.multiply(cuf.two)) == 2
    assert cuf.four(cuf.multiply(cuf.three)) == 12

def test_divide():
    assert cuf.five(cuf.divide(cuf.five)) == 1
    assert cuf.zero(cuf.divide(cuf.six)) == 0
    assert cuf.one(cuf.divide(cuf.two)) == 0.5
    assert cuf.ten(cuf.divide(cuf.five)) == 2
    assert cuf.nine(cuf.divide(cuf.three)) == 3
