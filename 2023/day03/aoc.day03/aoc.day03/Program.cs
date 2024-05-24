


using System.Drawing;

var lines = File.ReadAllLines("../../../../../input");
//var lines = File.ReadAllLines("../../../../../part_1_example");
//var lines = File.ReadAllLines("../../../../../part_2_example");

var directions = new List<Point>()
{
    new(-1, 1), // left and up
    new(0, 1),  // up
    new(1, 1),  // right and up
    new(-1, 0), // left
    new(1, 0),  // right
    new(-1, -1),// left and down
    new(0, -1), // down
    new(1, -1)  // right and down
};

PartOne();
new PartTwo(lines, directions).Go();


return;


void PartOne()
{
    var sum = 0;
    
    for(var row = 0; row < lines.Length; row++)
    {
        string? num = null;
        var adjacentSymbol = false;
        
        for(var column = 0; column < lines[row].Length; column++)
        {
            var current = lines[row][column];
            
            if (char.IsDigit(current))
            {
                if(!adjacentSymbol)
                    adjacentSymbol = CheckForAdjacentSymbol(row, column);
                
                num += current;
            }
            
            // If the current character is not a digit, or we are at the end of the line
            if (!char.IsDigit(current) || column == lines[row].Length - 1)
            {
                if (num != null)
                {
                    sum = AddNumberToSum(sum, num, adjacentSymbol);
                    num = null;
                    adjacentSymbol = false;
                }
            }
           
        }
    }
    
    Console.WriteLine($"Part One Result: {sum.ToString()}"); // Correct answer is 556367
    
}

bool CheckForAdjacentSymbol(int row, int column)
{
    foreach (var point in directions)
    {
        var checkRow = row + point.X;
        var checkColumn = column + point.Y;
        
        if(checkRow < 0 || checkRow >= lines.Length || checkColumn < 0 || checkColumn >= lines[checkRow].Length)
            continue;

        if (!char.IsDigit(lines[checkRow][checkColumn]) && !lines[checkRow][checkColumn].Equals('.'))
        {
            return true;
        }
    }

    return false;
}

int AddNumberToSum(int sum, string? num, bool hasAdjacentSymbol)
{
    if(!hasAdjacentSymbol)
        return sum;
    
    if(string.IsNullOrEmpty(num))
        throw new Exception("Number should not be null");
    
    return  sum + int.Parse(num);
}

internal class PartTwo(string[] lines, List<Point> directions)
{
    public void Go()
    {
        var asterisks = new Dictionary<Point, List<int>>();
        string? num = null;
        var surroundingAsterisks = new HashSet<Point>();

        for (var row = 0; row < lines.Length; row++)
        {
            for (var column = 0; column < lines[row].Length; column++)
            {
                var current = lines[row][column];
            
                if (char.IsDigit(current))
                {
                    CheckForSurroundingAsterisks(row, column, surroundingAsterisks);
                    num += current;
                }
            
                // If we have found a number but the current character is not a digit, or we are at the end of the line
                // need to complete processing of the number
                if (num!= null && (!char.IsDigit(current) || column == lines[row].Length - 1))
                {
                    
                    foreach (var p in surroundingAsterisks)
                    {
                        if (asterisks.ContainsKey(p))
                            asterisks[p].Add(int.Parse(num));
                        else
                            asterisks.Add(p, [int.Parse(num)]);
                    }

                    num = null;
                    surroundingAsterisks.Clear();
                }
            }
        }

        var sum = asterisks.Values.Where(numbers => numbers.Count == 2).Sum(nums => nums[0] * nums[1]);

        Console.WriteLine($"Part Two Result: {sum.ToString()}"); // Correct answer is 89471771
    }

    private void CheckForSurroundingAsterisks(int row, int column, HashSet<Point> asterisks)
    {
        foreach(var direction in directions)
        {
            var currentRow = row + direction.X;
            var currentColumn = column + direction.Y;
            
            if(currentRow < 0 || currentRow >= lines.Length || currentColumn < 0 || currentColumn >= lines[currentRow].Length)
                continue;
            
            if(lines[currentRow][currentColumn].Equals('*'))
                asterisks.Add(new Point(currentColumn, currentRow));

        }
    }
}

