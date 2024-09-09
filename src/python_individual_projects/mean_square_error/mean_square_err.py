import math

def solution(array_a, array_b):
    count = 0
    my_list = []    
    for number in array_a:
        difference = array_b[count] - number
        my_list.append(math.pow(difference, 2))
        count += 1
    return sum(my_list) / (count)

print("working2")
array_a = [1, 2, 3]
array_b = [4, 5, 6]
print(solution(array_a, array_b))

# better answer:
# from sklearn.metrics import mean_squared_error

# def solution(array_a, array_b):
#     return mean_squared_error(array_a,array_b)