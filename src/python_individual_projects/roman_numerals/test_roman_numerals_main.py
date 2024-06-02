import roman_numerals_main as rmn

def test_find_from_roman_answer():
    input_list_converted = [1000, 100, 1000, 10, 100, 1, 5]
    assert rmn.RomanNumerals.find_from_roman_answer(input_list_converted) == 1994

def test_to_roman():
    normal_numeral = 1990
    assert rmn.RomanNumerals.to_roman(normal_numeral) == 'MCMXC'
    assert rmn.RomanNumerals.to_roman(1) == 'I'
    assert rmn.RomanNumerals.to_roman(2008) == 'MMVIII'
    assert rmn.RomanNumerals.to_roman(1666) == 'MDCLXVI'

def test_from_roman():
    assert rmn.RomanNumerals.from_roman('MCMXC') == 1990
    assert rmn.RomanNumerals.from_roman('I') == 1
    assert rmn.RomanNumerals.from_roman('XXI') == 21
    assert rmn.RomanNumerals.from_roman('MMVIII') == 2008
    assert rmn.RomanNumerals.from_roman('MDCLXVI') == 1666
    assert rmn.RomanNumerals.from_roman('M') == 1000

def test_roman_numeral_dict():
    dict = {
            'I': 1,
            'V': 5,
            'X': 10,
            'L': 50,
            'C': 100,
            'D': 500,
            'M': 1000
        }
    assert rmn.RomanNumerals.roman_numeral_dict() == dict, "The dictionaries are not identical"

def test_reverse_roman_numeral_dict():
    dict = {
            1: 'I',
            5: 'V',
            10: 'X',
            50: 'L',
            100: 'C',
            500: 'D',
            1000: 'M'
        }
    assert rmn.RomanNumerals.reverse_roman_numeral_dict() == dict, "The dictionaries are not identical"
