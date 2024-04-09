function Get-PhoneNumberValidation([String]$PhoneNumber)
{
    Write-Host "Input received: $strInput"  # Debug output
    $phonePattern = "^[(]\d\d\d[)]\s\d\d\d-\d\d\d\d$"
    
    $matchRegex = Select-String -InputObject $PhoneNumber -Pattern $phonePattern

    if ($matchRegex)
    {
        Write-Host "Match found"
        return $true
    }
    else
    {
        Write-Host "No Match Found"
        return $false
    }
}

# Call the function with a sample input string
$PhoneNumber = "(123) 456-7890"
$matchingResults = Get-PhoneNumberValidation -PhoneNumber $PhoneNumber

# Output the matching results
foreach ($match in $matchingResults)
{
    Write-Output "Match found: $($match.Line)"
}

