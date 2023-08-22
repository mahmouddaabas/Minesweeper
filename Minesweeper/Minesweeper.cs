namespace Minesweeper;

class Minesweeper
{
    private bool isGameRunning = true;
    static void Main()
    {
        var game = new Minesweeper();
        var field = new Minefield();
 
        //set the bombs...
        field.SetBomb(0, 0);
        field.SetBomb(0, 1);
        field.SetBomb(1, 1);
        field.SetBomb(1, 4);
        field.SetBomb(4, 2);

        //the mine field should look like this now:
        //  01234
        //4|1X1
        //3|11111
        //2|2211X
        //1|XX111
        //0|X31

        // Game code...
        game.StartGameLoop(field);
    }

    public void StartGameLoop(Minefield field)
    {
        field.DrawMineField();
        while (isGameRunning)
        {
            PrintMessage("Enter a X coordinate");
            int xCoord = Convert.ToInt32(Console.ReadLine());
            PrintMessage("Enter a Y coordinate");
            int yCoord = Convert.ToInt32(Console.ReadLine());
            field.CheckInputCoords(xCoord, yCoord);
            field.DrawMineField();
            if (field.GameOver() || field.CheckWin())
                isGameRunning = false;
   
        }
    }

    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }
}
