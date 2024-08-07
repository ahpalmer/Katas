# return a long
function Get-DisguisedSequence($n) {
    
}

function Invoke-DisSeqCalculation($Un, $Unplus1) {
    $answer = 6 * ($Un * $Unplus1) / ((5*$Un) - $Unplus1)
    return $answer
}