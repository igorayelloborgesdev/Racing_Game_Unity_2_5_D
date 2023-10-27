using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SeasonGeneral;

public class RaceManager : MonoBehaviour
{
    [SerializeField]
    private GameObject car;
    [SerializeField]
    private GameObject aiCar;
    [SerializeField]
    public Text speedMeter;
    [SerializeField]
    public Text chronometer;
    [SerializeField]
    public Text gear;
    [SerializeField]
    public GameObject RPMPointer;
    [SerializeField]
    public Text position;
    [SerializeField]
    public Text difference;
    [SerializeField]
    public GameObject mapBlock1;
    [SerializeField]
    public GameObject mapBlock2;
    [SerializeField]
    public GameObject[] mapReference;
    [SerializeField]
    public GameObject mapPlayer;
    [SerializeField]
    public GameObject mapAI;
    [SerializeField]
    public RacingTrackGenerator racingTrackGenerator;
    [SerializeField]
    public Text chronometerLastLap;
    [SerializeField]
    public Text chronometerBestLap;
    [SerializeField]
    public Text text_ScreenName = null;
    [SerializeField]
    public Text text_ScreenName_race = null;
    [SerializeField]
    public GameObject loadingPage = null;
    [SerializeField]
    public Transform[] menuGroup = null;
    [SerializeField]
    public GameObject menuPage = null;
    [SerializeField]
    public GameObject pausePage = null;
    [SerializeField]
    public Text laps = null;
    [SerializeField]
    private float timerGridBoard = 0.0f;
    [SerializeField]
    public GameObject lapGroup = null;
    [SerializeField]
    public GameObject gotoQualifyButton = null;
    [SerializeField]
    public GameObject gotoRaceButton = null;
    [SerializeField]
    public GameObject resetButton = null;
    [SerializeField]
    public GameObject gridScreen = null;
    [SerializeField]
    private float timerEndSession = 0.0f;
    [SerializeField]
    private GridObj[] gridObjList = null;
    [SerializeField]
    public GameObject raceGroup = null;
    [SerializeField]
    public Text positionPlayer = null;
    [SerializeField]
    public Text frontText = null;
    [SerializeField]
    public Text backText = null;
    [SerializeField]
    public GameObject standingsGroup = null;
    [SerializeField]
    public GameObject standingsBtnBack = null;
    [SerializeField]
    public GameObject standingsBtnPodium = null;
    [SerializeField]
    public StandingObj[] StandingObjList = null;
    [SerializeField]
    public DriverDTO[] drivers = null;
    [SerializeField]
    public TeamDTO[] teams = null;
    [SerializeField]
    public GameObject podium = null;
    [SerializeField]
    public ClothUI[] clothPodium = null;
    [SerializeField]
    public Image[] avatarPodium = null;
    [SerializeField]
    public GameObject[] Button_BackToMenu = null;
    [SerializeField]
    public GameObject[] Button_BackToSeason = null;

    [SerializeField]
    private GameObject challengeGroup = null;
    [SerializeField]
    private Text posText_Rival = null;

    [SerializeField]
    private AudioSource audioData1 = null;
    [SerializeField]
    private AudioSource audioData2 = null;

    public float raceTime = 0.0f;
    private bool startRun = false;
    private bool showPauseMenu = false;
    private int sessionLap = 0;    

    public enum GameSessionEnum
    {
        Practice = 0,
        Qualify = 1,
        Grid = 2,
        Race = 3,
        Finish = 4,
        Standing = 5
    };
    private static GameSessionEnum gameSession = GameSessionEnum.Practice;

    private bool raceFinished = false;

    public bool GetShowPauseMenu
    {
        get
        {
            return showPauseMenu;
        }        
    }
    List<Grid> gridList = new List<Grid>();
    private List<Car> cars = new List<Car>();
    private List<Car> standings = new List<Car>();

    public enum EnumRacingStatus
    {
        Begin = 0,
        Red = 1,
        Green = 2,
        Racing = 3,
        Finished = 4
    }
    EnumRacingStatus enumRacingStatus = EnumRacingStatus.Begin;
    private List<CarMapUI> carMapUIList;
    private List<MapUI> mapUIList;
    private int trackID = 0;
    private int teamID = 0;
    private int playerID = 0;
    private int difficultID = 0;

    private int[] difficultArray = { 500, 750, 1000 };
    //private int[] difficultArray = { 10, 20, 30 };//<-

    // Start is called before the first frame update
    void Start()
    {        
        Init();
        racingTrackGenerator.LoadTrack("track_" + trackID.ToString(), trackID);
        GetGridPosition();                
        InstatiatePlayerCar();
        cars.Where(x => x.isPlayer).First().GetComponent<CarPlayer>().EnableCheckeredFlag(false);        
        CreateMap();
        if (General.GetGameMode() == General.GameModesEnum.Career && !SeasonGeneral.isChallengeFinished)        
            challengeGroup.SetActive(true);                    
        else
            challengeGroup.SetActive(false);

        audioData1.Play();
        audioData2.Stop();    

    }    
    void Update()
    {
        PauseEvent();
        PracticeManager();
        LapCount();
        QualifyManager();
        RacingManager();
        RetireCheck();
    }

