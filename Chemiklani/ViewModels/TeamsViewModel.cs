using System;
using System.Collections.Generic;
using System.IO;
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
        public override string PageTitle => "Týmy";
        public override string PageDescription => "Správa týmu.";

        public TeamDTO NewTeamData { get; set; } = new TeamDTO();
        public List<TeamDTO> Teams { get; set; } = new List<TeamDTO>();
        public UploadedFilesCollection Files { get; set; } = new UploadedFilesCollection();

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

        private void test()
        {
            
        }

        public void DeleteTeam(int id)
        {
            service.DeleteTeam(id);
        }

        public void ProcessFile()
        {
            var storage = Context.Configuration.ServiceLocator.GetService<IUploadedFileStorage>();
            var file = Files.Files.First();
            if (file.IsAllowed)
            {
                // get the stream of the uploaded file and do whatever you need to do
                var stream = storage.GetFile(file.FileId);
                try
                {
                    var teams = service.GetTeamsFromCsv(stream);
                    service.AddTeams(teams);
                    SetSuccess("Týmy úspìšnì naèteny z csv.");
                }

                catch (InvalidDataException e)
                {
                    SetError(e.Message);
                }

                catch (Exception e)
                {
                    SetError("V aplikaci došlo k neznámé chybì.");
                }
            }
            else
            {
                SetError("CSV soubor není validní.");
            }
        }
    }
}