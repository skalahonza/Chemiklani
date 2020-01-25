using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Exceptions;
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
                team.Room = "No room";

            using (var dc = CreateDbContext())
            {
                var tmp = new Team();
                team.MapTo(tmp);

                dc.Teams.Add(tmp);
                dc.SaveChanges();
            }
        }

        /// <summary>
        /// Update the team info
        /// </summary>
        /// <param name="dto">Tem to be updated</param>
        public void UpdateTeam(TeamDTO dto)
        {
            using (var dc = CreateDbContext())
            {
                var team = dc.Teams.SingleOrDefault(t => t.Id == dto.Id);
                if(team != null)
                {
                    dto.MapTo(team);
                    dc.SaveChanges();
                }
                else
                {
                    throw new AppDataNotFound("Team not found.");
                }
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
                dc.Teams.AddRange(teams.Select(dto =>
                {
                    var team = new Team();
                    dto.MapTo(team);
                    return team;
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
                return dc.Teams.ToList().Select(t =>
                {
                    var team = new TeamDTO();
                    team.MapFrom(t);
                    return team;
                }).ToList();
            }
        }

        /// <summary>
        /// Search for teams that are in given room
        /// </summary>
        /// <param name="room">Room to filter</param>
        /// <param name="fulltext">Set false if you want the exact match, true if you wnat fulltext comparison</param>
        /// <returns></returns>
        public List<TeamDTO> LoadTeams(string room, bool fulltext = false)
        {
            using (var dc = CreateDbContext())
            {
                return dc.Teams
                    .Where(t => t.Room == room || fulltext && t.Room.Contains(room))
                    .ToList()
                    .Select(t =>
                {
                    var team = new TeamDTO();
                    team.MapFrom(t);
                    return team;
                }).ToList();
            }
        }



        /// <summary>
        /// Delete team from database
        /// </summary>
        /// <param name="id">Id to be deleted</param>
        public void DeleteTeam(int id)
        {
            try
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
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                throw new InvalidDeleteRequest("Team cannot be deleted if scored already.");
            }
        }

        /// <summary>
        /// Get teams from csv stream
        /// </summary>
        /// <param name="stream">Csv string loaded by stream object</param>
        /// <returns>List of teams serialized from csv</returns>
        public List<TeamDTO> GetTeamsFromCsv(Stream stream)
        {            
            var parser = new CsvParser(",",";","\t");
            parser.ParseDtos(stream, row =>
            {
                if (row.Length < 2)
                    throw new InvalidAppData("Invalid CSV format.");

                var dto = new TeamDTO
                {
                    Name = row[0],
                    Room = row[1]
                };

                //add category if provided
                if (row.Length >= 3)
                    dto.Category = int.Parse(row[2]);

                //add school if provided
                if (row.Length >= 4)
                    dto.School = row[3];

                return dto;
            }, out List<TeamDTO> dtos);
            return dtos;
        }
    }
}