using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chemiklani.BL.DTO;

namespace Chemiklani.BL.Utils
{
    public static class Results
    {
        public static string GenerateCompleteCsv(List<TeamScoreDTO> scores, string delimiter = ";")
        {
            if (scores.Any())
            {
                var parser = new CsvParser();
                var builder = new StringBuilder();
                //append header
                builder.AppendLine(string.Join(delimiter, "Místnost", "Název týmu",
                    //append task names
                    string.Join(delimiter, scores.First().TasksScores.Select(x => x.TaskName))));

                //append data
                builder.AppendLine(parser.ExportDtos(dto =>
                {
                    return string.Join(delimiter, dto.Team.Room,
                        dto.Team.Name,
                        string.Join(delimiter,
                            dto.TasksScores.Select(x => x.Points.ToString()).ToArray()));
                }, scores));
                return builder.ToString();
            }
            return "";
        }
    }
}