using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMOD
{
    private static string pathNameSave = "/Mods/";    
    private static string driversFileName = "DriversMOD.json";
    private static string teamsFileName = "TeamsMOD.json";
    #region Save MOD
    public static JSONResult SaveMod(DriverDTO[] drivers, TeamDTO[] teams)
    {
        try
        {
            DriverObjectDTO driverObjectDTO = new DriverObjectDTO();
            driverObjectDTO.driverDTO = drivers;
            var saveDriverReturn = SaveDriversOrTeams(driverObjectDTO, driversFileName);
            TeamObjectDTO teamObjectDTO = new TeamObjectDTO();
            teamObjectDTO.teamDTO = teams;
            var saveTeamsReturn = SaveDriversOrTeams(teamObjectDTO, teamsFileName);
            General.GetSetDrivers = drivers;
            General.GetSetTeams = teams;            
            return new JSONResult() { IsOK = saveDriverReturn && saveTeamsReturn, message = saveDriverReturn && saveTeamsReturn ?
                Language.GetLanguage[General.GetSetConfig.languageID][110].Replace("--TEXT--", Application.persistentDataPath + "/" + pathNameSave) : Language.GetLanguage[General.GetSetConfig.languageID][109] };
        }
        catch (Exception ex)
        {
            var exMsg = ex.Message;
            return new JSONResult() { IsOK = false, message = Language.GetLanguage[General.GetSetConfig.languageID][107] };
        }
    }

    public static bool SaveDriversOrTeams(object driversOrTeams, string fileName)
    {
        try
        {            
            string savePath = pathNameSave;            
            var saveReturnData = DataFile.SaveDataS(driversOrTeams, savePath, fileName);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool DeleteDriversOrTeams()
    {
        try
        {
            var returnDriverDataDelete = DataFile.DeleteDataS(pathNameSave + driversFileName);
            var returnTeamDataDelete = DataFile.DeleteDataS(pathNameSave + teamsFileName);
            return returnDriverDataDelete && returnTeamDataDelete;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #endregion
}
