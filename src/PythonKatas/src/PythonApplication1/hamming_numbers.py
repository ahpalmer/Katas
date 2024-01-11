def generate_hamming_numbers(n):
    hamming_numbers = [1]
    i2, i3, i5 = 0, 0, 0
    
    while len(hamming_numbers) < n:
        next_hamming = min(hamming_numbers[i2] * 2, hamming_numbers[i3] * 3, hamming_numbers[i5] * 5)
        
        if next_hamming == hamming_numbers[i2] * 2:
            i2 += 1
        if next_hamming == hamming_numbers[i3] * 3:
            i3 += 1
        if next_hamming == hamming_numbers[i5] * 5:
            i5 += 1
        
        hamming_numbers.append(next_hamming)
    
    return hamming_numbers

# Example: generate the first 10 Hamming numbers
hamming_numbers_10 = generate_hamming_numbers(10)
print(hamming_numbers_10)