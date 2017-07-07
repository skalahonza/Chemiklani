using System.Collections.Generic;
using System.Linq;
using Chemiklani.BL.DTO;
using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.Services
{
    public class TaskService:BaseService
    {
        public void AddTask(TaskDTO task)
        {
            using (var dc = CreateDbContext())
            {
                if (string.IsNullOrEmpty(task.Description))
                    task.Description = "Bez popisku.";

                var tmp = new Task
                {                    
                    Name = task.Name,
                    Description = task.Description,
                    MaximumPoints = task.MaximumPoints,
                };

                dc.Tasks.Add(tmp);
                dc.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var dc = CreateDbContext())
            {
                var entity = dc.Tasks.FirstOrDefault(x => x.Id == id);
                if (entity != null)
                {
                    dc.Tasks.Remove(entity);
                    dc.SaveChanges();
                }
            }
        }

        public List<TaskDTO> LoadTasks()
        {
            using (var dc = CreateDbContext())
            {
                IQueryable<Task> tasks = dc.Tasks;
                var queryable = tasks.Select(t => new TaskDTO()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    MaximumPoints = t.MaximumPoints,
                });

                return queryable.ToList();
            }
        }
    }
}