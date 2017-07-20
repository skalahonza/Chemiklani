using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chemiklani.BL.DTO
{
    public class NewScoreDTO : BaseDTO
    {
        public List<TaskDTO> Tasks { get; set; }
        [Required(ErrorMessage = "Musíte zvolit úlohu")]
        public TaskDTO SelectedTask { get; set; }
        public TeamDTO SelectedTeam { get; set; }
        public int Points { get; set; }
        public List<int> PointOptions { get; set; }
    }
}