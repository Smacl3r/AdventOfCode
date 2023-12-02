var dataLines = File.ReadAllLines("../../../data.txt");
var games = new List<Game>();

foreach (var line in dataLines)
{
    var splitSemicolon = line.Split(':');

    var setData = splitSemicolon[1].Split(';');

    var sets = ReadLine(setData);
    games.Add(new Game(int.Parse(splitSemicolon[0][5..]), sets));
}

var result = CalculateGameSum(games);
var cubeSum = CalculateCubeSum(games);

Console.WriteLine(result);
Console.WriteLine(cubeSum);

int CalculateGameSum(List<Game> games)
{
    int sum = games
        .Where(game => game.Sets.All(set => !IsOverLimit(set)))
        .Sum(game => game.Id);
    return sum;
}

int CalculateCubeSum(List<Game> games)
{
    var sum = 0;
    foreach (var game in games)
    {
        var calculatedCubes = FindHighestCubes(game);
        sum += (calculatedCubes.BlueCubes * calculatedCubes.RedCubes * calculatedCubes.GreenCubes);
    }
    return sum;
}

Set FindHighestCubes(Game game)
{
    var resultCubeSet = new Set(0, 0, 0);
    foreach (var set in game.Sets)
    {
        resultCubeSet.BlueCubes = Math.Max(resultCubeSet.BlueCubes, set.BlueCubes);
        resultCubeSet.GreenCubes = Math.Max(resultCubeSet.GreenCubes, set.GreenCubes);
        resultCubeSet.RedCubes = Math.Max(resultCubeSet.RedCubes, set.RedCubes);
    }
    return resultCubeSet;
}

bool IsOverLimit(Set set)
{
    if (set.RedCubes > 12 || set.GreenCubes > 13 || set.BlueCubes > 14)
        return true;
    return false;
}
static List<Set> ReadLine(string[] setData)
{
    var sets = new List<Set>();
    foreach (var set in setData)
    {
        Set setResult = new(0, 0, 0);
        var rolls = set.Split(',');
        foreach (var roll in rolls)
        {
            var splitRoll = roll.Trim().Split(' ');
            switch (splitRoll[1])
            {
                case "red":
                    setResult.RedCubes = int.Parse(splitRoll[0]); break;
                case "green":
                    setResult.GreenCubes = int.Parse(splitRoll[0]); break;
                case "blue":
                    setResult.BlueCubes = int.Parse(splitRoll[0]); break;
                default:
                    break;
            }
        }
        sets.Add(setResult);
    }
    return sets;
}

record Game(int Id, List<Set> Sets);
class Set
{
    public int BlueCubes { get; set; }
    public int GreenCubes { get; set; }
    public int RedCubes { get; set; }

    public Set(int blueCubes, int greenCubes, int redCubes)
    {
        BlueCubes = blueCubes;
        GreenCubes = greenCubes;
        RedCubes = redCubes;
    }
}