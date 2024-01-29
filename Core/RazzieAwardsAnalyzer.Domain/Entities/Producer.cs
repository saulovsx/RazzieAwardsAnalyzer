namespace RazzieAwardsAnalyzer.Domain.Entities
{
    public sealed class Producer: Entity
    {
        public string? Name { get; private set; }
        public List<Movie> Movies { get; set; } = [];
        public Producer(string name)
        {
            Name = name;
        }
    }
}