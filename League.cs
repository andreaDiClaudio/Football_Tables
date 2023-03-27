class League 
{
    public string Name { get; set; }
    public int ChampionsLeague { get; set; }
    public int EuropeLeague { get; set; }
    public int UpperLeague { get; set; }
    public int LowerLeague { get; set; }
    List<Team> Teams = new List<Team>();

    public League(string name, int championsLeague, int europeLeague, int upperLeague, int lowerLeague){
        this.Name = name;
        this.ChampionsLeague = championsLeague;
        this.EuropeLeague = europeLeague;
        this.UpperLeague = upperLeague;
        this.LowerLeague = lowerLeague;
    }

    public override string ToString()
    {
        return "League: " + Name + ", Teams for Champions League: " + ChampionsLeague + 
        ", Teams for Europe League: " + EuropeLeague + ", Teams that promote to an Upper League: " + UpperLeague + 
        ", Teams that relegate to a Lower League: " + LowerLeague; 
    }
}