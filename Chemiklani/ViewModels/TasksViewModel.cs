using System.Collections.Generic;
using System.Threading.Tasks;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Services;

namespace Chemiklani.ViewModels
{
	public class TasksViewModel : MasterPageViewModel
	{
	    public override string PageTitle => "Úlohy";
	    public override string PageDescription => "Správa úloh a jejich bodového ohodnocení.";

	    public List<TaskDTO>Tasks { get; set; } = new List<TaskDTO>();
	    public TaskDTO NewTask { get; set; } = new TaskDTO();

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
	        service.Delete(id);
	    }
	}
}

