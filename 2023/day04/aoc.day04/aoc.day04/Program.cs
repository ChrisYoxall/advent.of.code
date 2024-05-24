var lines = File.ReadAllLines("../../../../../input");
//var lines = File.ReadAllLines("../../../../../part_1_example");
//var lines = File.ReadAllLines("../../../../../part_2_example");


new PartOne(lines).Go();
new PartTwo(lines).Go();

internal class PartOne(string[] lines)
{
    private double _total;

    public void Go()
    {
        
        foreach (var line in lines)
        {
            var parts = line.Split(':');
            var numbers = parts[1].Split('|');
            var winningNumbers = numbers[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray();
            var cardNumbers = numbers[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray();

            var matchCount = winningNumbers.Intersect(cardNumbers).Count();
            
            if (matchCount > 0)
                _total += Math.Pow(2d, Convert.ToDouble(matchCount - 1));
        }
        
        Console.WriteLine($"Part One Result: {_total}"); // Correct answer is 24848
    }
    
}

internal class PartTwo(string[] lines)
{
    public void Go()
    {
        var cardCount = new int[lines.Length];
        for (var i = 0; i < cardCount.Length; i++)
        {
            cardCount[i] = 1;
        }

        for (var cardId = 0; cardId < lines.Length; cardId++)
        {
            var line = lines[cardId];
            
            var parts = line.Split(':');
            var numbers = parts[1].Split('|');
            var pickedNumbers = numbers[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray();
            var ourNumbers = numbers[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToArray();

            var matchCount = pickedNumbers.Intersect(ourNumbers).Count();

            for (int i = 0; i < matchCount; i++)
            {
                cardCount[cardId + 1 + i] += cardCount[cardId];
            }
        }
        
        Console.WriteLine($"Part Two Result: {cardCount.Sum()}"); // Correct answer is 7258152
    }
}
