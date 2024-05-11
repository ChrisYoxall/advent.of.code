var lines = File.ReadAllLines("../../../../../../input");
//var lines = File.ReadAllLines("../../../../../../part_1_example");
//var lines = File.ReadAllLines("../../../../../../part_2_example");

const int maxRed = 12;
const int maxBlue = 14;
const int maxGreen = 13;


PartOne();
PartTwo();
return;

void PartOne()
{
    long total = 0;
    
    foreach (var line in lines)
    {
        
        // First part is game id, wnd part is sets
        var gameParts = line.Split(':', StringSplitOptions.TrimEntries);
        
        var gameId = int.Parse((gameParts[0].Split(' ', StringSplitOptions.TrimEntries))[1]);
        
        // All the different sets in a game
        var gameSets = gameParts[1].Split(';', StringSplitOptions.TrimEntries);

        if(AreSetsPossible(gameSets))
            total += gameId;
    }
    
    Console.WriteLine($"Part One Result: {total}"); // Correct answer is 2061
}

bool AreSetsPossible(string[] gameSets)
{
    foreach (var set in gameSets)
    {
        // Get all cubes and colours in this set
        var colourCounts = set.Split(',', StringSplitOptions.TrimEntries);
        
        foreach (var colourCount in colourCounts)
        {
            var colour = colourCount.Split(' ', StringSplitOptions.TrimEntries);
            
            switch (colour[1])
            {
                case "red":
                    if (int.Parse(colour[0]) > maxRed)
                        return false;
                    break;
                    
                case "blue":
                    if (int.Parse(colour[0]) > maxBlue)
                        return false;
                    break;
                    
                case "green":
                    if (int.Parse(colour[0]) > maxGreen)
                        return false;
                    break;
                
                default:
                    throw new Exception("Unknown colour");
            }
        }
    }
    
    return true;
}



void PartTwo()
{
    long total = 0;
    
    foreach (var line in lines)
    {
        var redNeeded = 0;
        var blueNeeded = 0;
        var greenNeeded = 0;
        
        // First part is game id, wnd part is sets
        var gameParts = line.Split(':', StringSplitOptions.TrimEntries);
        
        var gameId = int.Parse((gameParts[0].Split(' ', StringSplitOptions.TrimEntries))[1]);
        
        // All the different sets in a game
        var gameSets = gameParts[1].Split(';', StringSplitOptions.TrimEntries);

        foreach (var set in gameSets)
        {
            // Get all cubes and colours in this set
            var colourCounts = set.Split(',', StringSplitOptions.TrimEntries);
            
            foreach (var colourCount in colourCounts)
            {
                var colour = colourCount.Split(' ', StringSplitOptions.TrimEntries);
                var count = int.Parse(colour[0]);
            
                switch (colour[1])
                {
                    case "red":
                        if (count > redNeeded)
                            redNeeded = count;
                        break;
                    
                    case "blue":
                        if (count > blueNeeded)
                            blueNeeded = count;
                        break;
                    
                    case "green":
                        if (count > greenNeeded)
                            greenNeeded = count;
                        break;
                
                    default:
                        throw new Exception("Unknown colour");
                }
            }
        }
        
        total += redNeeded * blueNeeded * greenNeeded;
    }
    
    Console.WriteLine($"Part Two Result: {total}"); // Correct answer is 72596
}