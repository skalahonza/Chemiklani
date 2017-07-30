using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.DTO
{
    public class TaskDTO : BaseDTO, IMappable<Task>
    {
        [Required(ErrorMessage = "Musíte vyplnit název úlohy.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int MaximumPoints { get; set; }

        public double TaskNumber => Name != null && double.TryParse(Name.Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture,
                                        out double tmp)
            ? tmp
            : double.MaxValue;

        public void MapFrom(Task entity)
        {
            Name = entity.Name;
            Description = entity.Description;
            MaximumPoints = entity.MaximumPoints;
        }

        public void MapTo(Task entity)
        {
            entity.Name = Name;
            entity.Description = Description;
            entity.MaximumPoints = MaximumPoints;
        }    
    }
}