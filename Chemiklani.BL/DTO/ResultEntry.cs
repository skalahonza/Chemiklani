using System.Collections.Generic;

namespace Chemiklani.BL.DTO
{
    public class ResultEntry
    {
        public string Room { get; set; }        
        public string TeamName { get; set; }
        public int Ammount { get; set; }
        public double AveragePointsForTask { get; set; }
        public int TotalPoints { get; set; }
        public List<TaskScoreDTO> TaskScores { get; set; }
    }

    public class ResultSummary
    {
        
    }
}