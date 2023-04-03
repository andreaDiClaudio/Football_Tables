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

            // Update Teams with Info from Matches.
            // Print Table again.

            // This works! =)
            List<Team> ordered = superLigaen.Teams.OrderByDescending(team => team.Points)
                                    .ThenByDescending(team => (team.GoalsFor - team.GoalsAgainst))
                                    .ThenByDescending(team => team.GoalsFor)
                                    .ThenBy(team => team.GoalsAgainst)
                                    .ThenBy(team => team.FullName)
                                    .ToList();

            foreach (Team team in ordered)
            {
                //Console.WriteLine(team.ToString()); TODO PUT IT BACK IN
            }
            
            //Andrea - Table formatting test with leagues
            printTable(superLigaen);
            //printTable(nordicBetLigaen);
            ReadSuperligaMatch(superligaMatches + "match_01.csv",superLigaen);
            printTable(superLigaen);
            
          
        } catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }

    /*READ SUPERLIGA MATCHES*/
    public static void ReadSuperligaMatch(string matchPath, League league) {
        if (File.Exists(matchPath)) 
        {
            using (StreamReader reader = new StreamReader(matchPath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(",");
                    string[] result = values[2].Split("-");

                    Console.WriteLine("/*MATCH*/");
                    Console.WriteLine($"Home results - {values[0]}: {result[0]}");
                    int homeResult = int.Parse(result[0]);
                    Console.WriteLine(value: $"away results - {values[1]}: {result[1]}");
                    int awayResult = int.Parse(result[1]);

                    Team teamHome = league.Teams.Find(team => team.Abbreviation == values[0]);
                    Team teamAway = league.Teams.Find(team => team.Abbreviation == values[1]);

                    //Updating
                    teamHome.GamesPlayed ++;
                    teamAway.GamesPlayed ++;

                    if (homeResult > awayResult)
                    {
                        teamHome.GamesWon ++;
                        teamHome.Points += 3;
                    } else if (homeResult < awayResult)
                    {
                        teamAway.GamesWon ++;
                        teamAway.Points += 3;
                    } else
                    {
                        teamHome.GamesTied ++;
                        teamAway.GamesTied ++;

                        teamHome.Points += 1;
                        teamAway.Points += 1;
                    }
                    teamHome.GoalsFor += homeResult;
                    teamAway.GoalsFor += awayResult;

                    teamHome.GoalsAgainst += awayResult;
                    teamAway.GoalsAgainst += homeResult;
                            
                    Console.WriteLine($"Team home updated: abbr: {teamHome.Abbreviation}, Games played:{teamHome.GamesPlayed}, won:{teamHome.GamesWon}, tied:{teamHome.GamesTied}, lost:{teamHome.GamesLost}, for:{teamHome.GoalsFor}, against:{teamHome.GoalsAgainst}, Difference: Lol , points: {teamHome.Points}, Winning Streak:{teamHome.WinningStreak} ");
                    Console.WriteLine($"Team away updated: abbr: {teamAway.Abbreviation}, Games played:{teamAway.GamesPlayed}, won:{teamAway.GamesWon}, tied:{teamAway.GamesTied}, lost:{teamAway.GamesLost}, for:{teamAway.GoalsFor}, against:{teamAway.GoalsAgainst}, Difference: Lol , points: {teamAway.Points}, Winning Streak:{teamAway.WinningStreak} ");
                    Console.WriteLine();

                }
            }
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

    /*TABLE FORMATTING*/
            static void printTable(League league) {
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------------------------------------------+");
            Console.Write("|");
            Console.Write("{0,-4} {1,-6} {2,-5} {3,-25} {4,-12} {5,-9} {6,-11} {7,-9} {8,-12} {9,-13} {10,-9} {11,-8} {12,-15}",
              "Pos", "Abbrev", "Mark", "Club-Name", "Games-Played", "Games-Won", "Games-Drawn", "Games-Lost",
              "Goals-For", "Goals-Against", "Goal-Diff", "Points", "Winning-Streak");
            Console.Write("|");
            Console.WriteLine();

            league.Teams.ForEach(team => {
                int difference = team.GoalsFor - team.GoalsAgainst;
                Console.Write("|");
                Console.Write("{0,-4} {1,-6} {2,-5} {3,-25} {4,-12} {5,-9} {6,-11} {7,-10} {8,-12} {9,-13} {10,-9} {11,-8} {12,-15}",
              "1", team.Abbreviation , team.SpecialRanking, team.FullName, team.GamesPlayed, team.GamesWon, team.GamesTied, team.GamesLost, team.GoalsFor, team.GoalsAgainst, difference, team.Points, team.WinningStreak);
                Console.Write("|");
                Console.WriteLine();
            });
            Console.WriteLine("+-------------------------------------------------------------------------------------------------------------------------------------------------------+");
            }



}