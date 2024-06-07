import sum_smallest_num as ssum

def test_sum_two_smallest_numbers():
    test1 = [5, 8, 12, 18, 22]
    test2 = [7, 15, 12, 18, 22]
    test3 = [25, 42, 12, 18, 22]
    assert ssum.sum_two_smallest_numbers(test1) == 13
    assert ssum.sum_two_smallest_numbers(test2) == 19
    assert ssum.sum_two_smallest_numbers(test3) == 30