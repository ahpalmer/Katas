from enum import Enum
from dataclasses import dataclass

class File(Enum):
    a = 1
    b = 2
    c = 3
    d = 4
    e = 5
    f = 6
    g = 7
    h = 8

    def __add__(self, offset: int) -> "File":
        new_value = self.value + offset
        if new_value < 1 or new_value > 8:
            raise ValueError("File out of bounds")
        return File(new_value)

class Rank(Enum):
    ONE = 1
    TWO = 2
    THREE = 3
    FOUR = 4
    FIVE = 5
    SIX = 6
    SEVEN = 7
    EIGHT = 8

    def __add__(self, offset: int) -> "Rank":
        new_value = self.value + offset
        if new_value < 1 or new_value > 8:
            raise ValueError("Rank out of bounds")
        return Rank(new_value)

@dataclass(frozen=True)
class Coordinates:
    file: File
    rank: Rank