var dataLines = File.ReadAllLines("../../../data.txt");

var races = ReadData(dataLines);

var result = CalculateResult(races);

Console.WriteLine(result);

List<Race> ReadData(string[] dataLines)
{
    var races = new List<Race>();
    var times = dataLines[0].Split(':')[1].Split(' ');
    var distances = dataLines[1].Split(':')[1].Split(' ');
    for(int i = 0; i < times.Length; i++)
    {
        races.Add(new Race(long.Parse(times[i]), long.Parse(distances[i])));
    }
    return races;
}

int CalculateResult(List<Race> races)
{
    var res = 1;
    var result = new List<int>();
    
    foreach (var race in races)
    {
        int winningWays = 0;
        for (int i = 0; i < race.Time; i++)
        {
            if(i * (race.Time - i) > race.Distance)
                winningWays++;
        }
        result.Add(winningWays);
    }

    foreach (var winCount in result)
    {
        res *= winCount;
    }
    return res;
}

record Race(long Time, long Distance);