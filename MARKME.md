# Type system
## Every variable and constant has a type, as does every expression that evaluates to a value. For example :
    List<League> leagues = new();
    League superLigaen;
    League nordicBetLigaen;

# Null handling
## We used the null coalescing operator to deal with nullable value types or reference types that may be null. Following there two example of how we implemented it:
    Example 1:
    string line = reader.ReadLine() ?? throw new Exception("Error in reading file");

    Example 2:
    Team teamHome = league.Teams.Find(team => team.Abbreviation == values[0]) ?? throw new Exception($"Team not found{values[0]}");

# String Interpolation
## We did use string interpolation for example when printing the league name above the scoreboard:
    Console.WriteLine($"/*{league.Name} - UPPER SCOREBOARD - Final*/");

## or when throwing a new exception:
    Team teamHome = league.Teams.Find(team => team.Abbreviation == values[0]) ?? throw new Exception($"Team not found{values[0]}");

# Pattern Matching
## We did pattern matching while polishing the code. We are using pattern matching in the method 'PrintTable()'. Following the example of pattern matching
    Console.ForegroundColor = (league.Name, position, teamNumber) switch
                {
                    ("Super Liga", 1, _) => ConsoleColor.DarkYellow,
                    ("Super Liga", 2, _) => ConsoleColor.Magenta,
                    (_, _, var n) when n == 1 || n == 2 => ConsoleColor.Blue,
                    (_, _, var n) when n >= teams.Count - 1 => ConsoleColor.Red,
                    _ => Console.ForegroundColor // fallback to the default color if no pattern is matched
                };

# Classes, structs and enums
## In the projec we only have used classes for 'Team' and 'League'.

# Properties
## We used properties in the classe 'League.cs':
    public string Name { get; set; }
    public int ChampionsLeague { get; set; }
    public int EuropeLeague { get; set; }
    public int UpperLeague { get; set; }
    public int LowerLeague { get; set; }
    public List<Team> Teams { get; set; }

    //I added the following properties because so i can divide easily the league into twi sections -Andrea
    public List<Team> UpperScoreboard { get; set; }
    public List<Team> LowerScoreboard { get; set; }

## and also in the class 'Team.cs'
    public string Abbreviation { get; set; }
    public string FullName { get; set; }
    public string SpecialRanking { get; set; }
    public int GamesPlayed { get; set;}
    public int GamesWon { get; set; }
    public int GamesLost { get; set; }
    public int GamesTied { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int Points { get; set; }
    public Queue<string> WinningStreak { get; set; }

# Named & optional parameters
## We did not use Named & Optional Parameters, but , for example, we could have use it inside the 'SetUp()' method where now we have:
    League league = new(values[0], 
                    Int32.Parse(values[1]), 
                    Int32.Parse(values[2]), 
                    Int32.Parse(values[3]), 
                    Int32.Pars(values[4]));

    leagues.Add(league);

## But we could have:
    League league = new(name: values[0], 
                     score: Int32.Parse(values[1]), 
                     wins: Int32.Parse(values[2]), 
                     losses: Int32.Parse(values[3]), 
                     draws: Int32.Parse(values[4]));

    leagues.Add(league);


# Tuples, deconstruction
## We did not use Tuples in our code, we decided to use Classes instead. Classes are better for when you want to have additional behaviours or functionalities beyond holding data. We could have used Tuples, for example, in the Setup part of the project, and created a couple of Tuples for the Leagues. The League Class holds some properties that are read only. After creation, we don't need to modify the data at all. 
## We could have created the Tuple for Leagues like so:
    (string Name, int ChampionsLeague, int EuropeLeague, int UpperLeague, int LowerLeague, List<Team>) league = ("values[0], Int32.Parse(values[1]), Int32.Parse(values[2]), Int32.Parse(values[3]), Int32.Parse(values[4]), new List<Team> {}");

# Exceptions
## We used exceptions on our project to notify the user when something went wrong. We made sure to also print a meaningful explanation of the problem to make it more clear. For example, on lines 81, 104, 124. All those exceptions are thrown and caught on the Main method in try-catch blocks of code, stopping the Project to continue. [WAITING]

# Attributes and DataValidation
## We have not used Attributes or Data Validation on our Project. 
## We could have done Attributes in the following way, for example to show who made which classes:
## First, creating the Custom Attribute:
    public class AuthorAttribute : Attribute
    {
        public string Name {get; private set } // We don't want it to be changed after creation!

        public AuthorAttribute(string name)
        {
            Name = name;
        }
    }
## And then in, for example, the Team Class:
    [Author("Nicolas")]
    public class Team
    ...
# Validation
## We could have used validation, for example in the Team class, for the Full Team Name:
    [Required]
    public string FullName { get; set; }
## And validate it with:
    List<ValidationResult> validationResult = new List<ValidationResult>;
    ValidationContext validationContext = new ValidationContext(team, null, null);
    Validator.TryValidateObject(team, validationContext, validationResults, true);

    if (validationResults.Count > 0)
    {
        // What to do if validation failed.
    }
    ...
## We did not use validation as we knew that the data that we were feeding the application was correct. With custom data or to ensure that the program has the least errors possible, Validation should be used.

# Arrays / Collections
## We have used Lists through all the Project: Lists of Leagues and Lists of Teams. Each League has its own List of Teams, and then we create a couple more of Lists for dividing the League in two sections. We have not created Arrays, as Lists are a bit more flexible. In the case of this particular project, we could have used Arrays instead as the teams are always the same and so are the leagues.
# For example, we used Lists in lines 6, 58, 250, etc. [WAITING]

# Ranges
## We have not used ranges in our project. The only place where we could have done so is Line [WAITING][ANDREA PLS PUSH SOON] while separating the List of Teams in two different arrays: 
    List<Team> upperLeague = ordered[0..6];
    List<Team> lowerLeague = superLiga.Team[7..12];

# Generics
## We have not used Generics in this project. The main point about Generics as we understand it is to create Reusable bits of code. While we know and understand that this can be useful, we could not find any place in our code where we could have benefitted from using Generics. It would only make the code less self-explanatory in our opinion. 
