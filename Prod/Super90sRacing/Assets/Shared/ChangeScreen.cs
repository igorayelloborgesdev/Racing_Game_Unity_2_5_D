using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreen
{
    #region Variables
    /// <summary>
    /// The screen array.
    /// </summary>
    private GameObject[] screens;
    /// <summary>
    /// The out screen position.
    /// </summary>
    private Vector3 outScreenPosition;
    /// <summary>
    /// The screen position.
    /// </summary>
    private Vector3 screenPosition;
    #endregion
    #region Constructor	
    public ChangeScreen(GameObject[] screens, Vector3 outScreenPosition, Vector3 screenPosition)
    {
        this.screens = screens;
        this.outScreenPosition = outScreenPosition;
        this.screenPosition = screenPosition;
    }
    #endregion
    #region Methods	
    public void ChangeScreenSelected(int id)
    {
        try
        {
            HideAll();
            screens[id].transform.position = screenPosition;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    private void HideAll()
    {
        try
        {
            foreach (GameObject screen in screens)
            {
                screen.transform.position = outScreenPosition;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    #endregion
}