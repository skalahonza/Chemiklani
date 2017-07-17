using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Utils;
using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.Services
{
    public class TaskService:BaseService
    {
        public void AddTask(TaskDTO task)
        {
            using (var dc = CreateDbContext())
            {
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

        public List<TaskDTO> GetTeamsFromCsv(Stream stream)
        {
            var parser = new CsvParser();
            parser.ParseDtos(stream, row =>
            {
                if (row.Length < 3)
                    throw new InvalidDataException("Neplatný formát csv.");

                return new TaskDTO
                {
                    Name = row[0],
                    Description = row[1],
                    MaximumPoints = Convert.ToInt32(row[2]),
                };
            }, out List<TaskDTO> dtos);
            return dtos;
        }

        public void AddTasks(List<TaskDTO> tasks)
        {
            using (var dc = CreateDbContext())
            {
                dc.Tasks.AddRange(tasks.Select(x => new Task
                {                    
                    Name = x.Name,
                    Description = x.Description,
                    MaximumPoints = x.MaximumPoints
                }));
                dc.SaveChanges();
            }
        }
    }
}