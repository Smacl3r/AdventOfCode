var dataLines = File.ReadAllLines("../../../data.txt");

string instructions = dataLines[0];

Dictionary<string, (string left, string right)> moves = [];

foreach (var line in dataLines.Skip(2))
{
    moves.Add(line[..3], (line.Substring(7, 3), line.Substring(12, 3)));
}

// PART 1
//int moveCount = 0;
//var currentMove = moves.First(x => x.Key == "AAA");
//while (currentMove.Key != "ZZZ")
//{
//    foreach (var instruction in instructions)
//    {
//        moveCount++;
//        currentMove = instruction == 'L' ? moves.First(x => x.Key == currentMove.Value.left) : moves.First(x => x.Key == currentMove.Value.right);

//        if (currentMove.Key == "ZZZ")
//        {
//            Console.WriteLine(moveCount);
//            break;
//        }
//    }
//}

//Console.WriteLine(moveCount);

// PART 2

List<long> moveCount2 = [];
var currentMoves = moves.Where(x => x.Key.EndsWith('A')).ToList();
foreach (var move in currentMoves)
{
    var currentMove = move;
    int crMC = 0;
    while (!currentMove.Key.EndsWith('Z'))
    {
        foreach (var instruction in instructions)
        {
            crMC++;
            currentMove = instruction == 'L' ? moves.First(x => x.Key == currentMove.Value.left) : moves.First(x => x.Key == currentMove.Value.right);

            if (currentMove.Key.EndsWith('Z'))
            {
                Console.WriteLine(crMC);
                break;
            }
        }
    }
    moveCount2.Add(crMC);
}

var res = LCMOfArray(moveCount2.ToArray());

Console.WriteLine(res);

static long GCD(long a, long b)
{
    while (b != 0)
    {
        long temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

static long LCM(long a, long b)
{
    return a * b / GCD(a, b);
}

static long LCMOfArray(long[] arr)
{
    long lcm = LCM(arr[0], arr[1]);

    for (int i = 2; i < arr.Length; i++)
    {
        lcm = LCM(lcm, arr[i]);
    }

    return lcm;
}