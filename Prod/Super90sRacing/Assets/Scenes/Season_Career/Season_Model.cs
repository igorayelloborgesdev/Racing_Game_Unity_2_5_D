using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Season_Model
{
    #region Variable
    private StandingObj[] standingObj = null;
    public TeamDTO[] teams = null;
    public DriverDTO[] drivers = null;    
    public GameObject loadingScreen = null;
    public int[] points = { 10, 6, 4, 3, 2, 1};
    private ChangeScreen changeScreen;
    private ChangeScreen changeScreenCareer;
    //private int finalRound = 16;//<-
    private int finalRound = 16;//<-
    private int finalYear = 3;
    private GameObject gameSavedScreen = null;
    private Text gameSavedScreentext = null;
    private GameObject[] screens = null;
    private GameObject outScreenPosition = null;
    private GameObject screenPosition = null;
    private LoadModel loadModel = null;
    private Text[] text_Save_Label = null;
    private GameObject seasonGroup = null;    
    private GameObject careerGroup = null;
    private GameObject[] careerScreen = null;
    private Text[] textBtnRadioCareer = null;
    private Color colorBtnSelected;    
    private Color colorBtnUnselected;
    private RadioButton radioButtonCareer = null;
    private HistoricManager[] historicManager = null;
    private int[] challengeLevel = new int[] { 0, 3, 6, 9, 12 };
    private List<List<int>> challengeTier = new List<List<int>>();
    private TeamButton[] teamButtonCareer = null;
    private List<int> challengeIndexList = new List<int>();    
    private Text[] textBtnRadioDriverCareer = null;
    private RadioButton radioButtonDriverCareer = null;
    private List<DriverDTO> driversChallenge = null;    
    private Text textTeamNameCareer = null;
    private SkillGraph[] skillGraph = null;
    private SkillGraph skillGraphDriver = null;
    private Image driverAvatarButton = null;
    private Image driverCountryFlag = null;
    private Car2D car2D = null;

    private Image driverAvatarChallenger = null;
    private Image driverCountryFlagChallenger = null;
    private Car2D car2DChallenger = null;
    private Text textTeamNameCareerChallenger = null;

    private Image driverAvatarCurrent = null;
    private Image driverCountryFlagCurrent = null;
    private Car2D car2DCurrent = null;
    private Text textTeamNameCareerCurrent = null;
    
    private Image driverAvatarChallengeYou = null;
    private Image driverCountryFlagChallengeYou = null;    
    private Car2D car2DChallengeYou = null;    
    private Text textTeamNameCareerChallengeYou = null;

    #endregion
    #region Constructor
    public Season_Model(StandingObj[] _standingObj, GameObject _loadingScreen, GameObject[] screens, 
        GameObject outScreenPosition, GameObject screenPosition, GameObject gameSavedScreen, 
        Text[] text_Save_Label, Text gameSavedScreentext, GameObject seasonGroup, GameObject careerGroup,
        GameObject[] careerScreen, Text[] textBtnRadioCareer, Color colorBtnSelected, Color colorBtnUnselected,
        HistoricManager[] historicManager, TeamButton[] teamButtonCareer, Text[] textBtnRadioDriverCareer,
        Text textTeamNameCareer, SkillGraph[] skillGraph, SkillGraph skillGraphDriver, Image driverAvatarButton,
        Image driverCountryFlag, Car2D car2D,
        Image driverAvatarChallenger, Image driverCountryFlagChallenger, Car2D car2DChallenger, Text textTeamNameCareerChallenger, 
        Image driverAvatarCurrent, Image driverCountryFlagCurrent, Car2D car2DCurrent, Text textTeamNameCareerCurrent,
        Image driverAvatarChallengeYou, Image driverCountryFlagChallengeYou, Car2D car2DChallengeYou, Text textTeamNameCareerChallengeYou)
    {
        this.standingObj = _standingObj;
        this.loadingScreen = _loadingScreen;        
        this.gameSavedScreen = gameSavedScreen;
        this.screens = screens;
        this.outScreenPosition = outScreenPosition;
        this.screenPosition = screenPosition;
        this.text_Save_Label = text_Save_Label;
        this.gameSavedScreentext = gameSavedScreentext;
        this.seasonGroup = seasonGroup;
        this.careerGroup = careerGroup;
        this.careerScreen = careerScreen;
        this.textBtnRadioCareer = textBtnRadioCareer;
        this.colorBtnSelected = colorBtnSelected;
        this.colorBtnUnselected = colorBtnUnselected;
        this.historicManager = historicManager;
        this.teamButtonCareer = teamButtonCareer;
        this.textBtnRadioDriverCareer = textBtnRadioDriverCareer;
        this.textTeamNameCareer = textTeamNameCareer;
        this.skillGraph = skillGraph;
        this.skillGraphDriver = skillGraphDriver;
        this.driverAvatarButton = driverAvatarButton;
        this.driverCountryFlag = driverCountryFlag;
        this.car2D = car2D;
        this.driverAvatarChallenger = driverAvatarChallenger;
        this.driverCountryFlagChallenger = driverCountryFlagChallenger;
        this.car2DChallenger = car2DChallenger;
        this.textTeamNameCareerChallenger = textTeamNameCareerChallenger;
        this.driverAvatarCurrent = driverAvatarCurrent;
        this.driverCountryFlagCurrent = driverCountryFlagCurrent;
        this.car2DCurrent = car2DCurrent;
        this.textTeamNameCareerCurrent = textTeamNameCareerCurrent;
        this.driverAvatarChallengeYou = driverAvatarChallengeYou;
        this.driverCountryFlagChallengeYou = driverCountryFlagChallengeYou;
        this.car2DChallengeYou = car2DChallengeYou;
        this.textTeamNameCareerChallengeYou = textTeamNameCareerChallengeYou;
    }
    #endregion
    #region Methods
    public void Init()
    {
        changeScreen = new ChangeScreen(screens, outScreenPosition.transform.position, screenPosition.transform.position);
        changeScreenCareer = new ChangeScreen(careerScreen, outScreenPosition.transform.position, screenPosition.transform.position);
        changeScreenCareer.ChangeScreenSelected(0);
        loadModel = new LoadModel(text_Save_Label);
        loadModel.FillSaveGamesInfo();
        gameSavedScreen.SetActive(false);
        radioButtonCareer = new RadioButton(textBtnRadioCareer, colorBtnSelected, colorBtnUnselected);
        radioButtonCareer.ChangeButton(0);

        radioButtonDriverCareer = new RadioButton(textBtnRadioDriverCareer, colorBtnSelected, colorBtnUnselected);
        radioButtonDriverCareer.ChangeButton(0);

        if (General.GetGameMode() == General.GameModesEnum.Season)
        {
            seasonGroup.SetActive(true);
            careerGroup.SetActive(false);
        }
        if (General.GetGameMode() == General.GameModesEnum.Career)
        {
            seasonGroup.SetActive(false);
            careerGroup.SetActive(true);
            InitHistoric();
            var count = 0;
            for (int i = 0; i < 5; i++)
            {
                challengeTier.Add(new List<int>());
                challengeTier[i].Add(count);
                count++;
                challengeTier[i].Add(count);
                count++;
            }            
        }        
    }
    public void SeasonDriverFacadeInit()
    {
        changeScreen.ChangeScreenSelected(0);
        loadingScreen.SetActive(false);
        if (General.GetGameMode() == General.GameModesEnum.Season)
        {
            SetTeamsAndDrivers();
            if (SeasonGeneral.enumRacingStatus == SeasonGeneral.EnumSeasonInitStatus.NewGame)
            {                
                CreateDriversNewGame();
            }
            else if (SeasonGeneral.enumRacingStatus == SeasonGeneral.EnumSeasonInitStatus.FromRace)
            {
                CreateDriversFromRace();
            }
            else if (SeasonGeneral.enumRacingStatus == SeasonGeneral.EnumSeasonInitStatus.Fromload)
            {
                SetStandingsTableFromLoad();
            };
        }
        else if (General.GetGameMode() == General.GameModesEnum.Career)
        {
            SetTeamsAndDriversCareer();
            if (SeasonGeneral.enumRacingStatus == SeasonGeneral.EnumSeasonInitStatus.NewGame)
            {
                CreateDriversNewGame();
            }
            else if (SeasonGeneral.enumRacingStatus == SeasonGeneral.EnumSeasonInitStatus.FromRace)
            {
                DefineChallengeResult();
                CreateDriversFromRace();
                SetDriverResult();                
            }
            else if (SeasonGeneral.enumRacingStatus == SeasonGeneral.EnumSeasonInitStatus.Fromload)
            {
                SetStandingsTableFromLoad();
                SetDriverResult();
            };
        }

    }

    private void DefineChallengeResult()
    {
        if (!SeasonGeneral.isChallengeFinished)
        {
            if (SeasonGeneral.currentPosition < SeasonGeneral.challengerPosition)
                SeasonGeneral.currentPoints++;
            else
                SeasonGeneral.challengerPoints++;            

            PrintChallengeResult();
        }        
    }

    private void DefineChallengeTeamChange()
    {
        if (SeasonGeneral.isChallengeFinished)
            return;

        if (SeasonGeneral.currentPoints == 2 || SeasonGeneral.challengerPoints == 2)
        {
            SeasonGeneral.isChallengeFinished = true;

            var challengerDriver = SeasonGeneral.careerDrivers.Where(x => x.id == SeasonGeneral.challengerId).First();
            var auxDriverTeamId = challengerDriver.teamId;
            var currentDriver = SeasonGeneral.careerDrivers.Where(x => x.id == SeasonGeneral.currentId).First();

            if (SeasonGeneral.challengerPoints > SeasonGeneral.currentPoints)
            {                
                challengerDriver.teamId = currentDriver.teamId;
                currentDriver.teamId = auxDriverTeamId;

                SeasonGeneral.driverSeasonGeneralList.Where(x => x.driverID == SeasonGeneral.challengerId).First().teamID = challengerDriver.teamId;
                SeasonGeneral.driverSeasonGeneralList.Where(x => x.driverID == SeasonGeneral.currentId).First().teamID = currentDriver.teamId;


                if (challengerDriver.id == SeasonGeneral.playerIDSeason)
                {
                    SeasonGeneral.teamIDSeason = challengerDriver.teamId;
                    SeasonGeneral.isPlayerWonLastChallenge = true;
                }
                else
                {
                    SeasonGeneral.teamIDSeason = currentDriver.teamId;
                    SeasonGeneral.isPlayerWonLastChallenge = false;
                }
            }
            else
            {
                if (currentDriver.id == SeasonGeneral.playerIDSeason)
                {                    
                    SeasonGeneral.isPlayerWonLastChallenge = true;
                }
                else
                {                    
                    SeasonGeneral.isPlayerWonLastChallenge = false;
                }
            }

            int playerTierID = GetTierId();
            if (playerTierID == 0)
                SeasonGeneral.isPlayerWonLastChallenge = false;
            else if (playerTierID == 4)
                SeasonGeneral.isPlayerWonLastChallenge = true;
        }        
    }

    private void PrintChallengeResult()
    {
        var driverChallenge = drivers.Where(x => x.id == SeasonGeneral.challengerId).First();
        var driverCurrent = drivers.Where(x => x.id == SeasonGeneral.currentId).First();
        
        driverAvatarChallenger.sprite = Resources.Load<Sprite>(driverChallenge.avatarId);
        driverAvatarCurrent.sprite = Resources.Load<Sprite>(driverCurrent.avatarId);

        driverCountryFlagChallenger.sprite = Resources.Load<Sprite>(General.GetSetCountries[driverChallenge.countryId].countryFlag);
        driverCountryFlagCurrent.sprite = Resources.Load<Sprite>(General.GetSetCountries[driverCurrent.countryId].countryFlag);

        car2DChallenger.ChangeChassi(teams[driverChallenge.teamId].id);
        var newColor0_1 = new Color(
                    teams[driverChallenge.teamId].clothColorList[0].r,
                    teams[driverChallenge.teamId].clothColorList[0].g,
                    teams[driverChallenge.teamId].clothColorList[0].b
                    );
        car2DChallenger.ChangeCloth(newColor0_1);
        var newColor0_2 = new Color(
            driverChallenge.helmetColor.r,
            driverChallenge.helmetColor.g,
            driverChallenge.helmetColor.b
            );
        car2DChallenger.ChangeHelmet(newColor0_2);        

        car2DCurrent.ChangeChassi(teams[driverCurrent.teamId].id);
        var newColor1_1 = new Color(
                    teams[driverCurrent.teamId].clothColorList[0].r,
                    teams[driverCurrent.teamId].clothColorList[0].g,
                    teams[driverCurrent.teamId].clothColorList[0].b
                    );
        car2DCurrent.ChangeCloth(newColor1_1);
        var newColor1_2 = new Color(
            driverCurrent.helmetColor.r,
            driverCurrent.helmetColor.g,
            driverCurrent.helmetColor.b
            );
        car2DCurrent.ChangeHelmet(newColor1_2);
        
        textTeamNameCareerChallenger.text = SeasonGeneral.challengerPoints.ToString();
        textTeamNameCareerCurrent.text = SeasonGeneral.currentPoints.ToString();
        changeScreen.ChangeScreenSelected(12);
    }

    private void SetTeamsAndDrivers()
    {
        teams = General.GetSetTeams;
        drivers = General.GetSetDrivers;
    }
    private void SetTeamsAndDriversCareer()
    {
        teams = General.GetSetTeams;
        drivers = SeasonGeneral.careerDrivers.ToArray();
    }
    private void CreateDriversNewGame()
    {
        SeasonGeneral.driverSeasonGeneralList = new List<DriverSeasonGeneral>();
        for (int i = 0; i < drivers.Length; i++)
        {                        
            var driverNew = new DriverSeasonGeneral()
            {
                driverID = drivers[i].id,
                teamID = drivers[i].teamId,
                isPlayer = drivers[i].id == SeasonGeneral.playerIDSeason
            };
            SeasonGeneral.driverSeasonGeneralList.Add(driverNew);
        }
        SetStandingsTable();
    }
    private void CreateDriversFromRace()
    {        
        SetStandingsTable();
    }    

    private void SetStandingsTable()
    {
        if (SeasonGeneral.trackIDSeason == 0)
        {
            for (int i = 0; i < SeasonGeneral.driverSeasonGeneralList.Count(); i++)
            {
                var driver = drivers.Where(x => x.id == SeasonGeneral.driverSeasonGeneralList[i].driverID).First();
                standingObj[i].driverName.text = driver.name;
                standingObj[i].SetCountryFlag(driver.countryId);
                standingObj[i].ChangeChassi(driver.teamId);
                standingObj[i].ChangeCloth(driver.teamId);
                standingObj[i].ChangeHelmet(driver.helmetColor.r, driver.helmetColor.g, driver.helmetColor.b);
                standingObj[i].points.text = "0";

                SeasonGeneral.driverSeasonGeneralList[i].posList = new List<int>();
                for (int a = 0; a < 16; a++)
                    SeasonGeneral.driverSeasonGeneralList[i].posList.Add(0);

                for (int j = 0; j < SeasonGeneral.driverSeasonGeneralList[i].posList.Count(); j++)
                {
                    standingObj[i].pos[j].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < SeasonGeneral.driverSeasonGeneralList.Count(); i++)
            {                
                SeasonGeneral.driverSeasonGeneralList[i].posList[SeasonGeneral.trackIDSeason - 1] = SeasonGeneral.driversResult.FindIndex(x => x == SeasonGeneral.driverSeasonGeneralList[i].driverID) + 1;
                for (int j = 0; j < SeasonGeneral.driverSeasonGeneralList[i].posList.Count(); j++)                
                    standingObj[i].pos[j].gameObject.SetActive(false);
                for (int a = 0; a < SeasonGeneral.trackIDSeason; a++)                
                    standingObj[i].pos[a].gameObject.SetActive(true);                                    
            }
            SetDriversPoints();
            PrintDriverPoints();            
        }
    }

    private void SetStandingsTableFromLoad()
    {        
        for (int i = 0; i < SeasonGeneral.driverSeasonGeneralList.Count(); i++)
        {
            for (int j = 0; j < SeasonGeneral.driverSeasonGeneralList[i].posList.Count(); j++)
                standingObj[i].pos[j].gameObject.SetActive(false);
            for (int a = 0; a < SeasonGeneral.trackIDSeason; a++)
                standingObj[i].pos[a].gameObject.SetActive(true);
        }
        OrderSeason();
        PrintDriverPoints();        
    }
    
    private void SetDriversPoints()
    {
        for (int i = 0; i < SeasonGeneral.driverSeasonGeneralList.Count(); i++)
        {            
            SeasonGeneral.driverSeasonGeneralList[i].points += SeasonGeneral.driverSeasonGeneralList[i].posList[SeasonGeneral.trackIDSeason - 1] < 7 
                ? GetPoints(SeasonGeneral.driverSeasonGeneralList[i].posList[SeasonGeneral.trackIDSeason - 1]) : 0;            
        }
        OrderSeason();
    }

    private void OrderSeason()
    {
        SeasonGeneral.driverSeasonGeneralList = SeasonGeneral.driverSeasonGeneralList.OrderByDescending(x => x.points)
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 1).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 2).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 3).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 4).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 5).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 6).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 7).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 8).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 9).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 10).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 11).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 12).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 13).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 14).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 15).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 16).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 17).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 18).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 19).Count())
            .ThenByDescending(x => x.posList.GroupBy(x1 => x1 == 20).Count())
            .ToList();
    }

    private void PrintDriverPoints()
    {
        for (int i = 0; i < SeasonGeneral.driverSeasonGeneralList.Count(); i++)
        {
            for (int a = 0; a < SeasonGeneral.trackIDSeason; a++)
            {
                standingObj[i].pos[a].gameObject.SetActive(true);
                standingObj[i].pos[a].text = SeasonGeneral.driverSeasonGeneralList[i].posList[a].ToString();
            }
            standingObj[i].points.text = SeasonGeneral.driverSeasonGeneralList[i].points.ToString();

            var driver = drivers.Where(x => x.id == SeasonGeneral.driverSeasonGeneralList[i].driverID).First();
            standingObj[i].driverName.text = driver.name;
            standingObj[i].SetCountryFlag(driver.countryId);
            standingObj[i].ChangeChassi(driver.teamId);
            standingObj[i].ChangeCloth(driver.teamId);
            standingObj[i].ChangeHelmet(driver.helmetColor.r, driver.helmetColor.g, driver.helmetColor.b);
        }
    }

    private int GetPoints(int pos)
    {
        return points[pos - 1];
    }

    public void SetLoading()
    {
        loadingScreen.SetActive(true);
    }

    public void QuitButtonBack()
    {
        changeScreen.ChangeScreenSelected(0);
    }
    public void QuitButton()
    {
        SceneManager.LoadScene(SceneInfo.GetSceneNames[1], LoadSceneMode.Single);
    }
    public void QuitButtonOpenModal()
    {
        changeScreen.ChangeScreenSelected(1);
    }

    public void SaveButtonOpenModal()
    {
        changeScreen.ChangeScreenSelected(5);
    }    

    public void GotoSeasonRace()
    {
        if (General.GetGameMode() == General.GameModesEnum.Season)
        {
            if (SeasonGeneral.trackIDSeason == finalRound)
            {
                var posFinal = SeasonGeneral.driverSeasonGeneralList.FindIndex(x => x.isPlayer);
                if (posFinal == 0)
                    changeScreen.ChangeScreenSelected(3);
                else
                    changeScreen.ChangeScreenSelected(4);
            }
            else
            {
                SetLoading();
                SceneManager.LoadScene(SceneInfo.GetSceneNames[2], LoadSceneMode.Single);
            }
        }
        else if (General.GetGameMode() == General.GameModesEnum.Career)
        {
            if (SeasonGeneral.trackIDSeason == finalRound)
            {
                SeasonGeneral.careerYear++;
                var posFinal = SeasonGeneral.driverSeasonGeneralList.FindIndex(x => x.isPlayer);
                if (SeasonGeneral.careerYear == finalYear)
                {
                    if (posFinal == 0)
                        changeScreen.ChangeScreenSelected(8);
                    else
                        changeScreen.ChangeScreenSelected(9);
                }
                else
                {                    
                    if (posFinal == 0)
                        changeScreen.ChangeScreenSelected(6);
                    else
                        changeScreen.ChangeScreenSelected(7);                    
                }
                ResetCareerYear();
            }
            else
            {
                
                ChallengeManager();

                
            }
        }
    }

    private void DefineTierWin()
    {
        var driver = SeasonGeneral.driverSeasonGeneralList.Where(x => x.isPlayer).First();
        int index = GetTierId();        
        if (index != 0)
        {
            challengeIndexList = challengeTier[index - 1];
        }
        else
        {
            challengeIndexList = challengeTier[index + 1];
        }
        SetTeamsButtonStyleCareer();
    }

    private void DefineTierLose()
    {
        var driver = SeasonGeneral.driverSeasonGeneralList.Where(x => x.isPlayer).First();
        int index = GetTierId();
        if (index != 4)
        {
            challengeIndexList = challengeTier[index + 1];
        }
        else
        {
            challengeIndexList = challengeTier[index - 1];
        }

        int randomNumber = UnityEngine.Random.Range(0, challengeIndexList.Count);        
        var challengeDrivers = drivers.Where(x => x.teamId == challengeIndexList[randomNumber]).ToList();
        int randomNumberDriver = UnityEngine.Random.Range(0, challengeDrivers.Count);        
        var ChallengeDriver = challengeDrivers[randomNumberDriver];

        SetAIChallengerStyleCareer(ChallengeDriver);

        SeasonGeneral.challengerId = ChallengeDriver.id;            
        SeasonGeneral.currentId = SeasonGeneral.driverSeasonGeneralList.Where(x => x.isPlayer).First().driverID;
    }

    private void SetAIChallengerStyleCareer(DriverDTO driverChallengeDTO)
    {
        textTeamNameCareerChallengeYou.text = driverChallengeDTO.name;
        driverAvatarChallengeYou.sprite = Resources.Load<Sprite>(driverChallengeDTO.avatarId);
        driverCountryFlagChallengeYou.sprite = Resources.Load<Sprite>(General.GetSetCountries[driverChallengeDTO.countryId].countryFlag);

        car2DChallengeYou.ChangeChassi(teams[driverChallengeDTO.teamId].id);        
        
        var newColor1 = new Color(
            teams[driverChallengeDTO.teamId].clothColorList[0].r,
            teams[driverChallengeDTO.teamId].clothColorList[0].g,
            teams[driverChallengeDTO.teamId].clothColorList[0].b
            );
        car2DChallengeYou.ChangeCloth(newColor1);
        
        
        var newColor2 = new Color(
            driverChallengeDTO.helmetColor.r,
            driverChallengeDTO.helmetColor.g,
            driverChallengeDTO.helmetColor.b
            );
        car2DChallengeYou.ChangeHelmet(newColor2);

        
    }


    private int GetTierId()
    {
        var driver = SeasonGeneral.driverSeasonGeneralList.Where(x => x.isPlayer).First();        
        for (int i = 0; i < challengeTier.Count; i++)
        {
            for (int j = 0; j < challengeTier[i].Count; j++)
            {
                if (challengeTier[i][j] == driver.teamID)
                {
                    return i;                    
                }
            }
        }
        return 0;
    }
    
    private void SetTeamsButtonStyleCareer()
    {
        for (int i = 0; i < teamButtonCareer.Length; i++)
        {
            teamButtonCareer[i].ChangeName(teams[challengeIndexList[i]].name, 
                new Color(teams[challengeIndexList[i]].teamLogoFontColor.r, teams[challengeIndexList[i]].teamLogoFontColor.g, teams[challengeIndexList[i]].teamLogoFontColor.b));
            teamButtonCareer[i].ChangeColor(
                new Color(teams[challengeIndexList[i]].teamLogoColorList[0].r, teams[challengeIndexList[i]].teamLogoColorList[0].g, teams[challengeIndexList[i]].teamLogoColorList[0].b),
                new Color(teams[challengeIndexList[i]].teamLogoColorList[1].r, teams[challengeIndexList[i]].teamLogoColorList[1].g, teams[challengeIndexList[i]].teamLogoColorList[1].b),
                new Color(teams[challengeIndexList[i]].teamLogoColorList[2].r, teams[challengeIndexList[i]].teamLogoColorList[2].g, teams[challengeIndexList[i]].teamLogoColorList[2].b)
                );
        }
        ChooseTeamChallenge(0);
    }

    public void ChooseTeamChallenge(int id)
    {
        driversChallenge = drivers.Where(x => x.teamId == challengeIndexList[id]).ToList();        
        for(int i = 0; i < textBtnRadioDriverCareer.Count(); i++)
        {
            textBtnRadioDriverCareer[i].text = driversChallenge[i].name;
        }        
        textTeamNameCareer.text = teams[challengeIndexList[id]].name;
        skillGraph[0].SetSkillGraph(teams[challengeIndexList[id]].speed);
        skillGraph[1].SetSkillGraph(teams[challengeIndexList[id]].accel);
        skillGraph[2].SetSkillGraph(teams[challengeIndexList[id]].corner);
        SetCarImage(id);
        SetCarClothColor(id);
        ChooseDriverChallenge(0);
    }

    public void ChooseDriverChallenge(int id)
    {
        skillGraphDriver.SetSkillGraph(driversChallenge[id].skill);
        radioButtonDriverCareer.ChangeButton(id);
        SetAvatarSprite(id);
        SetCountryFlagSprite(id);
        SetDriverHelmetColor(id);
        SeasonGeneral.challengerId = SeasonGeneral.driverSeasonGeneralList.Where(x => x.isPlayer).First().driverID;
        SeasonGeneral.currentId = driversChallenge[id].id;     
    }

    private void SetCarImage(int id)
    {
        car2D.ChangeChassi(teams[challengeIndexList[id]].id);
    }
    private void SetCarClothColor(int id)
    {
        var newColor = new Color(
            teams[challengeIndexList[id]].clothColorList[0].r,
            teams[challengeIndexList[id]].clothColorList[0].g,
            teams[challengeIndexList[id]].clothColorList[0].b
            );
        car2D.ChangeCloth(newColor);
    }
    private void SetDriverHelmetColor(int id)
    {        
        var newColor = new Color(
            driversChallenge[id].helmetColor.r,
            driversChallenge[id].helmetColor.g,
            driversChallenge[id].helmetColor.b
            );
        car2D.ChangeHelmet(newColor);
    }

    private void SetCountryFlagSprite(int id)
    {
        driverCountryFlag.sprite = Resources.Load<Sprite>(General.GetSetCountries[driversChallenge[id].countryId].countryFlag);
    }

    private void SetAvatarSprite(int id)
    {
        driverAvatarButton.sprite = Resources.Load<Sprite>(driversChallenge[id].avatarId);
    }

    private void ChallengeManager()
    {
        if (challengeLevel.ToList().Where(x => x == SeasonGeneral.trackIDSeason).Any())
        {
            DefineChallengeTeamChange();
            SeasonGeneral.ResetChallengeInGame();

            int playerTierID = GetTierId();
            
            if (SeasonGeneral.isPlayerWonLastChallenge)
            {
                DefineTierWin();
                changeScreen.ChangeScreenSelected(10);
            }
            else
            {
                DefineTierLose();
                changeScreen.ChangeScreenSelected(11);
            }            
        }
        else
        {
            DefineChallengeTeamChange();
            GotoRace();
        }
    }

    public void GotoRace()
    {
        SetLoading();
        SceneManager.LoadScene(SceneInfo.GetSceneNames[2], LoadSceneMode.Single);
    }

    private void ResetCareerYear()
    {
        SeasonGeneral.trackIDSeason = 0;
        foreach (var driverSeasonGeneral in SeasonGeneral.driverSeasonGeneralList)
        {
            driverSeasonGeneral.points = 0;
            for (int i = 0; i < driverSeasonGeneral.posList.Count(); i++)
            {
                driverSeasonGeneral.posList[i] = 0;
            }            
        }
        SetStandingsTableFromLoad();
    }

    public void SaveGame(int id)
    {
        var returnObj = SaveLoadGame.SaveGame(id);        
        gameSavedScreen.SetActive(true);
        gameSavedScreentext.text = returnObj.message;
        loadModel.FillSaveGamesInfo();     
    }
    public void CloseSaveGame()
    {
        gameSavedScreen.SetActive(false);
    }
    public void ChangeCareerButton(int id)
    {
        changeScreenCareer.ChangeScreenSelected(id);
        radioButtonCareer.ChangeButton(id);
    }

    public void InitHistoric()
    {
        foreach (var historic in historicManager)
        {
            historic.HideAll();
        }
    }

    public void SetDriverResult()
    {
        if (SeasonGeneral.careerYear == 0 && SeasonGeneral.trackIDSeason == 0)
        {
            for(int i = 0; i < historicManager.Length; i++)
            {
                historicManager[i].ShowHidePos(true);
            }            
            return;
        }

        var driverSeason = SeasonGeneral.driverSeasonGeneralList.Where(x => x.isPlayer).First();
        var driverSeasonPos = SeasonGeneral.driverSeasonGeneralList.FindIndex(x => x.isPlayer);

        SeasonGeneral.historicSeasonDTO[SeasonGeneral.careerYear].finalPos = driverSeasonPos + 1;

        if (SeasonGeneral.trackIDSeason != 0)
        {
            SeasonGeneral.historicSeasonDTO[SeasonGeneral.careerYear].historicDTO[SeasonGeneral.trackIDSeason - 1].pos = driverSeason.posList[SeasonGeneral.trackIDSeason - 1];
            SeasonGeneral.historicSeasonDTO[SeasonGeneral.careerYear].historicDTO[SeasonGeneral.trackIDSeason - 1].teamId = SeasonGeneral.teamIDSeason;
        }
        else
        {
            SeasonGeneral.historicSeasonDTO[SeasonGeneral.careerYear].historicDTO[SeasonGeneral.trackIDSeason].pos = driverSeason.posList[SeasonGeneral.trackIDSeason];
            SeasonGeneral.historicSeasonDTO[SeasonGeneral.careerYear].historicDTO[SeasonGeneral.trackIDSeason].teamId = SeasonGeneral.teamIDSeason;
        }
        

        for (int i = 0; i < SeasonGeneral.careerYear + 1; i++)
        {

            if (SeasonGeneral.careerYear == i)
            {
                for (int j = 0; j < SeasonGeneral.trackIDSeason; j++)
                {
                    historicManager[i].ShowHistoric(j,

                        SeasonGeneral.historicSeasonDTO[i].historicDTO[j].pos,
                        SeasonGeneral.historicSeasonDTO[i].historicDTO[j].teamId,

                        SeasonGeneral.helmetColor.r,
                        SeasonGeneral.helmetColor.g,
                        SeasonGeneral.helmetColor.b);
                }
            }
            else
            {
                for (int j = 0; j < 16; j++)
                {
                    historicManager[i].ShowHistoric(j,

                        SeasonGeneral.historicSeasonDTO[i].historicDTO[j].pos,
                        SeasonGeneral.historicSeasonDTO[i].historicDTO[j].teamId,

                        SeasonGeneral.helmetColor.r,
                        SeasonGeneral.helmetColor.g,
                        SeasonGeneral.helmetColor.b);
                }
            }

            
            historicManager[i].ShowHidePos(true);
            historicManager[i].SetPos(SeasonGeneral.historicSeasonDTO[i].finalPos);
        }
    }

    #endregion
}
