using System;
using System.ComponentModel.DataAnnotations;
using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.DTO
{
    public class TaskDTO : BaseDTO, IMappable<Task, TaskDTO>
    {
        [Required(ErrorMessage = "Musíte vyplnit název úlohy.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int MaximumPoints { get; set; }

        public void MapFrom(Task entity)
        {
            Name = entity.Name;
            Description = entity.Description;
            MaximumPoints = entity.MaximumPoints;
        }

        public Task MapTo(TaskDTO dto)
        {
            return new Task
            {
                Name = dto.Name,
                Description = dto.Description,
                MaximumPoints = dto.MaximumPoints,
            };
        }
    }
}