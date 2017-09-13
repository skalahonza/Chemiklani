using Chemiklani.BL.Services;

namespace Chemiklani.ViewModels
{
	public class DefaultViewModel : MasterPageViewModel
	{
	    private readonly ScoreService scoreService = new ScoreService();

        public override string PageTitle => "Domù";
	    public override string PageDescription => "Hodnotící systém pro soutìž chemiklání";

	    public bool NewContestDialogVisible { get; set; }

        /// <summary>
        /// New contest confirm button pressed
        /// </summary>
	    public void ConfirmNewContest()
	    {
	        scoreService.NewGame();
	        NewContestDialogVisible = false;
            SetSuccess("Nová hra vytvoøena, všechna stará data vymazána.");
	    }
    }    
}

