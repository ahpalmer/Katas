function Get-SumOfArrays 
{
    param (
        [int[]]$Array1,
        [int[]]$Array2
    )

    # Combine both arrays into one
    $combinedArray = $Array1 + $Array2

    # Calculate the sum of all elements in the combined array
    $sum = ($combinedArray | Measure-Object -Sum).Sum

    # Output the sum
    return $sum
}


# Establish the arrays
$Array1 = @(1, 2, 3)
$Array2 = @(4, 5, 6)

# Call the function and store the result
$sumOfElements = Get-SumOfArrays -Array1 $Array1 -Array2 $Array2

# Output the result
$sumOfElements