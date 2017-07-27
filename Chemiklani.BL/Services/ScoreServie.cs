using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Chemiklani.BL.DTO;
using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.Services
{
    public class ScoreServie : BaseService
    {
        /// <summary>
        /// Set points gained by the team from a certain task
        /// </summary>
        /// <param name="teamId">Team to be ranked</param>
        /// <param name="taskId">Task the team competed in</param>
        /// <param name="points">Points gained</param>
        public void ScoreTeam(int teamId, int taskId, int points)
        {
            using (var dc = CreateDbContext())
            {
                var team = dc.Teams.Find(teamId);
                var task = dc.Tasks.Find(taskId);

                if (team == null)
                {
                    throw new InvalidDataException("Vybraný tým neexistuje.");
                }
                if (task == null)
                {
                    throw new InvalidDataException("Vybraná úloha neexistuje.");
                }

                if (points > task.MaximumPoints)
                {
                    throw new InvalidDataException("Body pøekraèují bodové maximum úlohy.");
                }

                var score = dc.Scores.FirstOrDefault(s => s.Team.Id == team.Id && s.Task.Id == task.Id);
                //update existing
                if (score != null)
                {
                    score.Points = points;
                    dc.SaveChanges();
                }

                else
                {
                    var newScore = dc.Scores.Create();
                    newScore.Team = team;
                    newScore.Task = task;
                    newScore.Points = points;
                    dc.Scores.Add(newScore);
                    dc.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Sum points of given
        /// </summary>
        /// <param name="teamId">Team id to examine</param>
        /// <returns>-1 if not yet evaluated</returns>
        public int? GetPointsOfTeam(int teamId)
        {
            using (var dc = CreateDbContext())
            {
                var team = dc.Teams.Find(teamId);
                if (team == null)
                {
                    throw new DataException("Vybraný tým neexistuje.");
                }

                var scores = dc.Scores.Where(x => x.Team.Id == team.Id);
                if (!scores.Any())
                    return null;
                return scores.ToList().Select(x => x.Points).Sum();
            }
        }

        /// <summary>
        /// Filter available rooms from the database
        /// </summary>
        /// <returns>List of strings with room names</returns>
        public List<string> GetRooms()
        {
            using (var dc = CreateDbContext())
            {
                return dc.Teams.GroupBy(x => x.Room).Select(x => x.Key).ToList();
            }
        }

        /// <summary>
        /// Get sore dataset for given list of teams
        /// </summary>
        /// <param name="room"></param>
        /// <param name="completeDataset">True: you can view points for each task</param>
        /// <returns></returns>
        public List<TeamScoreDTO> GetResults(string room = "", bool completeDataset = false)
        {
            var result = new List<TeamScoreDTO>();
            using (var dc = CreateDbContext())
            {
                //get all teams
                IQueryable<Team> teamsQuerry = dc.Scores
                    .Select(x => x.Team)
                    .Distinct();

                //filter by room if required
                if (!string.IsNullOrEmpty(room))
                    teamsQuerry = teamsQuerry.Where(t => t.Room == room);

                //transfer to dto
                var teams = teamsQuerry.ToList()
                    .Select(team =>
                    {
                        var tmp = new TeamDTO();
                        tmp.MapFrom(team);
                        return tmp;
                    })
                    .ToList();

                var competedTasks = dc.Tasks.ToList();

                //get score for each team
                teams.ForEach(t =>
                {
                    //get collection containing tasks and points the team gained

                    result.Add(new TeamScoreDTO
                    {
                        Team = t,
                        TasksScores =
                            completeDataset ? new List<TaskScoreDTO>(TaskScores(t.Id, competedTasks).OrderBy(x => x.TaskNumber)) : new List<TaskScoreDTO>(),
                        TotalPoints = GetPointsOfTeam(t.Id),
                    });
                });
            }

            result = result
                .OrderByDescending(x => x.TotalPoints)
                .ThenBy(x => GetHighestScoredTask(x.TasksScores))
                .ToList();

            //fill in placings
            for (var i = 0; i < result.Count; i++)
                result[i].Placings = i + 1;
            return result;
        }

        /// <summary>
        /// Get tass with points for given team
        /// </summary>
        /// <param name="teamId">Team to be examined</param>
        /// <param name="competedTasks">Tasks the team competed in</param>
        /// <returns></returns>
        private List<TaskScoreDTO> TaskScores(int teamId, List<Task> competedTasks)
        {
            using (var dc = CreateDbContext())
            {
                var result = new List<TaskScoreDTO>();

                //get all tasks, team was evaluated in
                var scores = dc.Scores
                    .Where(x => x.Team.Id == teamId)
                    .Include(x => x.Task);        

                //add points where the team competed
                foreach (var competedTask in competedTasks)
                {
                    var score = scores.FirstOrDefault(x => x.Task.Id == competedTask.Id);
                    var dto = new TaskScoreDTO();
                    dto.MapFrom(competedTask);
                    if (score != null)
                    {
                        dto.Points = score.Points;
                    }
                    result.Add(dto);
                }

                return result;
            }
        }

        /// <summary>
        /// Get highest solved task for given teamid
        /// </summary>
        /// <param name="completedTasks">Task completed by the team</param>
        /// <returns>Nuber name of the highest task the team solved</returns>
        private double GetHighestScoredTask(IEnumerable<TaskScoreDTO> completedTasks)
        {
            var tasks = completedTasks.Where(x => x.Points > 0);
            double max = double.MinValue;

            foreach (var task in tasks)
            {
                if (double.TryParse(task.TaskName, out double tmp))
                    if (tmp > max)
                        max = tmp;
            }
            return max;
        }

        public void NewGame()
        {
            using (var dc = CreateDbContext())
            {
                dc.Scores.RemoveRange(dc.Scores);
                dc.Tasks.RemoveRange(dc.Tasks);
                dc.Teams.RemoveRange(dc.Teams);
                dc.SaveChanges();
            }
        }
    }
}