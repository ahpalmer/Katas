# Get-Fibonnaci.psm1
. $PSScriptRoot\Get-FibonnaciUtility.ps1

function Get-Fibonnaci($prod) {
    $tempFibList = @()
    $boolAnswer = $false

    $tempFibList = Get-FibonnaciFirstTen

    if (($tempFibList[8] * $tempFibList[9]) -ge $prod) {
        $prodList = Get-FibonnaciProduct -fibonnaciList $tempFibList
        $prodAnswer = $prodList | Where-Object {$_ -ge $prod}

        if ($prodAnswer -eq $prod) {
            $boolAnswer = $true
        }
    }

    $index = $prodList.IndexOf($prodAnswer)
    $answerOne = $tempFibList[$index]
    $answerTwo = $tempFibList[$index + 1]

    return ($answerOne, $answerTwo, $boolAnswer) 
}

Export-ModuleMember -Function 'Get-Fibonnaci'

# Establish the variables
$prod = 714

# Call the function and store the result
$answer = Get-Fibonnaci -prod $prod

# Output the result
$answer