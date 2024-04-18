Describe "Get-Euler.ps1 Tests" {
    It "Should return -1" {
        $x = 0
        $y = 1
        $answer = Invoke-calculatefxy -x $x -y $y
        $answer | Should -Be -1
    }
    It "Should return -0.47032" {
        $x = .1
        $y = .9
        $yTwo = Invoke-calculatefxy -x $x -y $y
        $answer = [Math]::Round($yTwo, 5)
        $answer | Should -Be -0.47032
    }
    It "calculate-ynPlusOne Should return 0.9" {
        $y = 1
        $h = 0.1
        $fxy = -1
        $answer = Invoke-calculateynPlusOne -y $y -h $h -fxy $fxy
        $answer | Should -Be 0.9
    }
    It "calculate-ynPlusOne Should return 0.85296" {
        $x = 0.1
        $y = 0.9
        $h = 0.1
        $fxy = Invoke-calculatefxy -x $x -y $y
        $yTwo = Invoke-calculateynPlusOne -y $y -h $h -fxy $fxy
        $answer = [Math]::Round($yTwo, 5)
        $answer | Should -Be 0.85297
    }
    It "Invoke-RunEulersMethod Should return list" {
        $h = 0.1
        [double]$xZero = 0
        [double]$yZero = 1
        # $yList = New-Object System.Collections.Generic.List[double];
        $yList = Invoke-RunEulersMethod -h $h -xZero $xZero -yZero $yZero -nb 10;
        $yListAnswer = New-Object System.Collections.Generic.List[double];

        $yListExpected = New-Object System.Collections.Generic.List[double];
        $yListExpected.Add(1);
        $yListExpected.Add(0.9);
        $yListExpected.Add(0.85);
        $yListExpected.Add(0.84);
        $yListExpected.Add(0.84);
        $yListExpected.Add(0.85);
        $yListExpected.Add(0.87);
        $yListExpected.Add(0.89);
        $yListExpected.Add(0.90);
        $yListExpected.Add(0.92);
        $yListExpected.Add(0.93);

        foreach($y in $yList)
        {
            $yListAnswer.Add([Math]::Round($y, 2));
        }
        $yListAnswer | Should -Be $yListExpected
    }
    It "Invoke-ExactSolution Should return list" {
        $h = 0.1
        [double]$xZero = 0
        $zList = New-Object System.Collections.Generic.List[double];
        $zList = Invoke-ExactSolution -h $h -xZero $xZero -nb 10;
        $zListAnswer = New-Object System.Collections.Generic.List[double];

        $zListExpected = New-Object System.Collections.Generic.List[double];
        $zListExpected.Add(1);
        $zListExpected.Add(0.93);
        $zListExpected.Add(0.89);
        $zListExpected.Add(0.88);
        $zListExpected.Add(0.88);
        $zListExpected.Add(0.88);
        $zListExpected.Add(0.89);
        $zListExpected.Add(0.91);
        $zListExpected.Add(0.92);
        $zListExpected.Add(0.93);
        $zListExpected.Add(0.94);

        foreach($z in $zList)
        {
            $zListAnswer.Add([Math]::Round($z, 2));
        }
        $zListAnswer | Should -Be $zListExpected
    }
    It "Invoke-AverageCalc should return list of mean error between y and z list" {
        $nb = 10
        $h = 0.1
        $xZero = 0
        $yZero = 1
        $yList = Invoke-RunEulersMethod -h $h -xZero $xZero -yZero $yZero -nb $nb;
        $zList = Invoke-ExactSolution -h $h -xZero $xZero -nb $nb;

        $avgList = Invoke-AverageCalc -yList $yList -zList $zList;
        $avgListAnswer = New-Object System.Collections.Generic.List[double];
        $avgListExpected = New-Object System.Collections.Generic.List[double];
        $avgListExpected.Add(0);
        $avgListExpected.Add(0.02786);
        $avgListExpected.Add(0.04108);
        $avgListExpected.Add(0.04423);
        $avgListExpected.Add(0.0416);
        $avgListExpected.Add(0.03627);
        $avgListExpected.Add(0.03012);
        $avgListExpected.Add(0.02418);
        $avgListExpected.Add(0.0189);
        $avgListExpected.Add(0.01444);
        $avgListExpected.Add(0.0108);

        foreach($avg in $avgList)
        {
            $avgListAnswer.Add([Math]::Round($avg, 5));
        }
        $avgListAnswer | Should -Be $avgListExpected
    }
    It "Invoke-AverageSum should return 0.026314" {
        $nb = 10
        $h = 0.1
        $xZero = 0
        $yZero = 1
        $yList = Invoke-RunEulersMethod -h $h -xZero $xZero -yZero $yZero -nb $nb;
        $zList = Invoke-ExactSolution -h $h -xZero $xZero -nb $nb;

        $avgList = Invoke-AverageCalc -yList $yList -zList $zList;
        $avgSum = Invoke-AverageSum -avgList $avgList;
        [Math]::Round($avgSum, 6) | Should -Be 0.026314
    }
    It "Invoke-TruncateToSix should return 1.123456" {
        $value = 1.123456789
        $answer = Invoke-TruncateToSix -value $value
        $answer | Should -Be 1.123456
    }
}
