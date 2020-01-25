using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class NewScoreDTO : BaseDTO
    {
        public List<TaskDTO> Tasks { get; set; }
        [Required(ErrorMessage = "You must select a task.")]
        public TaskDTO SelectedTask { get; set; } = new TaskDTO();
        public TeamDTO SelectedTeam { get; set; }
        [Range(1,int.MaxValue,ErrorMessage = "You must fill points.")]
        public int Points { get; set; }
        public List<int> PointOptions { get; set; }
    }
}