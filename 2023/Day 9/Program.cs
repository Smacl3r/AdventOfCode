string[] inputLines = File.ReadAllLines("../../../data.txt");
List<List<int>> inputSequences = [];

foreach (string line in inputLines)
{
    inputSequences.Add(line.Split().Select(int.Parse).ToList());
}

List<int> resultNumbers = [];

foreach (List<int> inputSequence in inputSequences)
{
    List<int> currentSequence = new(inputSequence);
    currentSequence.Reverse(); // Part 2
    List<List<int>> differenceSequences = [];

    while (!currentSequence.SequenceEqual(Enumerable.Repeat(0, currentSequence.Count)))
    {
        List<int> differenceSequence = [];
        for (int i = 0; i < currentSequence.Count - 1; i++)
        {
            differenceSequence.Add(currentSequence[i + 1] - currentSequence[i]);
        }
        differenceSequences.Add(differenceSequence);
        currentSequence = new List<int>(differenceSequence);
    }

    int finalResult = inputSequence.First(); // Last() for part 1
    foreach (List<int> differenceSequence in differenceSequences)
    {
        finalResult += differenceSequence.Last();
    }

    resultNumbers.Add(finalResult);
}

int finalAnswer = resultNumbers.Sum();
Console.WriteLine(finalAnswer);