using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Services;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Runtime.Filters;
using DotVVM.Framework.Storage;

namespace Chemiklani.ViewModels
{
    [Authorize(Roles = new[] {"Admin"})]
    public class TeamsViewModel : MasterPageViewModel
    {
        public override string PageTitle => "Teams";
        public override string PageDescription => "Manage teams.";

        public TeamDTO NewTeamData { get; set; } = new TeamDTO();
        public TeamDTO EditedTeam { get; set; } = new TeamDTO();
        public List<TeamDTO> Teams { get; set; } = new List<TeamDTO>();
        public UploadedFilesCollection Files { get; set; } = new UploadedFilesCollection();
        public bool EditTeamDialogDisplayed { get; set; }

        private readonly TeamService service = new TeamService();

        public override Task PreRender()
        {
            Teams = service.LoadTeams();
            return base.PreRender();
        }

        public void AddTeam()
        {
            service.AddTeam(NewTeamData);
            NewTeamData = new TeamDTO();
        }

        public void DeleteTeam(int id)
        {
            ExecuteSafe(() => service.DeleteTeam(id));
        }

        public void ProcessFile()
        {
            //get upload service from service locator - registered in Startup.cs
            var storage = Context.Configuration.ServiceLocator.GetService<IUploadedFileStorage>();
            var file = Files.Files.First();
            if (file.IsAllowed)
            {
                // get the stream of the uploaded file and do whatever you need to do
                var stream = storage.GetFile(file.FileId);
                if (ExecuteSafe(() =>
                {
                    var teams = service.GetTeamsFromCsv(stream);
                    service.AddTeams(teams);
                }))
                {
                    SetSuccess("Teams loaded from CSV.");
                }

                storage.DeleteFile(file.FileId);
                Files.Clear();
            }
            else
            {
                SetError("CSV is invalid.");
            }
        }

        public void UpdateTeam(TeamDTO team)
        {
            EditedTeam = team;
            EditTeamDialogDisplayed = true;
        }

        public void Save()
        {
            if (ExecuteSafe(() => service.UpdateTeam(EditedTeam)))
                SetSuccess("Saved");

            EditedTeam = new TeamDTO();
            EditTeamDialogDisplayed = false;
        }
    }
}