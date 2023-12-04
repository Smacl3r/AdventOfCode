var dataLines = File.ReadAllLines("../../../data.txt");

var cards = ReadData(dataLines);
var result = CalculateResult(cards);
var resultPart2 = CalculateResultPart2(cards);

Console.WriteLine(result);
Console.WriteLine(resultPart2);

double CalculateResult(List<Card> cards)
{
    return cards.Sum(card =>
    {
        int count = card.WinningNumber.Count(num => card.Numbers.Contains(num));
        return (count != 0) ? Math.Pow(2.00, count - 1) : 0;
    });
}

int CalculateResultPart2(List<Card> cards)
{
    foreach (var card in cards)
    {
        for (int j = 0; j < card.Amount; j++)
        {
            int count = card.WinningNumber.Count(num => card.Numbers.Contains(num));

            for (int x = 0; x < count; x++)
            {
                cards[cards.IndexOf(card) + x + 1].Amount++;
            }
        }
    }

    return cards.Sum(card => card.Amount);
}
List<Card> ReadData(string[] dataLines)
{
    cards = new List<Card>();
    for (int i = 0; i < dataLines.Length; i++)
    {
        var numbersData = dataLines[i].Split(':')[1].Split('|');
        var winningNumbers = new List<int>();
        var numbers = new List<int>();
        foreach (var winNum in numbersData[0].Trim().Split(' '))
        {
            if (!string.IsNullOrEmpty(winNum))
                winningNumbers.Add(int.Parse(winNum.Trim()));
        }
        foreach (var num in numbersData[1].Trim().Split(' '))
        {
            if (!string.IsNullOrEmpty(num))
                numbers.Add(int.Parse(num.Trim()));
        }
        cards.Add(new Card(i + 1, winningNumbers, numbers));
    }
    return cards;
}

class Card
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public List<int> WinningNumber { get; set; }
    public List<int> Numbers { get; set; }

    public Card(int id, List<int> winNums, List<int> nums)
    {
        Id = id;
        Amount = 1;
        WinningNumber = winNums;
        Numbers = nums;
    }
};