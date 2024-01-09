def series_sum(n):
    count = 1
    answer = 0
    for i in range(n):
        answer = answer + (1/count)
        count += 3
    answerString = "This is the answer: {answer:.2f}".format(answer = answer)
    print(answerString)
    return "{:.2f}".format(answer)

series_sum(5)