namespace Memory_game
{
    class Program
    {
        private static readonly string DefaultTextFile = @"C:\Users\Tomek\Downloads\Coding Task Motorola Academy C#\Words.txt";
        private const int EXIT_OPTION = 0;
        private static ScoreBoard ScoreBoard = new ScoreBoard();
        private static List<Level> Levels = new List<Level>

        {
            new Level(LevelCode.Easy,"Easy", 10, 4),
            new Level(LevelCode.Hard,"Hard", 15, 8)
        };

        static void Main(string[] args)
        {
            ScoreBoard.Read();
            while (true)
            {
                Console.WriteLine("Choose difficulty level");

                foreach (var level in Levels)
                {
                    Console.WriteLine($"{(int)level.LevelCode}. {level.Name}");
                }

                Console.WriteLine($"{EXIT_OPTION}. Exit");
                Level selectedLevel = null;
                while (selectedLevel == null)
                {
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int selectedOption))
                    {
                        if (selectedOption == EXIT_OPTION)
                        {
                            return;
                        }

                        selectedLevel = Levels.SingleOrDefault(level => (int)level.LevelCode == selectedOption);
                        if (selectedLevel == null)
                            Console.WriteLine("Select correct number");
                    }
                    else
                    {
                        Console.WriteLine("Select correct number");
                    }
                }

                var avalibleWords = ReadAvalibleWords(args);
                MemoryGame game = new MemoryGame(selectedLevel, avalibleWords);
                var result = game.Play();
                if (result != null)
                {
                    ScoreBoard.AddResult(result);
                    ScoreBoard.Write();
                }
                ScoreBoard.Print();
                if (!ShouldContinue())
                {
                    break;
                }

            }

        }


        private static bool ShouldContinue()
        {
            Console.WriteLine("Would you like to restart the game?");
            Console.WriteLine("1.Yes");
            Console.WriteLine("2.No");
            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out int decision))
                {
                    if (decision == 1)
                    {
                        return true;
                    }
                    else if (decision == 2)
                    {
                        return false;
                    }


                }
                else
                {
                    Console.WriteLine("Select correct number");
                }

            }
        }

        private static string[] ReadAvalibleWords(string[] args)
        {
            if (args.Length == 0)
            {
                return File.ReadAllLines(DefaultTextFile);
            }
            else
            {
                return File.ReadAllLines(args[0]);
            }
        }
    }
}