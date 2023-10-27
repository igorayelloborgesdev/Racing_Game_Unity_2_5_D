using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using static General;

public class MainMenu_View : MonoBehaviour {
	#region Serialized Field
	[SerializeField]
	private GameObject[] screens = null;	
	[SerializeField]
	private Color colorBtnSelected = default;	
	[SerializeField]
	private Color colorBtnUnselected = default;	
	[SerializeField]
	private Text[] textBtnRadioLanguage = null;	
	[SerializeField]
	private Text[] textBtnRadioDifficult = null;	
	[SerializeField]
	private Text[] textBtnRadioControl = null;	
	[SerializeField]
	private LanguageText[] languageText = null;	
	[SerializeField]
	private LanguageText text_Title = null;	
	[SerializeField]
	private GameObject outScreenPosition = null;	
	[SerializeField]
	private GameObject screenPosition = null;	
	[SerializeField]
	private Button[] buttonMenu = null;            
    [SerializeField]
    private GameObject[] screensRedefineButtons = null;
    [SerializeField]
    private Button[] buttonInput = null;
    [SerializeField]
    private Color buttonInputSelected = default;
    [SerializeField]
    private Color buttonInputUnselected = default;                    
    [SerializeField]
    private Text[] text_Save_Label = null;
    [SerializeField]
    private GameObject Screen_12_LoadResult = null;
    [SerializeField]
    private Text Text_Save = null;                
    [SerializeField]
    private Text ErrorTextTitle = null;
    [SerializeField]
    private Text ErrorText = null;
    [SerializeField]
    private Text ErrorTextFile = null;
    [SerializeField]
    private Text[] textBtnRadioEditor = null;
    [SerializeField]
    private GameObject[] screensEditor;
    [SerializeField]
    private TeamButton[] teamButton;
    [SerializeField]
    private Text baseChooseTeamText;
    [SerializeField]
    private Text teamText;
    [SerializeField]
    private Text teamPlaceholderText;
    [SerializeField]
    private SkillGraph[] skillGraph;
    [SerializeField]
    private Slider[] slider;
    [SerializeField]
    private GameObject[] screensEditorTeam = null;
    [SerializeField]
    private RGBSelector[] rgbSelector = null;
    [SerializeField]
    private TeamButton teamButtonCustomize = null;    
    [SerializeField]
    private RGBSelector rgbSelectorTeamCockpit0 = null;
    [SerializeField]
    private RGBSelector rgbSelectorTeamCockpit1 = null;
    [SerializeField]
    private RGBSelector rgbSelectorTeamCockpit2 = null;
    [SerializeField]
    private RGBSelector rgbSelectorTeamCockpit3 = null;    
    [SerializeField]
    private CarPlayerGraphics carPlayerGraphics = null;
    [SerializeField]
    private RGBSelector rgbSelectorTeamCloth0 = null;
    [SerializeField]
    private RGBSelector rgbSelectorTeamCloth1 = null;
    [SerializeField]
    private RGBSelector rgbSelectorTeamCloth2 = null;
    [SerializeField]
    private RGBSelector rgbSelectorTeamCloth3 = null;
    [SerializeField]
    private ClothUI clothUI = null;    
    [SerializeField]
    private Text[] textDrivers = null;        
    [SerializeField]
    private InputField inputFieldName = null;
    [SerializeField]
    private InputField inputFieldCode = null;
    [SerializeField]
    private SkillGraph skillGraphDriver = null;
    [SerializeField]
    private Slider sliderDriver = null;
    [SerializeField]
    private RGBSelector rgbSelectorDriverHelmet = null;
    [SerializeField]
    private GameObject helmet = null;    
    [SerializeField]
    private GameObject[] screensEditorDriverCountry = null;
    [SerializeField]
    private Image driverCountryFlag = null;
    [SerializeField]
    private Text text_Country_Code = null;
    [SerializeField]
    private GameObject[] screensEditorDriverAvatar = null;
    [SerializeField]
    private Image driverAvatarButton = null;
    [SerializeField]
    private GameObject[] screensEditorDriverAvatarConfirmSuccess = null;
    [SerializeField]
    private Text text_ScreenEditorMOD = null;
    [SerializeField]
    private GameObject[] editorGameObjectHideShow = null;
    [SerializeField]
    private TeamButton[] teamButtonSelectTeamDriver;
    [SerializeField]
    private Text textTeamName = null;
    [SerializeField]
    private SkillGraph[] skillGraphTeam = null;
    [SerializeField]
    private Car2D car2D = null;
    [SerializeField]
    private Text[] text_DriverNameSelect = null;
    [SerializeField]
    private SkillGraph driverSkillGraph = null;
    [SerializeField]
    private Image driverCountryFlagSelect = null;
    [SerializeField]
    private Image driverAvatarImage = null;
    [SerializeField]
    private GameObject[] buttonGroup_Practise = null;
    [SerializeField]
    private Text trackName = null;    
    [SerializeField]
    private GameObject[] buttonGroup_TrackSelect = null;
    [SerializeField]
    private Image trackImage = null;
    [SerializeField]
    private GameObject[] ButtonGroup_Difficult_Select = null;
    [SerializeField]
    private Text avatarNamePlaceholderText = null;
    [SerializeField]
    private Text avatarCodePlaceholderText = null;
    [SerializeField]
    private Text avatarNameText = null;
    [SerializeField]
    private Text avatarCodeText = null;
    [SerializeField]
    private RGBSelector rgbSelectorDriverHelmetCareer = null;
    [SerializeField]
    private GameObject contryFlagCareer = null;
    [SerializeField]
    private GameObject avatarCareer = null;
    [SerializeField]
    private Image driverCountryFlagCareer = null;
    [SerializeField]
    private Text text_Country_CodeCareer = null;
    [SerializeField]
    private GameObject avatarCareerScreen = null;
    [SerializeField]
    private Image driverAvatarButtonCareer = null;
    [SerializeField]
    private GameObject[] Screen_Career = null;
    [SerializeField]
    private TeamButton[] teamButtonCareer = null;
    [SerializeField]
    private SkillGraph[] skillGraphCareer = null;
    [SerializeField]
    private Text teamNameCareer = null;
    [SerializeField]
    private Car2D car2DCareer = null;
    [SerializeField]
    private Text[] buttonInputTutorial = null;
    #endregion
    #region Variables    
    private MainMenu_Model mainMenu_Model; 
	private Options_Model options_Model;	
	private int selectedMainScreen = 0;        
    private int selectedscreensRedefineButtons = 0;
    private LoadModel loadModel;
    private Editor_Model editor_Model;
    private CarDriverSelect_Model carDriverSelect_Model;
    private TrackSelect_Model trackSelect_Model;
    private Career_Model career_Model;
    #endregion
    #region Behavior    
    void Awake () {
		Init ();        
    }
    private void Update()
    {
        if(CalibrateControl.GetSetinputID != -1)
        {
            options_Model.CalibrateControls();
        }        
    }
    #endregion
    #region Method    
    private void Init()
	{
		int[] mainMenuID = new int[]{18, 19, 20, 22, 23, 18, 0, 1, 2, 46, 21, 113, 114, 27, 9, 130};
		mainMenu_Model = new MainMenu_Model (text_Title, outScreenPosition.transform.position, screenPosition.transform.position, mainMenuID, buttonMenu, 
            ErrorTextTitle, ErrorText, buttonGroup_Practise, buttonGroup_TrackSelect, ButtonGroup_Difficult_Select, buttonInputTutorial);
		mainMenu_Model.Init (screens, screensRedefineButtons);
		options_Model = new Options_Model (colorBtnSelected, colorBtnUnselected, textBtnRadioLanguage,
            textBtnRadioDifficult, textBtnRadioControl, buttonInput, buttonInputSelected, buttonInputUnselected);
		options_Model.Init ();
        editor_Model = new Editor_Model(colorBtnSelected, colorBtnUnselected, textBtnRadioEditor, screensEditor, outScreenPosition.transform.position, 
            screenPosition.transform.position, teamButton, baseChooseTeamText, teamText, teamPlaceholderText, skillGraph, slider, screensEditorTeam, rgbSelector,
            teamButtonCustomize, rgbSelectorTeamCockpit0, rgbSelectorTeamCockpit1, rgbSelectorTeamCockpit2, rgbSelectorTeamCockpit3, carPlayerGraphics,
            rgbSelectorTeamCloth0, rgbSelectorTeamCloth1, rgbSelectorTeamCloth2, rgbSelectorTeamCloth3, clothUI, textDrivers,
            inputFieldName, inputFieldCode, skillGraphDriver, sliderDriver, rgbSelectorDriverHelmet, helmet, screensEditorDriverCountry, 
            driverCountryFlag, text_Country_Code, screensEditorDriverAvatar, driverAvatarButton, screensEditorDriverAvatarConfirmSuccess, 
            text_ScreenEditorMOD, editorGameObjectHideShow, teamButtonSelectTeamDriver, teamButtonCareer);
        editor_Model.Init();
        carDriverSelect_Model = new CarDriverSelect_Model(textTeamName, skillGraphTeam, car2D, text_DriverNameSelect, colorBtnSelected, colorBtnUnselected, driverSkillGraph, 
            driverCountryFlagSelect, driverAvatarImage);
        carDriverSelect_Model.Init();
        trackSelect_Model = new TrackSelect_Model(trackName, trackImage);
        trackSelect_Model.Init();
        ChangeScreenConfig (0);
        ChangeScreenConfigDefineKey(0);
        carDriverSelect_Model.SelectTeam(0);
        options_Model.RadioButtonDifficultChangeButton(0);
        if (!General.GetSetisShowErrorMessage)
        {
            loadModel = new LoadModel(text_Save_Label, Screen_12_LoadResult, Text_Save);
            loadModel.FillSaveGamesInfo();
            loadModel.EnableDeleteWarning(false);            
        }
        career_Model = new Career_Model(avatarNamePlaceholderText, avatarCodePlaceholderText, avatarNameText, avatarCodeText, 
            rgbSelectorDriverHelmetCareer, contryFlagCareer, driverCountryFlagCareer, text_Country_CodeCareer, avatarCareerScreen, 
            driverAvatarButtonCareer, Screen_Career, skillGraphCareer, teamNameCareer, car2DCareer);
        career_Model.Init();
    }
	#endregion
	#region Events	
	public void ChangeScreenConfig(int id)
	{
		selectedMainScreen = id;
		mainMenu_Model.ChangeScreenSelected (id);
	}       
    public void SelectGameMode(int id)
    {
        mainMenu_Model.SelectGameMode(id);        
    }	
	public void QuitGame()
	{
		Application.Quit ();
	}	
	public void RadioButtonLanguageChangeButton(int id)
	{
		options_Model.RadioButtonLanguageChangeButton(id, languageText, text_Title);
        loadModel.FillSaveGamesInfo();
    }	
	public void RadioButtonDifficultChangeButton(int id)
	{
		options_Model.RadioButtonDifficultChangeButton(id);
	}	
	public void RadioButtonControlChangeButton(int id)
	{
		options_Model.RadioButtonControlChangeButton(id);
	}	
	public void SelectTeam(int id)
	{
		ChangeScreenConfig (7);                
    }	
	public void LoadTrainningMode(int id)
	{
		mainMenu_Model.ChangeScreenSelected (8);
		mainMenu_Model.EnableDisableButtons (false);
		SceneManager.LoadSceneAsync(SceneInfo.GetSceneNames[id], LoadSceneMode.Single);
	}        
    public void GoToGamePlay()
    {
        mainMenu_Model.ChangeScreenSelected(13, false);
        General.SetGamePlayStates(General.GamePlayStatesEnum.NewGame);
        SceneManager.LoadScene(SceneInfo.GetSceneNames[2], LoadSceneMode.Single);
    }
    public void ChangeScreenConfigDefineKey(int id)
    {
        selectedscreensRedefineButtons = id;
        mainMenu_Model.ChangeScreenConfigDefineKey(id);
    }
    public void SelectInput(int id)
    {
        options_Model.SelectInput(id);
    }
    public void LoadTournament(int id)
    {
        General.GetSetTournamentID = id;
        mainMenu_Model.ChangeScreenSelected(9);        
    }    
    public void GotoTournamentScreen()
    {
        General.SetGamePlayStates(General.GamePlayStatesEnum.NewGame);
        SceneManager.LoadScene(SceneInfo.GetSceneNames[3], LoadSceneMode.Single);
    }
    public void DeleteSave(int id)
    {
        try
        {
            General.GetSetisShowErrorMessage = false;
            loadModel.CheckDeleteSlot(id, this, ErrorTextFile);            
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }        
    }
    public void LoadSave(int id)
    {
        loadModel.LoadSave(id, this, ErrorTextFile);
    }
    public void ConfirmDelete()
    {
        loadModel.ConfirmDelete(this, ErrorTextFile);
    }
    public void CancelDelete()
    {
        loadModel.EnableDeleteWarning(false);
    }
    public void GoToTutorial()
    {        
        mainMenu_Model.SelectGameMode(6);
        mainMenu_Model.ChangeScreenSelected(13, false);        
        SceneManager.LoadScene(SceneInfo.GetSceneNames[2], LoadSceneMode.Single);
    }
    public void RadioButtonEditorChangeButton(int id)
    {
        editor_Model.RadioButtonEditorChangeButton(id);
    }
    public void SelectTeamEditor(int id)
    {
        editor_Model.SelecteTeam(id);
    }
    public void ChangeTeamName()
    {
        editor_Model.ChangeTeamName();
    }
    public void SetSkillGraphNewSpeed()
    {
        editor_Model.SetSkillGraphNewSpeed();
    }
    public void SetSkillGraphNewAccel()
    {
        editor_Model.SetSkillGraphNewAccel();
    }
    public void SetSkillGraphNewCorner()
    {
        editor_Model.SetSkillGraphNewCorner();
    }
    public void ChangeScreenEditorTeamEvent(int id)
    {
        editor_Model.ChangeScreenEditorTeamEvent(id);
    }
    public void GetSliderColorValue()
    {
        editor_Model.GetSliderColorValue();
    }
    public void GetSliderColorTextValue()
    {
        editor_Model.GetSliderColorTextValue();
    }
    public void GetSliderCockpitColorValue(int id)
    {        
        editor_Model.GetSliderCockpitColorValue(id);
    }
    public void GetSliderClothColorValue(int id)
    {
        editor_Model.GetSliderClothColorValue(id);
    }
    public void GetSliderHelmetColorValue()
    {
        editor_Model.GetSliderHelmetColorValue();
    }
    public void SetTextDriversEvents(int id)
    {
        editor_Model.SetTextDriversEvents(id);
    }
    public void ChangeDriverName()
    {
        editor_Model.ChangeDriverName();
    }
    public void ChangeDriverCode()
    {
        editor_Model.ChangeDriverCode();
    }
    public void SetDriverSkillGraph()
    {
        editor_Model.SetDriverSkillGraph();
    }
    public void ChangeScreenEditorDriverCountryEvent(int id)
    {
        editor_Model.ChangeScreenEditorDriverCountryEvent(id);
    }
    public void ChangeScreenEditorDriverCountryIDEvent(int id)
    {        
        editor_Model.ChangeScreenEditorDriverCountryIDEvent(id);
    }
    public void ChangeScreenEditorDriverAvatarEvent(int id)
    {
        editor_Model.ChangeScreenEditorDriverAvatarEvent(id);
    }
    public void ChangeScreenEditorDriverAvatarIDEvent(int id)
    {
        editor_Model.ChangeScreenEditorDriverAvatarIDEvent(id);
    }
    public void SaveTeamsAndDriversEvent()
    {
        editor_Model.SaveTeamsAndDriversEvent();
    }
    public void ChangeScreenEditorSaveEvent(int id)
    {
        editor_Model.ChangeScreenEditorSaveEvent(id);
    }
    public void CallRestoreEvent()
    {
        editor_Model.CallRestoreEvent();
    }
    public void SelectTeamForGamePlay(int id)
    {
        carDriverSelect_Model.SelectTeam(id);        
    }
    public void SelectDriver(int id)
    {
        carDriverSelect_Model.SelectDriver(id);        
    }
    public void SetButtonGroupPractise(int id)
    {
        mainMenu_Model.SetButtonGroupPractise(id);
    }
    public void SelectTrack(int id)
    {
        trackSelect_Model.SelectTrack(id);
    }
    public void GotoGamePlay(int id)
    {
        mainMenu_Model.GotoGamePlay(id);
    }

