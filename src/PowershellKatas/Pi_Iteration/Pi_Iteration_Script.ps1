Import-Module "C:\Users\andrewpalmer\Documents\CloudRepos\Katas\src\PowershellKatas\Pi_Iteration\Pi_Iteration"

function Get-iter-pi
{
    Param ([double]$epsilon)

    $result = Invoke-LeibnizFormula -epsilon $epsilon

    return $result
}