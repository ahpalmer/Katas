function array_plus_arrayv2($input)
{
    $arr1 = @([int]1, [int]2, $input)
    
    [int] $sum = 0

    foreach($num in $arr1)
    {
        $sum+=$num
    }

    return $sum
}