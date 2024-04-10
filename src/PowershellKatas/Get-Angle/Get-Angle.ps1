function Get-Angle([int] $n) {
    return 180 * ($n - 2)
}

# Establish variables
$n = 4

# Call the function and store the result
$answer = Get-Angle -n $n

# Output result
$answer