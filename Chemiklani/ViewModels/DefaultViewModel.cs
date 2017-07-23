using Chemiklani.BL.Services;

namespace Chemiklani.ViewModels
{
	public class DefaultViewModel : MasterPageViewModel
	{
	    private readonly ScoreServie scoreServie = new ScoreServie();

        public override string PageTitle => "Domù";
	    public override string PageDescription => "Hodnotící systém pro soutìž chemiklání";

	    public bool NewContestDialogVisible { get; set; }

	    public void ConfirmNewContest()
	    {
	        scoreServie.NewGame();
	        NewContestDialogVisible = false;
            SetSuccess("Nová hra vytvoøena, všechna stará data vymazány.");
	    }
    }    
}

