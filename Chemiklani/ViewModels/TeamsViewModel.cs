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
        public override string PageTitle => "T�my";
        public override string PageDescription => "Spr�va t�mu.";

        public TeamDTO NewTeamData { get; set; } = new TeamDTO();
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
            try
            {
                service.DeleteTeam(id);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                SetError("T�m nelze vymazat pokud ji� byl hodnocen.");
            }

            catch (Exception e)
            {
                SetError(e.Message);
            }
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
                    SetSuccess("T�my �sp�n� na�teny z csv.");
                }

                catch (InvalidDataException e)
                {
                    SetError(e.Message);
                }

                catch (Exception)
                {
                    SetError("V aplikaci do�lo k nezn�m� chyb�.");
                }
                finally
                {
                    storage.DeleteFile(file.FileId);
                    Files.Clear();
                }
            }
            else
            {
                SetError("CSV soubor nen� validn�.");
            }
        }

        public void UpdateTeam(TeamDTO team)
        {
            NewTeamData = team;
            EditTeamDialogDisplayed = true;
        }

        public void Save()
        {
            try
            {
                service.UpdateTeam(NewTeamData);
                SetSuccess("Ulo�eno");
            }
            catch (Exception e)
            {
                SetError(e.Message);
            }
            finally {
                NewTeamData = new TeamDTO();
                EditTeamDialogDisplayed = false;
            }
        }
    }
}