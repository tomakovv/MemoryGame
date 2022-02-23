

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

        public void Play()
        {
            for (int i = 0; i < DifficlutyLevel.GuessChances; i++)
            {
                ShowTheBoard();
                var word1 = PickTheWord();
                word1.SetStatus(ElementState.Revealed);
                ShowTheBoard();
                var word2 = PickTheWord();
                word2.SetStatus(ElementState.Revealed);
                if (word1.Back == word2.Back)
                {
                    word2.SetStatus(ElementState.Matched);
                    word1.SetStatus(ElementState.Matched);
                    ShowTheBoard();
                }
                else if(word2.Back != word1.Back)
                {
                    ShowTheBoard();
                    word2.SetStatus(ElementState.Hidden);
                    word1.SetStatus(ElementState.Hidden);
                    DifficlutyLevel.GuessChances--;
                }
               

            }
          
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

        public void ShowTheBoard()
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Level: " + DifficlutyLevel.Name);
            Console.WriteLine("Guess chances: " + DifficlutyLevel.GuessChances);
            Console.WriteLine();
            for (int i = 0; i < DifficlutyLevel.NumberOfWords; i++)
            {
                Console.Write($"{i + 1} ");
            }
            Console.WriteLine();
            for (int i = 0; i < 2; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.Write("A ");
                        break;
                    case 1:
                        Console.Write("B ");
                        break;

                    default:
                        break;
                }


                for (int j = 0; j < DifficlutyLevel.NumberOfWords; j++)
                {
                    Console.Write(Board[i, j].ShowElement() + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-----------------------------");
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
                    }

                }

            }
            return selectedElement;
        }
    }
}

