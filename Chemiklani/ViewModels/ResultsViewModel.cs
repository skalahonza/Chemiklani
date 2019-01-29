using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Chemiklani.BL.DTO;
using Chemiklani.BL.Services;
using Chemiklani.BL.Utils;

namespace Chemiklani.ViewModels
{
    public class ResultsViewModel : SafeViewModel
    {
        private readonly ScoreService scoreService = new ScoreService();
        private readonly TaskService taskService = new TaskService();

        public List<TeamScoreDTO> Scores { get; set; }
        public string Room { get; set; }
        public int? Category { get; set; }
        public List<TaskDTO> Tasks { get; set; }
        public List<string> Rooms { get; set; }
        public List<int> Categories { get; set; }

        /// <summary>
        /// Load necessary data before rendering html 
        /// </summary>
        /// <returns></returns>
        public override Task PreRender()
        {
            Tasks = taskService.LoadTasks();
            Rooms = scoreService.GetRooms();
            Categories = scoreService.GetCategories();

            if (Context.Parameters.ContainsKey("Room"))
                Room = Context.Parameters["Room"].ToString();

            //Scores = !string.IsNullOrEmpty(Room) ? scoreService.GetResults(Room) : scoreService.GetResults();
            Scores = scoreService.GetResults(Room, Category);
            return base.PreRender();
        }        

        /// <summary>
        /// Room changed in dropdown
        /// </summary>
        /// <param name="room"></param>
        public void RoomChanged(string room)
        {
            Room = room;            
        }

        public void CategoryChanged(int category)
        {
            Category = category;            
        }

        public void ClearCategory()
        {
            Category = null;
        }

        /// <summary>
        /// Export csv button pressed
        /// </summary>
        public void ExportCSV()
        {
            ExecuteSafe(() =>
            {
                const string delimiter = ";";
                var parser = new CsvParser();
                //serialize results into csv
                string csv = parser.ExportDtos(
                    dto => string.Join(delimiter, dto.Placings, dto.Team.Name, dto.Team.Room, dto.TotalPoints), Scores);

                //return csv as file
                Context.ReturnFile(Encoding.Default.GetBytes(csv), "vysledky.csv", "application/csv");
            });
        }

        /// <summary>
        /// Export full csv button pressed
        /// </summary>
        public void ExportFullCsv()
        {
            ExecuteSafe(() =>
            {
                string csv = Results.GenerateCompleteCsv(scoreService.GetResults(Room, completeDataSet:true));

                //return csv as file
                Context.ReturnFile(Encoding.Default.GetBytes(csv), "vysledkyCompleteDataset.csv", "application/csv");
            });
        }
    }
}