    private void QualifyManager()
    {
        if (gameSession == GameSessionEnum.Qualify && cars.Where(x => x.isPlayer).First().GetComponent<Car>().laps > sessionLap)
        {
            cars.Where(x => x.isPlayer).First().GetComponent<Car>().qualifyLap = cars.Where(x => x.isPlayer).First().GetComponent<CarPlayer>().timeBestLap;
            cars.Where(x => x.isPlayer).First().GetComponent<Car>().qualifyFinished = true;
            if (timerEndSession < 2.0f)
            {
                cars.Where(x => x.isPlayer).First().GetComponent<CarPlayer>().EnableCheckeredFlag(true);
                timerEndSession += 1.0f * Time.deltaTime;
            }
            else
            {
                GoToGrid();
            }
        }        
    }

    private void OrderGrid()
    {
        var rank = cars.OrderByDescending(x => x.qualifyFinished).ThenBy(x => x.qualifyLap).ToList();
        for (int i = 0; i < rank.Count(); i++)
        {
            var driver = drivers.Where(x => x.id == rank[i].driverId).First();
            gridObjList[i].SetDriverCode(driver.code);
            gridObjList[i].SetDriverTime(rank[i].qualifyLap);
            gridObjList[i].SetCountryFlag(driver.countryId);
            gridObjList[i].ChangeChassi(driver.teamId);
            gridObjList[i].ChangeCloth(driver.teamId);
            gridObjList[i].ChangeHelmet(driver.helmetColor.r, driver.helmetColor.g, driver.helmetColor.b);
            cars.Where(x => x.driverId == rank[i].driverId).First().gameObject.transform.position = gridList[i].transform.position;
            cars.Where(x => x.driverId == rank[i].driverId).First().gameObject.transform.rotation = gridList[i].transform.rotation;         
        }
    }

    private void LapCount()
    {
        if (gameSession != GameSessionEnum.Practice)        
            laps.text = cars.Where(x => x.isPlayer).First().GetComponent<Car>().laps.ToString() + "/" + sessionLap;            
    }

    private void SetUpMenu()
    {
        Transform menuG = menuGroup[(int)General.GetGameMode() == 0 ? 0 : 1];
        GroupButtons[] menuGList = menuG.GetComponentsInChildren<GroupButtons>();
        menuPage = menuGList[0].gameObject;
        pausePage = menuGList[1].gameObject;

        foreach (var obj in menuGroup)
        {
            obj.gameObject.SetActive(false);
        }
        if((int)General.GetGameMode() == 0)
            menuGroup[0].gameObject.SetActive(true);
        else
            menuGroup[1].gameObject.SetActive(true);
    }

    private void PracticeManager()
    {
        if (startRun)
        {
            SetTimerGridBoard();
            SetPlayerMap();
        }            
    }
    
    public void Init()
    {
        frontText.text = "--:--.---";
        backText.text = "--:--.---";
        SetUpMenu();
        ShowLoading();
        trackID = SetTrackID();
        teamID = SetTeamID();
        difficultID = SetDifficult();
        if (General.GetGameMode() != General.GameModesEnum.Career)
            drivers = General.GetSetDrivers;
        else
            drivers = SeasonGeneral.careerDrivers.ToArray();
        teams = General.GetSetTeams;
        raceGroup.SetActive(false);
        standingsGroup.SetActive(false);
        podium.SetActive(false);
        standingsBtnBack.SetActive(false);
        standingsBtnPodium.SetActive(false);
        SetTextScreenName();
        if (General.GetGameMode() != General.GameModesEnum.Practice)
        {
            gotoQualifyButton.SetActive(true);
            resetButton.SetActive(true);
            gotoRaceButton.SetActive(false);
        }
        ShowHideBackMenuButton();
        Time.timeScale = 1;
    }

    private void ShowHideBackMenuButton()
    {
        if (General.GetGameMode() == General.GameModesEnum.Practice || General.GetGameMode() == General.GameModesEnum.SingleRace)
        {
            foreach (var backToMenu in Button_BackToMenu)
            {
                backToMenu.SetActive(true);
            }
            foreach (var backToSeason in Button_BackToSeason)
            {
                backToSeason.SetActive(false);
            }
        }
        else
        {
            foreach (var backToMenu in Button_BackToMenu)
            {
                backToMenu.SetActive(false);
            }
            foreach (var backToSeason in Button_BackToSeason)
            {
                backToSeason.SetActive(true);
            }
        }
    }

