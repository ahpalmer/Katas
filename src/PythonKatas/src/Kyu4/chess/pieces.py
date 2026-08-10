from .coordinates import Coordinates
from dataclasses import dataclass

@dataclass
class Knight:
    position: Coordinates

    def move_to(self, destination: Coordinates) -> None:
        if destination not in self.legal_moves():
            raise ValueError("Illegal move")

        self.position = destination

    def legal_moves(self) -> list[Coordinates]:
        possible_moves: list[Coordinates] = []
        for file_offset in [-2, -1, 1, 2]:
            for rank_offset in [-2, -1, 1, 2]:
                if abs(file_offset) != abs(rank_offset):
                    try:
                        new_file = self.position.file + file_offset
                        new_rank = self.position.rank + rank_offset
                        possible_moves.append(Coordinates(new_file, new_rank))
                    except:
                        continue
        return possible_moves