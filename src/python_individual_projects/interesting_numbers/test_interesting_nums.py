import interesting_nums as interesting

def test_is_interesting():
    awesome_phrases = [1337, 256]
    assert interesting.is_interesting(3, awesome_phrases) == 0
    assert interesting.is_interesting(1336, awesome_phrases) == 1
    assert interesting.is_interesting(1337, awesome_phrases) == 2
    assert interesting.is_interesting(11208, awesome_phrases) == 0
    assert interesting.is_interesting(11209, awesome_phrases) == 1
    assert interesting.is_interesting(11211, awesome_phrases) == 2
    assert interesting.is_interesting(30, awesome_phrases) == 0
    assert interesting.is_interesting(88, awesome_phrases) == 0
    assert interesting.is_interesting(97, awesome_phrases) == 0

def test_number_exists():
    num = 1337
    awesome_phrases = [420, 1234567, 1337]
    assert interesting.number_exists(num, awesome_phrases) == True
    awesome_phrases = [1]
    assert interesting.number_exists(num, awesome_phrases) == False

def test_palindrom_digits():
    n = 123321
    assert interesting.palindrome_digits(n) == True
    n = 1234321
    assert interesting.palindrome_digits(n) == True
    n = 12345677
    assert interesting.palindrome_digits(n) == False

def test_sequential_digits_decrement():
    n = 4321
    assert interesting.sequential_digits_decrement(n) == True
    n = 126675438
    assert interesting.sequential_digits_decrement(n) == False
    n = 43210
    assert interesting.sequential_digits_decrement(n) == True
    n = 432109
    assert interesting.sequential_digits_decrement(n) == False

def test_sequential_digits_increment():
    n = 1234
    assert interesting.sequential_digits_increment(n) == True
    n = 12567
    assert interesting.sequential_digits_increment(n) == False
    n = 67890

def test_every_digit_same():
    n = 2222
    assert interesting.every_digit_same(n) == True
    n = 1110
    assert interesting.every_digit_same(n) == False
    n = 1109
    assert interesting.every_digit_same(n) == False
    n = 5234
    assert interesting.every_digit_same(n) == False

def test_followed_by_zeros():
    n = 1000
    assert interesting.followed_by_zeros(n) == True
    n = 999
    assert interesting.followed_by_zeros(n) == False
    n = 998
    assert interesting.followed_by_zeros(n) == False
    n = 997
    assert interesting.followed_by_zeros(n) == False