    public void ShowHideButtonGroup_Difficult_Select(int id)
    {
        mainMenu_Model.ShowHideButtonGroup_Difficult_Select(id);
    }

    public void GotoSeasonCareer()
    {
        mainMenu_Model.GotoSeasonCareer();
    }

    public void ChangeDriverNameCareer()
    {
        career_Model.ChangeDriverNameCareer();
    }
    public void ChangeDriverCodeCareer()
    {
        career_Model.ChangeDriverCodeCareer();
    }

    public void GetSliderHelmetCareerColorValue()
    {
        career_Model.GetSliderHelmetColorValue();
    }

    public void OpenCloseCountryModal(bool show)
    {
        career_Model.OpenCloseCountryModal(show);
    }
    public void SetCountryId(int id)
    {
        career_Model.SetCountryId(id);
    }
    public void OpenCloseAvatarModal(bool show)
    {
        career_Model.OpenCloseAvatarModal(show);
    }
    public void SetAvatarId(int id)
    {
        career_Model.SetAvatarId(id);
    }

    public void SetCareerScreen(int id)
    {
        career_Model.SetCareerScreen(id);
        career_Model.SetTeamId(8);
    }

    public void GotoCareer()
    {
        mainMenu_Model.GotoCareer();
    }

    public void SetTeamId(int id)
    {
        career_Model.SetTeamId(id);
    }
    #endregion
}
