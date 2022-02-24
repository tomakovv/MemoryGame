using System.Text.Json.Serialization;

namespace Memory_game
{
    public class Result
    {
        public Result(string name, int tries, TimeSpan time, LevelCode dificultyLevel)
        {
            Name = name;
            Tries = tries;
            Date = DateTime.Now;
            Time = time;
            DifficultyLevel = dificultyLevel;

        }
        public Result()
        {

        }

        [JsonInclude] public string Name { get; init; }
        [JsonInclude] public int Tries { get; init; }
        [JsonInclude] public DateTime Date { get; init; }
        [JsonInclude] public TimeSpan Time { get; init; }
        [JsonInclude] public LevelCode DifficultyLevel { get; init; }


        public string Write()
        {
            return $"{Name} | {DateTime.Now.ToString()} | {Tries} | {Time.TotalSeconds}";
        }
    }
}
