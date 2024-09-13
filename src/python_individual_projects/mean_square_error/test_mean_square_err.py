import mean_square_err as mse

def test_solution():
    array_a = [1, 2, 3]
    array_b = [4, 5, 6]
    assert mse.solution(array_a, array_b) == 3