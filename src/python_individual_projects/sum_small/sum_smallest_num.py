import numpy as np

def sum_two_smallest_numbers(numbers):
    sorted_array = np.sort(numbers)
    return sorted_array[0] + sorted_array[1]
