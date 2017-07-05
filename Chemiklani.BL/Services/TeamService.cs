using System.Collections.Generic;
using System.Linq;
using Chemiklani.BL.DTO;
using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.Services
{
    public class TeamService : BaseService
    {
        public void AddTeam(TeamDetailDTO team)
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

        public List<TeamListDTO> LoadTeams()
        {
            using (var dc = CreateDbContext())
            {
                IQueryable<Team> teams = dc.Teams;
                var queryable = teams.Select(t => new TeamListDTO
                {
                    Id = t.Id,
                    Name = t.Name,
                    Room = t.Room,
                });

                return queryable.ToList();
            }
        }

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
    }
}