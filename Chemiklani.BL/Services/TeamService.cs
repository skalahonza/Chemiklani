using System.Collections.Generic;
using System.Linq;
using Chemiklani.BL.DTO;
using Chemiklani.DAL.Entities;
using DotVVM.Framework.Controls;

namespace Chemiklani.BL.Services
{
    public class TeamService : BaseService
    {
        public void AddTeam(TeamDetailDTO team)
        {
            using (var dc = CreateDbContext())
            {
                var tmp = new Team
                {
                    Members = team.Members,
                    Name = team.Name
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
                });

                return queryable.ToList();
            }
        }
    }
}