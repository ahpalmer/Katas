import sys
from pathlib import Path
sys.path.insert(0, str(Path(__file__).resolve().parent.parent / 'src'))

from Kyu6.find_nb import find_nb

def test_basic_cases():
    assert find_nb(4) == -1
    assert find_nb(16) == -1
    assert find_nb(4183059834009) == 2022
    assert find_nb(24723578342962) == -1
    assert find_nb(135440716410000) == 4824
    assert find_nb(40539911473216) == 3568
    assert find_nb(26825883955641) == 3218