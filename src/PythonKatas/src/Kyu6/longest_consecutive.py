def longest_consecutive(strarr, k):
    if k == 0 or k < 0 or strarr == []:
        return ""
    elif len(strarr) < k:
        return ""
    elif len(strarr) == k:
        concatenated_string = ''.join(strarr)
        return concatenated_string
    
    answer = ""
    for i in range (len(strarr) - k + 1):
        concatenated_array = strarr[i:(i + k)]
        concatenated_string = ''.join(concatenated_array)
        if len(concatenated_string) > len(answer):
            answer = concatenated_string
    return answer

print(longest_consecutive(["zone", "abigail", "theta", "form", "libe", "zas"], 2)) 