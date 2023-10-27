using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car2D : MonoBehaviour
{
    #region Serialized field
    [SerializeField]
    private Image chassi = null;
    [SerializeField]
    private Image cloth = null;
    [SerializeField]
    private Image helmet = null;
    #endregion
    #region Variable    
    private const string carImageName = "Car_";
    #endregion
    void Start()
    {

    }   
    #region Methods
    public void ChangeChassi(int id)
    {
        chassi.sprite = Resources.Load<Sprite>(carImageName + id.ToString());
    }
    public void ChangeCloth(Color newColor)
    {
        cloth.color = newColor;
    }
    public void ChangeHelmet(Color newColor)
    {
        helmet.color = newColor;
    }
    #endregion
}
