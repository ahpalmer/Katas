def smash(words):
    answer = ''
    for string in words:
        answer += string + ' '
    
    return answer[:-1]

answer = smash(['hello', 'world', 'this', 'is', 'a', 'test'])

print(answer)