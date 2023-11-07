# Define the function
function Get-TestRegexPS2([string] $strInput)
{
    Write-Host "Input received: $strInput"  # Debug output
    $testRegex = "12345"
    $matchRegex = Select-String -InputObject $strInput -Pattern $testRegex -CaseSensitive
    Write-Host "matchRegex: $matchRegex"  # Debug output
    if ($matchRegex)
    {
        Write-Host "Match found"
    }
    else
    {
        Write-Host "No Match Found"
    }
    return $matchRegex
}

# Call the function with a sample input string
$inputString = "12345"
$matchingResults = Get-TestRegexPS2 -strinput $inputString

# Output the matching results
foreach ($match in $matchingResults)
{
    Write-Output "Match found: $($match.Line)"
}