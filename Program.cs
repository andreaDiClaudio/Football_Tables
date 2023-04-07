internal class Program
{

    private static void Main(string[] args)
    {
        List<League> leagues = new();
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
            List<Team> orderedSuperLiga = superLigaen.Teams.OrderByDescending(team => team.Points)
                                    .ThenByDescending(team => (team.GoalsFor - team.GoalsAgainst))
                                    .ThenByDescending(team => team.GoalsFor)
                                    .ThenBy(team => team.GoalsAgainst)
                                    .ThenBy(team => team.FullName)
                                    .ToList();


            List<Team> orderedNordicBetLiga = nordicBetLigaen.Teams.OrderByDescending(team => team.Points)
                                    .ThenByDescending(team => (team.GoalsFor - team.GoalsAgainst))
                                    .ThenByDescending(team => team.GoalsFor)
                                    .ThenBy(team => team.GoalsAgainst)
                                    .ThenBy(team => team.FullName)
                                    .ToList();

            /*TODO I commented out this do we need it ? - Andrea 
            foreach (Team team in ordered)
            {
                //Console.WriteLine(team.ToString()); 
            }
            */

            ReadMatch(SuperLigaMatchesFolder, superLigaen);
            ReadMatch(NordicBetLigaMatchesFolder, nordicBetLigaen);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static List<League> Setup()
    {
        // Path to Setup File:
        string setupPath = "./csv/setup.csv";
        List<League> leagues = new();
        if (File.Exists(setupPath))
        {
            // Creates an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader:
            using (StreamReader reader = new(setupPath))
            {
                // For skipping the first line:
                reader.ReadLine();
                // This reads from the file while the end is not yet reached:
                while (!reader.EndOfStream)
                {
                    // Reading lines, then working with them. Easy peasy.
                    string line = reader.ReadLine() ?? throw new Exception("Error in reading file"); //TODO

                    string[] values = line.Split(",");
                    League league = new(values[0], Int32.Parse(values[1]), Int32.Parse(values[2]), Int32.Parse(values[3]), Int32.Parse(values[4]));
                    leagues.Add(league);
                }
            }
            return leagues;
        }
        else
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
            using (StreamReader reader = new(superLigaPath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine() ?? throw new Exception("Error in reading file"); //TODO Throws an error based on the assignment 
                    string[] values = line.Split(",");
                    Team team = new(values[0], values[1], values[2]);
                    leagues[0].Teams.Add(team);
                }
            }
        }
        else
        {
            Console.WriteLine("Error reading the Super Liga file. Maybe it changed places?");
            throw new FileNotFoundException();
        }

        // Add to Nordic Bet League List of Teams:
        if (File.Exists(nordicBetLigaPath))
        {
            using (StreamReader reader = new(nordicBetLigaPath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine() ?? throw new Exception("Error in reading file"); //TODO
                    string[] values = line.Split(",");
                    Team team = new Team(values[0], values[1], values[2]);
                    leagues[1].Teams.Add(team);
                }
            }
        }
        else
        {
            Console.WriteLine("Error reading the Nordic Bet Liga file. Maybe it changed places?");
            throw new FileNotFoundException();
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
                using (StreamReader reader = new(match))
                {
                    // Again, ignore first line in every file:
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine() ?? throw new Exception("Error in reading file"); //TODO I throw an error based on the assignement
                        string[] values = line.Split(",");
                        Team teamA = teams.Find(team => team.Abbreviation == values[0]) ?? throw new Exception($"Team not found: {values[0]}"); ;
                        Team teamB = teams.Find(team => team.Abbreviation == values[1]) ?? throw new Exception($"Team not found: {values[1]}"); //TODO I throw an error based on the assignement
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

    // A method to read match data from CSV files and update the league.
    public static void ReadMatch(string csvFolder, League league)
    {
        int counter = 1;
        //List<Team> upperScoreboard = new();
        //List<Team> lowerScoreboard = new();

        if (Directory.Exists(csvFolder))
        {
            // Get all CSV files in the specified folder
            string[] matchPath = Directory.GetFiles(csvFolder, "*.csv");

            // Process each CSV file
            foreach (string match in matchPath)
            {
                using StreamReader reader = new(match);

                //Skip the first line (header row)
                reader.ReadLine();

                //Process each line of the CSV file
                while (!reader.EndOfStream)
                {
                    // Read a line from the CSV file and split it into an array of values
                    string line = reader.ReadLine() ?? throw new Exception("Error in reading file"); //TODO
                    string[] values = line.Split(",");

                    // Split the third value (match result) into home and away scores
                    string[] result = values[2].Split("-");
                    int homeResult = int.Parse(result[0]);
                    int awayResult = int.Parse(result[1]);

                    // Find the home and away teams in the league by their abbreviations
                    Team teamHome = league.Teams.Find(team => team.Abbreviation == values[0]) ?? throw new Exception($"Team not found{values[0]}"); //TODO
                    Team teamAway = league.Teams.Find(team => team.Abbreviation == values[1]) ?? throw new Exception($"Team not found{values[1]}"); //TODO

                    // Update the teams' info based on the match result
                    teamHome.GamesPlayed++;
                    teamAway.GamesPlayed++;

                    if (homeResult > awayResult)
                    {
                        teamHome.GamesWon++;
                        teamAway.GamesLost++;
                        teamHome.Points += 3;

                        teamHome.WinningStreak.Enqueue("W");
                        if (teamHome.WinningStreak.Count > 5)
                        {
                            teamHome.WinningStreak.Dequeue();
                        }

                        teamAway.WinningStreak.Enqueue("L");
                        if (teamAway.WinningStreak.Count > 5)
                        {
                            teamAway.WinningStreak.Dequeue();
                        }
                    }
                    else if (homeResult < awayResult)
                    {
                        teamAway.GamesWon++;
                        teamHome.GamesLost++;
                        teamAway.Points += 3;

                        teamHome.WinningStreak.Enqueue("L");
                        if (teamHome.WinningStreak.Count > 5)
                        {
                            teamHome.WinningStreak.Dequeue();
                        }

                        teamAway.WinningStreak.Enqueue("W");
                        if (teamAway.WinningStreak.Count > 5)
                        {
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

                    //print statmente wrapped in a if statement because terminal was to crowded
                    if (counter == 126)
                    {
                        Console.WriteLine($"/*{league.Name} - Round n.22*/ ");
                        PrintTable(league, league.Teams);
                    }

                    if (counter > 131)
                    {
                        //The idea behind this was to save the general scorebord at match 22 and dived it into two boards. then i am doing the same as before, with the only difference that now the teams are displayed on the two boards. Not sure if this make sense. -Andrea 
                        if (counter == 132)
                        {
                            //saves the first 6 poistion in the list
                            league.UpperScoreboard = league.Teams
                                    .OrderByDescending(team => team.Points)
                                    .ThenByDescending(team => (team.GoalsFor - team.GoalsAgainst))
                                    .ThenByDescending(team => team.GoalsFor)
                                    .ThenBy(team => team.GoalsAgainst)
                                    .ThenBy(team => team.FullName)
                                    .Take(6).ToList();
                            //saves the last 6 poistion in the list
                            league.LowerScoreboard = league.Teams
                                    .OrderByDescending(team => team.Points)
                                    .ThenByDescending(team => (team.GoalsFor - team.GoalsAgainst))
                                    .ThenByDescending(team => team.GoalsFor)
                                    .ThenBy(team => team.GoalsAgainst)
                                    .ThenBy(team => team.FullName)
                                    .Skip(6)
                                    .Take(6)
                                    .ToList();
                        }

                    }
                    counter++; // the counter is here so that i have more control on the matches. Each file is composed by 6 iterations (because of 6 lines of matches and results) so i can decide better if i want triggere events at counter = 1 or at counter = 6 (which are in the same csv files) -Andrea
                }
            }
        }
        //prints the lists
        Console.WriteLine($"/*{league.Name} - UPPER SCOREBOARD - Final*/");
        PrintTable(league, league.UpperScoreboard);
        Console.WriteLine($"/*{league.Name} - LOWER SCOREBOARD - Final*/");
        PrintTable(league, league.LowerScoreboard);

    }

    static void PrintTable(League league, List<Team> teams)
    {
        //prints Table header
        PrintTableHeader();

        //orders the teams
        teams = teams.OrderByDescending(team => team.Points)
            .ThenByDescending(team => (team.GoalsFor - team.GoalsAgainst))
            .ThenByDescending(team => team.GoalsFor)
            .ThenBy(team => team.GoalsAgainst)
            .ThenBy(team => team.FullName)
            .ToList();

        // Assign positions to each team based on their sorted order, checking for the same position
        int position = 1;
        int prevPoints = -1;
        int teamNumber = 1;
        for (int i = 0; i < teams.Count; i++)
        {
            Team team = teams[i];
            int difference = team.GoalsFor - team.GoalsAgainst;
            string winningStreak = string.Join("|", team.WinningStreak);
            Console.Write("|");

            // Check if the team has the same points as the previous or next team
            if (team.Points != prevPoints)
            {
                // Coloring for Superliga: First place goes to Champions League, second place goes to Europa League.
                //Changed to Pattern Matching TODO
                Console.ForegroundColor = (league.Name, position, teamNumber) switch
                {
                    //match on the league.Name and position values to set the foreground color for the top two teams in the "Super Liga" league
                    ("Super Liga", 1, _) => ConsoleColor.DarkYellow,

                    //match on the league.Name and position values to set the foreground color for the top two teams in the "Super Liga" league
                    ("Super Liga", 2, _) => ConsoleColor.Magenta,

                    //matches on the teamNumber value to set the foreground color for the first and second teams in any league that is not "Super Liga". 
                    (_, _, var n) when n == 1 || n == 2 => ConsoleColor.Blue,

                    //matches on the teamNumber value checking whether the team is in the last two positions of the league using the teams.Count property
                    (_, _, var n) when n >= teams.Count - 1 => ConsoleColor.Red,
                    _ => Console.ForegroundColor // fallback to the default color if no pattern is matched
                };
                // Assign the team the next available position
                Console.Write("{0,-4} {1,-6} {2,-5} {3,-25} {4,-12} {5,-9} {6,-11} {7,-10} {8,-12} {9,-13} {10,-9} {11,-8} {12,-15}",
                position, team.Abbreviation, team.SpecialRanking, team.FullName, team.GamesPlayed, team.GamesWon, team.GamesTied, team.GamesLost,
                team.GoalsFor, team.GoalsAgainst, difference, team.Points, winningStreak);

                Console.ResetColor();
                position++;
                teamNumber++;
            }
            else
            {
                // Assign the team a position of '-'
                Console.Write("{0,-4} {1,-6} {2,-5} {3,-25} {4,-12} {5,-9} {6,-11} {7,-10} {8,-12} {9,-13} {10,-9} {11,-8} {12,-15}",
                "-", team.Abbreviation, team.SpecialRanking, team.FullName, team.GamesPlayed, team.GamesWon, team.GamesTied, team.GamesLost,
                team.GoalsFor, team.GoalsAgainst, difference, team.Points, winningStreak);
                teamNumber++;
            }
            Console.Write("|");
            Console.WriteLine();
            prevPoints = team.Points;
        }

        // Prints the table footer
        PrintTableFooter();
    }

    //Prints the table header
    static void PrintTableHeader()
    {
        Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------------------------------------------+");
        Console.Write("|");
        Console.Write("{0,-4} {1,-6} {2,-5} {3,-25} {4,-12} {5,-9} {6,-11} {7,-9} {8,-12} {9,-13} {10,-9} {11,-8} {12,-15}",
        "Pos", "Abbrev", "Mark", "Club-Name", "Games-Played", "Games-Won", "Games-Drawn", "Games-Lost",
        "Goals-For", "Goals-Against", "Goal-Diff", "Points", "Winning-Streak");
        Console.Write("|");
        Console.WriteLine();
    }

    // Prints the table footer
    static void PrintTableFooter()
    {
        Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------------------------------------------+");
        Console.WriteLine();
    }
}