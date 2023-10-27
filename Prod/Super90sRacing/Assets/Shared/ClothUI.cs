using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothUI : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject cloth1;
    [SerializeField]
    private GameObject cloth2;
    [SerializeField]
    private GameObject cloth3;
    [SerializeField]
    private GameObject cloth4;
    #endregion    
    #region Methods
    public void ChangeClothColor(int id, Color color)
    {
        if (id == 0)
            cloth1.GetComponent<SpriteRenderer>().color = color;
        else if (id == 1)
            cloth2.GetComponent<SpriteRenderer>().color = color;
        else if (id == 2)
            cloth3.GetComponent<SpriteRenderer>().color = color;
        else if (id == 3)
            cloth4.GetComponent<SpriteRenderer>().color = color;
    }
    public void ChangeClothColorPodium(int id, Color color)
    {
        if (id == 0)
            cloth1.GetComponent<Image>().color = color;
        else if (id == 1)
            cloth2.GetComponent<Image>().color = color;
        else if (id == 2)
            cloth3.GetComponent<Image>().color = color;
        else if (id == 3)
            cloth4.GetComponent<Image>().color = color;
    }
    #endregion
}
