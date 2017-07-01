using System.Collections.Generic;
using System.Threading.Tasks;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Services;

namespace Chemiklani.ViewModels
{
	public class TeamsViewModel : MasterPageViewModel
	{
	    public override string PageTitle => "Týmy";
	    public override string PageDescription => "Správa týmu.";

        public TeamDetailDTO NewTeamData { get; set; } = new TeamDetailDTO();
        public List<TeamListDTO> Teams { get; set; } = new List<TeamListDTO>();

        private readonly TeamService service = new TeamService();

	    public override Task PreRender()
	    {
	        Teams = service.LoadTeams();
	        return base.PreRender();
	    }

	    public void AddTeam()
	    {
	        service.AddTeam(NewTeamData);
            NewTeamData = new TeamDetailDTO();
	    }

	    public void DeleteTeam(int id)
	    {
	        service.DeleteTeam(id);
	    }
    }
}

