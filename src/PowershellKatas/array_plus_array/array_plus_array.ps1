function Get-array_plus_array($arr1,$arr2)
{
    for (($i=0); ($i -lt ($arr1.Length)); ($i++))
    {
        $answer += $arr1[$i]
        if($count -eq 20)
        {
            exit;
        }
    }
    for (($a=0); ($a -lt ($arr2.Length)); ($a++))
    {
        $answer2 = $arr2[$a] + $answer2
        if($count -eq 20)
        {
            exit;
        }
    }
    Write-Host "Answer:"
    Write-Host $answer + $answer2
    return $answer + $answer2
}