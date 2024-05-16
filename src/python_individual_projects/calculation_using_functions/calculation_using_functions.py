from functools import partial

def zero(arg = ""): 
    return eval("0" + arg)
def one(arg = ""):
    return eval("1" + arg)
def two(arg = ""): 
    return eval("2" + arg)
def three(arg = ""): 
    return eval("3" + arg)
def four(arg = ""): 
    return eval("4" + arg)
def five(arg = ""):
    return eval("5" + arg)
def six(arg = ""): 
    return eval("6" + arg)
def seven(arg = ""): 
    return eval("7" + arg)
def eight(arg = ""): 
    return eval("8" + arg)
def nine(arg = ""): 
    return eval("9" + arg)

def plus(n):
    return "+" + str(n)
def minus(n): 
    return "-" + str(n)
def times(n): 
    return "*" + str(n)
def divided_by(n): 
    return "//" + str(n)


# Solution with lambdas:
# def zero(func=None): return 0 if not func else func(0)
# def one(func=None): return 1 if not func else func(1)
# def two(func=None): return 2 if not func else func(2)
# def three(func=None): return 3 if not func else func(3)
# def four(func=None): return 4 if not func else func(4)
# def five(func=None): return 5 if not func else func(5)
# def six(func=None): return 6 if not func else func(6)
# def seven(func=None): return 7 if not func else func(7)
# def eight(func=None): return 8 if not func else func(8)
# def nine(func=None): return 9 if not func else func(9)


# def plus(b): return lambda a: a + b
# def minus(b): return lambda a: a - b
# def times(b): return lambda a: a * b
# def divided_by(b): return lambda a: int(a / b)

def lambda_method(a, b):
    lambda a, b: a + b

# def how_do_delegates():
#     add_five = partial(plus, 5)
#     result = add_five(3)
#     print(result)
#     return result

# def test_partial(operation, operand):
#     add_five = partial(operation, operand)
#     result = add_five(one())
#     print(result)
#     return result

# test_partial(plus, five())

# def test_one(operation = None):
#     if (operation is None):
#         return 1
#     partial_method = partial(operation)
#     result = partial_method()

# def test_plus(operation):
#     partial_method = partial(operation)
#     result = partial_method()
#     return result

# add = lambda x, y: x + y

print (one(plus(five())))
