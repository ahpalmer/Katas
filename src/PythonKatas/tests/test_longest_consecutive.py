import sys
from pathlib import Path
sys.path.insert(0, str(Path(__file__).resolve().parent.parent / 'src'))

from Kyu6.longest_consecutive import longest_consecutive

def test_longest_consecutive():
    assert(longest_consecutive(["zone", "abigail", "theta", "form", "libe", "zas"], 2) == "abigailtheta")
    assert(longest_consecutive(["ejjjjmmtthh", "zxxuueeg", "aanlljrrrxx", "dqqqaaabbb", "oocccffuucccjjjkkkjyyyeehh"], 1) == "oocccffuucccjjjkkkjyyyeehh")
    assert(longest_consecutive([], 3) == "")
    assert(longest_consecutive(["itvayloxrp","wkppqsztdkmvcuwvereiupccauycnjutlv","vweqilsfytihvrzlaodfixoyxvyuyvgpck"], 2) == "wkppqsztdkmvcuwvereiupccauycnjutlvvweqilsfytihvrzlaodfixoyxvyuyvgpck")
    assert(longest_consecutive(["wlwsasphmxx","owiaxujylentrklctozmymu","wpgozvxxiu"], 2) == "wlwsasphmxxowiaxujylentrklctozmymu")
    assert(longest_consecutive(["zone", "abigail", "theta", "form", "libe", "zas"], -2) == "")
    assert(longest_consecutive(["it","wkppv","ixoyx", "3452", "zzzzzzzzzzzz"], 3) == "ixoyx3452zzzzzzzzzzzz")
    assert(longest_consecutive(["it","wkppv","ixoyx", "3452", "zzzzzzzzzzzz"], 15) == "")
    assert(longest_consecutive(["it","wkppv","ixoyx", "3452", "zzzzzzzzzzzz"], 0) == "")