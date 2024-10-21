# Time: 25 Minutes

def divisors(integer):
    list = []
    for i in range (2, integer):
        if integer % i == 0:
            list.append(i)
    if len(list) == 0:
        return f"{integer} is prime"
    return list

print(divisors(13))