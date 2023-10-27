using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelmetUI : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject helmet;    
    #endregion    
    #region Methods
    public void ChangeHelmetColor(Color color)
    {        
        helmet.GetComponent<Image>().color = color;
    }
    #endregion
}
