using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Historic : MonoBehaviour
{
    [SerializeField]
    private Text positionHistoric = null;
    [SerializeField]
    private Car2D car2D = null;
    public void SetPositionHistoric(string pos)
    {
        positionHistoric.text = pos;
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
