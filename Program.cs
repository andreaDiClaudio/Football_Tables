internal class Program
{
    private static void Main(string[] args)
    {
        try 
        {
            Setup();
        } catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }

    public static void Setup()
    {
        // Path to Setup File:
        string setupPath = "./csv/setup.csv";
        List<League> leagues = new List<League>();
        if (File.Exists(setupPath))
        {
            // Creates an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader:
            using (StreamReader reader = new StreamReader(setupPath))
            {
                // For skipping the first line:
                reader.ReadLine();
                // This reads from the file while the end is not yet reached:
                while (!reader.EndOfStream)
                {
                    // Reading lines, then working with them. Easy peasy.
                    string line = reader.ReadLine();
                    string[] values = line.Split(",");
                    League league = new League(values[0], Int32.Parse(values[1]), Int32.Parse(values[2]), Int32.Parse(values[3]), Int32.Parse(values[4]));
                    leagues.Add(league);
                }
            }
            Console.WriteLine(leagues[1].ToString());
        } else {
            Console.WriteLine("Error reading the setup.csv file. Maybe it changed places?");
            throw new FileNotFoundException();
        }
    }
}

// What can we make to make this more efficient?
// Reutilize methods for reading from files? (Only for teams I guess, the Setup needs its own?)
// Maybe create a Teams Class, so the program holds state of the teams?