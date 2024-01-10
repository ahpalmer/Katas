def find_nb(m):
    lastVolume = 0
    matchFound = False
    count = 0
    while not matchFound:
        currentVolume = lastVolume + (1 + count)**3
        count += 1
        if currentVolume == m:
            matchFound = True
            return count
        elif m > lastVolume and m < currentVolume:
            matchFound = True
            return -1
        else:
            lastVolume = currentVolume
