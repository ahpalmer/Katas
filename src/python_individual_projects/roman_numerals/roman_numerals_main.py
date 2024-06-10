class RomanNumerals:
    @staticmethod
    def to_roman(val : int) -> str:
        roman_numeral_final = ""
        roman_numeral_interim = ""
        while val > 0:
            val, roman_numeral_interim = RomanNumerals.interim_roman_letter(val)
            roman_numeral_final = roman_numeral_final + roman_numeral_interim
        print("val: ", val)
        print("roman_numeral_final", roman_numeral_final)
        return roman_numeral_final


    @staticmethod
    def interim_roman_letter(val):
        if val >= 1000:
            return val - 1000, "M"
        elif val >= 900:
            return val - 900, "CM"
        elif val >= 500:
            return val - 500, "D"
        elif val >= 400:
            return val - 400, "CD"
        elif val >= 100:
            return val - 100, "C"
        elif val >= 90:
            return val - 90, "XC"
        elif val >= 50:
            return val - 50, "L"
        elif val >= 40:
            return val - 40, "XL"
        elif val >= 10:
            return val - 10, "X"
        elif val >= 9:
            return val - 9, "IX"
        elif val >= 5:
            return val - 5, "V"
        elif val >= 4:
            return val - 4, "IV"
        elif val >= 1:
            return val - 1, "I"
        elif val == 0:
            return 0, ""
        

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
    
# RomanNumerals.to_roman(1987)
# input_list_converted = [1000, 100, 1000, 10, 100, 1, 5]
# RomanNumerals.find_from_roman_answer(input_list_converted)