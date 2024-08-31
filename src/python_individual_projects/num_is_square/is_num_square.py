import math

def is_square(n):
    if n <= -1:
        return False
    if n == 0:
        return True
    square = math.sqrt(n)
    if n % square == 0:
        return True
    return False

answer = is_square(-1)
print(answer)