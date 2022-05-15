using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class HillClimber
{
    Chess chess;
    Chess visual;

    bool visuals;

    public HillClimber(Chess chess, bool visuals)
    {
        this.chess = chess;
        visual = new Chess();
        this.visuals = visuals;
    }

    public void Display()
    {
        chess.Display();
    }

    public int AnalyzeState(int[] state)
    {
        int h = 0;
        for (int i = 0; i < Chess.size; i++)
        {
            for (int j = i + 1; j < Chess.size; j++)
            {
                if (state[i] == state[j]) h++; // Horizontal
                if (j - i == state[j] - state[i]) h++; // Lower Diagonal
                if (j - i == state[i] - state[j]) h++; // Upper Diagonal
            }
        }

        return h;
    }

    public int[] Climb(bool zero = true)
    {
        chess.Randomize();

        int[] baseState = chess.GetBoard();
        int[] optimalState = Clone(baseState);
        int optimalK = AnalyzeState(optimalState);

        Console.WriteLine("Climbing From " + Stringify(chess.GetBoard()) + " | " + optimalK);

        bool local;
        while (optimalK != 0)
        {
            local = true;

            for (int i = 0; i < Chess.size; i++)
            {
                for (int j = 1; j < Chess.size; j++)
                {
                    int[] state = Clone(baseState);

                    state[i] = (state[i] + j) % Chess.size;
                    int k = AnalyzeState(state);

                    if (visuals)
                    {
                        Console.Clear();

                        visual.SetBoard(state);
                        visual.Display();

                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                    }

                    if (k < optimalK)
                    {
                        optimalK = k;
                        optimalState = state;

                        local = false;

                        Console.WriteLine("Found New Optimal " + Stringify(optimalState) + " | " + optimalK + " < " + k);
                    }
                }
            }

            baseState = Clone(optimalState);

            if (local)
            {
                if (zero) return Climb();
                else break; // Local Minimum
            }
        }

        if (zero)
        {
            Console.WriteLine();
            chess.SetBoard(optimalState);
            chess.Display();

            Console.WriteLine("\nSolved " + Stringify(optimalState) + " | " + optimalK + "\n");
        }
        else Console.WriteLine("Final Found " + Stringify(optimalState) + " | " + optimalK + "\n");

        return optimalState;
    }

    public void Climb(int amount)
    {
        int optimalK = int.MaxValue;
        int[] optimalState = null;

        for (int i = 0; i < amount; i++)
        {
            int[] state = Climb(false);
            int k = AnalyzeState(state);

            if (k < optimalK)
            {
                optimalK = k;
                optimalState = state;

                if (k == 0) break;
            }
        }

        Console.WriteLine();
        chess.SetBoard(optimalState);
        chess.Display();

        Console.WriteLine("\nFound Minimum " + Stringify(optimalState) + " | " + optimalK);
        Console.ReadLine();
    }

    public static void Copy(int[] copy, int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            copy[i] = array[i];
        }
    }

    public static int[] Clone(int[] array)
    {
        int[] clone = new int[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            clone[i] = array[i];
        }

        return clone;
    }

    public static string Stringify(int[] array)
    {
        string stringify = "";

        for (int i = 0; i < array.Length; i++)
        {
            stringify += array[i] + (i < array.Length - 1 ? "," : "");
        }

        return stringify;
    }
}