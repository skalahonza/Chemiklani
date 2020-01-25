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
    public class TasksViewModel : MasterPageViewModel
    {
        public override string PageTitle => "Tasks";
        public override string PageDescription => "Manage tasks.";

        public List<TaskDTO>Tasks { get; set; } = new List<TaskDTO>();
        public TaskDTO NewTask { get; set; } = new TaskDTO();
        public TaskDTO EditedTask { get; set; } = new TaskDTO();
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
            ExecuteSafe(() => service.Delete(id));
        }

        public void ProcessFile()
        {
            //get upload service from service locator - registered in Startup.cs
            var storage = Context.Configuration.ServiceLocator.GetService<IUploadedFileStorage>();
            var file = Files.Files.First();
            if (file.IsAllowed)
            {
                // get the stream of the uploaded file
                var stream = storage.GetFile(file.FileId);
                if (ExecuteSafe(() =>
                {
                    var tasks = service.GetTasksFromCsv(stream);
                    service.AddTasks(tasks);
                }))
                {
                    SetSuccess("Tasks loaded from CSV.");
                }

                storage.DeleteFile(file.FileId);
                Files.Clear();
            }
            else
            {
                SetError("CSV is invalid.");
            }
        }

        public void EditTask(TaskDTO task)
        {
            EditedTask = task;
            EditTaskDialogDisplayed = true;
        }

        public void Save()
        {
            if (ExecuteSafe(() => service.UpdateTask(EditedTask)))
            {
                SetSuccess("Saved.");
            }
            EditTaskDialogDisplayed = false;
            EditedTask = new TaskDTO();
        }
    }
}