function Invoke-LeibnizFormula
{
    param([double]$epsilon)

    $iterCount = 0
    $count = 1
    [double]$testPi = 0.0
    $addOrSubtract = $true
    $meetsCriteria = $false
    $checkPi = 0

    while((!$meetsCriteria) -and ($iterCount -lt 100000)) {
        if($addOrSubtract) {
            $testPi = $testPi + (1 / $count)
        }
        else {
            $testPi = $testPi - (1 / $count)
        }

        $checkPi = $testPi * 4
        $iterCount += 1

        $meetsCriteria = Invoke-DiffBetweenPiEpsilon -epsilon $epsilon -testPi $checkPi

        if ($iterCount -eq 9)
        {
            $something = $true
        }

        $count = $count + 2
        $addOrSubtract = !$addOrSubtract

    }

    [double]$PiApprox = [Math]::Round($checkPi, 10)

    [string]$answer = Invoke-StringBuilder -iterCount $iterCount -PiApprox $PiApprox

    $answer
}

function Invoke-DiffBetweenPiEpsilon
{
    param(
        [double]$epsilon,
        [double]$testPi
    )

    $M_PI = 3.14159265358979323846

    $difference = $M_PI - $testPi

    if ([Math]::Abs($difference) -lt $epsilon)
    {
        return $true
    }

    return $false
}

function Invoke-StringBuilder
{
    param(
        [int]$iterCount,
        [double]$PiApprox
    )

    $iterCountString = $iterCount.ToString()
    $PiApproxString = $PiApprox.ToString()

    $answer = "[$iterCountString, $PiApproxString]"

    $answer
}

Export-ModuleMember -Function 'Invoke-LeibnizFormula'