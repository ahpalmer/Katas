from functools import partial

def zero(): pass #your code here
def one(): pass #your code here
def two(): pass #your code here
def three(): pass #your code here
def four(): pass #your code here
def five(): pass #your code here
def six(): pass #your code here
def seven(): pass #your code here
def eight(): pass #your code here
def nine(): pass #your code here

def plus(a, b):
    return a + b
def minus(a, b): 
    return a - b
def times(a, b): 
    return a * b
def divided_by(a, b): 
    return a / b


def how_do_delegates():
    add_five = partial(plus, 5)
    result = add_five(3)
    print(result)
    return result

how_do_delegates()