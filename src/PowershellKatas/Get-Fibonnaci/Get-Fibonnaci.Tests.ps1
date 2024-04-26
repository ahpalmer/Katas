Describe "Get-FibonnaciFirstTen" {
    It "Should return 0, 1, 1, 2, 3, 5, 8, 13, 21, 34" {
        $answer = Get-FibonnaciFirstTen
        $answer | Should -Be @(0, 1, 1, 2, 3, 5, 8, 13, 21, 34)
    }

}

Describe "Get-FibonnaciBatch Tests" {
    It "Should return 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181" {
        $n = 10
        $answer = Get-FibonnaciBatch -n $n
        $answer | Should -Be @(55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181)
    }
}

Describe "Get-FibonnaciBatch Tests" {
    It "Should return 6765, 10946, 17711, 28657, 46368, 75025, 121393, 196418, 317811, 514229" {
        $n = 20
        $answer = Get-FibonnaciBatch -n $n
        $answer | Should -Be @(6765, 10946, 17711, 28657, 46368, 75025, 121393, 196418, 317811, 514229)
    }
}

Describe "Get-FibonnaciProduct Tests" {
    It "Should return 0, 1, 2, 6, 15, 40, 104, 273, 714" {
        $fibonnaciList = @(0, 1, 1, 2, 3, 5, 8, 13, 21, 34)
        $answer = Get-FibonnaciProduct -fibonnaciList $fibonnaciList
        $answer | Should -Be @(0, 1, 2, 6, 15, 40, 104, 273, 714)
    }
}