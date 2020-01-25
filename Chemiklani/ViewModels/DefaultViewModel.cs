using Chemiklani.BL.Services;

namespace Chemiklani.ViewModels
{
	public class DefaultViewModel : MasterPageViewModel
	{
	    private readonly ScoreService scoreService = new ScoreService();

        public override string PageTitle => "Home";
	    public override string PageDescription => "Chemistryrace, evaluation system";

	    public bool NewContestDialogVisible { get; set; }

        /// <summary>
        /// New contest confirm button pressed
        /// </summary>
	    public void ConfirmNewContest()
	    {
	        scoreService.NewGame();
	        NewContestDialogVisible = false;
            SetSuccess("New game created, all previous data erased.");
	    }

	    public void GoToEvaluation()
	    {
	        Context.RedirectToRoute("Score");
	    }
    }    
}

