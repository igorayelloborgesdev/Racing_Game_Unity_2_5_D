using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_Model
{
    #region Variable
    /// <summary>
    /// The radio button language.
    /// </summary>
    private RadioButton radioButtonLanguage;
    /// <summary>
    /// The radio button difficult.
    /// </summary>
    private RadioButton radioButtonDifficult;
    /// <summary>
    /// The radio button control.
    /// </summary>
    private RadioButton radioButtonControl;
    /// <summary>
    /// The color button selected.
    /// </summary>
    private Color colorBtnSelected;
    /// <summary>
    /// The color button unselected.
    /// </summary>
    private Color colorBtnUnselected;
    /// <summary>
    /// The text button radio language.
    /// </summary>
    private Text[] textBtnRadioLanguage;
    /// <summary>
    /// The text button radio difficult.
    /// </summary>
    private Text[] textBtnRadioDifficult;
    /// <summary>
    /// The text button radio points.
    /// </summary>
    private Text[] textBtnRadioPoints;
    /// <summary>
    /// The text button radio sets.
    /// </summary>
    private Text[] textBtnRadioSets;
    /// <summary>
    /// The text button radio control.
    /// </summary>
    private Text[] textBtnRadioControl;
    /// <summary>
    /// The radio button main menu.
    /// </summary>
    private RadioButton radioButtonMainMenu;
    private CalibrateControl calibrateControl;
    private Button[] buttonInput;
    private Color selected;
    private Color unSelected;
    private const string pathNameConfig = "/Saves";
    private const string fileNameConfig = "/config.json";
    #endregion
    #region Constructor
    public Options_Model(Color colorBtnSelected, Color colorBtnUnselected,
        Text[] textBtnRadioLanguage, Text[] textBtnRadioDifficult, Text[] textBtnRadioControl,
        Button[] buttonInput, Color selected, Color unSelected)
    {
        this.colorBtnSelected = colorBtnSelected;
        this.colorBtnUnselected = colorBtnUnselected;
        this.textBtnRadioLanguage = textBtnRadioLanguage;
        this.textBtnRadioDifficult = textBtnRadioDifficult;
        this.textBtnRadioControl = textBtnRadioControl;
        this.buttonInput = buttonInput;
        this.selected = selected;
        this.unSelected = unSelected;
    }
    #endregion
    #region Methods	
    public void Init()
    {
        try
        {
            radioButtonLanguage = new RadioButton(textBtnRadioLanguage, colorBtnSelected, colorBtnUnselected);
            radioButtonLanguage.ChangeButton(General.GetSetConfig.languageID);
            radioButtonDifficult = new RadioButton(textBtnRadioDifficult, colorBtnSelected, colorBtnUnselected);
            radioButtonDifficult.ChangeButton(General.GetSetConfig.difficultID);
            radioButtonControl = new RadioButton(textBtnRadioControl, colorBtnSelected, colorBtnUnselected);
            radioButtonControl.ChangeButton(General.GetSetConfig.controlID);
            calibrateControl = new CalibrateControl(buttonInput, selected, unSelected);
            calibrateControl.Init();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    #endregion
    #region Events
    public void RadioButtonLanguageChangeButton(int id, LanguageText[] languageText, LanguageText mainMenuText)
    {
        try
        {
            radioButtonLanguage.ChangeButton(id);
            Language.ChangeLanguage(id, languageText);
            mainMenuText.ChangeTextDynamically(19);
            SaveConfig();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public void RadioButtonDifficultChangeButton(int id)
    {
        try
        {
            radioButtonDifficult.ChangeButton(id);
            General.GetSetConfig.difficultID = id;
            SeasonGeneral.difficultIDSeason = id;
            SaveConfig();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public void RadioButtonControlChangeButton(int id)
    {
        try
        {
            radioButtonControl.ChangeButton(id);
            General.GetSetConfig.controlID = id;
            calibrateControl.SetDirectionalDefalt();
            SaveConfig();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public void SelectInput(int id)
    {
        try
        {
            calibrateControl.SelectInput(id);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public void CalibrateControls()
    {
        try
        {
            calibrateControl.CalibrateControls();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public static void SaveConfig()
    {
        try
        {
            ConfigDTO configDTO = new ConfigDTO();
            configDTO.controlID = General.GetSetConfig.controlID;
            configDTO.difficultID = General.GetSetConfig.difficultID;
            configDTO.languageID = General.GetSetConfig.languageID;
            configDTO.controlsKeycode = new int[General.GetSetConfig.controlsKeycode.Length];
            for (int a = 0; a < configDTO.controlsKeycode.Length; a++)
            {
                configDTO.controlsKeycode[a] = (int)General.GetSetConfig.controlsKeycode[a];
            }
            DataFile.SaveDataS(configDTO, pathNameConfig, fileNameConfig);
        }
        catch (Exception ex)
        {
            var exMsg = ex.Message;
        }
    }
    #endregion
}