//var dataLines = File.ReadAllLines("../../../mockData.txt");

//var cards = new List<Card>() 
//{ 
//    new('A', 0),
//    new('K', 1),
//    new('Q', 2),
//    new('J', 3),
//    new('T', 4),
//    new('9', 5),
//    new('8', 6),
//    new('7', 7),
//    new('6', 8),
//    new('5', 9),
//    new('4', 10),
//    new('3', 11),
//    new('2', 12)
//};

//var hands = ReadData(dataLines);

//var result = CalculateResult(hands, cards);

//Console.WriteLine(result);

//static List<Hand> ReadData(string[] dataLines)
//{
//    var hands = new List<Hand>();
//    foreach (var line in dataLines)
//    {
//        var dataParts = line.Split(' ');
//        hands.Add(new Hand([.. dataParts[0].ToCharArray()], int.Parse(dataParts[1])));
//    }
//    return hands;
//}

//int CalculateResult(List<Hand> hands, List<Card> cards)
//{
//    hands.Sort();
//    var sum = 0;
//    for (int i = 0; i < hands.Count; i ++)
//    {
//        sum += i * hands[i].Bid;
//    }
//    return 0;
//}


//record Card(char symbol, int rank);

//class Hand : IComparable<Hand>
//{
//    public List<char> Symbols { get; set; }
//    public int Bid { get; set; }

//    public Hand(List<char> symbols, int bid)
//    {
//        Symbols = symbols;
//        Bid = bid;
//    }

//    public override string ToString()
//    {
//        return string.Concat(Symbols);
//    }

//    public int CompareTo(Hand? other)
//    {
//        if(other?.Symbols.Distinct().Count() < this.Symbols.Distinct().Count())
//            return 0;

//        return 1;
//    }
//}

using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        var dataLines = File.ReadAllLines("../../../mockData.txt");

        var hands = new List<string[]>();
        foreach (var line in dataLines)
        {
            hands.Add(line.Split());
        }

        var sortedHands = SortHands(hands);
        sortedHands.Reverse();

        int answer = 0;
        for (int i = 0; i < sortedHands.Count; i++)
        {
            answer += int.Parse(sortedHands[i][1]) * (i + 1);
        }

        Console.WriteLine(answer);
    }

    static List<string[]> SortHands(List<string[]> hands)
    {
        bool isSorted = true;
        for (int i = 0; i < hands.Count; i++)
        {
            if (i + 1 >= hands.Count)
            {
                break;
            }

            string[] hand1 = hands[i];
            string[] hand2 = hands[i + 1];

            if (IsHand2Stronger(hand1, hand2))
            {
                SwapHands(hands, i, i + 1);
                isSorted = false;
            }
        }

        if (!isSorted)
        {
            return SortHands(hands);
        }

        return hands;
    }

    static void SwapHands(List<string[]> hands, int index1, int index2)
    {
        var temp = hands[index1];
        hands[index1] = hands[index2];
        hands[index2] = temp;
    }

    static bool IsHand2Stronger(string[] hand1, string[] hand2)
    {
        var cardStrength = new List<char> { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };

        var hand1ComboStr = GetHandComboStrength(hand1);
        var hand2ComboStr = GetHandComboStrength(hand2);

        if (hand1ComboStr < hand2ComboStr)
        {
            return true;
        }

        if (hand1ComboStr == hand2ComboStr)
        {
            for (int i = 0; i < hand1.Length; i++)
            {
                if (cardStrength.IndexOf(hand1[i][0]) == cardStrength.IndexOf(hand2[i][0]))
                {
                    continue;
                }

                return cardStrength.IndexOf(hand1[i][0]) > cardStrength.IndexOf(hand2[i][0]);
            }
        }

        return false;
    }

    static int GetHandComboStrength(string[] hand)
    {
        var cardCount = new Dictionary<char, int>();
        foreach (var card in hand)
        {
            if (!cardCount.ContainsKey(card[0]))
            {
                cardCount[card[0]] = 0;
            }

            cardCount[card[0]]++;
        }

        foreach (var kvp in cardCount)
        {
            if (kvp.Value == 5)
            {
                return 6; // Five of a kind
            }

            if (kvp.Value == 4)
            {
                return 5; // Four of a kind
            }

            if (kvp.Value == 3)
            {
                if (hand.Count(crd => crd[0] == kvp.Key) == 2)
                {
                    return 4; // Full house
                }
            }

            if (kvp.Value == 2)
            {
                if (hand.Count(crd => crd[0] == kvp.Key) == 3)
                {
                    return 4; // Full house
                }
            }
        }

        if (cardCount.ContainsValue(3))
        {
            return 3; // Three of a kind
        }

        if (cardCount.Values.Count(value => value == 2) == 2)
        {
            return 2; // Two pair
        }

        if (cardCount.ContainsValue(2))
        {
            return 1; // One pair
        }

        return 0; // High card
    }
}