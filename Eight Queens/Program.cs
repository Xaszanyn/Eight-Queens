HillClimber hillClimber;

Console.WriteLine("Press 'V' to enable visuals or continue directly to the result by pressing any other key.");

if (Console.ReadKey().Key == ConsoleKey.V) hillClimber = new HillClimber(new Chess(), true);
else hillClimber = new HillClimber(new Chess(), false);

Console.WriteLine();

hillClimber.Climb();

Console.ReadLine();