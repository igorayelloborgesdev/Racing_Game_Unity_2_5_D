using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// Player Timer
/// </summary>
public class Timer
{
    #region Variables
    /// <summary>
    /// Current time
    /// </summary>
    private float currentTime = 0.0f;
    #endregion
    #region Methods
    /// <summary>
    /// Run time
    /// </summary>
    /// <returns></returns>
    public float RunTimer()
    {
        try
        {
            currentTime += Time.deltaTime + 0.0175f;
            return currentTime;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return 0.0f;
        }
    }
    /// <summary>
    /// Reset timer
    /// </summary>
    public void ResetTimer()
    {
        try
        {
            currentTime = 0;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);         
        }
    }
    #endregion
}