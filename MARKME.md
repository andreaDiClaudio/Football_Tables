## Type system

## Null handling

## String interpolation

## Pattern Matching

## Classes, structs and enums

## Properties

## Named & optional parameters


## Tuples, deconstruction
# We did not use Tuples in our code, we decided to use Classes instead. Classes are better for when you want to have additional behaviours or functionalities beyond holding data. We could have used Tuples, for example, in the Setup part of the project, and created a couple of Tuples for the Leagues. The League Class holds some properties that are read only. After creation, we don't need to modify the data at all. 
# We could have created the Tuple for Leagues like so:
    (string Name, int ChampionsLeague, int EuropeLeague, int UpperLeague, int LowerLeague, List<Team>) league = ("values[0], Int32.Parse(values[1]), Int32.Parse(values[2]), Int32.Parse(values[3]), Int32.Parse(values[4]), new List<Team> {}");

## Exceptions
# We used exceptions on our project to notify the user when something went wrong. We made sure to also print a meaningful explanation of the problem to make it more clear. For example, on lines 81, 104, 124. All those exceptions are thrown and caught on the Main method in try-catch blocks of code, stopping the Project to continue. [WAITING]

## Attributes and DataValidation
# We have not used Attributes or Data Validation on our Project. 
# We could have done Attributes in the following way, for example to show who made which classes:
# First, creating the Custom Attribute:
    public class AuthorAttribute : Attribute
    {
        public string Name {get; private set } // We don't want it to be changed after creation!

        public AuthorAttribute(string name)
        {
            Name = name;
        }
    }
# And then in, for example, the Team Class:
    [Author("Nicolas")]
    public class Team
    ...
# Validation: We could have used validation, for example in the Team class, for the Full Team Name:
    [Required]
    public string FullName { get; set; }
# And validate it with:
    List<ValidationResult> validationResult = new List<ValidationResult>;
    ValidationContext validationContext = new ValidationContext(team, null, null);
    Validator.TryValidateObject(team, validationContext, validationResults, true);

    if (validationResults.Count > 0)
    {
        // What to do if validation failed.
    }
    ...
# We did not use validation as we knew that the data that we were feeding the application was correct. With custom data or to ensure that the program has the least errors possible, Validation should be used.

## Arrays / Collections
# We have used Lists through all the Project: Lists of Leagues and Lists of Teams. Each League has its own List of Teams, and then we create a couple more of Lists for dividing the League in two sections. We have not created Arrays, as Lists are a bit more flexible. In the case of this particular project, we could have used Arrays instead as the teams are always the same and so are the leagues.
# For example, we used Lists in lines 6, 58, 250, etc. [WAITING]

## Ranges
# We have not used ranges in our project. The only place where we could have done so is Line [WAITING][ANDREA PLS PUSH SOON] while separating the List of Teams in two different arrays: 
    List<Team> upperLeague = ordered[0..6];
    List<Team> lowerLeague = superLiga.Team[7..12];

# We have not used Generics in this project. The main point about Generics as we understand it is to create Reusable bits of code. While we know and understand that this can be useful, we could not find any place in our code where we could have benefitted from using Generics. It would only make the code less self-explanatory in our opinion. 
