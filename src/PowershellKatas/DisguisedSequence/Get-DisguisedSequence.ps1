# return a long
function Get-DisguisedSequence($n) {
    $Un = 1
    $Unplus1 = 2
    for ($i = 1; $i -le $n; $i++)
    {
        $UnTemp = $Unplus1
        $Unplus1 = Invoke-DisSeqCalculation $Un $Unplus1
        $Un = $UnTemp
    }
    return $Un
}

function Invoke-DisSeqCalculation($Un, $Unplus1) {
    $answer = 6 * ($Un * $Unplus1) / ((5*$Un) - $Unplus1)
    return $answer
}

