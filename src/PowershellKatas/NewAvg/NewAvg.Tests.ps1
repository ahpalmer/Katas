# Load the module
Describe "Get-NewAvg.ps1 Tests" {
    It "Should return 149" {
        $arr = @(14, 30, 5, 7, 9, 11, 15)
        $navg = 30
        $answer = Get-NewAvg -arr $arr -navg $navg
        $answer | Should -Be 149
    }
}