using System.Collections.Generic;
using System.Threading.Tasks;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Services;

namespace Chemiklani.ViewModels
{
    public class ScoreViewModel : MasterPageViewModel
    {
        private readonly ScoreServie scoreServie = new ScoreServie();
        private readonly TeamService teamService = new TeamService();
        private readonly TaskService taskService = new TaskService();

        public override string PageTitle => "Hodnocení";
        public override string PageDescription => "Hodnocení týmù v jednotlivých úlohách.";

        //Value from which, the points will turn form series of radiobuttons into numberpicker
        public int MiniSore => 20;

        public List<TeamDTO> Teams { get; set; } = new List<TeamDTO>();
        public List<string> Rooms { get; set; } = new List<string>();
        public string SelectedRoom { get; set; }
        public NewScoreDTO NewScore { get; set; }

        public bool Displayed { get; set; }
        public bool MiniScoreDisplayed { get; set; }
        public bool SummarryDisplayed { get; set; }
        
        public override Task PreRender()
        {
            Rooms = scoreServie.GetRooms();
            return base.PreRender();
        }

        public void LoadAllTeams()
        {
            SelectedRoom = default(string);
            Teams = teamService.LoadTeams();
            Teams.ForEach(dto => dto.Points = scoreServie.GetPointsOfTeam(dto.Id));
        }

        public void FilterTeams()
        {
            Teams = teamService.LoadTeams(SelectedRoom);
            Teams.ForEach(dto => dto.Points = scoreServie.GetPointsOfTeam(dto.Id));
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
            TaskChanged();
        }

        public void Evaluate()
        {
            if (ExecuteSafe(() => scoreServie.ScoreTeam(NewScore.SelectedTeam.Id, NewScore.SelectedTask.Id,
                NewScore.Points)))
            {
                SummarryDisplayed = true;
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
            if (ExecuteSafe(() => scoreServie.ScoreTeam(NewScore.SelectedTeam.Id,
                NewScore.SelectedTask.Id,0)))
            {
                SummarryDisplayed = false;
                SetSuccess("Ohodnocení zrušeno.");
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