using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Services;
using Chemiklani.BL.Utils;
using DotVVM.Framework.ViewModel;

namespace Chemiklani.ViewModels
{
    public class ResultsViewModel : DotvvmViewModelBase
    {
        private readonly ScoreServie scoreServie = new ScoreServie();
        private readonly TaskService taskService = new TaskService();

        public List<TeamScoreDTO> Scores { get; set; }
        public string Room { get; set; }
        public List<TaskDTO> Tasks { get; set; }
        public List<string> Rooms { get; set; }

        public override Task PreRender()
        {
            Tasks = taskService.LoadTasks();
            Rooms = scoreServie.GetRooms();

            if (Context.Parameters.ContainsKey("Room"))
                Room = Context.Parameters["Room"].ToString();

            Scores = !string.IsNullOrEmpty(Room) ? scoreServie.GetResults(Room) : scoreServie.GetResults();
            return base.PreRender();
        }

        public void RoomChanged(string room)
        {
            Room = room;
            Scores = scoreServie.GetResults(Room);
        }

        public void ExportCSV()
        {           
            const string delimiter = ";";
            var parser = new CsvParser();
            //serialize results into csv
            string csv = parser.ExportDtos(
                dto => string.Join(delimiter, dto.Placings, dto.Team.Name, dto.Team.Room, dto.TotalPoints), Scores);

            //return csv as file
            Context.ReturnFile(Encoding.Default.GetBytes(csv), "vysledky.csv", "application/csv");
        }

        public void ExportFullCsv()
        {
            string csv = Results.GenerateCompleteCsv(Scores);

            //return csv as file
            Context.ReturnFile(Encoding.Default.GetBytes(csv), "vysledkyCompleteDataset.csv", "application/csv");
        }
    }
}