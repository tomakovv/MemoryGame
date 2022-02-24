using System.Text.Json;

namespace Memory_game
{
    public class ScoreBoard
    {
        private const int MAX_RESULTS = 10;
        public string Path = @"scoreBoard.txt";
        List<Result> results;


        public ScoreBoard()
        {
            results = new List<Result>();
        }

        public void AddResult(Result result)
        {
            results.Add(result);
        }

        public void Print()
        {
            foreach (var resultGroup in results.GroupBy(x => x.DifficultyLevel))
            {
                var i = 0;
                Console.WriteLine($"Difficulty level: {resultGroup.Key}");
                foreach (var result in resultGroup.OrderBy(x => x.Time).Take(MAX_RESULTS))
                {
                    Console.WriteLine($"{++i}. Name: {result.Name} | time: {result.Time.TotalSeconds:0.00} sec | tries: {result.Tries} | date: {result.Date} ");
                }
            }

        }
        public void Read()
        {
            if (File.Exists(Path))
            {
                var text = File.ReadAllText(Path);
                results = JsonSerializer.Deserialize<IEnumerable<Result>>(text).ToList();
            }
        }
        public void Write()
        {
            File.WriteAllText(Path, JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
