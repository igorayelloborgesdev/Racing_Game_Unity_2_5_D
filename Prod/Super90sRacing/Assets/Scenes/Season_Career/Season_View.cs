using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Season_View : MonoBehaviour
{
    #region Variables
    private Season_Model season_Model = null;
    [SerializeField]
    public StandingObj[] standingObj = null;
    [SerializeField]
    public GameObject loadingScreen = null;
    [SerializeField]
    private GameObject[] screens = null;
    [SerializeField]
    private GameObject outScreenPosition = null;
    [SerializeField]
    private GameObject screenPosition = null;
    [SerializeField]
    private Text[] text_Save_Label = null;
    [SerializeField]
    private GameObject gameSavedScreen = null;
    [SerializeField]
    private Text gameSavedScreentext = null;
    [SerializeField]
    private GameObject seasonGroup = null;
    [SerializeField]
    private GameObject careerGroup = null;
    [SerializeField]
    private GameObject[] careerScreen = null;
    [SerializeField]
    private Text[] textBtnRadioCareer = null;
    [SerializeField]
    private Color colorBtnSelected;
    [SerializeField]
    private Color colorBtnUnselected;
    [SerializeField]
    private HistoricManager[] historicManager = null;
    [SerializeField]
    private TeamButton[] teamButtonCareer = null;
    [SerializeField]
    private Text[] textBtnRadioDriverCareer = null;
    [SerializeField]
    private Text textTeamNameCareer = null;
    [SerializeField]
    private SkillGraph[] skillGraph = null;
    [SerializeField]
    private SkillGraph skillGraphDriver = null;
    [SerializeField]
    private Image driverAvatarButton = null;
    [SerializeField]
    private Image driverCountryFlag = null;
    [SerializeField]
    private Car2D car2D = null;

    [SerializeField]
    private Image driverAvatarChallenger = null;
    [SerializeField]
    private Image driverCountryFlagChallenger = null;
    [SerializeField]
    private Car2D car2DChallenger = null;
    [SerializeField]
    private Text textTeamNameCareerChallenger = null;

    [SerializeField]
    private Image driverAvatarCurrent = null;
    [SerializeField]
    private Image driverCountryFlagCurrent = null;
    [SerializeField]
    private Car2D car2DCurrent = null;
    [SerializeField]
    private Text textTeamNameCareerCurrent = null;

    [SerializeField]
    private Image driverAvatarChallengeYou = null;
    [SerializeField]
    private Image driverCountryFlagChallengeYou = null;
    [SerializeField]
    private Car2D car2DChallengeYou = null;
    [SerializeField]
    private Text textTeamNameCareerChallengeYou = null;

    #endregion
    #region Behavior
    // Start is called before the first frame update
    void Start()
    {
        season_Model = new Season_Model(standingObj, loadingScreen, screens, outScreenPosition, screenPosition, gameSavedScreen, text_Save_Label, 
            gameSavedScreentext, seasonGroup, careerGroup, careerScreen, textBtnRadioCareer, colorBtnSelected, colorBtnUnselected, historicManager,
            teamButtonCareer, textBtnRadioDriverCareer, textTeamNameCareer, skillGraph, skillGraphDriver, driverAvatarButton, driverCountryFlag, car2D,
            driverAvatarChallenger, driverCountryFlagChallenger, car2DChallenger, textTeamNameCareerChallenger,
            driverAvatarCurrent, driverCountryFlagCurrent, car2DCurrent, textTeamNameCareerCurrent,
            driverAvatarChallengeYou, driverCountryFlagChallengeYou, car2DChallengeYou, textTeamNameCareerChallengeYou);
        season_Model.Init();
        season_Model.SeasonDriverFacadeInit();                
    }
    #endregion
    #region Events
    public void GotoSeasonRace()
    {
        season_Model.GotoSeasonRace();        
    }
    public void QuitButtonBack()
    {
        season_Model.QuitButtonBack();
    }
    public void QuitButton()
    {
        season_Model.QuitButton();
    }
    public void QuitButtonOpenModal()
    {
        season_Model.QuitButtonOpenModal();
    }
    public void SaveButtonOpenModal()
    {
        season_Model.SaveButtonOpenModal();
    }
    public void SaveGame(int id)
    {
        season_Model.SaveGame(id);        
    }
    public void CloseSaveGame()
    {
        season_Model.CloseSaveGame();
    }
    public void ChangeCareerButton(int id)
    {
        season_Model.ChangeCareerButton(id);
    }
    public void GotoRace()
    {
        season_Model.GotoRace();
    }

    public void ChooseTeamChallenge(int id)
    {
        season_Model.ChooseTeamChallenge(id);
    }
    public void ChooseDriverChallenge(int id)
    {
        season_Model.ChooseDriverChallenge(id);
    }
    #endregion
}
