class Chess
{
    public static int size = 8;

    int[] board;

    public Chess()
    {
        board = new int[size];

        Random random = new Random();
        for (int i = 0; i < size; i++)
        {
            board[i] = random.Next(0, size);
        }
    }

    public void Randomize()
    {
        Random random = new Random();
        for (int i = 0; i < size; i++)
        {
            board[i] = random.Next(0, size);
        }
    }

    public void Display()
    {
        Console.WriteLine("┌───┬───┬───┬───┬───┬───┬───┬───┐");

        for (int i = 0; i < size; i++)
        {
            string line = "│";
            for (int j = 0; j < size; j++)
            {
                line += " " + (board[j] == i ? "X" : " ") + " │";
            }
            Console.WriteLine(line);

            if (i < size - 1) Console.WriteLine("├───┼───┼───┼───┼───┼───┼───┼───┤");
            else Console.WriteLine("└───┴───┴───┴───┴───┴───┴───┴───┘");
        }
    }

    public int[] GetBoard()
    {
        return board;
    }

    public void SetBoard(int[] state)
    {
        board = state;
    }
}