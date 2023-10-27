using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CarDriverSelect_Model
{
    #region Serialized field
    private Text textTeamName = null;
    private SkillGraph[] skillGraph = null;
    private Car2D car2D = null;
    private Text[] text_DriverNameSelect = null;
    private Color colorBtnSelected;    
    private Color colorBtnUnselected;
    private SkillGraph driverSkillGraph = null;
    private Image driverCountryFlag = null;
    private Image driverAvatarImage = null;
    #endregion
    #region variables
    private int selectedTeam = 0;
    private DriverDTO driverSelected = null;
    private List<DriverDTO> driversList = null;
    private RadioButton radioButtonDriverSelect;
    #endregion
    #region Constructor
    public CarDriverSelect_Model(Text textTeamName, SkillGraph[] skillGraph, Car2D car2D, Text[] text_DriverNameSelect, Color colorBtnSelected, 
        Color colorBtnUnselected, SkillGraph driverSkillGraph, Image driverCountryFlag, Image driverAvatarButton)
    {
        this.textTeamName = textTeamName;
        this.skillGraph = skillGraph;
        this.car2D = car2D;
        this.text_DriverNameSelect = text_DriverNameSelect;
        this.colorBtnSelected = colorBtnSelected;
        this.colorBtnUnselected = colorBtnUnselected;
        this.driverSkillGraph = driverSkillGraph;
        this.driverCountryFlag = driverCountryFlag;
        this.driverAvatarImage = driverAvatarButton;
    }
    #endregion
    #region Init
    public void Init()
    {
        SetTeamName(0);
        radioButtonDriverSelect = new RadioButton(text_DriverNameSelect, colorBtnSelected, colorBtnUnselected);
        radioButtonDriverSelect.ChangeButton(0);
    }
    #endregion
    #region Events
    public void SelectTeam(int id)
    {
        General.GetSetTeam = id;
        SetTeamName(id);
        SetCurrentDrivers();
        SetSkillGraph();
        SetCarImage();
        SetCarClothColor();
        SetDriverHelmetColor(0);
        SetDriverNameSelect();
        SetDriver(0);
    }
    public void SelectDriver(int id)
    {
        SetDriver(id);
    }
    
    #endregion
    #region Methods
    public void SetTeamName(int id)
    {
        selectedTeam = id;
        textTeamName.text = General.GetSetTeams[selectedTeam].name;        
    }
    private void SetSkillGraph()
    {        
        skillGraph[0].SetSkillGraph(General.GetSetTeams[selectedTeam].speed);
        skillGraph[1].SetSkillGraph(General.GetSetTeams[selectedTeam].accel);
        skillGraph[2].SetSkillGraph(General.GetSetTeams[selectedTeam].corner);
    }
    private void SetCarImage()
    {
        car2D.ChangeChassi(selectedTeam);
    }
    private void SetCarClothColor()
    {
        var newColor = new Color(
            General.GetSetTeams[selectedTeam].clothColorList[0].r,
            General.GetSetTeams[selectedTeam].clothColorList[0].g,
            General.GetSetTeams[selectedTeam].clothColorList[0].b
            );
        car2D.ChangeCloth(newColor);
    }
    private void SetDriverHelmetColor(int id)
    {
        var selectedDriver = id;        
        driverSelected = driversList[selectedDriver];
        var newColor = new Color(
            driverSelected.helmetColor.r,
            driverSelected.helmetColor.g,
            driverSelected.helmetColor.b
            );
        car2D.ChangeHelmet(newColor);
    }
    private void SetDriverNameSelect()
    {
        for (int i = 0; i < text_DriverNameSelect.Length; i++)
        {
            text_DriverNameSelect[i].text = driversList[i].name;
        }
    }
    private void SetCurrentDrivers()
    {
        driversList = General.GetSetDrivers.ToList().Where(x => x.teamId == selectedTeam).ToList();
    }
    private void SetDriver(int id)
    {
        radioButtonDriverSelect.ChangeButton(id);
        SetDriverHelmetColor(id);
        SetDriverSkillGraph();
        SetCountryFlagSprite();
        SetAvatarSprite();
        General.GetSetDriverID = id;
    }
    private void SetDriverSkillGraph()
    {
        driverSkillGraph.SetSkillGraph(driverSelected.skill);        
    }
    private void SetCountryFlagSprite()
    {
        driverCountryFlag.sprite = Resources.Load<Sprite>(General.GetSetCountries[driverSelected.countryId].countryFlag);
    }
    private void SetAvatarSprite()
    {        
        driverAvatarImage.sprite = Resources.Load<Sprite>(driverSelected.avatarId);
    }
    #endregion

}
