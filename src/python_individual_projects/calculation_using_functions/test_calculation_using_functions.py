import calculation_using_functions.calculation_using_functions as cuf

# def test_isFrameworkWorking():
#     assert cuf.plus(1, 2) == 3

# def test_first():
#     assert cuf.how_do_delegates() == 8

def test_numbers():
    assert cuf.one(cuf.plus(cuf.one())) == 2

def test_add():
    assert cuf.five(cuf.plus(cuf.five())) == 10
    assert cuf.zero(cuf.plus(cuf.six())) == 6
    assert cuf.one(cuf.plus(cuf.two())) == 3

def test_subtract():
    assert cuf.zero(cuf.minus(cuf.six())) == -6
    assert cuf.seven(cuf.minus(cuf.two())) == 5
    assert cuf.nine(cuf.minus(cuf.eight())) == 1
    assert cuf.five(cuf.minus(cuf.five())) == 0

def test_multiply():
    assert cuf.five(cuf.times(cuf.five())) == 25
    assert cuf.zero(cuf.times(cuf.six())) == 0
    assert cuf.one(cuf.times(cuf.two())) == 2
    assert cuf.four(cuf.times(cuf.three())) == 12

def test_divide():
    assert cuf.five(cuf.divided_by(cuf.five())) == 1
    assert cuf.zero(cuf.divided_by(cuf.six())) == 0
    assert cuf.one(cuf.divided_by(cuf.two())) == 0
    assert cuf.nine(cuf.divided_by(cuf.three())) == 3
