using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioButton
{
    #region Variables	
    private Text[] textBtnRadio;
    private Color colorBtnSelected;
    private Color colorBtnUnselected;
    private Image[] imageBtnRadio;
    #endregion
    #region Constructor	
    public RadioButton(Text[] textBtnRadio, Color colorBtnSelected, Color colorBtnUnselected)
    {
        this.textBtnRadio = textBtnRadio;
        this.colorBtnSelected = colorBtnSelected;
        this.colorBtnUnselected = colorBtnUnselected;
    }
    public RadioButton(Image[] imageBtnRadio, Color colorBtnSelected, Color colorBtnUnselected)
    {
        this.imageBtnRadio = imageBtnRadio;
        this.colorBtnSelected = colorBtnSelected;
        this.colorBtnUnselected = colorBtnUnselected;
    }
    #endregion
    #region Events	
    public void ChangeButton(int id)
    {
        try
        {
            UnSelectAll();
            if (textBtnRadio != null)
            {
                textBtnRadio[id].color = colorBtnSelected;
            }
            else
            {
                imageBtnRadio[id].color = colorBtnSelected;
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    private void UnSelectAll()
    {
        try
        {
            if (textBtnRadio != null)
            {
                for (int i = 0; i < textBtnRadio.Length; i++)
                {
                    textBtnRadio[i].color = colorBtnUnselected;
                }
            }
            else
            {
                for (int i = 0; i < imageBtnRadio.Length; i++)
                {
                    imageBtnRadio[i].color = colorBtnUnselected;
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    #endregion
}