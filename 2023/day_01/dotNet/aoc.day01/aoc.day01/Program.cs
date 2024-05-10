
var lines = File.ReadAllLines("../../../../../../input");
//var lines = File.ReadAllLines("../../../../../../part_1_example");
//var lines = File.ReadAllLines("../../../../../../part_2_example");

PartOne();
PartTwo();
return;

void PartOne()
{
    long total = 0;
    
    foreach (var line in lines)
    {
        // Could quickly get the value by taking advantage of the ASCII values. As char '0' has a value of 48
        // if '0' is subtracted from the found digit we get difference in the ASCII values, which will be
        // its value. That is, do:
        // var firstDigit = line.First(c => char.IsDigit(c)) - '0';
        
        var firstDigit = line.First(c => char.IsDigit(c));
        var firstNumber = ParseStrToInt(firstDigit.ToString());
        
        var lastDigit = line.Last(c => char.IsDigit(c));
        var lastNumber = ParseStrToInt(lastDigit.ToString());

        total += firstNumber * 10 + lastNumber;
        
    }
    
    Console.WriteLine($"Part One Result: {total}"); // Correct answer is 56465
    
    
}

int ParseStrToInt(string input)
{
    if (int.TryParse(input.ToString(), out var result))
        return result;

    throw new ArgumentException($"Could not parse string to int: {input}");
}

void PartTwo()
{
    var txtDigits = new Dictionary<string, int>()
    {
        {"one", 1},
        {"two", 2},
        {"three", 3},
        {"four", 4},
        {"five", 5},
        {"six", 6},
        {"seven", 7},
        {"eight", 8},
        {"nine", 9}
    };

    for (var i = 1; i < 10; i++)
        txtDigits.Add(i.ToString(), i);

    long total = 0;
    
    foreach (var line in lines)
    {
        var firstIndex = line.Length;
        var lastIndex = -1;
        var firstNumber = 0;
        var lastNumber = 0;

        foreach (var digit in txtDigits)
        {
            var index = line.IndexOf(digit.Key, StringComparison.Ordinal);
            
            if (index == -1)
                continue;

            if (index < firstIndex)
            {
                firstIndex = index;
                firstNumber = digit.Value;
            }
            
            index = line.LastIndexOf(digit.Key, StringComparison.Ordinal);

            if (index > lastIndex)
            {
                lastIndex = index;
                lastNumber = digit.Value;
            }
        }
        
        total += firstNumber * 10 + lastNumber;
    }
    
    Console.WriteLine($"Part Two Result: {total}"); // Correct answer is 55902
}