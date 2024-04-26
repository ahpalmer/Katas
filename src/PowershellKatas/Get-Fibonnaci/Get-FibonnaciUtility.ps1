function Get-FibonnaciFirstTen() {
    $FibonnaciList = @(0, 1, 1, 2, 3, 5, 8, 13, 21, 34)
    return $FibonnaciList
}

function Get-FibonnaciBatch($n) {
    $lastFibonnaci = 0
    $currentFibonnaci = 1
    for($i = 2; $i -lt $n; $i++) {
        $temp = $currentFibonnaci + $lastFibonnaci
        $lastFibonnaci = $currentFibonnaci
        $currentFibonnaci = $temp
    }

    $fibonnaciBatch = New-Object System.Collections.Generic.List[int];
    for ($i = $n; $i -lt $n + 10; $i++) {
        $temp = $currentFibonnaci + $lastFibonnaci
        $lastFibonnaci = $currentFibonnaci
        $currentFibonnaci = $temp
        $fibonnaciBatch.Add($currentFibonnaci)
    }

    return $fibonnaciBatch
}

function Get-FibonnaciProduct($fibonnaciList) {
    $productList = New-Object System.Collections.Generic.List[int];
    for($i = 0; $i -lt $fibonnaciList.Count - 1; $i++) {
        $product = $fibonnaciList[$i] * $fibonnaciList[$i + 1]
        $productList.Add($product)
    }
    return $productList
}