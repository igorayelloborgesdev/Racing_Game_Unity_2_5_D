using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayerGraphics : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject cockPitDetail1;
    [SerializeField]
    private GameObject cockPitDetail;
    [SerializeField]
    private GameObject gloveRight;
    [SerializeField]
    private GameObject gloveLeft;
    [SerializeField]
    private GameObject gloveRightDetail;
    [SerializeField]
    private GameObject gloveLeftDetail;
    #endregion    
    #region Methods
    public void ChangeCockpitDetail1Color(Color color)
    {
        cockPitDetail1.GetComponent<Renderer>().material.color = color;
    }
    public void ChangeCockpitDetailColor(Color color)
    {
        cockPitDetail.GetComponent<Renderer>().material.color = color;
    }
    public void ChangeGloveColor(Color color)
    {
        gloveRight.GetComponent<Renderer>().material.color = color;
        gloveLeft.GetComponent<Renderer>().material.color = color;
    }
    public void ChangeGloveDetailColor(Color color)
    {
        gloveRightDetail.GetComponent<Renderer>().material.color = color;
        gloveLeftDetail.GetComponent<Renderer>().material.color = color;
    }
    #endregion

}
