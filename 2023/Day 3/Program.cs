var dataLines = File.ReadAllLines("../../../data.txt");

var (parts, symbols) = ReadDataPart1(dataLines);

var result = CalculateResultPart1(parts, symbols);

var (possibleParts, gearSymbols) = ReadDataPart2(dataLines);

var gearResult = CalculateGearResult(possibleParts, gearSymbols);

Console.WriteLine(result);
Console.WriteLine(gearResult);

int CalculateGearResult(List<PossiblePart> parts, List<Symbol> symbols)
{
    int sum = 0;
    foreach (var symbol in symbols)
    {
        var adjacentParts = GetAdjacentParts(symbol, parts);
        if (adjacentParts.Count > 1)
        {
            sum += (adjacentParts.First().Number * adjacentParts.Last().Number);
        }

    }
    return sum;
}

List<PossiblePart> GetAdjacentParts(Symbol symbol, List<PossiblePart> parts)
{
    var (startX, endX, startY, endY) = CheckPossibleGearCoordinates(symbol);
    return parts.Where(p => ContainsXCoordinates(startX, endX, p.X, p.Length) && (p.Y >= startY && p.Y <= endY)).ToList();
}

bool ContainsXCoordinates(int startX, int endX, int x, int length)
{
    if ((startX <= x && x <= endX) || (startX <= (x + length - 1) && (x + length - 1) <= endX))
        return true;
    return false;
}

int CalculateResultPart1(List<PossiblePart> parts, List<Symbol> symbols)
{
    int sum = 0;
    foreach (var part in parts)
    {
        if (HasAdjacentSymbol(part, symbols))
            sum += part.Number;
    }
    return sum;
}

bool HasAdjacentSymbol(PossiblePart part, List<Symbol> symbols)
{
    var (startX, endX, startY, endY) = CheckPossibleCoordinates(part);
    if (symbols.Any(s => (s.X >= startX && s.X <= endX) && (s.Y >= startY && s.Y <= endY)))
        return true;
    return false;
}

(int startX, int endX, int startY, int endY) CheckPossibleCoordinates(PossiblePart part)
{
    int startX = part.X - 1;
    int endX = part.X + part.Length;
    int startY = part.Y - 1;
    int endY = part.Y + 1;
    return (startX, endX, startY, endY);
}

(int startX, int endX, int startY, int endY) CheckPossibleGearCoordinates(Symbol symbol)
{
    int startX = symbol.X - 1;
    int endX = symbol.X + 1;
    int startY = symbol.Y - 1;
    int endY = symbol.Y + 1;
    return (startX, endX, startY, endY);
}

static (List<PossiblePart> parts, List<Symbol> symbols) ReadDataPart2(string[] dataLines)
{
    var parts = new List<PossiblePart>();
    var symbols = new List<Symbol>();

    for (int i = 0; i < dataLines.Length; i++)
    {
        bool isNumber = false;
        string number = "";
        PossiblePart? possiblePart = null;
        for (int j = 0; j < dataLines[i].Length; j++)
        {
            if (char.IsDigit(dataLines[i][j]))
            {
                if (!isNumber)
                {
                    possiblePart = new PossiblePart(0, j, i, 1);
                    parts.Add(possiblePart);
                    isNumber = true;
                }
                number += dataLines[i][j];
            }
            else
            {
                if (isNumber && possiblePart is not null)
                {
                    possiblePart.Number = int.Parse(number);
                    possiblePart.Length = number.Length;
                }

                isNumber = false;
                number = "";
                if (dataLines[i][j] == '*')
                    symbols.Add(new Symbol(j, i));
            }
        }
        if (isNumber && possiblePart is not null)
        {
            possiblePart.Number = int.Parse(number);
            possiblePart.Length = number.Length;
        }
    }
    return (parts, symbols);

}
static (List<PossiblePart> parts, List<Symbol> symbols) ReadDataPart1(string[] dataLines)
{
    var parts = new List<PossiblePart>();
    var symbols = new List<Symbol>();

    for (int i = 0; i < dataLines.Length; i++)
    {
        bool isNumber = false;
        string number = "";
        PossiblePart? possiblePart = null;
        for (int j = 0; j < dataLines[i].Length; j++)
        {
            if (char.IsDigit(dataLines[i][j]))
            {
                if (!isNumber)
                {
                    possiblePart = new PossiblePart(0, j, i, 1);
                    parts.Add(possiblePart);
                    isNumber = true;
                }
                number += dataLines[i][j];
            }
            else
            {
                if (isNumber && possiblePart is not null)
                {
                    possiblePart.Number = int.Parse(number);
                    possiblePart.Length = number.Length;
                }

                isNumber = false;
                number = "";
                if (dataLines[i][j] != '.')
                    symbols.Add(new Symbol(j, i));
            }
        }
        if (isNumber && possiblePart is not null)
        {
            possiblePart.Number = int.Parse(number);
            possiblePart.Length = number.Length;
        }
    }
    return (parts, symbols);

}

class PossiblePart
{
    public int Number { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Length { get; set; }

    public PossiblePart(int number, int x, int y, int length)
    {
        Number = number;
        X = x;
        Y = y;
        Length = length;
    }
}
record Symbol(int X, int Y);