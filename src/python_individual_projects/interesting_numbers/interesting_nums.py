def is_interesting(number, awesome_phrases):
    interesting_or_not = 0
    first_bool = True
    boolean_list = []
    for i in range(3):
        if (len(str(number)) == 1 and not number_exists(number, awesome_phrases)):
            continue
        boolean_list.append(followed_by_zeros(number))
        boolean_list.append(every_digit_same(number))
        boolean_list.append(sequential_digits_increment(number))
        boolean_list.append(sequential_digits_decrement(number))
        boolean_list.append(palindrome_digits(number))
        boolean_list.append(number_exists(number, awesome_phrases))

        if (True in boolean_list and first_bool):
            return 2
        elif (True in boolean_list and not first_bool):
            interesting_or_not = 1
        else:
            first_bool = False
        boolean_list = []
        number += 1
    return interesting_or_not

def number_exists(number, awesome_phrases):
    for phrase in awesome_phrases:
        if (number == phrase):
            return True
    return False

def palindrome_digits(number):
    str_number = str(number)
    if len(str_number) == 1 or len(str_number) == 2: return False
    negative_count = -1
    count = int(len(str_number) / 2)
    for i in range(count):
        first_num = int(str_number[i])
        last_num = int(str_number[negative_count])
        if first_num != last_num:
            return False
        negative_count -= 1
    return True

def sequential_digits_decrement(number):
    str_number = str(number)
    if len(str_number) == 1 or len(str_number) == 2: return False
    for z in range(len(str_number) -1):
        test_int = int(str_number[z])
        test_int_two = int(str_number[z + 1])
        if test_int == 1 and test_int_two == 0:
            continue
        elif test_int -1 != test_int_two:
            return False
    return True

def sequential_digits_increment(number):
    str_number = str(number)
    if len(str_number) == 1 or len(str_number) == 2: return False
    for z in range(len(str_number) -1):
        test_int = int(str_number[z])
        test_int_two = int(str_number[z + 1])
        if test_int == 9 and test_int_two == 0:
            continue
        elif test_int + 1 != test_int_two:
            return False
    return True

def every_digit_same(number):
    str_number = str(number)
    if len(str_number) == 1 or len(str_number) == 2: return False
    if len(set(str_number)) == 1: return True
    return False

def followed_by_zeros(number):
    str_number = str(number)
    if len(str_number) == 1 or len(str_number) == 2: return False
    if len(str_number.rstrip('0')) == 1: return True
    return False

is_interesting(1336, [1337, 256])