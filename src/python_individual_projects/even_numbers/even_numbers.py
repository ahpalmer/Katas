def even_numbers(arr,n):
    temp_list = []
    for i in arr:
        if i % 2 == 0:
            temp_list.append(i)
    temp_list.reverse()
    answer_list = temp_list[:n]
    answer_list.reverse()
    return answer_list

list = even_numbers([1,2,3,4,5,6,7,8,9,10], 3)

print(list)