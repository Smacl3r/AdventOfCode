var dataLines = File.ReadAllLines("../../../data.txt");

var (seeds, maps) = ReadData(dataLines);

var recalculatedSeeds = RecalculateSeeds(seeds);
var result = CalculateLowestDestinationValue(seeds, maps);

var answerList = new List<long>();
Parallel.For(0, 10000, i =>
{
    int startIndex = i * (recalculatedSeeds.Count / 10000);
    var subsetSeeds = recalculatedSeeds.Skip(startIndex).Take(recalculatedSeeds.Count / 10000).ToList();
    answerList.Add(CalculateLowestDestinationValue(subsetSeeds, maps));
    Console.WriteLine("Progress: {0}/{1}", i, 10000);
});

var recalculatedResult = answerList.Min();

Console.WriteLine(result);
Console.WriteLine(recalculatedResult);

(List<long> seeds, List<Map> maps) ReadData(string[] dataLines)
{
    var seeds = dataLines[0].Split(':')[1].Split(' ').Select(x => long.Parse(x)).ToList();


	var maps = new List<Map>();
	var matches = new List<Match>();
	foreach (var line in dataLines.Skip(2))
	{
		if(string.IsNullOrEmpty(line))
		{
			maps.Add(new Map(matches));
			matches = new List<Match>();
			continue;
		}
		if (line.Contains(':'))
			continue;
		var matchParts = line.Split(' ');
		matches.Add(new Match(long.Parse(matchParts[0]), long.Parse(matchParts[1]), long.Parse(matchParts[2])));
	}
	return (seeds, maps);
}

List<long> RecalculateSeeds(List<long> seeds)
{
	var result = new List<long>();
	for (int i = 0; i < seeds.Count/2; i++)
	{
		result.Add(seeds[i*2]);
		for (int j = 0; j < seeds[1+i*2]; j++)
		{
			result.Add(seeds[i * 2] + j);
		}
	}
	return result;
}

long CalculateLowestDestinationValue(List<long> seeds, List<Map> maps)
{
    var seedMatching = new List<MatchingSeed>();
    seedMatching.AddRange(seeds.Select(x => new MatchingSeed(x)));
    foreach (var map in maps)
    {
        seedMatching = seedMatching.Select(x => new MatchingSeed(x.SeedNr, false)).ToList();
        foreach (var match in map.Matches)
        {
            seedMatching = seedMatching.Select(x =>
                (x.SeedNr >= match.Source && x.SeedNr <= match.Source + match.Length - 1 && !x.IsChanged) ?
                (match.Source > match.Destination ? 
				new MatchingSeed(x.SeedNr - match.Source + match.Destination, true) : 
				new MatchingSeed(x.SeedNr - match.Source + match.Destination, true)) :
                x)
                .ToList();
        }
    }
    return seedMatching.Min(x => x.SeedNr);
}

record Map(List<Match> Matches);
record Match(long Destination, long Source, long Length);

class MatchingSeed
{
	public long SeedNr { get; set; }
	public bool IsChanged { get; set; }

	public MatchingSeed(long seedNr)
	{
		SeedNr = seedNr;
		IsChanged = false;
	}

    public MatchingSeed(long seedNr, bool isChanged)
    {
        SeedNr = seedNr;
        IsChanged = isChanged;
    }
}