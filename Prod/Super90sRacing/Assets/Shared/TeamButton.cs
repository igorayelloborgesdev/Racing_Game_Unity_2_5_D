using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamButton : MonoBehaviour
{
    #region Serialized field
    [SerializeField]
    private Text text;
    [SerializeField]
    private Image color1;
    [SerializeField]
    private Image color2;
    [SerializeField]
    private Image color3;   
    #endregion
    #region Methods
    public void ChangeName(string name, Color color)
    {
        text.text = name;
        text.color = color;
    }
    public void ChangeName(string name)
    {
        text.text = name;        
    }
    public void ChangeColor(Color color1, Color color2, Color color3)
    {
        this.color1.color = color1;
        this.color2.color = color2;
        this.color3.color = color3;
    }
    public void ChangeTextColor(Color color)
    {
        this.text.color = color;
    }
    #endregion
}