    public void SetTextScreenName()
    {
        if(General.GetGameMode() == General.GameModesEnum.Practice)
            text_ScreenName.text = Language.GetLanguage[General.GetSetConfig.languageID][0];
        if (General.GetGameMode() == General.GameModesEnum.SingleRace)
            text_ScreenName_race.text = Language.GetLanguage[General.GetSetConfig.languageID][1];
        else if (General.GetGameMode() == General.GameModesEnum.Season)
            text_ScreenName_race.text = Language.GetLanguage[General.GetSetConfig.languageID][2];
        else
            text_ScreenName_race.text = Language.GetLanguage[General.GetSetConfig.languageID][3];
    }
    
    private void GetGridPosition()
    {
        GameObject[] gridArray = GameObject.FindGameObjectsWithTag("grid");
        List<GameObject> gridListGameObject = new List<GameObject>(gridArray);
        foreach (GameObject gridObj in gridListGameObject)
        {
            gridList.Add(gridObj.GetComponent<Grid>());
        }
        gridList = gridList.OrderBy(a => a.id).ToList();
    }
    
    private void InstatiatePlayerCar()
    {
        var driversList = drivers.Where(x => x.teamId == teamID).ToList();
        playerID = SetPlayerID(driversList);
        
        for (int i = 0; i < teams.Length; i++)
        {
            var teamsList = teams[i];
            driversList = drivers.Where(x => x.teamId == teamsList.id).ToList();
            for (int j = 0; j < 2; j++)
            {
                var driver = driversList[j];
                GameObject instantiated = null;
                if (teamID == teamsList.id && driver.id == playerID)
                {
                    instantiated = Instantiate(car, gridList[driver.id].transform.position, gridList[driver.id].transform.rotation);
                    instantiated.GetComponent<Car>().isPlayer = true;
                    instantiated.GetComponent<Car>().SetSpeedMax((10 - teamsList.speed) / 1000.0f);
                    instantiated.GetComponent<Car>().SetHandleIncreaseMax((10 - teamsList.corner) * 3);
                    instantiated.GetComponent<Car>().SetspeedIncreaseArray(teamsList.accel / 10.0f);
                    InstantiatePlayerCar(instantiated, teamsList.id);
                }
                else
                {
                    var randomAI = CalculateAIRandomSkill(driver);
                    instantiated = Instantiate(aiCar, gridList[driver.id].transform.position, gridList[driver.id].transform.rotation);
                    instantiated.GetComponent<Car>().SetSpeedMax((10 - (teamsList.speed - randomAI)) / 1000.0f);
                    instantiated.GetComponent<Car>().SetspeedIncreaseArray((teamsList.accel - randomAI) / 10.0f);
                    instantiated.GetComponent<CarAI>().raceManager = this;
                    InstantiateAICar(instantiated, teamsList.id, new Color(driver.helmetColor.r, driver.helmetColor.g, driver.helmetColor.b));

                    instantiated.GetComponent<Car>().qualifyLap = CalculateAIGrid(driver, teamsList);
                    instantiated.GetComponent<Car>().qualifyFinished = true;                    
                }
                instantiated.GetComponent<Car>().SetEnumRacingStatus((int)enumRacingStatus);
                instantiated.GetComponent<Car>().id = driver.id;
                instantiated.GetComponent<Car>().driverId = driver.id;
                instantiated.GetComponent<Car>().teamId = driver.teamId;
                instantiated.GetComponent<Car>().driverName = driver.name;                
                instantiated.GetComponent<Car>().tag = "Car";                
                cars.Add(instantiated.GetComponent<Car>());                
            }
        }
        
        foreach (var car in cars)
        {
            //if (car.GetComponent<Car>().id != playerID)
            if (!car.GetComponent<Car>().isPlayer)
            {
                car.gameObject.SetActive(false);
            }
        }
        var carPlayer = cars.Where(x => x.isPlayer).First();
        carPlayer.transform.position = gridList[0].transform.position;
        carPlayer.transform.rotation = gridList[0].transform.rotation;
    }

    private float CalculateAIRandomSkill(DriverDTO driver)
    {
        int difficult = difficultArray[difficultID];
        float skill = driver.skill/ difficult;
        float randomNumberMax = 1.0f - ((skill) / 10.0f);
        float randomNumber = UnityEngine.Random.Range(0.0f, randomNumberMax);        
        return randomNumber;
    }

    private float CalculateAIGrid(DriverDTO driver, TeamDTO team)
    {
        float sumSkill = driver.skill + team.accel + team.speed + team.corner;        
        float timeOffset = General.GetSetTracks[trackID].poleTime * (0.3f);                
        float randomNumber = UnityEngine.Random.Range(sumSkill / 2.5f, sumSkill);
        float result = (randomNumber * timeOffset) / 40.0f;
        float difficult = 1.0f + ((2.0f - General.GetSetConfig.difficultID) / 10.0f);        
        return (difficult * ((General.GetSetTracks[trackID].poleTime + (timeOffset - result)) - (driver.skill/ 10.0f)));
    }

