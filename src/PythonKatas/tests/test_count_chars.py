import codewars_test as test
from count_chars import count_characters

@test.describe("Basic Tests")
def basic_tests():
    @test.it("Basic Tests")
    def basic_tests():
        test.assert_equals(count_characters('aba'), {'a': 2, 'b': 1})
        test.assert_equals(count_characters(''), {})
        test.assert_equals(count_characters('aa'), {'a' : 2})
        test.assert_equals(count_characters('aabb'), {'b' : 2, 'a' : 2})