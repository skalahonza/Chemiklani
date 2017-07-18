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

	    public override string PageTitle => "Hodnocení";
	    public override string PageDescription => "Hodnocení týmù v jednotlivých úlohách.";

	    public List<TeamDTO> Teams { get; set; } = new List<TeamDTO>();
	    public List<string> Rooms { get; set; } = new List<string>();
	    public string SelectedRoom { get; set; }

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
	    }

	    public void EvaluateTeam(int teamId)
	    {
	    }
	}
}

