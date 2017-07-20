﻿using Chemiklani.DAL.Entities;

namespace Chemiklani.BL.DTO
{
    public class ScoreDTO : BaseDTO,IMappable<Score,ScoreDTO>
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int MaximumPoints { get; set; }

        public int TeamId { get; set; }
        public string TeamName { get; set; }

        public void MapFrom(Score entity)
        {
            Id = entity.Id;
            TaskId = entity.Task.Id;
            TaskName = entity.Task.Name;
            MaximumPoints = entity.Task.MaximumPoints;

            TeamId = entity.Team.Id;
            TeamName = entity.Team.Name;
        }

        public Score MapTo(ScoreDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}