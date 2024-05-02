function Get-Fortune {
    [OutputType([boolean])]
    Param ([int]$f0, [double]$p, [int]$c0, [int]$n, [double]$i)

    $fN = Get-FortuneNFinal -f0 $f0 -p $p -c0 $c0 -n $n -i $i

    if ($fN -ge 0) {
        return $true
    }
    else {
        return $false
    }
}

function Get-FortuneN($fNMinusOne, $p, $cNMinusOne) {
    $fortuneN = $fNMinusOne * (1 + ($p / 100)) - $cNMinusOne
    return $fortuneN
}

function Get-ConstWithdrawN($cNMinusOne, $i) {
    $constWithdrawN = $cNMinusOne * (1 + ($i / 100))
    return $constWithdrawN
}

function Get-FortuneNFinal([int]$f0, [double]$p, [int]$c0, [int]$n, [double]$i)
{
    [int]$fNMinusOne = $f0
    [int]$cNMinusOne = $c0
    $fN = 0
    $cN = 0

    for ($count = 1; $count -lt $n; $count ++) {
        $fN = Get-FortuneN -fNMinusOne $fNMinusOne -p $p -cNMinusOne $cNMinusOne
        $fN = [Math]::Floor($fN)
        $cN = Get-ConstWithdrawN -cNMinusOne $cNMinusOne -i $i
        $cN = [Math]::Floor($cN)
        $fNMinusOne = $fN
        $cNMinusOne = $cN
    }

    return $fN
}