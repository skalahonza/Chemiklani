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
    [Authorize(Roles = new[] { "Admin" })]
    public class TasksViewModel : MasterPageViewModel
	{
	    public override string PageTitle => "Úlohy";
	    public override string PageDescription => "Správa úloh a jejich bodového ohodnocení.";

	    public List<TaskDTO>Tasks { get; set; } = new List<TaskDTO>();
	    public TaskDTO NewTask { get; set; } = new TaskDTO();
	    public UploadedFilesCollection Files { get; set; } = new UploadedFilesCollection();
	    public bool EditTaskDialogDisplayed { get; set; }

        private readonly TaskService service = new TaskService();

	    public override Task PreRender()
	    {
	        Tasks = service.LoadTasks();
	        return base.PreRender();
	    }

	    public void AddTask()
	    {
            service.AddTask(NewTask);
	        NewTask = new TaskDTO();
	    }

	    public void DeleteTask(int id)
	    {
	        try
	        {
	            service.Delete(id);
	        }
	        catch (System.Data.Entity.Infrastructure.DbUpdateException)
	        {
	            SetError("Úlohu nelze vymazat pokud byla použita v hodnocení.");
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
	                var tasks = service.GetTasksFromCsv(stream);
	                service.AddTasks(tasks);
	                SetSuccess("Úlohy úspìšnì naèteny z csv.");
	            }

	            catch (InvalidDataException e)
	            {
	                SetError(e.Message);
	            }

	            catch (Exception)
	            {
	                SetError("V aplikaci došlo k neznámé chybì.");
	            }
	            finally
	            {
	                storage.DeleteFile(file.FileId);
	                Files.Clear();
	            }
            }
	        else
	        {
	            SetError("CSV soubor není validní.");
	        }
	    }

	    public void EditTask(TaskDTO task)
	    {
	        NewTask = task;
	        EditTaskDialogDisplayed = true;
	    }

	    public void Save()
	    {
	        try
	        {
	            service.UpdateTask(NewTask);
                SetSuccess("Uloženo.");
	        }
	        catch (Exception e)
	        {
	            SetError(e.Message);
	        }
	        finally
	        {
	            EditTaskDialogDisplayed = false;
                NewTask = new TaskDTO();
	        }
	    }
    }
}

