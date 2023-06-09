class League
{
    public string Name { get; set; }
    public int ChampionsLeague { get; set; }
    public int EuropeLeague { get; set; }
    public int UpperLeague { get; set; }
    public int LowerLeague { get; set; }
    public List<Team> Teams { get; set; }

    //I added the following properties because so i can divide easily the league into twi sections -Andrea
    public List<Team> UpperScoreboard { get; set; }
    public List<Team> LowerScoreboard { get; set; }

    public League(string name, int championsLeague, int europeLeague, int upperLeague, int lowerLeague)
    {
        this.Name = name;
        this.ChampionsLeague = championsLeague;
        this.EuropeLeague = europeLeague;
        this.UpperLeague = upperLeague;
        this.LowerLeague = lowerLeague;
        this.Teams = new List<Team>();
        this.UpperScoreboard = new List<Team>();
        this.LowerScoreboard = new List<Team>();
    }

    public override string ToString()
    {
        return "League: " + Name + ", Teams for Champions League: " + ChampionsLeague +
        ", Teams for Europe League: " + EuropeLeague + ", Teams that promote to an Upper League: " + UpperLeague +
        ", Teams that relegate to a Lower League: " + LowerLeague;
    }
}