# Football Tables
## Solution to 'Mandatory I' of C# elective

Our implementation sees two classes one for 'League' and 'Team' that stores all information related to those.

The application is booted with 'dotnet run' and, as first thing, it will set up the teams (create Teams, saves them inside the corresponding league) and then the program will validate the matches (it will read all the csv files and in case of error, an exception is thrown).

After both leagues are sorted by points, then by goal different, then by goals for, then by goal against, and finally by alphabetical order.

Finally each csv files is processed and each team info is updated based on the information in the csv file. At the end of each round, a table is printed to show the current scoreboared. 

Once reached round 22, the scoreboard is divided in two: 'upperScoreboard' (first 6 position in the scoreboard) and 'lowerScoreboard' (last 6 postion in the scoreboard).

The process continues until it reaches the final csv file.

#### By Nicolas Javier Jan and Andrea Di Claudio
