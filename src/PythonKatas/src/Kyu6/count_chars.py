def count_characters(s):
    answer_dict = {}
    if s == "":
        return answer_dict
    for char in s:
        if char in answer_dict:
            answer_dict[char] += 1
        else:
            answer_dict[char] = 1
    return answer_dict