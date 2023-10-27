using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandingObj : MonoBehaviour
{
    [SerializeField]
    public Text driverName = null;
    [SerializeField]
    public Image countryFlag = null;
    [SerializeField]
    public Car2D car2D = null;
    [SerializeField]
    public Text points = null;
    [SerializeField]
    public Text[] pos = null;

    public void SetDriverName(string value)
    {
        driverName.text = value;
    }    
    public void SetCountryFlag(int value)
    {
        countryFlag.sprite = Resources.Load<Sprite>(General.GetSetCountries[value].countryFlag);
    }
    public void ChangeChassi(int value)
    {
        car2D.ChangeChassi(value);
    }
    public void ChangeCloth(int value)
    {
        var newColor = new Color(
            General.GetSetTeams[value].clothColorList[0].r,
            General.GetSetTeams[value].clothColorList[0].g,
            General.GetSetTeams[value].clothColorList[0].b
            );
        car2D.ChangeCloth(newColor);
    }
    public void ChangeHelmet(float r, float g, float b)
    {
        var newColor = new Color(r, g, b);
        car2D.ChangeHelmet(newColor);
    }
}
