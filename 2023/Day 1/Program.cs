var dataLines = File.ReadAllLines("../../../data.txt");

var sum = 0;

string[] possibleDigitWords = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
var possibleDigits = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
foreach (var line in dataLines)
{
    var digits = CalculateDigits(line);
    sum += digits;
}

int CalculateDigits(string line)
{

    var firstDigitIndex = line.IndexOfAny(possibleDigits);
    var (firstWordDigitIndex, firstDigit) = CalculateFirstNumberWordIndex(line);
    var lastDigitIndex = line.LastIndexOfAny(possibleDigits);
    var (lastWordDigitIndex, lastDigit) = CalculateLastNumberWordIndex(line);

    double answerFirstDigit = 0;
    double answerLastDigit = 0;

    if (firstDigitIndex < firstWordDigitIndex)
        answerFirstDigit = char.GetNumericValue(line[firstDigitIndex]);
    else
        answerFirstDigit = firstDigit;

    if(lastDigitIndex > lastWordDigitIndex)
        answerLastDigit = char.GetNumericValue(line[lastDigitIndex]);
    else
        answerLastDigit = lastDigit;

    return int.Parse(answerFirstDigit.ToString() + answerLastDigit.ToString());
}

(int index, int digit) CalculateFirstNumberWordIndex(string line)
{
    var index = int.MaxValue;
    var digit = 0;
    for (int i = 0; i < possibleDigitWords.Length; i++)
    {
        if (line.Contains(possibleDigitWords[i]))
        {
            var digitIndex = line.IndexOf(possibleDigitWords[i]);
            if (digitIndex < index)
            {
                index = digitIndex;
                digit = i + 1;
            }
        }
    }
    return (index, digit);
}

(int index, int digit) CalculateLastNumberWordIndex(string line)
{
    var index = int.MinValue;
    var digit = 0;
    for (int i = 0; i < possibleDigitWords.Length; i++)
    {
        if (line.Contains(possibleDigitWords[i]))
        {
            var digitIndex = line.LastIndexOf(possibleDigitWords[i]);
            if (digitIndex > index)
            {
                index = digitIndex;
                digit = i + 1;
            }
        }
    }
    return (index, digit);
}

System.Console.WriteLine(sum);