import pete_the_baker as ptb

def test_cakes():
    recipe = {"flour": 500, "sugar": 200, "eggs": 1}
    available = {"flour": 1200, "sugar": 1200, "eggs": 5, "milk": 200}
    assert ptb.cakes(recipe, available) == 2

    recipe2 = {"apples": 3, "flour": 300, "sugar": 150, "milk": 100, "oil": 100}
    available2 = {"sugar": 500, "flour": 2000, "milk": 2000}
    assert ptb.cakes(recipe2, available2) == 0