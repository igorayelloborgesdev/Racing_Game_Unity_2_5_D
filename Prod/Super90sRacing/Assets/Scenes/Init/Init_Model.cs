using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public class Init_Model {
	#region Variables	
	private const float presentTime = 1.0f;
	public float GetPresentTime
	{
		get{
			return presentTime;
		}
	}	
	private float period = 0.0f;
	public float GetSetPeriod
	{
		get{
			return period;
		}
		set{ 
			this.period = value;
		}
	}        
    private string configName = "/Saves/config.json";//----- Use this to load all config, save or database
    private string countryName = "countries";//----- Use this to load all config, save or database
    private string trackName = "tracks";//----- Use this to load all config, save or database
    private string teamName = "teams";//----- Use this to load all config, save or database
    private string driverName = "drivers";//----- Use this to load all config, save or database
    
    private string filePath = Application.persistentDataPath;    
    private string driversFileName = "/Mods/DriversMOD.json";
    private string teamsFileName = "/Mods/TeamsMOD.json";
    private string filePathDrivers = null;
    private string filePathTeams = null;
    #endregion
    #region Methods    
    public bool GoToNextScreen(float currentTime)
	{
        try
        {
            if (currentTime > presentTime)
                return true;
            return false;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return false;
        }        
	}
	public void Init()
	{
        try
        {
            InitFilePaths();
            Language.LoadLanguage();
            General.GetSetCountries = DataFile.GetData<CountryObjectDTO>(countryName).countryDTO;//----- Use this to load all config, save or database
            General.GetSetTracks = DataFile.GetData<TrackObjectDTO>(trackName).trackDTO;
            InitDriversAndTeams();
            string pathConfig = Application.persistentDataPath + "/" + configName;
            if (File.Exists(pathConfig))
            {
                General.GetSetConfig = DataFile.GetDataS<ConfigDTO>(configName);
                //General.GetSetshowTutorialInit = false;//----- Use this to enable auto tutorial
            }
            else
            {
                General.GetSetConfig = new ConfigDTO()
                {
                    languageID = 0,
                    difficultID = 0,
                    controlID = 0,
                    controlsKeycode = new int[] {
                        276,
                        275,
                        115,
                        97,
                        27
                    }
                };
                //General.GetSetshowTutorialInit = true;//----- Use this to enable auto tutorial
            }
            //General.GetSetTournament = DataFile.GetData<TournamentObjectDTO>(tournamentName).tournamentDTOArray;//----- Use this to load all config, save or database
            //Season.GetSetQualifiedTeams = new List<List<int>>();//----- Use this to load all config, save or database
            //for (int i = 0; i < General.GetSetTournament.Count; i++)//----- Use this to load all config, save or database
            //{
            //    Season.GetSetQualifiedTeams.Add(new List<int>());
            //}
            //General.GetSetSeasonTournamentDTO = DataFile.GetDataSResources<List<List<SeasonTournamentDTO>>>("season");//----- Use this to load all config, save or database
            General.GetSetisShowErrorMessage = false;
        }
        catch (Exception ex)
        {
            General.GetSetisShowErrorMessage = true;
            Debug.Log(ex.Message);         
        }        
    }

    private void InitFilePaths()
    {
        filePathDrivers = filePath + driversFileName;
        filePathTeams = filePath + teamsFileName;
    }

    public void InitDriversAndTeams()
    {
        General.GetSetDrivers = File.Exists(filePathDrivers) ? DataFile.GetDataS<DriverObjectDTO>(driversFileName).driverDTO : DataFile.GetData<DriverObjectDTO>(driverName).driverDTO;
        General.GetSetTeams = File.Exists(filePathTeams) ? DataFile.GetDataS<TeamObjectDTO>(teamsFileName).teamDTO : DataFile.GetData<TeamObjectDTO>(teamName).teamDTO;
    }

	#endregion
}