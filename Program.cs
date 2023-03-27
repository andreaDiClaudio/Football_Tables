﻿internal class Program
{
    private static void Main(string[] args)
    {
        List<League> leagues = new List<League>();

        try 
        {
            // Create Leagues, adds them to a List, and saves it:
            leagues = Setup();
            // Create Teams, saves them inside the corresponding league:
            LoadTeams("./csv/teams_superligaen.csv", "./csv/teams_nordicbetligaen.csv", leagues);
            // Setup is now ready! There are two leagues, each with its own List of teams,
            // And the teams are initialized as well!
            // We can now start working with it.
        } catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }

    public static List<League> Setup()
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
            return leagues;
        } else 
        {
            Console.WriteLine("Error reading the setup.csv file. Maybe it changed places?");
            throw new FileNotFoundException();
        }
    }

    public static void LoadTeams(string superLigaPath, string nordicBetLigaPath, List<League> leagues)
    {
        // Add to Super Liga List of Teams:
        if (File.Exists(superLigaPath))
        {
            using (StreamReader reader = new StreamReader(superLigaPath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(",");
                    Team team = new Team(values[0], values[1], values[2]);
                    leagues[0].Teams.Add(team);
                }
            }
        } else 
        {
            Console.WriteLine("Error reading the Super Liga file. Maybe it changed places?");
            throw new FileNotFoundException();
        }

        // Add to Nordic Bet League List of Teams:
        if (File.Exists(nordicBetLigaPath))
        {
            using (StreamReader reader = new StreamReader(nordicBetLigaPath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(",");
                    Team team = new Team(values[0], values[1], values[2]);
                    leagues[1].Teams.Add(team);
                }
            }
        } else 
        {
            Console.WriteLine("Error reading the Nordic Bet Liga file. Maybe it changed places?");
            throw new FileNotFoundException();
        }
    }

}