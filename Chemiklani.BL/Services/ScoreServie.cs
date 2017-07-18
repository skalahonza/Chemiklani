using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chemiklani.BL.DTO;

namespace Chemiklani.BL.Services
{
    public class ScoreServie : BaseService
    {
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

                if (points > task?.MaximumPoints)
                {
                    throw new InvalidDataException("Body pøekraèují bodové maximum úlohy.");
                }

                var score = dc.Scores.First(s => s.Team == team && s.Task == task);
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

        public List<ScoreDTO> GetScoresForTeam(int teamId)
        {
            using (var dc = CreateDbContext())
            {
                var team = dc.Teams.Find(teamId);
                if (team == null)
                {
                    throw new InvalidDataException("Vybraný tým neexistuje.");
                }

                return dc.Scores.Where(s => s.Team == team).ToList().Select(x =>
                {
                    var dto = new ScoreDTO();
                    dto.MapFrom(x);
                    return dto;
                }).ToList();
            }
        }
    }
}