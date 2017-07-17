using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Utils;
using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.Services
{
    public class TeamService : BaseService
    {
        /// <summary>
        /// Add team to the database
        /// </summary>
        /// <param name="team">Team data</param>
        public void AddTeam(TeamDTO team)
        {
            if (string.IsNullOrEmpty(team.Room))
                team.Room = "Žádná místnost";

            using (var dc = CreateDbContext())
            {
                var tmp = new Team
                {
                    Members = team.Members,
                    Name = team.Name,
                    Room = team.Room,
                };

                dc.Teams.Add(tmp);
                dc.SaveChanges();
            }
        }

        /// <summary>
        /// Add multiple teams to database
        /// </summary>
        /// <param name="teams">List of teams</param>
        public void AddTeams(List<TeamDTO> teams)
        {
            using (var dc = CreateDbContext())
            {
                dc.Teams.AddRange(teams.Select(x => new Team
                {
                    Members = x.Members,
                    Name = x.Name,
                    Room = x.Room
                }));
                dc.SaveChanges();
            }
        }

        /// <summary>
        /// Load all teams from database
        /// </summary>
        /// <returns>All teams as list</returns>
        public List<TeamDTO> LoadTeams()
        {
            using (var dc = CreateDbContext())
            {
                IQueryable<Team> teams = dc.Teams;
                var queryable = teams.Select(t => new TeamDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    Room = t.Room,
                });

                return queryable.ToList();
            }
        }

        /// <summary>
        /// Delete team from database
        /// </summary>
        /// <param name="id">Id to be deleted</param>
        public void DeleteTeam(int id)
        {
            using (var dc = CreateDbContext())
            {
                var entity = dc.Teams.FirstOrDefault(x => x.Id == id);
                if (entity != null)
                {
                    dc.Teams.Remove(entity);
                    dc.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Get teams from csv stream
        /// </summary>
        /// <param name="stream">Csv string loaded by stream object</param>
        /// <returns>List of teams serialized from csv</returns>
        public List<TeamDTO> GetTeamsFromCsv(Stream stream)
        {
            List<TeamDTO> dtos;
            var parser = new CsvParser();
            parser.ParseDtos(stream, row =>
            {
                if (row.Length < 2)
                    throw new InvalidDataException("Neplatný formát csv.");

                return new TeamDTO
                {
                    Name = row[0],
                    Room = row[1]
                };
            }, out dtos);
            return dtos;
        }
    }
}