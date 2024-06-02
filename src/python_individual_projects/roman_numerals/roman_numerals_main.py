class RomanNumerals:
    @staticmethod
    def to_roman(val : int) -> str:
        return ''

    @staticmethod
    def from_roman(roman_num : str) -> int:
        roman_dict = RomanNumerals.roman_numeral_dict()
        input_list = list(roman_num)
        input_list_converted = []
        for char in input_list:
            input_list_converted.append(roman_dict[char])
        answer = RomanNumerals.find_from_roman_answer(input_list_converted)
        
        print(answer)
        return answer
    
    @staticmethod
    def find_from_roman_answer(input_list_converted):
        last_number = 100000
        answer = 0
        for roman_str in input_list_converted:
            answer = answer + roman_str
            if last_number < roman_str:
                answer = answer - 2 * last_number
            last_number = roman_str
        return answer

    @staticmethod
    def reverse_roman_numeral_dict():
        dict = RomanNumerals.roman_numeral_dict()
        reversed_dict = {value: key for key, value in dict.items()}
        return reversed_dict

    @staticmethod
    def roman_numeral_dict():
        dict = {
            'I': 1,
            'V': 5,
            'X': 10,
            'L': 50,
            'C': 100,
            'D': 500,
            'M': 1000
        }
        return dict
    

input_list_converted = [1000, 100, 1000, 10, 100, 1, 5]
RomanNumerals.find_from_roman_answer(input_list_converted)