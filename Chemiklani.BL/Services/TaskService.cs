using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Exceptions;
using Chemiklani.BL.Utils;
using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.Services
{
    public class TaskService : BaseService
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
            try
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
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                throw new InvalidDeleteRequest("Úlohu nelze vymazat pokud byla použita v hodnocení.");
            }
        }

        /// <summary>
        /// Load all tasks from database
        /// </summary>
        /// <returns>List of all tasks</returns>
        public List<TaskDTO> LoadTasks(int? teamId = null,int? category = null)
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

                var result = queryable.ToList();

                //if category provided, remove unwanted tasks
                if (category != null)
                {
                    for (var i = 0; i < result.Count; i++)
                    {
                        if (result[i].TaskNumber < category)
                        {
                            result.RemoveAt(i);
                            i--;
                        }
                    }
                }

                //check if result already evaluated
                if (teamId != null)
                {
                    foreach (var dto in result)
                    {                        
                        var score = dc.Scores.FirstOrDefault(x => x.Task.Id == dto.Id && x.Team.Id == teamId);
                        if (score != null)
                        {
                            dto.AlreadyEvaluated = true;
                            dto.AlreadyEvaluatedPoints = score.Points;
                        }
                    }
                }

                return result;
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
                    throw new InvalidAppData("Neplatný formát csv.");

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

        /// <summary>
        /// Update task info
        /// </summary>
        /// <param name="dto">Task to be updated</param>
        public void UpdateTask(TaskDTO dto)
        {
            using (var dc = CreateDbContext())
            {
                var task = dc.Tasks.SingleOrDefault(t => t.Id == dto.Id);
                if (task != null)
                {
                    dto.MapTo(task);
                    dc.SaveChanges();
                }
                else
                {
                    throw new AppDataNotFound("Úloha nenalezena.");
                }
            }
        }
    }
}