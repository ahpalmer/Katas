Describe "Get-DisguisedSequence.ps1 Tests" {
    It "Test Calculation - Should return 4" {
        $Un = 1
        $Unplus1 = 2
        $answer = Invoke-DisSeqCalculation -Un $Un -Unplus1 $Unplus1
        $answer | Should -Be 4
    }
    
    It "Should return 131072" {
        $n = 17
        $answer = Get-DisguisedSequence -n $n
        $answer | Should -Be 131072
    }
    It "Should return 2097152" {
        $n = 21
        $answer = Get-DisguisedSequence $n
        $answer | Should -Be 2097152
    }
    It "Should return 16384" {
        $n = 14
        $answer = Get-DisguisedSequence $n
        $answer | Should -Be 16384
    }
}