import is_num_square as ins

def test_is_square():
    assert ins.is_square(-1) == False
    assert ins.is_square(0) == True
    assert ins.is_square(3) == False
    assert ins.is_square(4) == True
    assert ins.is_square(25) == True
    assert ins.is_square(26) == False
