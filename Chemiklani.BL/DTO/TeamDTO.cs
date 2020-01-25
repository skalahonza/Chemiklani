using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.DTO
{
    public class TeamDTO : BaseDTO,IMappable<Team>
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public ICollection<string> Members { get; set; }
        public string Room { get; set; }
        public int? Category { get; set; }
        public string School { get; set; }

        //Aditional fields
        public int? Points { get; set; }
        public bool Evaluated => Points != null;

        public void MapFrom(Team entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Room = entity.Room;
            Category = entity.Category;
            School = entity.School;
        }

        public void MapTo(Team entity)
        {
            entity.Name = Name;
            entity.Room = Room;
            entity.Category = Category;
            entity.School = School;
        }
    }
}