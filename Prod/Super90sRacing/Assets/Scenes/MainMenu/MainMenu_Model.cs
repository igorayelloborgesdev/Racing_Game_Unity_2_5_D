using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static General;

public class MainMenu_Model {
    #region Serialized field    
    private Button[] buttonMenu;        
    private Text ErrorTextTitle;    
    private Text ErrorText;
    private GameObject[] buttonGroup_Practise = null;
    private GameObject[] buttonGroup_TrackSelect = null;
    private GameObject[] buttonGroup_Difficult_Select = null;
    private Text[] buttonInputTutorial = null;
    #endregion
    #region Variables    
    private ChangeScreen changeScreen;    
    private LanguageText text_Title;    
    private int[] menuID;    
    private Vector3 outScreenPosition;    
    private Vector3 screenPosition;
    private ChangeScreen changeScreenRedefineButtons;
    private KeyCode[] keyCodes = new KeyCode[5];
    #endregion    
    #region Constructor    
    public MainMenu_Model(LanguageText text_Title, Vector3 outScreenPosition, Vector3 screenPosition, int[] menuID, Button[] buttonMenu, Text ErrorTextTitle, Text ErrorText, 
        GameObject[] buttonGroup_Practise, GameObject[] buttonGroup_TrackSelect, GameObject[] ButtonGroup_Difficult_Select, Text[] buttonInputTutorial)
	{
		this.text_Title = text_Title;
		this.outScreenPosition = outScreenPosition;
		this.screenPosition = screenPosition;
		this.menuID = menuID;
		this.buttonMenu = buttonMenu;        
        this.ErrorTextTitle = ErrorTextTitle;
        this.ErrorText = ErrorText;
        this.buttonGroup_Practise = buttonGroup_Practise;
        this.buttonGroup_TrackSelect = buttonGroup_TrackSelect;
        this.buttonGroup_Difficult_Select = ButtonGroup_Difficult_Select;
        this.buttonInputTutorial = buttonInputTutorial;
    }
    #endregion
    #region Events
    public void SetButtonGroupPractise(int id)
    {
        ChangeButtonGroupPractise(id);
    }
    #endregion
    #region Method
    public void Init(GameObject[] screens, GameObject[] screensRedefineButtons)
	{
        try
        {
            if (General.GetSetisShowErrorMessage)
            {
                DefineError(screens[6]);                
            }
            else
            {
                changeScreen = new ChangeScreen(screens, outScreenPosition, screenPosition);
                changeScreenRedefineButtons = new ChangeScreen(screensRedefineButtons, outScreenPosition, screenPosition);
            }
            SeasonGeneral.Reset();
            SetTutorialControls();
        }
        catch(System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
	public void ChangeScreenSelected(int id, bool isChangeTitle = true)
	{
        try
        {
            changeScreen.ChangeScreenSelected(id);
            if(isChangeTitle)
                text_Title.ChangeTextDynamically(menuID[id]);
        }
		catch(System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
	}    
    public void EnableDisableButtons(bool isEnable)
	{
		for(int i = 0; i < buttonMenu.Length; i++)
		{
			buttonMenu [i].enabled = isEnable;
		}	
	}         
    public void SelectGameMode(int id)
    {
        General.SetGameMode((GameModesEnum)id);
    }
    public void ChangeScreenConfigDefineKey(int id)
    {
        try
        {
            if(changeScreenRedefineButtons != null)
                changeScreenRedefineButtons.ChangeScreenSelected(id);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }    
    private void DefineError(GameObject screen)
    {        
        screen.SetActive(true);
        this.ErrorTextTitle.text = "Error";
        this.ErrorText.text = "Could not load language, please close the game and start again.";
    }
    private void ChangeButtonGroupPractise(int id)
    {
        foreach (var button in buttonGroup_Practise)        
            button.SetActive(false);
        buttonGroup_Practise[id].SetActive(true);
        if(id < 2)
            ChangeButtonGroupTrackSelect(id);
    }
    private void ChangeButtonGroupTrackSelect(int id)
    {
        if (id <= buttonGroup_TrackSelect.Length)
        {
            foreach (var button in buttonGroup_TrackSelect)
                button.SetActive(false);
            buttonGroup_TrackSelect[id].SetActive(true);
        }        
    }
    public void GotoGamePlay(int id)
    {
        changeScreen.ChangeScreenSelected(13);        
        SceneManager.LoadScene(SceneInfo.GetSceneNames[id], LoadSceneMode.Single);
    }
    public void GotoSeasonCareer()
    {
        SeasonGeneral.SetVariables();
        SeasonGeneral.SetEnumRacingStatus(0);
        SceneManager.LoadScene(SceneInfo.GetSceneNames[3], LoadSceneMode.Single);
    }
    public void ShowHideButtonGroup_Difficult_Select(int id)
    {
        foreach (var ButtonGroup in buttonGroup_Difficult_Select)
        {
            ButtonGroup.SetActive(false);
        }
        buttonGroup_Difficult_Select[id].SetActive(true);
    }

    public void GotoCareer()
    {
        SeasonGeneral.careerDrivers = CreateDriverDTOList(General.GetSetDrivers);
        var driverNew = SeasonGeneral.careerDrivers.Where(x => x.teamId == SeasonGeneral.teamIDSeason).ToList()[1];
        driverNew.name = SeasonGeneral.name;
        driverNew.avatarId = "avatar_" + SeasonGeneral.avatarId.ToString();
        driverNew.countryId = SeasonGeneral.countryId;
        driverNew.helmetColor = SeasonGeneral.helmetColor;
        driverNew.code = SeasonGeneral.code;
        SeasonGeneral.careerYear = 0;
        SeasonGeneral.teamIDSeason = driverNew.teamId;
        SeasonGeneral.playerIDSeason = driverNew.id;
        SeasonGeneral.careerDrivers[driverNew.id] = driverNew;
        SeasonGeneral.SetEnumRacingStatus(0);
        SeasonGeneral.ResetChallenge();
        SeasonGeneral.historicSeasonDTO = new List<HistoricSeasonDTO>();
        for (int i = 0; i < 3; i++)
        {
            SeasonGeneral.historicSeasonDTO.Add(new HistoricSeasonDTO());
            SeasonGeneral.historicSeasonDTO[i].historicDTO = new List<HistoricDTO>();
            for (int j = 0; j < 16; j++)
            {
                SeasonGeneral.historicSeasonDTO[i].historicDTO.Add(new HistoricDTO());
            }
        }

        SceneManager.LoadScene(SceneInfo.GetSceneNames[3], LoadSceneMode.Single);
    }

    private List<DriverDTO> CreateDriverDTOList(DriverDTO[] drivers)
    {
        List<DriverDTO> newDriversList = new List<DriverDTO>();
        foreach (var driver in drivers)
        {
            newDriversList.Add(
                new DriverDTO()
                {
                    id = driver.id,
                    name = driver.name,
                    code = driver.code,
                    countryId = driver.countryId,
                    skill = driver.skill,
                    avatarId = driver.avatarId,
                    helmetColor = driver.helmetColor,
                    teamId = driver.teamId
                });
        }
        return newDriversList;
    }

    private void SetTutorialControls()
    {
        for (int i = 0; i < General.GetSetConfig.controlsKeycode.Length; i++)
        {
            keyCodes[i] = (KeyCode)General.GetSetConfig.controlsKeycode[i];
            buttonInputTutorial[i].text = keyCodes[i].ToString();
        }
    }

    #endregion
}