    private void InstantiatePlayerCar(GameObject instantiated, int id)
    {
        instantiated.GetComponent<CarPlayer>().chronometer = chronometer;
        instantiated.GetComponent<CarPlayer>().chronometerLastLap = chronometerLastLap;
        instantiated.GetComponent<CarPlayer>().chronometerBestLap = chronometerBestLap;
        instantiated.GetComponent<CarPlayer>().RPMPointer = RPMPointer;
        instantiated.GetComponent<CarPlayer>().gear = gear;
        instantiated.GetComponent<CarPlayer>().speedMeter = speedMeter;
        instantiated.GetComponent<CarPlayer>().raceManager = this;

        instantiated.GetComponent<Car>().carPlayerGraphics.ChangeCockpitDetail1Color(
                   new Color(
                       teams[id].cockpitColorList[0].r,
                       teams[id].cockpitColorList[0].g,
                       teams[id].cockpitColorList[0].b
                       )
                   );
        instantiated.GetComponent<Car>().carPlayerGraphics.ChangeCockpitDetailColor(
            new Color(
                teams[id].cockpitColorList[1].r,
                teams[id].cockpitColorList[1].g,
                teams[id].cockpitColorList[1].b
                )
            );

        instantiated.GetComponent<Car>().carPlayerGraphics.ChangeGloveColor(
            new Color(
                teams[id].cockpitClothColorList[0].r,
                teams[id].cockpitClothColorList[0].g,
                teams[id].cockpitClothColorList[0].b
                )
            );
        instantiated.GetComponent<Car>().carPlayerGraphics.ChangeGloveDetailColor(
            new Color(
                teams[id].cockpitClothColorList[1].r,
                teams[id].cockpitClothColorList[1].g,
                teams[id].cockpitClothColorList[1].b
                )
            );
    }

    private void InstantiateAICar(GameObject instantiated, int id, Color color)
    {
        instantiated.GetComponent<CarAI>().SetGraph(id);
        instantiated.GetComponent<CarAI>().SetHelmetColor(color);
    }

