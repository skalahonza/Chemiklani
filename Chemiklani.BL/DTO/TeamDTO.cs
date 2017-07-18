using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.DTO
{
    public class TeamDTO : BaseDTO,IMappable<Team,TeamDTO>
    {
        [Required(ErrorMessage = "Musíte vyplnit název týmu.")]
        public string Name { get; set; }
        public ICollection<string> Members { get; set; }
        public string Room { get; set; }

        //Aditional fields
        public int? Points { get; set; }
        public bool Evaluated => Points != null;

        public void MapFrom(Team entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Room = entity.Room;
        }

        public Team MapTo(TeamDTO dto)
        {
            return new Team
            {
                Id = dto.Id,
                Name = dto.Name,
                Room = dto.Room
            };
        }
    }
}