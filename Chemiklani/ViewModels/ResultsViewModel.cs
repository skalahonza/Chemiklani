using System.Collections.Generic;
using System.Threading.Tasks;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Services;
using DotVVM.Framework.ViewModel;

namespace Chemiklani.ViewModels
{
    public class ResultsViewModel : DotvvmViewModelBase
    {
        private readonly ScoreServie scoreServie = new ScoreServie();
        private readonly TeamService teamService = new TeamService();
        private readonly TaskService taskService = new TaskService();

        public List<TeamScoreDTO> Scores { get; set; }
        public string Room { get; set; }
        public List<TaskDTO> Tasks { get; set; }

        public override Task PreRender()
        {
            Tasks = taskService.LoadTasks();

            if (Context.Parameters.ContainsKey("Room"))
                Room = Context.Parameters["Room"].ToString();

            Scores = !string.IsNullOrEmpty(Room) ? scoreServie.GetResults(Room) : scoreServie.GetResults();
            return base.PreRender();
        }
    }
}