namespace Memory_game
{
    class Program
    {
        private static readonly string DefaultTextFile = @"C:\Users\Tomek\Downloads\Coding Task Motorola Academy C#\Words.txt";
        private const int EXIT_OPTION = 0;
        private static List<Level> Levels = new List<Level>
        {
            new Level(LevelCode.Easy,"Easy", 10, 4),
            new Level(LevelCode.Hard,"Hard", 15, 8)
        };

        static void Main(string[] args)
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
            game.Play();

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