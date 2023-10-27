using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Car : MonoBehaviour
{
    public int id;
    public int idGround;
    public int laps;
    public Dictionary<int, float> timeDiff = new Dictionary<int, float>();
    [SerializeField]
    public RaceManager raceManager;
    public float speed = 0.0f;
    [SerializeField]
    public CarPlayerGraphics carPlayerGraphics;
    public int enumRacingStatus;

    public float speedDecrease = 0.0001f;
    public float[] speedIncreaseArray = new float[] { 0.00007f, 0.00006f, 0.00005f, 0.00004f, 0.00003f, 0.00002f };
    public float speedIncrease = 0.000001f;
    public float handleIncrease = 0.0f;
    public float handleIncreaseMax = 100.0f;
    public float handleIncreaseMin = 40.0f;
    public float speedMax = 0.03f;
    public float speedMaxInit = 0.03f;
    public float vacuumAdd = 0.0005f;
    public float speedmeterMax = 350.0f;
    public float speedCurrent = 0.03f;

    public bool isPlayer = false;

    public float qualifyLap = 0.0f;
    public bool qualifyFinished = false;

    public int driverId = 0;
    public string driverName = "";
    public int teamId = 0;

    public float raceTime = 0.0f;

    public enum RaceStatusEnum { running, finished, retired };
    public RaceStatusEnum raceStatus = RaceStatusEnum.running;
    

    public void SetEnumRacingStatus(int valueEnumRacingStatus)
    {
        enumRacingStatus = valueEnumRacingStatus;
    }
    public void SetSpeedMax(float speedNew)
    {
        speedMax -= speedNew;
    }
    public void SetHandleIncreaseMax(float handleIncrease)
    {
        handleIncreaseMax -= handleIncrease;
    }
    public void SetspeedIncreaseArray(float speedIncreaseRatio)
    {
        for (int i = 0; i < speedIncreaseArray.Length; i++)
        {
            speedIncreaseArray[i] = 0.00002f + (speedIncreaseArray[i] * speedIncreaseRatio);
        }        
    }
    
    public void ResetXPosition()
    {
        if (this.transform.rotation.x < -45.0f || this.transform.rotation.x > 45.0f)
        {
            this.transform.eulerAngles = new Vector3(0.0f, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        }        
    }

    public void RetireCheck()
    {        
        if (this.transform.position.y < -5.0f)
        {
            raceStatus = Car.RaceStatusEnum.retired;
            this.gameObject.SetActive(false);
        }         
    }    
}
