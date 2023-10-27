using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class General {
    #region variables    
    public enum GameModesEnum {
        Practice = 0,
        SingleRace = 1,
        Season = 2,
        Career = 3        
    };
    private static GameModesEnum gameMode = GameModesEnum.Practice;
	public static GameModesEnum GetGameMode()
	{		
		return gameMode;		
	}
    public static void SetGameMode(GameModesEnum value)
    {
        gameMode = value;
    }            
    private static ConfigDTO config;
    public static ConfigDTO GetSetConfig
    {
        get
        {
            return config;
        }
        set
        {
            config = value;
        }
    }    
    private static int tournamentID;
    public static int GetSetTournamentID {
        get
        {
            return tournamentID;
        }
        set
        {
            tournamentID = value;
        }
    }
    public enum GamePlayStatesEnum
    {
        NewGame = 0,
        BackToTournamentSeason = 1,
        LoadGame = 2,
        BackToSeasonCeremony = 3
    };
    private static GamePlayStatesEnum gamePlayStates = GamePlayStatesEnum.NewGame;
    public static GamePlayStatesEnum GetGamePlayStates()
    {
        return gamePlayStates;
    }
    public static void SetGamePlayStates(GamePlayStatesEnum value)
    {
        gamePlayStates = value;
    }
    private static SaveGameDTO[] saveGameDTO = new SaveGameDTO[5];
    public static SaveGameDTO[] GetSetSaveGameDTO
    {
        get
        {
            return saveGameDTO;
        }
        set
        {
            saveGameDTO = value;
        }        
    }
    private static int[] teamsIdPodium;
    public static int[] GetSetteamsIdPodium
    {
        get
        {
            return teamsIdPodium;
        }
        set
        {
            teamsIdPodium = value;
        }
    }    
    private static int tutorialStep = 0;
    public static int GetSettutorialStep
    {
        get
        {
            return tutorialStep;
        }
        set
        {
            tutorialStep = value;
        }
    }
    private static bool showTutorialInit = false;
    public static bool GetSetshowTutorialInit
    {
        get
        {
            return showTutorialInit;
        }
        set
        {
            showTutorialInit = value;
        }
    }
    private static bool isShowErrorMessage = false;
    public static bool GetSetisShowErrorMessage
    {
        get
        {
            return isShowErrorMessage;
        }
        set
        {
            isShowErrorMessage = value;
        }
    }    
    
    private static bool isSteamEnable = false;
    public static bool GetSetisSteamEnable
    {
        get
        {
            return isSteamEnable;
        }
        set
        {
            isSteamEnable = value;
        }
    }

    //------------------------------------------Super 90s Racing
    private static CountryDTO[] countries;
    public static CountryDTO[] GetSetCountries
    {
        get { return countries; }
        set { countries = value; }
    }
    private static DriverDTO[] drivers;
    public static DriverDTO[] GetSetDrivers
    {
        get { return drivers; }
        set { drivers = value; }
    }
    private static TeamDTO[] teams;
    public static TeamDTO[] GetSetTeams
    {
        get { return teams; }
        set { teams = value; }
    }
    private static TrackDTO[] tracks;
    public static TrackDTO[] GetSetTracks
    {
        get { return tracks; }
        set { tracks = value; }
    }
    private static int driverID = -1;
    public static int GetSetDriverID
    {
        get
        {
            return driverID;
        }
        set
        {
            driverID = value;
        }
    }
    private static int trackID = 0;
    public static int GetSetTrack
    {
        get
        {
            return trackID;
        }
        set
        {
            trackID = value;
        }
    }

    private static int teamID = -1;
    public static int GetSetTeam
    {
        get
        {
            return teamID;
        }
        set
        {
            teamID = value;
        }
    }
    #endregion
}