    private void SetTimerGridBoard()
    {        
        if (timerGridBoard < 5.0f)
        {
            timerGridBoard += 1.0f * Time.deltaTime;
            if (enumRacingStatus == EnumRacingStatus.Begin && (timerGridBoard > 1.5f && timerGridBoard < 3.0f))
            {
                enumRacingStatus = EnumRacingStatus.Red;
                //instantiatedCar.Find(x => x.GetComponent<Car>().isPlayer).GetComponent<Car>().SetEnumRacingStatus((int)enumRacingStatus);
            }
            else if (enumRacingStatus == EnumRacingStatus.Red && (timerGridBoard >= 3.0f && timerGridBoard < 4.5f))
            {
                enumRacingStatus = EnumRacingStatus.Green;
                //instantiatedCar.Find(x => x.GetComponent<Car>().isPlayer).GetComponent<Car>().SetEnumRacingStatus((int)enumRacingStatus);
            }
            else if (enumRacingStatus == EnumRacingStatus.Green && timerGridBoard >= 4.5f)
            {
                enumRacingStatus = EnumRacingStatus.Racing;
                //instantiatedCar.Find(x => x.GetComponent<Car>().isPlayer).GetComponent<Car>().SetEnumRacingStatus((int)enumRacingStatus);
            }
            foreach (var instCar in cars)
            {
                instCar.GetComponent<Car>().SetEnumRacingStatus((int)enumRacingStatus);
            }

        }
    }
    private void CreateMap()
    {
        GameObject newObj = new GameObject();
        TrackSegmentDTO trackSegmentDTO = racingTrackGenerator.LoadTrackJson(trackID);
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTO.trackVertexDTOArray)
        {
            GameObject mapBlockNew2 = Instantiate(mapBlock2, new Vector2(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].z), Quaternion.identity);
            mapBlockNew2.transform.SetParent(newObj.transform);
        }
        mapUIList = new List<MapUI>();
        int materialIndex = 0;
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTO.trackVertexDTOArray)
        {
            GameObject mapBlockNew1 = Instantiate(mapBlock1, new Vector2(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].z), Quaternion.identity);
            mapBlockNew1.transform.SetParent(newObj.transform);
            mapBlockNew1.AddComponent<MapUI>();
            mapBlockNew1.GetComponent<MapUI>().id = materialIndex;
            mapUIList.Add(mapBlockNew1.GetComponent<MapUI>());
            materialIndex++;
        }

        GameObject mapBlockNewFinishLine = Instantiate(mapBlock2, new Vector2(trackSegmentDTO.trackVertexDTOArray[0].pointsArray[0].x, trackSegmentDTO.trackVertexDTOArray[0].pointsArray[0].z), Quaternion.identity);
        mapBlockNewFinishLine.transform.SetParent(newObj.transform);


        carMapUIList = new List<CarMapUI>();        
        int playerId = 0;
        for (int i = 0; i < teams.Length; i++)
        {
            var driversList = drivers.Where(x => x.teamId == teams[i].id).ToList();
            for (int j = 0; j < 2; j++)
            {
                var driver = driversList[j];
                GameObject mapAIPlayerUI = null;
                if (teamID == teams[i].id && driver.id == playerID)
                {
                    playerId = driver.id;                    
                }
                else
                {                    
                    mapAIPlayerUI = Instantiate(mapAI, Vector3.zero, Quaternion.identity);
                    var color1 = new Color(teams[i].teamLogoColorList[0].r, teams[i].teamLogoColorList[0].g, teams[i].teamLogoColorList[0].b);
                    mapAIPlayerUI.GetComponent<AIMap>().ChangeImage1(color1);
                    var color2 = new Color(teams[i].teamLogoColorList[1].r, teams[i].teamLogoColorList[1].g, teams[i].teamLogoColorList[1].b);
                    mapAIPlayerUI.GetComponent<AIMap>().ChangeImage2(color2);
                    mapAIPlayerUI.AddComponent<CarMapUI>();
                    mapAIPlayerUI.GetComponent<CarMapUI>().id = driver.id;
                    mapAIPlayerUI.transform.SetParent(newObj.transform);
                    carMapUIList.Add(mapAIPlayerUI.GetComponent<CarMapUI>());
                }                
            }            
        }

        GameObject mapPlayerUI = Instantiate(mapPlayer, Vector3.zero, Quaternion.identity);
        mapPlayerUI.AddComponent<CarMapUI>();
        mapPlayerUI.GetComponent<CarMapUI>().id = playerId;
        mapPlayerUI.transform.SetParent(newObj.transform);
        carMapUIList.Add(mapPlayerUI.GetComponent<CarMapUI>());

        newObj.transform.position = mapReference[trackID].transform.position;
        newObj.transform.SetParent(mapReference[trackID].transform);
        newObj.transform.localScale = new Vector3(30.0f, 30.0f, 30.0f);
        
        Quaternion q = newObj.transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 90.0f);
        newObj.transform.rotation = q;

        ShowOnlyPlayerMap(playerID);
        ShowMainMenu();
    }

    private void ShowOnlyPlayerMap(int id)
    {
        foreach(var carMap in carMapUIList)
        {
            carMap.gameObject.SetActive(false);
        }
        carMapUIList.Where(x => x.GetComponent<CarMapUI>().id == id).First().gameObject.SetActive(true);        
    }

    private void ShowAllPlayerMap()
    {
        foreach (var carMap in carMapUIList)
        {
            carMap.gameObject.SetActive(true);
        }        
    }

    private void Timer()
    {
        if (enumRacingStatus == EnumRacingStatus.Racing)
        {
            raceTime += Time.deltaTime;
        }
    }
    private void SetPlayerMap()
    {
        try
        {
            for (int i = 0; i < cars.Count(); i++)
            {
                carMapUIList.Where(x => x.id == cars[i].id).First().transform.position = mapUIList[cars[i].idGround].transform.position;
            }
        }
        catch (Exception ex)
        {
            
        }        
    }

    private void ShowLoading()
    {        
        loadingPage.SetActive(true);
        menuPage.SetActive(false);
        pausePage.SetActive(false);
        gridScreen.SetActive(false);
    }
    private void ShowMainMenu()
    {     
        loadingPage.SetActive(false);
        menuPage.SetActive(true);
        pausePage.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneInfo.GetSceneNames[1], LoadSceneMode.Single);
    }
    public void GoToPractice()
    {     
        loadingPage.SetActive(false);
        menuPage.SetActive(false);
        pausePage.SetActive(false);
        StartRunSession();
        gameSession = GameSessionEnum.Practice;
        lapGroup.SetActive(false);

        audioData1.Play();
        audioData2.Stop();
    }
    public void StartRunSession()
    {
        startRun = true;
    }
    private void PauseEvent()
    {
        if (startRun)
        {
            if (Controls.KeyCodePAUSE())
            {
                showPauseMenu = !showPauseMenu;                
                TimeScaleManager();
            }
        }        
    }
    public void BackToRace()
    {        
        showPauseMenu = false;        
        TimeScaleManager();
    }
    private void TimeScaleManager()
    {
        pausePage.SetActive(showPauseMenu);
        Time.timeScale = showPauseMenu ? 0 : 1;
    }

    public void ResetPractice()
    {
        timerGridBoard = 0.0f;
        raceTime = 0.0f;
        enumRacingStatus = EnumRacingStatus.Begin;
        var car = cars.Where(x => x.isPlayer).First();
        car.transform.SetPositionAndRotation(gridList[0].transform.position, gridList[0].transform.rotation);
        car.GetComponent<CarPlayer>().ResetAll();
        gameSession = GameSessionEnum.Practice;
        BackToRace();
        lapGroup.SetActive(false);
    }
    public void GoToQualify()
    {
        loadingPage.SetActive(false);
        menuPage.SetActive(false);
        pausePage.SetActive(false);
        StartRunSession();
        ResetQualify();
        lapGroup.SetActive(true);
        sessionLap = 1;
        cars.Where(x => x.isPlayer).First().GetComponent<Car>().laps = 0;
        gotoQualifyButton.SetActive(false);
        resetButton.SetActive(false);
        gotoRaceButton.SetActive(true);

        audioData1.Play();
        audioData2.Stop();
    }
    public void ResetQualify()
    {
        timerGridBoard = 0.0f;
        raceTime = 0.0f;
        enumRacingStatus = EnumRacingStatus.Begin;
        var car = cars.Where(x => x.isPlayer).First();
        car.transform.SetPositionAndRotation(gridList[0].transform.position, gridList[0].transform.rotation);
        car.GetComponent<CarPlayer>().ResetAll();
        gameSession = GameSessionEnum.Qualify;
        BackToRace();
        lapGroup.SetActive(true);
        sessionLap = 1;
        cars.Where(x => x.isPlayer).First().GetComponent<Car>().laps = 0;
    }
    public void GoToGrid()
    {        
        OrderGrid();
        startRun = false;
        gameSession = GameSessionEnum.Grid;
        cars.Where(x => x.isPlayer).First().gameObject.SetActive(false);
        gridScreen.SetActive(true);
        cars.Where(x => x.isPlayer).First().GetComponent<CarPlayer>().EnableCheckeredFlag(false);

        audioData1.Stop();
        audioData2.Play();
    }
    public void GoToRace()
    {
        timerEndSession = 0.0f;
        startRun = true;
        gameSession = GameSessionEnum.Race;
        gridScreen.SetActive(false);
        sessionLap = 3;//<-
        foreach (var car in cars)
        {                        
            car.gameObject.SetActive(true);            
        }
        cars.Where(x => x.isPlayer).First().GetComponent<CarPlayer>().ResetAll();
        ResetRace();
        ShowAllPlayerMap();
        showPauseMenu = false;
        TimeScaleManager();
        menuPage.SetActive(false);
        raceGroup.SetActive(true);
        gotoQualifyButton.SetActive(false);
        resetButton.SetActive(false);
        gotoRaceButton.SetActive(false);

        audioData1.Play();
        audioData2.Stop();
    }
    public void ResetRace()
    {
        timerGridBoard = 0.0f;
        raceTime = 0.0f;
        enumRacingStatus = EnumRacingStatus.Begin;        
    }

    private void RacingManager()
    {
        if (gameSession >= GameSessionEnum.Race)
        {
            GetAllDriversPoistion();
            Timer();
            CheckIfSomeoneFinishTheRace();
        }
    }
    private void GetAllDriversPoistion()
    {
        var carsRacing = cars.Where(x => x.GetComponent<Car>().raceStatus != Car.RaceStatusEnum.retired).ToList();
        List<Car> carsListed = cars.OrderByDescending(a => a.laps).ThenByDescending(b => b.idGround).ToList();
        int pos = carsListed.FindIndex(a => a.driverId == playerID);        
        positionPlayer.text = (pos + 1).ToString();
        GetDifference(carsListed, pos);
        if (General.GetGameMode() == General.GameModesEnum.Career && !SeasonGeneral.isChallengeFinished)
        {
            GetRivalPoistion(carsListed);
        }
    }

    private void GetRivalPoistion(List<Car> carsListed)
    {
        int pos = -1;
        if (SeasonGeneral.currentId != playerID)
        {
            pos = carsListed.FindIndex(a => a.driverId == SeasonGeneral.currentId);            
        }
        else
        {
            pos = carsListed.FindIndex(a => a.driverId == SeasonGeneral.challengerId);        
        }
        posText_Rival.text = (pos + 1).ToString();     
    }

    private void GetDifference(List<Car> carsListed, int pos)
    {
        try
        {
            float diffFront = 0.0f;
            float diffBack = 0.0f;
            if (pos == 0)
            {
                frontText.text = "--:--.---";
                diffBack = -1.0f * (carsListed[pos].timeDiff[carsListed[pos + 1].idGround] - carsListed[pos + 1].timeDiff[carsListed[pos + 1].idGround]);
                backText.text = FormatTime(diffBack);
            }
            else if (pos == (carsListed.Count - 1))
            {
                diffFront = carsListed[pos].timeDiff[carsListed[pos].idGround] - carsListed[pos - 1].timeDiff[carsListed[pos].idGround];
                frontText.text = FormatTime(diffFront);
                backText.text = "--:--.---";
            }
            else
            {
                diffFront = carsListed[pos].timeDiff[carsListed[pos].idGround] - carsListed[pos - 1].timeDiff[carsListed[pos].idGround];
                diffBack = -1.0f * (carsListed[pos].timeDiff[carsListed[pos + 1].idGround] - carsListed[pos + 1].timeDiff[carsListed[pos + 1].idGround]);
                frontText.text = FormatTime(diffFront);
                backText.text = FormatTime(diffBack);
            }
        }
        catch (Exception ex)
        {

        }
    }
    private string FormatTime(float timeDiff)
    {
        TimeSpan t = TimeSpan.FromSeconds(timeDiff);
        return string.Format("{1:D2}:{2:D2}.{3:D3}",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);        
    }

    private void CheckIfSomeoneFinishTheRace()
    {
        if (cars.Where(x => x.GetComponent<Car>().laps > sessionLap).Any())
        {
            raceFinished = true;
        }
        SetFinalRaceTime();
    }
    private void SetFinalRaceTime()
    {
        if (raceFinished && gameSession != GameSessionEnum.Finish)
        {
            foreach (var car in cars)
            {
                if (car.GetComponent<Car>().laps > sessionLap && car.GetComponent<Car>().raceStatus != Car.RaceStatusEnum.finished)
                {
                    car.GetComponent<Car>().raceTime = raceTime;
                    car.GetComponent<Car>().raceStatus = Car.RaceStatusEnum.finished;

                    if (!car.GetComponent<Car>().isPlayer)
                    {
                        var carMapUI = carMapUIList.Where(x => x.GetComponent<CarMapUI>().id == car.GetComponent<Car>().driverId).First();
                        carMapUI.gameObject.SetActive(false);
                        car.GetComponent<Car>().gameObject.SetActive(false);
                    }
                    else
                    {
                        raceGroup.SetActive(false);
                        gameSession = GameSessionEnum.Finish;
                        cars.Where(x => x.isPlayer).First().GetComponent<CarPlayer>().EnableCheckeredFlag(true);                        
                    }                    
                }                
            }
        }
        if (gameSession == GameSessionEnum.Finish)
        {
            if (timerEndSession < 2.0f)
            {
                timerEndSession += 1.0f * Time.deltaTime;

                foreach (var car in cars)
                {
                    if(!car.GetComponent<Car>().isPlayer)
                        car.GetComponent<Car>().gameObject.SetActive(false);
                }
            }
            else
            {
                gameSession = GameSessionEnum.Standing;                
                OrganizeStandings();
                SetChallengeResult();
                HideAllCars();
                PrintStandings();
                standingsGroup.SetActive(true);

                var index = standings.FindIndex(a => a.isPlayer);

                if (index < 3)
                {
                    standingsBtnBack.SetActive(false);
                    standingsBtnPodium.SetActive(true);
                    SetPodiumAvatar();

                    audioData1.Stop();
                    audioData2.Play();
                }
                else
                {
                    standingsBtnBack.SetActive(true);
                    standingsBtnPodium.SetActive(false);
                }
            }
        }
    }

    private void SetPodiumAvatar()
    {
        for (int i = 0; i < 3; i++)
        {            
            Color color0 = new Color(
                teams[standings[i].teamId].clothColorList[0].r,
                teams[standings[i].teamId].clothColorList[0].g,
                teams[standings[i].teamId].clothColorList[0].b
            );
            clothPodium[i].ChangeClothColorPodium(0, color0);

            Color color1 = new Color(
                teams[standings[i].teamId].clothColorList[1].r,
                teams[standings[i].teamId].clothColorList[1].g,
                teams[standings[i].teamId].clothColorList[1].b
                );
            clothPodium[i].ChangeClothColorPodium(1, color1);

            Color color2 = new Color(
                teams[standings[i].teamId].clothColorList[2].r,
                teams[standings[i].teamId].clothColorList[2].g,
                teams[standings[i].teamId].clothColorList[2].b
                );
            clothPodium[i].ChangeClothColorPodium(2, color2);

            Color color3 = new Color(
                teams[standings[i].teamId].clothColorList[3].r,
                teams[standings[i].teamId].clothColorList[3].g,
                teams[standings[i].teamId].clothColorList[3].b
                );
            clothPodium[i].ChangeClothColorPodium(3, color3);
            avatarPodium[i].sprite = Resources.Load<Sprite>(drivers.Where(x => x.id == standings[i].driverId).First().avatarId);
        }        
    }

    private void OrganizeStandings()
    {        
        var carsFinished = cars.Where(x => x.raceStatus == Car.RaceStatusEnum.finished).OrderByDescending(x => x.laps).ThenBy(x => x.raceTime).ToList();
        foreach (var car in carsFinished)
        {
            standings.Add(car);
        }
        var carsRunning = cars.Where(x => x.raceStatus == Car.RaceStatusEnum.running).OrderByDescending(x => x.laps).ThenByDescending(x => x.idGround).ToList();
        foreach (var car in carsRunning)
        {
            standings.Add(car);
        }
        var carsRetired = cars.Where(x => x.raceStatus == Car.RaceStatusEnum.retired).OrderByDescending(x => x.laps).ThenByDescending(x => x.idGround).ToList();
        foreach (var car in carsRetired)
        {
            standings.Add(car);
        }        
    }

    private void SetChallengeResult()
    {
        //<-
        if (General.GetGameMode() == General.GameModesEnum.Career && !SeasonGeneral.isChallengeFinished)
        {
            SeasonGeneral.currentPosition = standings.FindIndex(x => x.driverId == SeasonGeneral.currentId);
            SeasonGeneral.challengerPosition = standings.FindIndex(x => x.driverId == SeasonGeneral.challengerId);
        }            
    }

    private void PrintStandings()
    {        
        for (int i = 0; i < standings.Count(); i++)
        {
            var driver = drivers.Where(x => x.id == standings[i].driverId).First();
            StandingObjList[i].SetDriverName(driver.name);
            StandingObjList[i].SetCountryFlag(driver.countryId);
            StandingObjList[i].ChangeChassi(driver.teamId);
            StandingObjList[i].ChangeCloth(driver.teamId);
            StandingObjList[i].ChangeHelmet(driver.helmetColor.r, driver.helmetColor.g, driver.helmetColor.b);
        }
    }

    private void HideAllCars()
    {
        foreach (var car in cars)
        {                        
            car.gameObject.SetActive(false);            
        }
    }

    private void RetireCheck()
    {
        if (gameSession >= GameSessionEnum.Race && gameSession != GameSessionEnum.Standing)
        {
            if (cars.Where(x => x.raceStatus == Car.RaceStatusEnum.retired && x.isPlayer).Any())
            {
                raceFinished = true;
                gameSession = GameSessionEnum.Finish;
                raceGroup.SetActive(false);
                foreach (var car in cars)
                {
                    var carMapUI = carMapUIList.Where(x => x.GetComponent<CarMapUI>().id == car.GetComponent<Car>().driverId).First();
                    carMapUI.gameObject.SetActive(false);
                }
                gameSession = GameSessionEnum.Standing;
                OrganizeStandings();
                HideAllCars();
                PrintStandings();
                standingsGroup.SetActive(true);
                standingsBtnBack.SetActive(true);
            }
        }
    }

    public void BackEndRace()
    {        
        if (General.GetGameMode() == General.GameModesEnum.SingleRace)
        {
            SceneManager.LoadScene(SceneInfo.GetSceneNames[1], LoadSceneMode.Single);
        }
        else if (General.GetGameMode() == General.GameModesEnum.Season || General.GetGameMode() == General.GameModesEnum.Career)
        {            
            SeasonGeneral.trackIDSeason++;
            SeasonGeneral.driversResult = standings.Select(x => x.driverId).ToList();
            SeasonGeneral.enumRacingStatus = EnumSeasonInitStatus.FromRace;
            SceneManager.LoadScene(SceneInfo.GetSceneNames[3], LoadSceneMode.Single);
        }
    }
    public void GotoPodium()
    {
        standingsGroup.SetActive(false);
        podium.SetActive(true);
    }

    public int SetTrackID()
    {
        if (General.GetGameMode() == General.GameModesEnum.SingleRace)
            return General.GetSetTrack;
        else if (General.GetGameMode() == General.GameModesEnum.Season || General.GetGameMode() == General.GameModesEnum.Career)
            return SeasonGeneral.trackIDSeason;
        else
            return General.GetSetTrack;
    }

    public int SetTeamID()
    {
        if (General.GetGameMode() == General.GameModesEnum.Practice || General.GetGameMode() == General.GameModesEnum.SingleRace)
            return General.GetSetTeam;
        else if (General.GetGameMode() == General.GameModesEnum.Season || General.GetGameMode() == General.GameModesEnum.Career)
            return SeasonGeneral.teamIDSeason;
        else
            return General.GetSetTeam;
    }

    public int SetPlayerID(List<DriverDTO> driversList)
    {
        if (General.GetGameMode() == General.GameModesEnum.Practice || General.GetGameMode() == General.GameModesEnum.SingleRace)
            return driversList[General.GetSetDriverID].id;
        else if (General.GetGameMode() == General.GameModesEnum.Season || General.GetGameMode() == General.GameModesEnum.Career)
            return SeasonGeneral.playerIDSeason;
        else
            return driversList[General.GetSetDriverID].id;

    }

    public int SetDifficult()
    {
        if (General.GetGameMode() == General.GameModesEnum.Practice || General.GetGameMode() == General.GameModesEnum.SingleRace)
            return General.GetSetConfig.difficultID;
        else if (General.GetGameMode() == General.GameModesEnum.Season || General.GetGameMode() == General.GameModesEnum.Career)
            return SeasonGeneral.difficultIDSeason;
        else
            return General.GetSetConfig.difficultID;
    }


}
