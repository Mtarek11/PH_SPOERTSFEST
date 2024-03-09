namespace Models
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CaptainId { get; set; }
        public virtual User Captian { get; set; }
        public Sport Sport { get; set; }
        public SportType SportType { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
    public enum Sport
    {
       FTM = 1,
       FTF = 2,
       BTM = 3,
       BTF = 4,
       BIM = 5,
       BIF = 6,
       TS = 7,
       TD = 8,
       P = 9,
       UnKnown = 10,
    }
    public enum SportType
    {
        U12 = 1,
        U14 = 2,
        U18 = 3,
        OAT = 4,
        P45 = 5,
        DC = 6,
        TPC = 7,
        UnKnown = 8
    }
}
