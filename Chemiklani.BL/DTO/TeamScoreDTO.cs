using System.Collections.Generic;

namespace Chemiklani.BL.DTO
{
    public class TeamScoreDTO : BaseDTO
    {
        public TeamDTO Team { get; set; }
        public List<TaskScoreDTO> TasksScores { get; set; }
        public int? TotalPoints { get; set; }
        public int Placings { get; set; }
    }
}