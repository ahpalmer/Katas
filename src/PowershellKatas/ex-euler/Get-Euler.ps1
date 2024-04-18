# Time: 3ish hours
function Get-Euler([int]$nb)
{
    # Initialize variables
    $h = Invoke-calculateH -nb $nb;
    $yZero = 1;
    $xZero = 0;

    # Run Euler's method for y values
    $yList = Invoke-RunEulersMethod -h $h -xZero $xZero -yZero $yZero -nb $nb;

    # Run known differential equation for z values
    $zList = Invoke-ExactSolution -h $h -xZero $xZero -nb $nb;

    # Find average difference between y and z values
    $avgList = Invoke-AverageCalc -yList $yList -zList $zList;
    $answer = Invoke-AverageSum -avgList $avgList;

    return Invoke-TruncateToSix -value $answer;
}

function Invoke-calculateH([int]$nb)
{
    return 1/$nb;
}

function Invoke-calculatefxy([double]$x, [double]$y)
{
    $eValue = [Math]::Exp(-4*$x);
    return 2 - $eValue - 2*$y;
}

function Invoke-calculateynPlusOne([double]$y, [double]$h, [double]$fxy)
{
    return $y + $h*$fxy;
}

function Invoke-RunEulersMethod([double]$h, [double]$xZero, [double]$yZero, [int]$nb)
{
    $yList = New-Object System.Collections.Generic.List[double];
    $yList.Add($yZero);
    $xN = $xZero;
    $yN = $yZero;
    for ($i = 1; $i -le $nb; $i++)
    {
        $fxy = Invoke-calculatefxy -x $xN -y $yN;
        $ynPlusOne = Invoke-calculateynPlusOne -y $yN -h $h -fxy $fxy;
        $yList.Add($ynPlusOne);
        $xn = $xn + $h;
        $yn = $ynPlusOne;
    }
    return $yList;
}

function Invoke-ExactSolution([double]$h, [double]$xZero, [int]$nb)
{
    $zList = New-Object System.Collections.Generic.List[double];
    $xN = $xZero;
    for ($i = 1; $i -le ($nb + 1) ; $i++)
    {
        $z = 1 + 0.5*([Math]::Exp(-4*$xN)) - (0.5*([Math]::Exp(-2*$xN)));
        $zList.Add($z);
        $xN = $xN + $h;
    }
    return $zList;
}

function Invoke-AverageCalc([System.Collections.Generic.List[double]]$yList, [System.Collections.Generic.List[double]]$zList)
{
    $avg = 0;
    $avgList = New-Object System.Collections.Generic.List[double];
    for ($i = 0; $i -lt $yList.Count; $i++)
    {
        $avg = ([Math]::Abs($yList[$i] - $zList[$i])) / $zList[$i];
        $avgList.Add($avg);
    }

    return $avgList;
}

function Invoke-AverageSum([System.Collections.Generic.List[double]]$avgList)
{
    $sum = 0;
    for ($i = 0; $i -lt $avgList.Count; $i++)
    {
        $sum = $sum + $avgList[$i];
    }
    return $sum / $avgList.Count;
}

function Invoke-TruncateToSix([double]$value)
{
    $valueTruncated = ([Math]::Truncate($value * 1000000)) / 1000000;
    
    return $valueTruncated;
}