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
        /// <summary>
        /// Add new task to the database
        /// </summary>
        /// <param name="task">Task to be added</param>
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

        /// <summary>
        /// Delete team from database
        /// </summary>
        /// <param name="id">Team to be deleted</param>
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

        /// <summary>
        /// Load all tasks from database
        /// </summary>
        /// <returns>List of all tasks</returns>
        public List<TaskDTO> LoadTasks()
        {
            using (var dc = CreateDbContext())
            {
                IQueryable<Task> tasks = dc.Tasks;
                var queryable = tasks.Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    MaximumPoints = t.MaximumPoints,
                });

                return queryable.ToList();
            }
        }

        /// <summary>
        /// Parse tasks from csv file
        /// </summary>
        /// <param name="stream">Stram of csv file</param>
        /// <returns>Collection of parsed tasks</returns>
        public List<TaskDTO> GetTasksFromCsv(Stream stream)
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

        /// <summary>
        /// Add new tasks to the database
        /// </summary>
        /// <param name="tasks">List of tasks to be added</param>
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