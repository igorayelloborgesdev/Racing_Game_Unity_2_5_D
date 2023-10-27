using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TeamDTO
{
    public int id;
    public string name;
    public int tier;
    public int accel;
    public int speed;
    public int corner;

    public string carImage;
    public string carFrontImage;
    public string carBackImage;
    public string carImageIconStanding;
    public string carImageIconGrid;

    public ColorDTO teamLogoFontColor;
    public ColorDTO[] teamLogoColorList;
    public ColorDTO[] cockpitColorList;
    public ColorDTO[] cockpitClothColorList;
    public ColorDTO[] clothColorList;
}
