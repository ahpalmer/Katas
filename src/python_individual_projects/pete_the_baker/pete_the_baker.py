import math

def cakes(recipe, available):
    min_cakes = 10000
    for key in recipe:
        if key not in available:
            return 0
        temp_min_cakes = math.floor(available[key] / recipe[key])
        min_cakes = temp_min_cakes if temp_min_cakes < min_cakes  else min_cakes
    
    return min_cakes