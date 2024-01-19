import codewars_test as test
from ..src.Kyu6.find_nb import find_nb

@test.describe("Fixed Tests")
def fixed_tests():
    @test.it('Basic Test Cases')
    def basic_test_cases():
        test.assert_equals(find_nb(4), -1)
        test.assert_equals(find_nb(16), -1)
        test.assert_equals(find_nb(4183059834009), 2022)
        test.assert_equals(find_nb(24723578342962), -1)
        test.assert_equals(find_nb(135440716410000), 4824)
        test.assert_equals(find_nb(40539911473216), 3568)
        test.assert_equals(find_nb(26825883955641), 3218)