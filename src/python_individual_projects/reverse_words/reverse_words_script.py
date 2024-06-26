import re

def reverse_words(text):
    result = re.sub(r'\b\S+', replace_with_reverse, text)
    print(result)
    return result


def replace_with_reverse(match):
    word = match.group(0)
    return word[::-1]

reverse_words("The quick brown fox jumps over the lazy dog.")