using System.Globalization;
using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.DTO
{
    public class TaskScoreDTO : BaseDTO, IMappable<Task>
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public int? Points { get; set; }
        public double TaskNumber => double.TryParse(TaskName.Replace(",","."), NumberStyles.Number, CultureInfo.InvariantCulture, out double tmp) ? tmp : double.MaxValue;
        public void MapFrom(Task entity)
        {
            Id = entity.Id;
            TaskName = entity.Name;
            TaskDescription = entity.Description;
        }

        public void MapTo(Task entity)
        {
            throw new System.InvalidOperationException("Cannot map to entity from this DTO.");
        }
    }
}