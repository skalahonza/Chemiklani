namespace Chemiklani.BL.DTO
{
    public class TaskScoreDTO : BaseDTO
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public int Points { get; set; }
    }
}