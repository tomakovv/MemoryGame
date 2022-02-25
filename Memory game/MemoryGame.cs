

using ConsoleTables;
using System.Diagnostics;

namespace Memory_game
{
    public class MemoryGame
    {
        private List<string> AvalibleWords { get; set; }
        private static Random Random = new Random();
        private Element[,] Board { get; set; }
        private Level DifficlutyLevel { get; set; }
        public MemoryGame(Level level, string[] avalibleWords)
        {
            DifficlutyLevel = level;
            Board = new Element[2, DifficlutyLevel.NumberOfWords];
            AvalibleWords = new List<string>(avalibleWords);
            FillInTheBoard();
        }

        public Result Play()
        {
            Stopwatch sw = Stopwatch.StartNew();
            var guessChancesLeft = DifficlutyLevel.GuessChances;
            for (int i = 0; i < DifficlutyLevel.GuessChances; i++)
            {
                ShowTheBoard();

                Console.WriteLine($"Guess chances:{guessChancesLeft}");
                var word1 = PickTheWord();
                word1.SetStatus(ElementState.Revealed);
                ShowTheBoard();
                var word2 = PickTheWord();
                word2.SetStatus(ElementState.Revealed);
                if (word1.Back == word2.Back && word1.Id != word2.Id)
                {
                    word2.SetStatus(ElementState.Matched);
                    word1.SetStatus(ElementState.Matched);
                    ShowTheBoard();
                }
                else
                {
                    guessChancesLeft--;
                    ShowTheBoard();
                    word2.SetStatus(ElementState.Hidden);
                    word1.SetStatus(ElementState.Hidden);

                }
                if (AllElementsMatched())
                {
                    var tries = i + 1;
                    sw.Stop();
                    TimeSpan stopwatchElapsed = sw.Elapsed;
                    Console.WriteLine($"You solved the memory game after {tries} chances." +
                        $" It tooks you {Convert.ToInt32(stopwatchElapsed.TotalSeconds)} seconds");
                    Console.WriteLine("type your name:");
                    var name = Console.ReadLine();
                    return new Result(name, tries, stopwatchElapsed, DifficlutyLevel.LevelCode);

                }

            }
            return null;

        }

        private string GetRandomWord()
        {
            var word = AvalibleWords[Random.Next(AvalibleWords.Count)];
            AvalibleWords.Remove(word);
            return word;
        }
        private void FillInTheBoard()
        {
            List<string> randomWords = new List<string>();
            for (int i = 0; i < DifficlutyLevel.NumberOfWords; i++)
            {
                var randomWord = GetRandomWord();
                randomWords.Add(randomWord);
                randomWords.Add(randomWord);
            }
            randomWords = randomWords.OrderBy(x => Guid.NewGuid()).ToList();
            int counter = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < DifficlutyLevel.NumberOfWords; j++)
                {
                    Board[i, j] = new Element(randomWords[counter]);
                    counter++;
                }

            }
        }
        private bool AllElementsMatched()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < DifficlutyLevel.NumberOfWords; j++)
                {
                    if (Board[i, j].State != ElementState.Matched)
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        public void ShowTheBoard()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Level: {DifficlutyLevel.Name}");
            Console.WriteLine();
            var columns = new List<string>() { "" };
            columns.AddRange(Enumerable.Range(1, DifficlutyLevel.NumberOfWords).Select(x => x.ToString()));
            var table = new ConsoleTable(columns.ToArray());
            table.Options.EnableCount = false;
            var row1 = new List<string>() { "A" };
            var row2 = new List<string>() { "B" };


            for (int j = 0; j < DifficlutyLevel.NumberOfWords; j++)
            {
                row1.Add(Board[0, j].ShowElement());
                row2.Add(Board[1, j].ShowElement());
            }
            table.AddRow(row1.ToArray());
            table.AddRow(row2.ToArray());
            table.Write();
            Console.WriteLine();
            Console.WriteLine("-----------------------------");
            Console.WriteLine();
        }
        public Element PickTheWord()
        {
            Element selectedElement = null;
            while (selectedElement == null)
            {
                Console.WriteLine("Pick the element");
                var pick = Console.ReadLine().ToUpper();

                int? row = pick[0] switch
                {
                    'A' => 0,
                    'B' => 1,
                    _ => null,
                };
                if (row == null)
                {
                    continue;
                }
                if (int.TryParse(pick.Substring(1), out var column))
                {
                    if (column > 0 && column <= DifficlutyLevel.NumberOfWords)
                    {
                        selectedElement = Board[row.Value, column - 1];
                        if (selectedElement.State == ElementState.Matched)
                        {
                            Console.WriteLine("This card is already matched");
                            selectedElement = null;
                        }
                    }

                }

            }
            return selectedElement;
        }
    }
}

