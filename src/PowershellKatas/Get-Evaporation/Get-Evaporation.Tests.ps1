Describe "Get-Evaporation.ps1 Tests" {
    It "Should return 29" {
        $content = 10
        $evapperday = 10
        $threshold = 5
        $answer = Get-Evaporator -content $content -evapperday $evapperday -threshold $threshold
        $answer | Should -Be 29
    }
}