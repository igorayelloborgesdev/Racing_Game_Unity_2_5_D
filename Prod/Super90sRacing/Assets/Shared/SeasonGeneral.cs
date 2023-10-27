using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SeasonGeneral
{
    #region Variables Save
    //Season
    public static int trackIDSeason = 0;
    public static int teamIDSeason = 0;
    public static int playerIDSeason = 0;
    public static int difficultIDSeason = 0;
    public static List<DriverSeasonGeneral> driverSeasonGeneralList = null;
    //Career
    public static string name = "Driver Name";
    public static string code = "DRI";
    public static int countryId = 0;
    public static int avatarId = 20;
    public static ColorDTO helmetColor = new ColorDTO();
    public static List<DriverDTO> careerDrivers = null;
    public static int careerYear = 0;
    public static List<HistoricSeasonDTO> historicSeasonDTO = null;
    //Career Challenge
    public static bool isPlayerWonLastChallenge = true;
    public static int challengerPoints = 0;
    public static int challengerPosition = 0;
    public static int challengerId = 0;
    public static int currentPoints = 0;
    public static int currentPosition = 0;
    public static int currentId = 0;
    public static bool isChallengeFinished = false;

    #endregion
    #region Variables provisory
    public enum EnumSeasonInitStatus
    {
        NewGame = 0,
        FromRace = 1,
        Fromload = 2
    }
    public static EnumSeasonInitStatus enumRacingStatus = EnumSeasonInitStatus.NewGame;
    public static List<int> driversResult = new List<int>();
    #endregion
    #region Methods
    public static void Reset()
    {
        trackIDSeason = 0;
        teamIDSeason = 0;
        playerIDSeason = 0;
        difficultIDSeason = 0;
        driverSeasonGeneralList = null;        
    }
    public static void SetVariables()
    {
        trackIDSeason = General.GetSetTrack;
        teamIDSeason = General.GetSetTeam;
        var driversList = General.GetSetDrivers.Where(x => x.teamId == teamIDSeason).ToList();
        playerIDSeason = driversList[General.GetSetDriverID].id;
        difficultIDSeason = General.GetSetConfig.difficultID;
    }

    public static void SetEnumRacingStatus(int id)
    {
        enumRacingStatus = (EnumSeasonInitStatus)id;
    }
    public static void ResetChallenge()
    {
        isPlayerWonLastChallenge = true;
        challengerPoints = 0;
        currentPoints = 0;
        challengerId = 0;
        currentId = 0;
        challengerPosition = 0;
        currentPosition = 0;
        isChallengeFinished = false;
    }
    public static void ResetChallengeInGame()
    {        
        challengerPoints = 0;
        currentPoints = 0;
        challengerId = 0;
        currentId = 0;
        challengerPosition = 0;
        currentPosition = 0;
        isChallengeFinished = false;
    }
    #endregion
}
