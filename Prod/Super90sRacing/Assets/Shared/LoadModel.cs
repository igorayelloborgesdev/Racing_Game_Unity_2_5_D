using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadModel
{
    #region Variables
    private Text[] text_Save_Label;
    private GameObject Screen_12_LoadResult;
    private Text Text_Save;
    private static string pathNameLoad = "/Saves/savegame";
    private int idDelete = 0;    
    #endregion
    #region Constructor
    public LoadModel(Text[] text_Save_Label, GameObject Screen_12_LoadResult, Text Text_Save)
    {
        this.text_Save_Label = text_Save_Label;
        this.Screen_12_LoadResult = Screen_12_LoadResult;
        this.Text_Save = Text_Save;        
    }
    public LoadModel(Text[] text_Save_Label)
    {
        this.text_Save_Label = text_Save_Label;                
    }
    #endregion
    #region Methods
    public void FillSaveGamesInfo()
    {
        try
        {
            SaveLoadGame.ReadAllSaves();
            for (int i = 0; i < General.GetSetSaveGameDTO.Length; i++)
            {
                if (General.GetSetSaveGameDTO[i] != null)
                {
                    text_Save_Label[i].text = (i + 1).ToString() + " - " + General.GetSetSaveGameDTO[i].dateTimeNow.ToString() + " - "
                    + (General.GetSetSaveGameDTO[i].idGameMode == 2 ? Language.GetLanguage[General.GetSetConfig.languageID][2] : Language.GetLanguage[General.GetSetConfig.languageID][3]).ToString();                    
                }
                else
                {
                    text_Save_Label[i].text = (i + 1).ToString() + " - " + Language.GetLanguage[General.GetSetConfig.languageID][41];
                }
            }
        }
        catch (Exception ex)
        {
            var exMsg = ex.Message;
        }
    }
    public void EnableDeleteWarning(bool show)
    {
        try
        {
            Screen_12_LoadResult.SetActive(show);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public void CheckDeleteSlot(int id, MainMenu_View mainMenu_View, Text ErrorTextFile)
    {
        try
        {            
            string loadFile = pathNameLoad + (id).ToString() + ".json";
            string filePath = Application.persistentDataPath + "/" + loadFile;
            if (SaveLoadGame.CheksIfSlotsHasSavePoint(filePath))
            {
                EnableDeleteWarning(true);
                idDelete = id;
            }
            else
            {
                EnableDeleteWarning(false);
            }
        }
        catch (Exception ex)
        {
            General.GetSetisShowErrorMessage = true;
            if (General.GetSetisShowErrorMessage)
            {
                mainMenu_View.ChangeScreenConfig(12);                
                ErrorTextFile.text = Language.GetLanguage[General.GetSetConfig.languageID][138];
                General.GetSetisShowErrorMessage = false;
            }
            Debug.Log(ex.Message);            
        }
    }
    public void ConfirmDelete(MainMenu_View mainMenu_View, Text ErrorTextFile)
    {
        try
        {            
            string deleteFile = pathNameLoad + (idDelete).ToString() + ".json";
            DataFile.DeleteDataS(deleteFile);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
            SaveLoadGame.ReadAllSaves();
            EnableDeleteWarning(false);
            FillSaveGamesInfo();
        }
        catch (Exception ex)
        {
            General.GetSetisShowErrorMessage = true;
            if (General.GetSetisShowErrorMessage)
            {
                mainMenu_View.ChangeScreenConfig(12);
                ErrorTextFile.text = Language.GetLanguage[General.GetSetConfig.languageID][138];
                mainMenu_View.CancelDelete();
                General.GetSetisShowErrorMessage = false;
            }
            Debug.Log(ex.Message);
        }
    }
    public void LoadSave(int id, MainMenu_View mainMenu_View, Text ErrorTextFile)
    {
        try
        {         
            SaveLoadGame.LoadGame(id);
        }
        catch (Exception ex)
        {
            General.GetSetisShowErrorMessage = true;
            if (General.GetSetisShowErrorMessage)
            {
                mainMenu_View.ChangeScreenConfig(12);
                ErrorTextFile.text = Language.GetLanguage[General.GetSetConfig.languageID][139];
                mainMenu_View.CancelDelete();
                General.GetSetisShowErrorMessage = false;
            }
            Debug.Log(ex.Message);
        }
    }
    #endregion


}
