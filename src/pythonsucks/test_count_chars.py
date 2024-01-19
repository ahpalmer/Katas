from count_chars import count_characters

def test_basic_tests():
    assert count_characters('aba') == {'a': 2, 'b': 1}
    assert count_characters('') == {}
    assert count_characters('aa') == {'a' : 2}
    assert count_characters('aabb') == {'b' : 2, 'a' : 2}