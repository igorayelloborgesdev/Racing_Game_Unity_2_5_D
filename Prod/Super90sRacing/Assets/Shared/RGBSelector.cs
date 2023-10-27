using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGBSelector : MonoBehaviour
{
    #region Variable
    [SerializeField]
    private Slider[] slider;
    [SerializeField]
    private Text textColor;
    [SerializeField]
    private Image imageColor;
    #endregion
    #region Methods    
    public float GetSliderColorValue(int id)
    {
        return slider[id].value;
    }
    public void SetColor()
    {
        if (imageColor != null)
        {
            imageColor.color = new Color(slider[0].value, slider[1].value, slider[2].value);
        }
        if (textColor != null)
        {
            textColor.color = new Color(slider[0].value, slider[1].value, slider[2].value);
        }
    }
    public void SetValueSlider(int id, float sliderValue)
    {        
        slider[id].value = sliderValue;
    }    
    #endregion
}
