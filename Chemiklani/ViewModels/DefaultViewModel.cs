using Chemiklani.BL.Services;

namespace Chemiklani.ViewModels
{
	public class DefaultViewModel : MasterPageViewModel
	{
	    private readonly ScoreServie scoreServie = new ScoreServie();

        public override string PageTitle => "Dom�";
	    public override string PageDescription => "Hodnot�c� syst�m pro sout� chemikl�n�";

	    public bool NewContestDialogVisible { get; set; }

	    public void ConfirmNewContest()
	    {
	        scoreServie.NewGame();
	        NewContestDialogVisible = false;
            SetSuccess("Nov� hra vytvo�ena, v�echna star� data vymaz�ny.");
	    }
    }    
}

