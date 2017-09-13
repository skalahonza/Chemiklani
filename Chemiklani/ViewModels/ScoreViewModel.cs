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

        public override string PageTitle => "Hodnocen�";
        public override string PageDescription => "Hodnocen� t�m� v jednotliv�ch �loh�ch.";

        //Value from which, the points will turn form series of radiobuttons into numberpicker
        public int MiniSore => 20;

        public List<TeamDTO> Teams { get; set; } = new List<TeamDTO>();
        public List<string> Rooms { get; set; } = new List<string>();
        public string SelectedRoom { get; set; }
        public NewScoreDTO NewScore { get; set; } = new NewScoreDTO();

        public bool Displayed { get; set; }
        public bool MiniScoreDisplayed { get; set; }
        public bool SummaryDisplayed { get; set; }
        
        public override Task PreRender()
        {
            Rooms = scoreService.GetRooms();
            return base.PreRender();
        }

        public void LoadAllTeams()
        {
            SelectedRoom = default(string);
            Teams = teamService.LoadTeams();
            Teams.ForEach(dto => dto.Points = scoreService.GetPointsOfTeam(dto.Id));
        }

        public void FilterTeams()
        {
            Teams = teamService.LoadTeams(SelectedRoom);
            Teams.ForEach(dto => dto.Points = scoreService.GetPointsOfTeam(dto.Id));
        }

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

        public void TaskChanged()
        {
            if (NewScore.SelectedTask != null)
            {
                MiniScoreDisplayed = NewScore.SelectedTask.MaximumPoints < MiniSore;
                if (MiniScoreDisplayed)
                    NewScore.PointOptions = InitializePoints(NewScore.SelectedTask.MaximumPoints);
            }
        }

        public void TaskChanged(TaskDTO task)
        {
            NewScore.SelectedTask = task;
            NewScore.Points = 0;
            TaskChanged();
        }

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
                SetSuccess("Ohodnocen� zru�eno.");
                //refresh dataset
                if (SelectedRoom == null)
                    LoadAllTeams();
                else
                    FilterTeams();
            }
        }

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