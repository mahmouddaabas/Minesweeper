namespace Minesweeper;

enum FieldState
{
    Covered,
    Uncovered,
    Mine
}

public class Minefield
{
    private bool[,] _bombLocations = new bool[5, 5];
    private FieldState[,] fieldStates = new FieldState[5, 5];
    private int clearedFieldCounter = 0;
    private bool hasLost;

    public void SetBomb(int x, int y)
    {
        _bombLocations[x, y] = true;
    }

    public void DrawMineField()
    {
        Console.Clear();
        Console.WriteLine("  01234");
        Console.WriteLine("  ------");

        for (int row = 0; row < fieldStates.GetLength(0); row++)
        {
            Console.Write(row + "|");
            for (int col = 0; col < fieldStates.GetLength(1); col++)
            {
                if (fieldStates[row, col] == FieldState.Covered)
                {
                    Console.Write("?");
                }
                else if (fieldStates[row, col] == FieldState.Uncovered)
                {
                    int adjacentBombs = CountAdjacentBombs(row, col);
                    if (adjacentBombs > 0)
                    {
                        Console.Write(adjacentBombs);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                else if (fieldStates[row, col] == FieldState.Mine)
                {
                    Console.Write("X");
                }
            }
            Console.WriteLine();
        }
    }

    public bool CheckInputCoords(int x, int y)
    {
        if (_bombLocations[x, y] == true)
        {
            fieldStates[x, y] = FieldState.Mine;
            hasLost = true;
            return true;
        }
        else
        {
            UncoverNearbyFields(x, y);
            return false;
        }
    }

    private void UncoverNearbyFields(int x, int y)
    {
        if (IsValidCoordinate(x, y) && IsCovered(x, y))
        {
            fieldStates[x, y] = FieldState.Uncovered;
            IncrementClearedFieldCounter();

            if (CountAdjacentBombs(x, y) == 0)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        UncoverNearbyFields(x + i, y + j);
                    }
                }
            }
        }
    }

    private bool IsValidCoordinate(int x, int y)
    {
        return x >= 0 && y >= 0 && x < fieldStates.GetLength(0) && y < fieldStates.GetLength(1);
    }

    private bool IsCovered(int x, int y)
    {
        return fieldStates[x, y] == FieldState.Covered;
    }

    public int CountAdjacentBombs(int x, int y)
    {
        int bombCount = 0;

        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                int neighborX = x + xOffset;
                int neighborY = y + yOffset;

                bool isWithinBoundsX = neighborX >= 0 && neighborX < fieldStates.GetLength(0);
                bool isWithinBoundsY = neighborY >= 0 && neighborY < fieldStates.GetLength(1);

                if (isWithinBoundsX && isWithinBoundsY && _bombLocations[neighborX, neighborY])
                {
                    bombCount++;
                }
            }
        }

        return bombCount;
    }


    public bool CheckWin()
    {
        if (clearedFieldCounter == 20)
        {
            Console.WriteLine("Congratulations, you've cleared all the tiles!");
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GameOver()
    {
        if(hasLost)
        {
            Console.WriteLine("You hit a bomb, game over!");
            return true;
        }
        else
        { 
            return false; 
        }
    }

    public void SetClearedFieldCounter(int value)
    {
        this.clearedFieldCounter = value;
    }

    private void IncrementClearedFieldCounter()
    {
        this.clearedFieldCounter++;
    }

    public void SetHasLost(bool value)
    {
        this.hasLost = value;
    }
}
