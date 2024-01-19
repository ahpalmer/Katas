import sys
from pathlib import Path
sys.path.insert(0, str(Path(__file__).resolve().parent.parent / 'src'))

from Kyu7.series_sum import series_sum

def test_series_sum():
    assert series_sum(1) == "1.00"
    assert series_sum(2) == "1.25"
    assert series_sum(3) == "1.39"