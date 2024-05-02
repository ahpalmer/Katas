Describe "Get-Fortune.ps1 Tests" {
    It "Should return 99000" {
        $fNMinusOne = 100000
        $p = 1
        $cNMinusOne = 2000
        $answer = Get-FortuneN -fNMinusOne $fNMinusOne -p $p -cNMinusOne $cNMinusOne
        $answer | Should -Be 99000
    }
    It "Should return 2020" {
        $cNMinusOne = 2000
        $i = 1
        $answer = Get-ConstWithdrawN -cNMinusOne $cNMinusOne -i $i
        $answer | Should -Be 2020
    }
    It "Should return -5" {
        $f0 = 100000
        $p = 1
        $c0 = 9185
        $n = 12
        $i = 1
        $answer = Get-FortuneNFinal -f0 $f0 -p $p -c0 $c0 -n $n -i $i
        $answer | Should -Be -5
    }
    It "Should return false" {
        $f0 = 100000
        $p = 1
        $c0 = 9185
        $n = 12
        $i = 1
        $answer = Get-Fortune -f0 $f0 -p $p -c0 $c0 -n $n -i $i
        $answer | Should -Be $false
    }
}
