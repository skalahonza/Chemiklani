using System.Collections.Generic;
using System.Data;
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
        /// Get all scores for given team
        /// </summary>
        /// <param name="teamId">Team for filtering</param>
        /// <returns>List of scores</returns>
        public List<ScoreDTO> GetScoresForTeam(int teamId)
        {
            using (var dc = CreateDbContext())
            {
                var team = dc.Teams.Find(teamId);
                if (team == null)
                {
                    throw new InvalidDataException("Vybraný tým neexistuje.");
                }

                return dc.Scores.Where(s => s.Team == team)
                    .ToList()
                    .Select(x =>
                    {
                        var dto = new ScoreDTO();
                        dto.MapFrom(x);
                        return dto;
                    })
                    .ToList();
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
    }
}