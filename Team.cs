class Team 
{
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

    public Team(string abbreviation, string fullName, string specialRanking)
    {
        this.Abbreviation = abbreviation;
        this.FullName = fullName;
        this.SpecialRanking = specialRanking;
        this.GamesPlayed = 0;
        this.GamesWon = 0;
        this.GamesLost = 0;
        this.GamesTied = 0;
        this.GoalsFor = 0;
        this.GoalsAgainst = 0;
        this.Points = 0;
        //Queue<int> Q1 = new Queue<int>(new int[] { 1, 2, 3} );
        this.WinningStreak = new Queue<string>(new string[] { "-", "-", "-", "-", "-" });
    }
    
    public override string ToString()
    {
        return FullName + ": " + Abbreviation + ", " + SpecialRanking;
    }
}