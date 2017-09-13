using Chemiklani.BL.Services;

namespace Chemiklani.ViewModels
{
	public class DefaultViewModel : MasterPageViewModel
	{
	    private readonly ScoreService scoreService = new ScoreService();

        public override string PageTitle => "Dom�";
	    public override string PageDescription => "Hodnot�c� syst�m pro sout� chemikl�n�";

	    public bool NewContestDialogVisible { get; set; }

        /// <summary>
        /// New contest confirm button pressed
        /// </summary>
	    public void ConfirmNewContest()
	    {
	        scoreService.NewGame();
	        NewContestDialogVisible = false;
            SetSuccess("Nov� hra vytvo�ena, v�echna star� data vymaz�na.");
	    }
    }    
}

