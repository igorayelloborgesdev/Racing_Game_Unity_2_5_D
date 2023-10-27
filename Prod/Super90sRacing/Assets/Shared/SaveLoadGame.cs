using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using static General;

public class SaveLoadGame
{
    private static string pathNameSave = "/Saves/";
    private static string pathNameLoad = "/Saves/savegame";
    #region Save Game
    public static JSONResult SaveGame(int id)
    {
        try
        {
            SaveGameDTO saveGameDTO = new SaveGameDTO()
            {
                id = id,
                idGameMode = (int)General.GetGameMode(),
                dateTimeNow = DateTime.Now.ToString(),
                trackIDSeason = SeasonGeneral.trackIDSeason,
                teamIDSeason = SeasonGeneral.teamIDSeason,
                playerIDSeason = SeasonGeneral.playerIDSeason,
                difficultIDSeason = SeasonGeneral.difficultIDSeason,
                driverSeasonGeneralList = SeasonGeneral.driverSeasonGeneralList,

                name = SeasonGeneral.name,
                code = SeasonGeneral.code,
                countryId = SeasonGeneral.countryId,
                avatarId = SeasonGeneral.avatarId,
                helmetColor = SeasonGeneral.helmetColor,
                careerDrivers = SeasonGeneral.careerDrivers,
                careerYear = SeasonGeneral.careerYear,
                historicSeasonDTO = SeasonGeneral.historicSeasonDTO,

                isPlayerWonLastChallenge = SeasonGeneral.isPlayerWonLastChallenge,
                challengerPoints = SeasonGeneral.challengerPoints,
                challengerPosition = SeasonGeneral.challengerPosition,
                challengerId = SeasonGeneral.challengerId,
                currentPoints = SeasonGeneral.currentPoints,
                currentPosition = SeasonGeneral.currentPosition,
                currentId = SeasonGeneral.currentId,
                isChallengeFinished = SeasonGeneral.isChallengeFinished

            };
            
            string savePath = pathNameSave;
            string fileName = "savegame" + (id).ToString() + ".json";
            return DataFile.SaveDataS(saveGameDTO, savePath, fileName);
        }
        catch (Exception ex)
        {
            var exMsg = ex.Message;
            return new JSONResult() { IsOK = false, message = Language.GetLanguage[General.GetSetConfig.languageID][107] }; ;
        }
    }
    #endregion
    #region Load Game
    public static void LoadGame(int id)
    {
        try
        {
            string loadFiles = pathNameLoad + (id).ToString() + ".json";
            SaveGameDTO saveGameDTO = DataFile.GetDataS<SaveGameDTO>(loadFiles);
            if (saveGameDTO != null)
            {
                SetSeasonVariables(saveGameDTO);
                SceneManager.LoadScene(SceneInfo.GetSceneNames[3], LoadSceneMode.Single);
            }
        }
        catch (Exception ex)
        {
            var exMsg = ex.Message;
        }
    }
    #endregion
    #region SetSave Variables
    private static void SetSeasonVariables(SaveGameDTO saveGameDTO)
    {
        SeasonGeneral.trackIDSeason = saveGameDTO.trackIDSeason;
        SeasonGeneral.teamIDSeason = saveGameDTO.teamIDSeason;
        SeasonGeneral.playerIDSeason = saveGameDTO.playerIDSeason;
        SeasonGeneral.difficultIDSeason = saveGameDTO.difficultIDSeason;        
        SeasonGeneral.driverSeasonGeneralList = saveGameDTO.driverSeasonGeneralList;
        SeasonGeneral.enumRacingStatus = SeasonGeneral.EnumSeasonInitStatus.Fromload;
        SeasonGeneral.driversResult = new List<int>();

        General.GetSetConfig.difficultID = saveGameDTO.difficultIDSeason;
        General.SetGameMode((General.GameModesEnum)saveGameDTO.idGameMode);

        if ((General.GameModesEnum)saveGameDTO.idGameMode == GameModesEnum.Career)
        {
            SeasonGeneral.name = saveGameDTO.name;
            SeasonGeneral.code = saveGameDTO.code;
            SeasonGeneral.countryId = saveGameDTO.countryId;
            SeasonGeneral.avatarId = saveGameDTO.avatarId;
            SeasonGeneral.helmetColor = saveGameDTO.helmetColor;
            SeasonGeneral.careerDrivers = saveGameDTO.careerDrivers;
            SeasonGeneral.careerYear = saveGameDTO.careerYear;
            SeasonGeneral.historicSeasonDTO = saveGameDTO.historicSeasonDTO;

            SeasonGeneral.isPlayerWonLastChallenge = saveGameDTO.isPlayerWonLastChallenge;
            SeasonGeneral.challengerPoints = saveGameDTO.challengerPoints;
            SeasonGeneral.challengerPosition = saveGameDTO.challengerPosition;
            SeasonGeneral.challengerId = saveGameDTO.challengerId;
            SeasonGeneral.currentPoints = saveGameDTO.currentPoints;
            SeasonGeneral.currentPosition = saveGameDTO.currentPosition;
            SeasonGeneral.currentId = saveGameDTO.currentId;
            SeasonGeneral.isChallengeFinished = saveGameDTO.isChallengeFinished;
        }        
    }
    #endregion
    #region read all saves
    public static void ReadAllSaves()
    {
        try
        {
            General.GetSetSaveGameDTO = new SaveGameDTO[5];
            for (int i = 0; i < 5; i++)
            {
                string loadFiles = pathNameLoad + (i).ToString() + ".json";
                SaveGameDTO saveGameDTO = DataFile.GetDataS<SaveGameDTO>(loadFiles);
                if (saveGameDTO != null)
                {
                    General.GetSetSaveGameDTO[i] = saveGameDTO;
                }
                else
                {
                    General.GetSetSaveGameDTO[i] = null;
                }

            }
        }
        catch (Exception ex)
        {
            var exMsg = ex.Message;
        }
    }
    #endregion
    #region Convert to DTO    
    #endregion
    #region Checks if is there a saved game in slot
    public static bool CheksIfSlotsHasSavePoint(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return false;
        }
    }
    #endregion
}
