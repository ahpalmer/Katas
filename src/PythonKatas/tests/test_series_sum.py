import codewars_test as test
from ..src.Kyu7.series_sum import series_sum

@test.describe("Fixed Tests")
def fixed_tests():
    @test.it('Basic Test Cases')
    def basic_test_cases():
        test.assert_equals(series_sum(1), "1.00")
        test.assert_equals(series_sum(2), "1.25")
        test.assert_equals(series_sum(3), "1.39")