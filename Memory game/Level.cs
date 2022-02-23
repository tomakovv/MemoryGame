
namespace Memory_game
{
    public enum LevelCode
    {
        Easy = 1,
        Hard = 2
    }

    public class Level
    {
        public Level(LevelCode levelCode, string name, int guessChances, int numberOfWords)
        {
            LevelCode = levelCode;
            Name = name;
            GuessChances = guessChances;
            NumberOfWords = numberOfWords;
        }

        public LevelCode LevelCode { get; }
        public string Name { get; }
        public int GuessChances { get; set; }
        public int NumberOfWords { get; }
    }
}
