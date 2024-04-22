function Get-Evaporator([double]$content, [double]$evapperday, [double]$threshold) {
    [double]$b = 1 - ($evapperday / 100);
    [double]$c = $threshold / 100;
    [double]$rawAnswer = ([Math]::Log10($c) / [Math]::Log10($b));

    [int]$answer = [Math]::Ceiling($rawAnswer);
    return $answer;
}