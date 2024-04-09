# Time: 34 minutes
function Get-NewAvg($arr, $navg)
{
    # Sum up all of the elements in the array
    $arrSum = 0
    $arr | ForEach-Object { $arrSum += $_}

    $ans = $navg * ($arr.Length + 1) - $arrSum

    if ($ans -lt 0)
    {
        return -1
    }

    return [Math]::Ceiling($ans)
}

# Establish the arrays
$arr = @(14, 30, 5, 7, 9, 11, 15)
$navg = 30

# Call the function and store the result
$answer = Get-NewAvg -arr $arr -navg $navg

# Output the result
$answer