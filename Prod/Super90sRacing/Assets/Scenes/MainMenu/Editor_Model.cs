using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Editor_Model
{
    #region Variable
    private RadioButton radioButtonEditor;    
    private Color selected;
    private Color unSelected;
    private Text[] textBtnRadioEditor;
    private ChangeScreen changeScreen;
    private GameObject[] screens;
    private Vector3 outScreenPosition;
    private Vector3 screenPosition;    
    private TeamButton[] teamButton;        
    private Text baseChooseTeamText;    
    private Text teamText;
    private Text teamPlaceholderText;    
    private SkillGraph[] skillGraph;
    private Slider[] slider;
    private GameObject[] screensEditorTeam;
    private ChangeScreen changeScreenEditorTeam;
    private RGBSelector[] rgbSelector = null;
    private TeamButton teamButtonCustomize = null;    
    private RGBSelector rgbSelectorTeamCockpit0 = null;    
    private RGBSelector rgbSelectorTeamCockpit1 = null;    
    private RGBSelector rgbSelectorTeamCockpit2 = null;    
    private RGBSelector rgbSelectorTeamCockpit3 = null;    
    private CarPlayerGraphics carPlayerGraphics = null;    
    private RGBSelector rgbSelectorTeamCloth0 = null;    
    private RGBSelector rgbSelectorTeamCloth1 = null;    
    private RGBSelector rgbSelectorTeamCloth2 = null;    
    private RGBSelector rgbSelectorTeamCloth3 = null;    
    private ClothUI clothUI = null;    
    private Text[] textDrivers = null;    
    private InputField inputFieldName;
    private InputField inputFieldCode;    
    private int selectedTeam = 0;
    private int selectedDriver = 0;
    private DriverDTO[] drivers = null;
    private DriverDTO driverSelected = null;
    private TeamDTO[] teams = null;
    private SkillGraph skillGraphDriver;
    private Slider sliderDriver;
    private RGBSelector rgbSelectorDriverHelmet = null;
    private GameObject helmet = null;
    private GameObject[] screensEditorDriverCountry = null;
    private ChangeScreen changeScreenEditorDriverCountry;
    private Image driverCountryFlag = null;
    private Text text_Country_Code = null;
    private GameObject[] screensEditorDriverAvatar = null;
    private ChangeScreen changeScreenEditorDriverAvatar;
    private Image driverAvatarButton = null;
    private GameObject[] screensEditorDriverAvatarConfirmSuccess = null;
    private Text text_ScreenEditorMOD = null;
    private ChangeScreen changeScreenEditorSaveMOD;
    private GameObject[] editorGameObjectHideShow = null;
    private Init_Model init_Model = null;
    private TeamButton[] teamButtonSelectTeamDriver = null;
    private TeamButton[] teamButtonCareer = null;
    #endregion
    #region Constructor
    public Editor_Model(Color selected, Color unSelected, Text[] textBtnRadioEditor, GameObject[] screens, Vector3 outScreenPosition, Vector3 screenPosition, TeamButton[] teamButton, Text baseChooseTeamText, 
        Text teamText, Text teamPlaceholderText, SkillGraph[] skillGraph, Slider[] slider, GameObject[] screensEditorTeam, RGBSelector[] rgbSelector, TeamButton teamButtonCustomize,        
        RGBSelector rgbSelectorTeamCockpit0, RGBSelector rgbSelectorTeamCockpit1, RGBSelector rgbSelectorTeamCockpit2, RGBSelector rgbSelectorTeamCockpit3, CarPlayerGraphics carPlayerGraphics,
        RGBSelector rgbSelectorTeamCloth0, RGBSelector rgbSelectorTeamCloth1, RGBSelector rgbSelectorTeamCloth2, RGBSelector rgbSelectorTeamCloth3, ClothUI clothUI, 
        Text[] textDrivers, InputField inputFieldName, InputField inputFieldCode, SkillGraph skillGraphDriver, Slider sliderDriver, RGBSelector rgbSelectorDriverHelmet, GameObject helmet, 
        GameObject[] screensEditorDriverCountry, Image driverCountryFlag, Text text_Country_Code, GameObject[] screensEditorDriverAvatar, Image driverAvatarButton, GameObject[] screensEditorDriverAvatarConfirmSuccess,
        Text text_ScreenEditorMOD, GameObject[] editorGameObjectHideShow, TeamButton[] teamButtonSelectTeamDriver, TeamButton[] _teamButtonCareer)
    {
        this.selected = selected;
        this.unSelected = unSelected;
        this.textBtnRadioEditor = textBtnRadioEditor;
        this.screens = screens;
        this.outScreenPosition = outScreenPosition;
        this.screenPosition = screenPosition;
        this.teamButton = teamButton;
        this.baseChooseTeamText = baseChooseTeamText;
        this.teamText = teamText;
        this.teamPlaceholderText = teamPlaceholderText;
        this.skillGraph = skillGraph;
        this.slider = slider;
        this.screensEditorTeam = screensEditorTeam;
        this.rgbSelector = rgbSelector;
        this.teamButtonCustomize = teamButtonCustomize;        
        this.rgbSelectorTeamCockpit0 = rgbSelectorTeamCockpit0;
        this.rgbSelectorTeamCockpit1 = rgbSelectorTeamCockpit1;
        this.rgbSelectorTeamCockpit2 = rgbSelectorTeamCockpit2;
        this.rgbSelectorTeamCockpit3 = rgbSelectorTeamCockpit3;
        this.carPlayerGraphics = carPlayerGraphics;
        this.rgbSelectorTeamCloth0 = rgbSelectorTeamCloth0;
        this.rgbSelectorTeamCloth1 = rgbSelectorTeamCloth1;
        this.rgbSelectorTeamCloth2 = rgbSelectorTeamCloth2;
        this.rgbSelectorTeamCloth3 = rgbSelectorTeamCloth3;
        this.clothUI = clothUI;        
        this.textDrivers = textDrivers;
        this.inputFieldName = inputFieldName;
        this.inputFieldCode = inputFieldCode;
        this.skillGraphDriver = skillGraphDriver;
        this.sliderDriver = sliderDriver;
        this.rgbSelectorDriverHelmet = rgbSelectorDriverHelmet;
        this.helmet = helmet;
        this.screensEditorDriverCountry = screensEditorDriverCountry;
        this.driverCountryFlag = driverCountryFlag;
        this.text_Country_Code = text_Country_Code;
        this.screensEditorDriverAvatar = screensEditorDriverAvatar;
        this.driverAvatarButton = driverAvatarButton;
        this.screensEditorDriverAvatarConfirmSuccess = screensEditorDriverAvatarConfirmSuccess;
        this.text_ScreenEditorMOD = text_ScreenEditorMOD;
        this.editorGameObjectHideShow = editorGameObjectHideShow;
        this.teamButtonSelectTeamDriver = teamButtonSelectTeamDriver;
        this.teamButtonCareer = _teamButtonCareer;
    }
    #endregion
    #region Init	
    public void Init()
    {
        try
        {
            radioButtonEditor = new RadioButton(textBtnRadioEditor, selected, unSelected);
            radioButtonEditor.ChangeButton(0);
            changeScreen = new ChangeScreen(screens, outScreenPosition, screenPosition);
            SetTeamsAndDriversLocalVariable();
            SetTeamsButtonStyle();
            SetSelectedTeam(selectedTeam);
            SetTeamText();
            SetSkillGraph();
            changeScreenEditorTeam = new ChangeScreen(screensEditorTeam, outScreenPosition, screenPosition);
            changeScreenEditorTeam.ChangeScreenSelected(0);
            changeScreenEditorDriverCountry = new ChangeScreen(screensEditorDriverCountry, outScreenPosition, screenPosition);
            changeScreenEditorDriverCountry.ChangeScreenSelected(0);
            changeScreenEditorDriverAvatar = new ChangeScreen(screensEditorDriverAvatar, outScreenPosition, screenPosition);
            changeScreenEditorDriverAvatar.ChangeScreenSelected(0);
            changeScreenEditorSaveMOD = new ChangeScreen(screensEditorDriverAvatarConfirmSuccess, outScreenPosition, screenPosition);
            changeScreenEditorSaveMOD.ChangeScreenSelected(0);
            ShowHideEditorGameObject(true);
            init_Model = new Init_Model();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    #endregion
    #region Events
    public void RadioButtonEditorChangeButton(int id)
    {
        try
        {
            radioButtonEditor.ChangeButton(id);
            ChangeScreenSelected(id);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }    
    public void SelecteTeam(int id)
    {
        SetSelectedTeam(id);        
    }
    public void ChangeTeamName()
    {        
        teams[selectedTeam].name = teamText.text;
        teamPlaceholderText.text = teams[selectedTeam].name;        
        SetSelectedTeam(selectedTeam);
        SetTeamsButtonStyle(selectedTeam);
        SetTeamButtonCustomizeColor();
        SetRGBEditor();
        SetTeamsSelectTeamsDriversButtonStyle();
    }   
    public void SetSkillGraphNewSpeed()
    {
        teams[selectedTeam].speed = (int)slider[0].value;
        skillGraph[0].SetSkillGraph(teams[selectedTeam].speed);
    }
    public void SetSkillGraphNewAccel()
    {
        teams[selectedTeam].accel = (int)slider[1].value;
        skillGraph[1].SetSkillGraph(teams[selectedTeam].accel);
    }
    public void SetSkillGraphNewCorner()
    {
        teams[selectedTeam].corner = (int)slider[2].value;
        skillGraph[2].SetSkillGraph(teams[selectedTeam].corner);
    }    
    public void ChangeScreenEditorTeamEvent(int id)
    {
        changeScreenEditorTeam.ChangeScreenSelected(id);
    }
    public void GetSliderColorValue()
    {
        Color[] newColor = new Color[3];
        for (int i = 1; i < rgbSelector.Length; i++)
        {
            newColor[i - 1] = new Color(rgbSelector[i].GetSliderColorValue(0), rgbSelector[i].GetSliderColorValue(1), rgbSelector[i].GetSliderColorValue(2));
            teams[selectedTeam].teamLogoColorList[i - 1].r = rgbSelector[i].GetSliderColorValue(0);
            teams[selectedTeam].teamLogoColorList[i - 1].g = rgbSelector[i].GetSliderColorValue(1);
            teams[selectedTeam].teamLogoColorList[i - 1].b = rgbSelector[i].GetSliderColorValue(2);
        }
        teamButtonCustomize.ChangeColor(newColor[0], newColor[1], newColor[2]);
        SetTeamsButtonStyle();
    }
    public void GetSliderHelmetColorValue()
    {        
        drivers[driverSelected.id].helmetColor = new ColorDTO()
        {
            r = rgbSelectorDriverHelmet.GetSliderColorValue(0),
            g = rgbSelectorDriverHelmet.GetSliderColorValue(1),
            b = rgbSelectorDriverHelmet.GetSliderColorValue(2)
        };
        Color newColor = new Color(
            rgbSelectorDriverHelmet.GetSliderColorValue(0),
            rgbSelectorDriverHelmet.GetSliderColorValue(1),
            rgbSelectorDriverHelmet.GetSliderColorValue(2)
        );
        helmet.GetComponent<HelmetUI>().ChangeHelmetColor(newColor);
    }
    public void GetSliderColorTextValue()
    {
        teamButtonCustomize.ChangeTextColor(
            new Color(rgbSelector[0].GetSliderColorValue(0), rgbSelector[0].GetSliderColorValue(1), rgbSelector[0].GetSliderColorValue(2))
            );

        teams[selectedTeam].teamLogoFontColor.r = rgbSelector[0].GetSliderColorValue(0);
        teams[selectedTeam].teamLogoFontColor.g = rgbSelector[0].GetSliderColorValue(1);
        teams[selectedTeam].teamLogoFontColor.b = rgbSelector[0].GetSliderColorValue(2);
        SetTeamsButtonStyle();
    }
    public void GetSliderCockpitColorValue(int id)
    {        
        if(id == 0)
        {
            carPlayerGraphics.ChangeCockpitDetail1Color(
            new Color(
                rgbSelectorTeamCockpit0.GetSliderColorValue(0),
                rgbSelectorTeamCockpit0.GetSliderColorValue(1),
                rgbSelectorTeamCockpit0.GetSliderColorValue(2)
                )
            );
            SetRGBCockpitEditorTeam0();
        }
        if (id == 1)
        {
            carPlayerGraphics.ChangeCockpitDetailColor(
            new Color(
                rgbSelectorTeamCockpit1.GetSliderColorValue(0),
                rgbSelectorTeamCockpit1.GetSliderColorValue(1),
                rgbSelectorTeamCockpit1.GetSliderColorValue(2)
                )
            );
            SetRGBCockpitEditorTeam1();
        }
        if (id == 2)
        {
            carPlayerGraphics.ChangeGloveColor(
            new Color(
                rgbSelectorTeamCockpit2.GetSliderColorValue(0),
                rgbSelectorTeamCockpit2.GetSliderColorValue(1),
                rgbSelectorTeamCockpit2.GetSliderColorValue(2)
                )
            );
            SetRGBCockpitEditorTeam2();
        }
        if (id == 3)
        {
            carPlayerGraphics.ChangeGloveDetailColor(
            new Color(
                rgbSelectorTeamCockpit3.GetSliderColorValue(0),
                rgbSelectorTeamCockpit3.GetSliderColorValue(1),
                rgbSelectorTeamCockpit3.GetSliderColorValue(2)
                )
            );
            SetRGBCockpitEditorTeam3();
        }        
    }
    public void GetSliderClothColorValue(int id)
    {
        if (id == 0)
        {
            clothUI.ChangeClothColor(0, 
            new Color(
                rgbSelectorTeamCloth0.GetSliderColorValue(0),
                rgbSelectorTeamCloth0.GetSliderColorValue(1),
                rgbSelectorTeamCloth0.GetSliderColorValue(2)
                )
            );
            SetRGBClothEditorTeam0();
        }
        if (id == 1)
        {
            clothUI.ChangeClothColor(1,
            new Color(
                rgbSelectorTeamCloth1.GetSliderColorValue(0),
                rgbSelectorTeamCloth1.GetSliderColorValue(1),
                rgbSelectorTeamCloth1.GetSliderColorValue(2)
                )
            );
            SetRGBClothEditorTeam1();
        }
        if (id == 2)
        {
            clothUI.ChangeClothColor(2,
            new Color(
                rgbSelectorTeamCloth2.GetSliderColorValue(0),
                rgbSelectorTeamCloth2.GetSliderColorValue(1),
                rgbSelectorTeamCloth2.GetSliderColorValue(2)
                )
            );
            SetRGBClothEditorTeam2();
        }
        if (id == 3)
        {
            clothUI.ChangeClothColor(3,
            new Color(
                rgbSelectorTeamCloth3.GetSliderColorValue(0),
                rgbSelectorTeamCloth3.GetSliderColorValue(1),
                rgbSelectorTeamCloth3.GetSliderColorValue(2)
                )
            );
            SetRGBClothEditorTeam3();            
        }
    }
    public void SetTextDriversEvents(int id)
    {
        SetTextDriversButtonColor(id);
        SetTextDriversData(id);
        SetDriverSkill();        
        SetDriverHelmetStyleSlider();
        SetCountryFlag();
        SetAvatarSprite();
    }
    public void ChangeDriverName()
    {        
        drivers[driverSelected.id].name = inputFieldName.text;        
    }
    public void ChangeDriverCode()
    {
        drivers[driverSelected.id].code = inputFieldCode.text;
    }
    public void SetDriverSkillGraph()
    {
        drivers[driverSelected.id].skill = (int)sliderDriver.value;
        skillGraphDriver.SetSkillGraph(drivers[driverSelected.id].skill);        
    }
    public void ChangeScreenEditorDriverCountryEvent(int id)
    {
        changeScreenEditorDriverCountry.ChangeScreenSelected(id);
    }
    public void ChangeScreenEditorDriverCountryIDEvent(int id)
    {
        ChangeScreenEditorDriverCountryID(id);
    }
    public void ChangeScreenEditorDriverAvatarEvent(int id)
    {
        changeScreenEditorDriverAvatar.ChangeScreenSelected(id);
    }
    public void ChangeScreenEditorDriverAvatarIDEvent(int id)
    {
        ChangeScreenEditorDriverAvatarID(id);
    }
    public void SaveTeamsAndDriversEvent()
    {
        SaveTeamsAndDrivers();        
    }
    public void ChangeScreenEditorSaveEvent(int id)
    {
        changeScreenEditorSaveMOD.ChangeScreenSelected(id);
        if(id == 0)
            ShowHideEditorGameObject(true);
    }
    public void CallRestoreEvent()
    {
        CallRestore();        
    }
    #endregion
    #region Methods
    private void ChangeScreenSelected(int id)
    {
        try
        {
            changeScreen.ChangeScreenSelected(id);
            SetDriverSkill();            
            SetDriverHelmetStyleSlider();
            SetCountryFlag();
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    private void SetTeamsButtonStyle()
    {
        for (int i = 0; i < teamButton.Length; i++)
        {
            teamButton[i].ChangeName(teams[i].name, new Color(teams[i].teamLogoFontColor.r, teams[i].teamLogoFontColor.g, teams[i].teamLogoFontColor.b));
            teamButton[i].ChangeColor(
                new Color(teams[i].teamLogoColorList[0].r, teams[i].teamLogoColorList[0].g, teams[i].teamLogoColorList[0].b),
                new Color(teams[i].teamLogoColorList[1].r, teams[i].teamLogoColorList[1].g, teams[i].teamLogoColorList[1].b),
                new Color(teams[i].teamLogoColorList[2].r, teams[i].teamLogoColorList[2].g, teams[i].teamLogoColorList[2].b)
                );
        }
        SetTeamsSelectTeamsDriversButtonStyle();
    }
    private void SetTeamsSelectTeamsDriversButtonStyle()
    {
        for (int i = 0; i < teamButtonSelectTeamDriver.Length; i++)
        {
            teamButtonSelectTeamDriver[i].ChangeName(teams[i].name, new Color(teams[i].teamLogoFontColor.r, teams[i].teamLogoFontColor.g, teams[i].teamLogoFontColor.b));
            teamButtonSelectTeamDriver[i].ChangeColor(
                new Color(teams[i].teamLogoColorList[0].r, teams[i].teamLogoColorList[0].g, teams[i].teamLogoColorList[0].b),
                new Color(teams[i].teamLogoColorList[1].r, teams[i].teamLogoColorList[1].g, teams[i].teamLogoColorList[1].b),
                new Color(teams[i].teamLogoColorList[2].r, teams[i].teamLogoColorList[2].g, teams[i].teamLogoColorList[2].b)
                );
        }
        SetTeamsButtonStyleCareer();
    }
    private void SetTeamsButtonStyle(int id)
    {
        teamButton[id].ChangeName(teams[id].name, new Color(teams[id].teamLogoFontColor.r, teams[id].teamLogoFontColor.g, teams[id].teamLogoFontColor.b));
        teamButton[id].ChangeColor(
            new Color(teams[id].teamLogoColorList[0].r, teams[id].teamLogoColorList[0].g, teams[id].teamLogoColorList[0].b),
            new Color(teams[id].teamLogoColorList[1].r, teams[id].teamLogoColorList[1].g, teams[id].teamLogoColorList[1].b),
            new Color(teams[id].teamLogoColorList[2].r, teams[id].teamLogoColorList[2].g, teams[id].teamLogoColorList[2].b)
            );
        SetTeamsButtonStyleCareer();
    }
    private void SetTeamButtonCustomizeColor()
    {
        teamButtonCustomize.ChangeName(teams[selectedTeam].name);
        teamButtonCustomize.ChangeColor(
            new Color(teams[selectedTeam].teamLogoColorList[0].r, teams[selectedTeam].teamLogoColorList[0].g, teams[selectedTeam].teamLogoColorList[0].b),
            new Color(teams[selectedTeam].teamLogoColorList[1].r, teams[selectedTeam].teamLogoColorList[1].g, teams[selectedTeam].teamLogoColorList[1].b),
            new Color(teams[selectedTeam].teamLogoColorList[2].r, teams[selectedTeam].teamLogoColorList[2].g, teams[selectedTeam].teamLogoColorList[2].b)
            );
        teamButtonCustomize.ChangeTextColor(
            new Color(teams[selectedTeam].teamLogoFontColor.r, teams[selectedTeam].teamLogoFontColor.g, teams[selectedTeam].teamLogoFontColor.b)
            );
        SetTeamsButtonStyleCareer();
    }
    private void SetSelectedTeam(int id)
    {        
        selectedTeam = id;
        baseChooseTeamText.text = teams[selectedTeam].name;
        teamPlaceholderText.text = teams[selectedTeam].name;
        SetSkillGraph();
        SetSkillGraphReset();
        SetTeamButtonCustomizeColor();
        SetRGBEditor();        
        SetCockpitColorValue();
        SetClothColorValue();
        SetTextDriversEvents(0);
        SetCountryFlag();
    }
    private void SetTeamText()
    {
        teamText.text = teams[selectedTeam].name;
        teamPlaceholderText.text = teams[selectedTeam].name;
        SetTeamButtonCustomizeColor();
    }
    private void SetSkillGraph()
    {
        skillGraph[0].SetSkillGraph(teams[selectedTeam].speed);
        skillGraph[1].SetSkillGraph(teams[selectedTeam].accel);
        skillGraph[2].SetSkillGraph(teams[selectedTeam].corner);
    }
    private void SetSkillGraphReset()
    {
        slider[0].value = teams[selectedTeam].speed;
        slider[1].value = teams[selectedTeam].accel;
        slider[2].value = teams[selectedTeam].corner;
    }
    private void SetRGBEditor()
    {        
        float[] fontColorNew = new float[]
        {
            teams[selectedTeam].teamLogoFontColor.r,
            teams[selectedTeam].teamLogoFontColor.g,
            teams[selectedTeam].teamLogoFontColor.b
        };

        rgbSelector[0].SetValueSlider(0, fontColorNew[0]);
        rgbSelector[0].SetValueSlider(1, fontColorNew[1]);
        rgbSelector[0].SetValueSlider(2, fontColorNew[2]);

        float[,] logoColorNew = new float[,]
        {
            {
                teams[selectedTeam].teamLogoColorList[0].r,
                teams[selectedTeam].teamLogoColorList[0].g,
                teams[selectedTeam].teamLogoColorList[0].b
            },
            {
                teams[selectedTeam].teamLogoColorList[1].r,
                teams[selectedTeam].teamLogoColorList[1].g,
                teams[selectedTeam].teamLogoColorList[1].b
            },
            {
                teams[selectedTeam].teamLogoColorList[2].r,
                teams[selectedTeam].teamLogoColorList[2].g,
                teams[selectedTeam].teamLogoColorList[2].b
            }
        };

        for (int j = 1; j < rgbSelector.Length; j++)
        {            
            rgbSelector[j].SetValueSlider(0, logoColorNew[j - 1, 0]);
            rgbSelector[j].SetValueSlider(1, logoColorNew[j - 1, 1]);
            rgbSelector[j].SetValueSlider(2, logoColorNew[j - 1, 2]);
        }
    }
    private void SetRGBCockpitEditor0()
    {        
        float[] cockpitColorList1 = new float[]
        {
            teams[selectedTeam].cockpitColorList[0].r,
            teams[selectedTeam].cockpitColorList[0].g,
            teams[selectedTeam].cockpitColorList[0].b
        };

        rgbSelectorTeamCockpit0.SetValueSlider(0, cockpitColorList1[0]);
        rgbSelectorTeamCockpit0.SetValueSlider(1, cockpitColorList1[1]);
        rgbSelectorTeamCockpit0.SetValueSlider(2, cockpitColorList1[2]);

        teams[selectedTeam].cockpitColorList[0].r = rgbSelectorTeamCockpit0.GetSliderColorValue(0);
        teams[selectedTeam].cockpitColorList[0].g = rgbSelectorTeamCockpit0.GetSliderColorValue(1);
        teams[selectedTeam].cockpitColorList[0].b = rgbSelectorTeamCockpit0.GetSliderColorValue(2);
    }
    private void SetRGBCockpitEditor1()
    {        
        float[] cockpitColorList2 = new float[]
        {
            teams[selectedTeam].cockpitColorList[1].r,
            teams[selectedTeam].cockpitColorList[1].g,
            teams[selectedTeam].cockpitColorList[1].b
        };

        rgbSelectorTeamCockpit1.SetValueSlider(0, cockpitColorList2[0]);
        rgbSelectorTeamCockpit1.SetValueSlider(1, cockpitColorList2[1]);
        rgbSelectorTeamCockpit1.SetValueSlider(2, cockpitColorList2[2]);

        teams[selectedTeam].cockpitColorList[1].r = rgbSelectorTeamCockpit1.GetSliderColorValue(0);
        teams[selectedTeam].cockpitColorList[1].g = rgbSelectorTeamCockpit1.GetSliderColorValue(1);
        teams[selectedTeam].cockpitColorList[1].b = rgbSelectorTeamCockpit1.GetSliderColorValue(2);
    }
    private void SetRGBCockpitEditor2()
    {        
        float[] cockpitClothColorList1 = new float[]
        {
            teams[selectedTeam].cockpitClothColorList[0].r,
            teams[selectedTeam].cockpitClothColorList[0].g,
            teams[selectedTeam].cockpitClothColorList[0].b
        };

        rgbSelectorTeamCockpit2.SetValueSlider(0, cockpitClothColorList1[0]);
        rgbSelectorTeamCockpit2.SetValueSlider(1, cockpitClothColorList1[1]);
        rgbSelectorTeamCockpit2.SetValueSlider(2, cockpitClothColorList1[2]);

        teams[selectedTeam].cockpitClothColorList[0].r = rgbSelectorTeamCockpit2.GetSliderColorValue(0);
        teams[selectedTeam].cockpitClothColorList[0].g = rgbSelectorTeamCockpit2.GetSliderColorValue(1);
        teams[selectedTeam].cockpitClothColorList[0].b = rgbSelectorTeamCockpit2.GetSliderColorValue(2);
    }
    private void SetRGBCockpitEditor3()
    {        
        float[] cockpitClothColorList2 = new float[]
        {
            teams[selectedTeam].cockpitClothColorList[1].r,
            teams[selectedTeam].cockpitClothColorList[1].g,
            teams[selectedTeam].cockpitClothColorList[1].b
        };

        rgbSelectorTeamCockpit3.SetValueSlider(0, cockpitClothColorList2[0]);
        rgbSelectorTeamCockpit3.SetValueSlider(1, cockpitClothColorList2[1]);
        rgbSelectorTeamCockpit3.SetValueSlider(2, cockpitClothColorList2[2]);

        teams[selectedTeam].cockpitClothColorList[1].r = rgbSelectorTeamCockpit3.GetSliderColorValue(0);
        teams[selectedTeam].cockpitClothColorList[1].g = rgbSelectorTeamCockpit3.GetSliderColorValue(1);
        teams[selectedTeam].cockpitClothColorList[1].b = rgbSelectorTeamCockpit3.GetSliderColorValue(2);
    }    
    private void SetRGBCockpitEditorTeam0()
    {        
        teams[selectedTeam].cockpitColorList[0].r = rgbSelectorTeamCockpit0.GetSliderColorValue(0);
        teams[selectedTeam].cockpitColorList[0].g = rgbSelectorTeamCockpit0.GetSliderColorValue(1);
        teams[selectedTeam].cockpitColorList[0].b = rgbSelectorTeamCockpit0.GetSliderColorValue(2);
    }
    private void SetRGBCockpitEditorTeam1()
    {        
        teams[selectedTeam].cockpitColorList[1].r = rgbSelectorTeamCockpit1.GetSliderColorValue(0);
        teams[selectedTeam].cockpitColorList[1].g = rgbSelectorTeamCockpit1.GetSliderColorValue(1);
        teams[selectedTeam].cockpitColorList[1].b = rgbSelectorTeamCockpit1.GetSliderColorValue(2);
    }
    private void SetRGBCockpitEditorTeam2()
    {        
        teams[selectedTeam].cockpitClothColorList[0].r = rgbSelectorTeamCockpit2.GetSliderColorValue(0);
        teams[selectedTeam].cockpitClothColorList[0].g = rgbSelectorTeamCockpit2.GetSliderColorValue(1);
        teams[selectedTeam].cockpitClothColorList[0].b = rgbSelectorTeamCockpit2.GetSliderColorValue(2);
    }
    private void SetRGBCockpitEditorTeam3()
    {        
        teams[selectedTeam].cockpitClothColorList[1].r = rgbSelectorTeamCockpit3.GetSliderColorValue(0);
        teams[selectedTeam].cockpitClothColorList[1].g = rgbSelectorTeamCockpit3.GetSliderColorValue(1);
        teams[selectedTeam].cockpitClothColorList[1].b = rgbSelectorTeamCockpit3.GetSliderColorValue(2);
    }    
    private void SetCockpitColorValue()
    {
        SetRGBCockpitEditor0();
        SetRGBCockpitEditor1();
        SetRGBCockpitEditor2();
        SetRGBCockpitEditor3();

        Color color1 = new Color(
            teams[selectedTeam].cockpitColorList[0].r,
            teams[selectedTeam].cockpitColorList[0].g,
            teams[selectedTeam].cockpitColorList[0].b
            );        

        carPlayerGraphics.ChangeCockpitDetail1Color(color1);

        Color color2 = new Color(
            teams[selectedTeam].cockpitColorList[1].r,
            teams[selectedTeam].cockpitColorList[1].g,
            teams[selectedTeam].cockpitColorList[1].b
            );

        carPlayerGraphics.ChangeCockpitDetailColor(color2);

        Color color3 = new Color(
            teams[selectedTeam].cockpitClothColorList[0].r,
            teams[selectedTeam].cockpitClothColorList[0].g,
            teams[selectedTeam].cockpitClothColorList[0].b
            );

        carPlayerGraphics.ChangeGloveColor(color3);

        Color color4 = new Color(
            teams[selectedTeam].cockpitClothColorList[1].r,
            teams[selectedTeam].cockpitClothColorList[1].g,
            teams[selectedTeam].cockpitClothColorList[1].b
            );

        carPlayerGraphics.ChangeGloveDetailColor(color4);        
    }
    private void SetClothColorValue()
    {
        SetRGBClothEditor0();
        SetRGBClothEditor1();
        SetRGBClothEditor2();
        SetRGBClothEditor3();

        Color color0 = new Color(
            teams[selectedTeam].clothColorList[0].r,
            teams[selectedTeam].clothColorList[0].g,
            teams[selectedTeam].clothColorList[0].b
            );
        clothUI.ChangeClothColor(0, color0);
        
        Color color1 = new Color(
            teams[selectedTeam].clothColorList[1].r,
            teams[selectedTeam].clothColorList[1].g,
            teams[selectedTeam].clothColorList[1].b
            );
        clothUI.ChangeClothColor(1, color1);
        
        Color color2 = new Color(
            teams[selectedTeam].clothColorList[2].r,
            teams[selectedTeam].clothColorList[2].g,
            teams[selectedTeam].clothColorList[2].b
            );
        clothUI.ChangeClothColor(2, color2);        

        Color color3 = new Color(
            teams[selectedTeam].clothColorList[3].r,
            teams[selectedTeam].clothColorList[3].g,
            teams[selectedTeam].clothColorList[3].b
            );
        clothUI.ChangeClothColor(3, color3);
        
    }
    private void SetRGBClothEditor0()
    {
        float[] clothColorList0 = new float[]
        {
            teams[selectedTeam].clothColorList[0].r,
            teams[selectedTeam].clothColorList[0].g,
            teams[selectedTeam].clothColorList[0].b
        };

        rgbSelectorTeamCloth0.SetValueSlider(0, clothColorList0[0]);
        rgbSelectorTeamCloth0.SetValueSlider(1, clothColorList0[1]);
        rgbSelectorTeamCloth0.SetValueSlider(2, clothColorList0[2]);

        teams[selectedTeam].clothColorList[0].r = rgbSelectorTeamCloth0.GetSliderColorValue(0);
        teams[selectedTeam].clothColorList[0].g = rgbSelectorTeamCloth0.GetSliderColorValue(1);
        teams[selectedTeam].clothColorList[0].b = rgbSelectorTeamCloth0.GetSliderColorValue(2);
    }
    private void SetRGBClothEditor1()
    {
        float[] clothColorList1 = new float[]
        {
            teams[selectedTeam].clothColorList[1].r,
            teams[selectedTeam].clothColorList[1].g,
            teams[selectedTeam].clothColorList[1].b
        };

        rgbSelectorTeamCloth1.SetValueSlider(0, clothColorList1[0]);
        rgbSelectorTeamCloth1.SetValueSlider(1, clothColorList1[1]);
        rgbSelectorTeamCloth1.SetValueSlider(2, clothColorList1[2]);

        teams[selectedTeam].clothColorList[1].r = rgbSelectorTeamCloth1.GetSliderColorValue(0);
        teams[selectedTeam].clothColorList[1].g = rgbSelectorTeamCloth1.GetSliderColorValue(1);
        teams[selectedTeam].clothColorList[1].b = rgbSelectorTeamCloth1.GetSliderColorValue(2);
    }
    private void SetRGBClothEditor2()
    {
        float[] clothColorList2 = new float[]
        {
            teams[selectedTeam].clothColorList[2].r,
            teams[selectedTeam].clothColorList[2].g,
            teams[selectedTeam].clothColorList[2].b
        };

        rgbSelectorTeamCloth2.SetValueSlider(0, clothColorList2[0]);
        rgbSelectorTeamCloth2.SetValueSlider(1, clothColorList2[1]);
        rgbSelectorTeamCloth2.SetValueSlider(2, clothColorList2[2]);

        teams[selectedTeam].clothColorList[2].r = rgbSelectorTeamCloth2.GetSliderColorValue(0);
        teams[selectedTeam].clothColorList[2].g = rgbSelectorTeamCloth2.GetSliderColorValue(1);
        teams[selectedTeam].clothColorList[2].b = rgbSelectorTeamCloth2.GetSliderColorValue(2);
    }
    private void SetRGBClothEditor3()
    {
        float[] clothColorList3 = new float[]
        {
            teams[selectedTeam].clothColorList[3].r,
            teams[selectedTeam].clothColorList[3].g,
            teams[selectedTeam].clothColorList[3].b
        };

        rgbSelectorTeamCloth3.SetValueSlider(0, clothColorList3[0]);
        rgbSelectorTeamCloth3.SetValueSlider(1, clothColorList3[1]);
        rgbSelectorTeamCloth3.SetValueSlider(2, clothColorList3[2]);

        teams[selectedTeam].clothColorList[3].r = rgbSelectorTeamCloth3.GetSliderColorValue(0);
        teams[selectedTeam].clothColorList[3].g = rgbSelectorTeamCloth3.GetSliderColorValue(1);
        teams[selectedTeam].clothColorList[3].b = rgbSelectorTeamCloth3.GetSliderColorValue(2);
    }
    private void SetRGBClothEditorTeam0()
    {
        teams[selectedTeam].clothColorList[0].r = rgbSelectorTeamCloth0.GetSliderColorValue(0);
        teams[selectedTeam].clothColorList[0].g = rgbSelectorTeamCloth0.GetSliderColorValue(1);
        teams[selectedTeam].clothColorList[0].b = rgbSelectorTeamCloth0.GetSliderColorValue(2);
    }
    private void SetRGBClothEditorTeam1()
    {
        teams[selectedTeam].clothColorList[1].r = rgbSelectorTeamCloth1.GetSliderColorValue(0);
        teams[selectedTeam].clothColorList[1].g = rgbSelectorTeamCloth1.GetSliderColorValue(1);
        teams[selectedTeam].clothColorList[1].b = rgbSelectorTeamCloth1.GetSliderColorValue(2);
    }
    private void SetRGBClothEditorTeam2()
    {
        teams[selectedTeam].clothColorList[2].r = rgbSelectorTeamCloth2.GetSliderColorValue(0);
        teams[selectedTeam].clothColorList[2].g = rgbSelectorTeamCloth2.GetSliderColorValue(1);
        teams[selectedTeam].clothColorList[2].b = rgbSelectorTeamCloth2.GetSliderColorValue(2);
    }
    private void SetRGBClothEditorTeam3()
    {
        teams[selectedTeam].clothColorList[3].r = rgbSelectorTeamCloth3.GetSliderColorValue(0);
        teams[selectedTeam].clothColorList[3].g = rgbSelectorTeamCloth3.GetSliderColorValue(1);
        teams[selectedTeam].clothColorList[3].b = rgbSelectorTeamCloth3.GetSliderColorValue(2);
    }
    private void SetTextDriversButtonColor(int id)
    {
        for (int i = 0; i < textDrivers.Length; i++)
            textDrivers[i].color = unSelected;
        textDrivers[id].color = selected;
    }    
    private void SetTextDriversData(int id)
    {
        selectedDriver = id;
        List<DriverDTO> driversList = drivers.Where(x => x.teamId == teams[selectedTeam].id).ToList();        
        driverSelected = driversList[selectedDriver];
        SetTextDriversName(driversList);        
    }
    private void SetTextDriversName(List<DriverDTO> driversList)
    {
        for(int i = 0; i < driversList.Count(); i++)
            textDrivers[i].text = driversList[i].name;
        SetTextDriverNameAndCode(driversList[selectedDriver]);
    }
    private void SetTextDriverNameAndCode(DriverDTO driverDTO)
    {      
        inputFieldName.text = driverDTO.name;
        inputFieldCode.text = driverDTO.code;
    }
    private void SetDriverSkill()
    {
        sliderDriver.value = drivers[driverSelected.id].skill;
        skillGraphDriver.SetSkillGraph(drivers[driverSelected.id].skill);
    }
    private void SetDriverHelmetStyleSlider()
    {
       
        float[] clothColorList2 = new float[]
        {
            drivers[driverSelected.id].helmetColor.r,
            drivers[driverSelected.id].helmetColor.g,
            drivers[driverSelected.id].helmetColor.b
        };
        rgbSelectorDriverHelmet.SetValueSlider(0, clothColorList2[0]);
        rgbSelectorDriverHelmet.SetValueSlider(1, clothColorList2[1]);
        rgbSelectorDriverHelmet.SetValueSlider(2, clothColorList2[2]);        
        Color newColor = new Color(
            drivers[driverSelected.id].helmetColor.r,
            drivers[driverSelected.id].helmetColor.g,
            drivers[driverSelected.id].helmetColor.b
        );
        helmet.GetComponent<HelmetUI>().ChangeHelmetColor(newColor);        
    }
    private void ChangeScreenEditorDriverCountryID(int id)
    {
        drivers[driverSelected.id].countryId = id;
        SetCountryFlag();
    }
    private void SetCountryFlag()
    {
        SetCountryFlagSprite();
        SetCountryCode();
    }
    private void SetCountryFlagSprite()
    {
        driverCountryFlag.sprite = Resources.Load<Sprite>(General.GetSetCountries[drivers[driverSelected.id].countryId].countryFlag);
    }
    private void SetCountryCode()
    {
        text_Country_Code.text = General.GetSetCountries[drivers[driverSelected.id].countryId].countryCode;
    }
    private void ChangeScreenEditorDriverAvatarID(int id)
    {        
        drivers[driverSelected.id].avatarId = "avatar_" +  id.ToString();
        SetAvatarSprite();
    }    
    private void SetAvatarSprite()
    {
        driverAvatarButton.sprite = Resources.Load<Sprite>(drivers[driverSelected.id].avatarId);
    }
    private void SaveTeamsAndDrivers()
    {        
        var SaveMODResponse = SaveMOD.SaveMod(drivers, teams);
        text_ScreenEditorMOD.text = SaveMODResponse.message;
        changeScreenEditorSaveMOD.ChangeScreenSelected(1);
        ShowHideEditorGameObject(false);
    }
    private void ShowHideEditorGameObject(bool show)
    {
        foreach (var ganeObject in editorGameObjectHideShow)        
            ganeObject.SetActive(show);        
    }
    private void CallRestore()
    {        
        changeScreenEditorSaveMOD.ChangeScreenSelected(1);
        ShowHideEditorGameObject(false);
        text_ScreenEditorMOD.text = DeleteDataFile() ? Language.GetLanguage[General.GetSetConfig.languageID][111] : Language.GetLanguage[General.GetSetConfig.languageID][112];
        init_Model.InitDriversAndTeams();
        SetTeamsAndDriversLocalVariable();
        SetTeamsAndDriversLocalVariable();
        SetTeamsButtonStyle();
        SetSelectedTeam(selectedTeam);
        SetTeamText();
        SetSkillGraph();
    }
    private bool DeleteDataFile()
    {
        return SaveMOD.DeleteDriversOrTeams();
    }
    private void SetTeamsAndDriversLocalVariable()
    {
        drivers = General.GetSetDrivers;
        teams = General.GetSetTeams;
    }    
    private void SetTeamsButtonStyleCareer()
    {
        for (int i = 0; i < teamButtonCareer.Length; i++)
        {
            teamButtonCareer[i].ChangeName(teams[8 + i].name, new Color(teams[8 + i].teamLogoFontColor.r, teams[8 + i].teamLogoFontColor.g, teams[8 + i].teamLogoFontColor.b));
            teamButtonCareer[i].ChangeColor(
                new Color(teams[8 + i].teamLogoColorList[0].r, teams[8 + i].teamLogoColorList[0].g, teams[8 + i].teamLogoColorList[0].b),
                new Color(teams[8 + i].teamLogoColorList[1].r, teams[8 + i].teamLogoColorList[1].g, teams[8 + i].teamLogoColorList[1].b),
                new Color(teams[8 + i].teamLogoColorList[2].r, teams[8 + i].teamLogoColorList[2].g, teams[8 + i].teamLogoColorList[2].b)
                );
        }        
    }
    #endregion
}
