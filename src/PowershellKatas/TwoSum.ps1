#Time: 1hr
function Get-TwoSum 
{
    param 
    (
        [System.Array]$numbers,
        [int]$target
    )
    $numberOfItems = $numbers.Count
    $count = 0

    foreach ($num in $numbers)
    {
        $secondCount = 0
        foreach($secondNum in $numbers)
        {
            if ($count -eq $secondCount)
            {
                $secondCount++
                continue
            }
            $answer = $num + $numbers[$secondCount]
            if ($answer -eq $target)
            {
                $answerArray = ($count, $secondCount)
                return $answerArray
            }
            $secondCount++
        }
        $count++
    }
    return -1
}

# Establish the variables
$numbers = @(1234, 5678, 9012)
$target = 14690

# Call the function and store the result
$sumOfElements = Get-TwoSum -numbers $numbers -target $target

# Output the result
$sumOfElements