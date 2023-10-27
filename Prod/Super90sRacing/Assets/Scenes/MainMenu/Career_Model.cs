using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Career_Model
{
    private Text avatarNamePlaceholderText;
    private Text avatarCodePlaceholderText;
    private Text avatarNameText;
    private Text avatarCodeText;
    private RGBSelector rgbSelectorDriverHelmetCareer = null;
    private GameObject contryFlagCareer = null;
    private Image driverCountryFlag = null;
    private Text text_Country_Code = null;
    private GameObject avatarCareerScreen = null;
    private Image driverAvatarButtonCareer = null;
    private GameObject[] Screen_Career = null;
    private SkillGraph[] skillGraph = null;
    private Text teamName = null;
    private Car2D car2DCareer = null;
    public Career_Model(Text _avatarNamePlaceholderText, Text _avatarCodePlaceholderText, Text _avatarNameText, Text _avatarCodeText, RGBSelector _rgbSelectorDriverHelmetCareer,
        GameObject _contryFlagCareer, Image _driverCountryFlag, Text _text_Country_Code, GameObject _avatarCareerScreen, Image _driverAvatarButtonCareer,
        GameObject[] _Screen_Career, SkillGraph[] _skillGraph, Text _teamName, Car2D _car2DCareer)
    {
        avatarNamePlaceholderText = _avatarNamePlaceholderText;
        avatarCodePlaceholderText = _avatarCodePlaceholderText;
        avatarNameText = _avatarNameText;
        avatarCodeText = _avatarCodeText;
        rgbSelectorDriverHelmetCareer = _rgbSelectorDriverHelmetCareer;
        contryFlagCareer = _contryFlagCareer;
        driverCountryFlag = _driverCountryFlag;
        text_Country_Code = _text_Country_Code;
        avatarCareerScreen = _avatarCareerScreen;
        driverAvatarButtonCareer = _driverAvatarButtonCareer;
        Screen_Career = _Screen_Career;
        skillGraph = _skillGraph;
        teamName = _teamName;
        car2DCareer = _car2DCareer;
    }
    #region Events
    public void Init()
    {
        avatarNamePlaceholderText.text = SeasonGeneral.name;
        avatarCodePlaceholderText.text = SeasonGeneral.code;

        avatarNameText.text = SeasonGeneral.name;
        avatarCodeText.text = SeasonGeneral.code;
        OpenCloseCountryModal(false);
        SetCountryId(0);
        OpenCloseAvatarModal(false);
        SetAvatarId(20);
        SeasonGeneral.helmetColor = new ColorDTO()
        {
            r = 1.0f,
            g = 1.0f,
            b = 1.0f
        };
        SeasonGeneral.teamIDSeason = 8;
    }
    public void ChangeDriverNameCareer()
    {
        SeasonGeneral.name = avatarNamePlaceholderText.text;        
    }
    public void ChangeDriverCodeCareer()
    {
        SeasonGeneral.code = avatarCodePlaceholderText.text;        
    }
    public void GetSliderHelmetColorValue()
    {
        SeasonGeneral.helmetColor = new ColorDTO()
        {
            r = rgbSelectorDriverHelmetCareer.GetSliderColorValue(0),
            g = rgbSelectorDriverHelmetCareer.GetSliderColorValue(1),
            b = rgbSelectorDriverHelmetCareer.GetSliderColorValue(2)
        };        
    }
    public void OpenCloseCountryModal(bool show)
    {
        contryFlagCareer.SetActive(show);
    }
    public void SetCountryId(int id)
    {
        SeasonGeneral.countryId = id;
        SetCountryFlagSprite();
        SetCountryCode();
    }
    private void SetCountryFlagSprite()
    {
        driverCountryFlag.sprite = Resources.Load<Sprite>(General.GetSetCountries[SeasonGeneral.countryId].countryFlag);
    }
    private void SetCountryCode()
    {
        text_Country_Code.text = General.GetSetCountries[SeasonGeneral.countryId].countryCode;
    }
    public void OpenCloseAvatarModal(bool show)
    {
        avatarCareerScreen.SetActive(show);
    }
    public void SetAvatarId(int id)
    {
        SeasonGeneral.avatarId = id;
        SetAvatarSprite();
    }
    private void SetAvatarSprite()
    {
        driverAvatarButtonCareer.sprite = Resources.Load<Sprite>("avatar_" + SeasonGeneral.avatarId.ToString());
    }
    public void SetCareerScreen(int id)
    {
        foreach (var screen in Screen_Career)
        {
            screen.SetActive(false);
        }
        Screen_Career[id].SetActive(true);
    }

    public void SetSkillGraphNewSpeed()
    {     
        skillGraph[0].SetSkillGraph(General.GetSetTeams[SeasonGeneral.teamIDSeason].speed);
    }
    public void SetSkillGraphNewAccel()
    {        
        skillGraph[1].SetSkillGraph(General.GetSetTeams[SeasonGeneral.teamIDSeason].accel);
    }
    public void SetSkillGraphNewCorner()
    {     
        skillGraph[2].SetSkillGraph(General.GetSetTeams[SeasonGeneral.teamIDSeason].corner);
    }
    public void SetTeamName()
    {
        teamName.text = General.GetSetTeams[SeasonGeneral.teamIDSeason].name;
    }

    public void SetTeamId(int id)
    {
        SeasonGeneral.teamIDSeason = id;
        SetTeamName();
        SetSkillGraphNewSpeed();
        SetSkillGraphNewAccel();
        SetSkillGraphNewCorner();
        SetCar2D();
    }

    private void SetCar2D()
    {
        car2DCareer.ChangeChassi(SeasonGeneral.teamIDSeason);
        var newColor = new Color(
            General.GetSetTeams[SeasonGeneral.teamIDSeason].clothColorList[0].r,
            General.GetSetTeams[SeasonGeneral.teamIDSeason].clothColorList[0].g,
            General.GetSetTeams[SeasonGeneral.teamIDSeason].clothColorList[0].b
        );
        car2DCareer.ChangeCloth(newColor);
        var newColorHelmet = new Color(
            SeasonGeneral.helmetColor.r,
            SeasonGeneral.helmetColor.g,
            SeasonGeneral.helmetColor.b
        );
        car2DCareer.ChangeHelmet(newColorHelmet);
    }

    #endregion
}
