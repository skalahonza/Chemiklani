using System.Collections.Generic;
using System.Threading.Tasks;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Services;

namespace Chemiklani.ViewModels
{
    public class ScoreViewModel : MasterPageViewModel
    {
        private readonly ScoreService scoreService = new ScoreService();
        private readonly TeamService teamService = new TeamService();
        private readonly TaskService taskService = new TaskService();

        public override string PageTitle => "Scoring";
        public override string PageDescription => "Score a team with points";

        //Value from which, the points will turn form series of radiobuttons into numberpicker
        public int MiniSore => 20;

        public List<TeamDTO> Teams { get; set; } = new List<TeamDTO>();
        public List<string> Rooms { get; set; } = new List<string>();
        public string SelectedRoom { get; set; }
        public NewScoreDTO NewScore { get; set; } = new NewScoreDTO();

        public bool Displayed { get; set; }
        public bool MiniScoreDisplayed { get; set; }
        public bool SummaryDisplayed { get; set; }
        
        /// <summary>
        /// Get available rooms before rendering html
        /// </summary>
        /// <returns></returns>
        public override Task PreRender()
        {
            Rooms = scoreService.GetRooms();
            return base.PreRender();
        }

        /// <summary>
        /// All rooms button clicked
        /// </summary>
        public void LoadAllTeams()
        {
            SelectedRoom = default(string);
            Teams = teamService.LoadTeams();
            Teams.ForEach(dto => dto.Points = scoreService.GetPointsOfTeam(dto.Id));
        }

        /// <summary>
        /// Filter teams by room in a dropdown
        /// </summary>
        public void FilterTeams()
        {
            Teams = teamService.LoadTeams(SelectedRoom);
            Teams.ForEach(dto => dto.Points = scoreService.GetPointsOfTeam(dto.Id));
        }

        /// <summary>
        /// Evaluate team in a selected task
        /// </summary>
        /// <param name="team"></param>
        public void EvaluateTeam(TeamDTO team)
        {
            NewScore = new NewScoreDTO
            {
                Tasks = taskService.LoadTasks(team.Id, team.Category),
                SelectedTeam = team
            };
            Displayed = true;
            NewScore.SelectedTask = null;
            NewScore.Points = -1;
        }

        /// <summary>
        /// Task selection changed
        /// </summary>
        public void TaskChanged()
        {
            if (NewScore.SelectedTask != null)
            {
                MiniScoreDisplayed = NewScore.SelectedTask.MaximumPoints < MiniSore;
                if (MiniScoreDisplayed)
                    NewScore.PointOptions = InitializePoints(NewScore.SelectedTask.MaximumPoints);
            }
        }

        /// <summary>
        /// Task selection changed
        /// </summary>
        /// <param name="task"></param>
        public void TaskChanged(TaskDTO task)
        {
            NewScore.SelectedTask = task;
            NewScore.Points = 0;
            TaskChanged();
        }

        /// <summary>
        /// Evaluate selected team in a selected task
        /// </summary>
        public void Evaluate()
        {
            if (ExecuteSafe(() => scoreService.ScoreTeam(NewScore.SelectedTeam.Id, NewScore.SelectedTask.Id,
                NewScore.Points)))
            {
                SummaryDisplayed = true;
                //refresh dataset
                if (SelectedRoom == null)
                    LoadAllTeams();
                else
                    FilterTeams();
            }

            Displayed = false;
        }

        /// <summary>
        /// Points selection changed
        /// </summary>
        /// <param name="points"></param>
        public void PointsChanged(int points)
        {
            NewScore.Points = points;
        }

        public void CancelEvaluation()
        {
            if (ExecuteSafe(() => scoreService.DeleteScore(NewScore.SelectedTeam.Id,
                NewScore.SelectedTask.Id)))
            {
                SummaryDisplayed = false;
                SetSuccess("Canceled.");
                //refresh dataset
                if (SelectedRoom == null)
                    LoadAllTeams();
                else
                    FilterTeams();
            }
        }

        /// <summary>
        /// Initialize points for buttons
        /// </summary>
        /// <param name="maximum">Maximum points for a task</param>
        /// <returns></returns>
        private List<int> InitializePoints(int maximum)
        {
            var list = new List<int>();
            //add points for checkboxes
            for (int i = 1; i <= maximum; i++)
                list.Add(i);
            return list;
        }
    }
}