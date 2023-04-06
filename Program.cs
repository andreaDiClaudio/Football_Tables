internal class Program
{
    
    private static void Main(string[] args)
    {
        List<League> leagues = new List<League>();
        League superLigaen;
        League nordicBetLigaen;

        string SuperLigaMatchesFolder = "./csv/superliga_matches";
        string NordicBetLigaMatchesFolder = "./csv/nordicbetliga_matches";

        try 
        {
            // Create Leagues, adds them to a List, and saves it:
            leagues = Setup();
            // Create Teams, saves them inside the corresponding league:
            LoadTeams("./csv/teams_superligaen.csv", "./csv/teams_nordicbetligaen.csv", leagues);
            // Setup is now ready! There are two leagues, each with its own List of teams,
            // And the teams are initialized as well!
            // We can now start working with it.
            superLigaen = leagues[0];
            nordicBetLigaen = leagues[1];


            // CSV FILE VALIDATION:
            ValidateMatches(SuperLigaMatchesFolder, superLigaen.Teams);
            ValidateMatches(NordicBetLigaMatchesFolder, nordicBetLigaen.Teams);

            // This works! =)
            List<Team> ordered = superLigaen.Teams.OrderByDescending(team => team.Points)
                                    .ThenByDescending(team => (team.GoalsFor - team.GoalsAgainst))
                                    .ThenByDescending(team => team.GoalsFor)
                                    .ThenBy(team => team.GoalsAgainst)
                                    .ThenBy(team => team.FullName)
                                    .ToList();

            foreach (Team team in ordered)
            {
                //Console.WriteLine(team.ToString());
            }
            
            //Andrea - Table formatting test with leagues
            //printTable(superLigaen);
            //printTable(nordicBetLigaen);

                //Console.WriteLine(team.ToString()); TODO PUT IT BACK IN
            }
            
            ReadMatch( SuperLigaMatchesFolder,superLigaen); //Refactor name?
            ReadMatch(NordicBetLigaMatchesFolder, nordicBetLigaen); //Refactor name?
          
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

    // A method to read match data from CSV files and update the league standings.
    public static void ReadMatch(string csvFolder, League league) {

    if (Directory.Exists(csvFolder)) {
        // Get all CSV files in the specified folder
        string[] matchPath = Directory.GetFiles(csvFolder, "*.csv");
        // Process each CSV file
        foreach (string match in matchPath) {
            using StreamReader reader = new(match);
            // Skip the first line (header row)
            reader.ReadLine();
            // Process each line of the CSV file
            while (!reader.EndOfStream) {

                // Read a line from the CSV file and split it into an array of values
                string line = reader.ReadLine();
                string[] values = line.Split(",");
                // Split the third value (match result) into home and away scores
                string[] result = values[2].Split("-");
                int homeResult = int.Parse(result[0]); 
                int awayResult = int.Parse(result[1]);

                // Find the home and away teams in the league by their abbreviations
                Team teamHome = league.Teams.Find(team => team.Abbreviation == values[0]); 
                Team teamAway = league.Teams.Find(team => team.Abbreviation == values[1]);

                // Update the teams' standings based on the match result
                teamHome.GamesPlayed++; 
                teamAway.GamesPlayed++;

                if (homeResult > awayResult) {
                    teamHome.GamesWon++; 
                    teamAway.GamesLost++;
                    teamHome.Points += 3;
                    teamHome.WinningStreak.Enqueue("W"); 
                    if (teamHome.WinningStreak.Count > 5) { 
                        teamHome.WinningStreak.Dequeue(); 
                    }
                    teamAway.WinningStreak.Enqueue("L"); 
                    if (teamAway.WinningStreak.Count > 5) {
                        teamAway.WinningStreak.Dequeue(); 
                    }
                } 
                else if (homeResult < awayResult) {
                    teamAway.GamesWon++; 
                    teamHome.GamesLost++; 
                    teamAway.Points += 3;
                    teamHome.WinningStreak.Enqueue("L"); 
                    if (teamHome.WinningStreak.Count > 5) { 
                        teamHome.WinningStreak.Dequeue(); 
                    }
                    teamAway.WinningStreak.Enqueue("W"); 
                    if (teamAway.WinningStreak.Count > 5) {
                        teamAway.WinningStreak.Dequeue(); 
                    }
                }    
                else 
                {
                    teamHome.GamesTied++; 
                    teamAway.GamesTied++;
                    teamHome.Points += 1; 
                    teamAway.Points += 1; 
                }

                teamHome.GoalsFor += homeResult; 
                teamAway.GoalsFor += awayResult;
                teamHome.GoalsAgainst += awayResult; 
                teamAway.GoalsAgainst += homeResult;

                // Print the league standings after each match
                Console.WriteLine($"/*{league.Name}*/"); 
                PrintTable(league); 
                }
            }

        // Read all the .csv files to check if the info inside is OK:
        public static void ValidateMatches(string csvFolder, List<Team> teams)
        {
            if (Directory.Exists(csvFolder))
            {
                string[] matchPath = Directory.GetFiles(csvFolder, "*.csv");
                foreach (string match in matchPath)
                {
                    using (StreamReader reader = new StreamReader(match))
                    {
                        // Again, ignore first line in every file:
                        reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] values = line.Split(",");
                            Team teamA = teams.Find(team => team.Abbreviation == values[0]);
                            Team teamB = teams.Find(team => team.Abbreviation == values[1]);

                            if (teamA == null || teamB == null)
                            {
                                Console.WriteLine(values[0] + " or " + values[1] + " in file: " + match + " does not exist as a team.");
                                throw new Exception();
                            }
                        }
                    }
                }         
            } 
            else 
            {
                Console.WriteLine("No such directory.");
                throw new DirectoryNotFoundException();
            }
        }
            
        }
    }

    static void PrintTable(League league) {
    //Prints the table header
    Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------------------------------------------+");
    Console.Write("|");
    Console.Write("{0,-4} {1,-6} {2,-5} {3,-25} {4,-12} {5,-9} {6,-11} {7,-9} {8,-12} {9,-13} {10,-9} {11,-8} {12,-15}",
    "Pos", "Abbrev", "Mark", "Club-Name", "Games-Played", "Games-Won", "Games-Drawn", "Games-Lost",
    "Goals-For", "Goals-Against", "Goal-Diff", "Points", "Winning-Streak");
    Console.Write("|");
    Console.WriteLine();

    // Sort the teams by points in descending order
    List<Team> sortedTeams = league.Teams.OrderByDescending(t => t.Points).ToList();

    // Assign positions to each team based on their sorted order, checking for the same position
    int position = 1;
    int prevPoints = -1;
    for (int i = 0; i < sortedTeams.Count; i++)
    {
        Team team = sortedTeams[i];
        int difference = team.GoalsFor - team.GoalsAgainst;
        string winningStreak = string.Join("|", team.WinningStreak);
        Console.Write("|");

        // Check if the team has the same points as the previous or next team
        if (team.Points != prevPoints && (i == sortedTeams.Count - 1 || team.Points != sortedTeams[i + 1].Points))
        {
            // Assign the team the next available position
            Console.Write("{0,-4} {1,-6} {2,-5} {3,-25} {4,-12} {5,-9} {6,-11} {7,-10} {8,-12} {9,-13} {10,-9} {11,-8} {12,-15}",
            position, team.Abbreviation, team.SpecialRanking, team.FullName, team.GamesPlayed, team.GamesWon, team.GamesTied, team.GamesLost,
            team.GoalsFor, team.GoalsAgainst, difference, team.Points, winningStreak);
            position++;
        }
        else
        {
            // Assign the team a position of '-'
            Console.Write("{0,-4} {1,-6} {2,-5} {3,-25} {4,-12} {5,-9} {6,-11} {7,-10} {8,-12} {9,-13} {10,-9} {11,-8} {12,-15}",
            "-", team.Abbreviation, team.SpecialRanking, team.FullName, team.GamesPlayed, team.GamesWon, team.GamesTied, team.GamesLost,
            team.GoalsFor, team.GoalsAgainst, difference, team.Points, winningStreak);
        }
        Console.Write("|");
        Console.WriteLine();
        prevPoints = team.Points;
    }

    // Prints the table footer
    Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------------------------------------------+");
    }
}