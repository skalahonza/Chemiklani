

namespace Chemiklani.DAL.Entities
{
    public class Task: IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MaximumPoints { get; set; }
    }
}