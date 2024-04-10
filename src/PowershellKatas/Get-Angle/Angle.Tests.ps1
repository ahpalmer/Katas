Describe "Get-Angle.ps1 Tests" {
    It "Should return 180" {
        $n = 3
        $answer = Get-Angle -n $n
        $answer | Should -Be 180
    }
    It "Should return 1260" {
        $n = 9
        $answer = Get-Angle -n $n
        $answer | Should -Be 1260
    }
}
