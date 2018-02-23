namespace Chemiklani.DAL.Entities
{
    public class Score : IEntity<int>
    {
        public int Id { get; set; }
        public Task Task { get; set; }
        public Team Team { get; set; }
        public int Points { get; set; }
    }
}