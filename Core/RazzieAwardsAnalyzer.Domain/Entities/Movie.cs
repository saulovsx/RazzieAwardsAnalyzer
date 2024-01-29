namespace RazzieAwardsAnalyzer.Domain.Entities
{
    public class Movie : Entity
    {
        public int Year { get; set; }
        public string Title { get; set; } = string.Empty;       
        public List<Producer> Producers { get; set; } = [];
        public bool Winner { get; set; }
    }
}