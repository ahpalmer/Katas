import reverse_words_script as rws

def test_reverse_words_script():
    assert rws.reverse_words("The quick brown fox jumps over the lazy dog.") == "ehT kciuq nworb xof spmuj revo eht yzal .god"

def test_reverse_words_script_double_space():
    assert rws.reverse_words("double  spaced  words") == "elbuod  decaps  sdrow"