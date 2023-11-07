function Get-TestRegexPS([string] $input)
{
    $testRegex = "[a]"
    $match = Select-String -InputObject $input -Pattern $testRegex -CaseSensitive -SimpleMatch

    foreach($m in $match)
    {
        Write-Output "Match found: $($m)"
    }

    return $match
}

#Don't forget to write the function as Get-TestRegexPS -input $input