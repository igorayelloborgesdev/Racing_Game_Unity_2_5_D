using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CarPlayer : Car
{
    Vector3 posContact = Vector3.zero;
    float speedBrake = 0.0003f;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    float timer = 0.0f;
    int activateTimerCarGrassState = 0;
    bool isGuardRailColliderTimerStart = false;
    float timerGuardRail = 0.0f;
    int isGuardrailLeft = 0;
    private float steeringIndex = 0.0f;
    private float steeringIndexInc = 0.0f;

    public float[] gears = new float[6];
    public float[] gearsIncreaseProv = new float[6];
    public float[] gearsIncrease = new float[6];
    public int currentGear = 0;
    GameObject[] wayPoints;
    public List<WayPointObject> wayPointObjectList;
    public int wayPointLookAtId = 0;
    public int wayPointLookAtIndex = 0;
    
    private enum TurningStateEnum { init, turning, finishTurning };
    private TurningStateEnum turningState = TurningStateEnum.init;
    private float timerTurning = 0.0f;
    private int lastTrackID = 0;
    private float timerChangeTyreAnimation = 0.0f;
    private bool[] checkPointArray = { false, false, false };
    private float timeLap = 0.0f;
    private float timeLastLap = 0.0f;
    public float timeBestLap = 0.0f;
    private enum VacuumStateEnum { init, inside, outside };
    private VacuumStateEnum vacuumState = VacuumStateEnum.init;
    
    private float guardrailOffSet = 0.001f;
    [SerializeField]
    public Tyre[] tyres;
    [SerializeField]
    private GameObject steering;
    [SerializeField]
    private GameObject leftTyre;
    [SerializeField]
    private GameObject leftTyreHighlight;
    [SerializeField]
    private GameObject rightTyre;
    [SerializeField]
    private GameObject rightTyreHighlight;
    [SerializeField]
    private Material[] tyreMaterial;
    [SerializeField]
    private Material[] tyreMaterialHighlight;
    [SerializeField]
    private GameObject gridLightBoard;
    [SerializeField]
    private GameObject[] gridLightRed;
    [SerializeField]
    private GameObject[] gridLightGreen;
    [SerializeField]
    private Material gridLightRedMaterial;
    [SerializeField]
    private Material gridLightGreenMaterial;
    [SerializeField]
    private Material gridLightRedMaterialReset;
    [SerializeField]
    private Material gridLightGreenMaterialReset;
    [SerializeField]
    public Text speedMeter;
    [SerializeField]
    public Text chronometer;
    [SerializeField]
    public Text chronometerLastLap;
    [SerializeField]
    public Text gear;
    [SerializeField]
    public GameObject RPMPointer;
    [SerializeField]
    public Text chronometerBestLap;
    [SerializeField]
    public PlayerVacuum playerVacuum;
    [SerializeField]
    public GameObject checkeredFlag;
    
    void Start()
    {
        GetWayPoints();
        SetToPoint();
        InitGears();
        InitTyre();        

        chronometer.text = "00:00.000";
        chronometerLastLap.text = "00:00.000";
        chronometerBestLap.text = "00:00.000";
    }
    void Update()
    {
        if (!raceManager.GetShowPauseMenu)
        {
            ChangeGridBoardLights();
            if (enumRacingStatus == 2 || enumRacingStatus == 3)
            {
                Control();
                Timer();
            }
            SetToPoint();
            //CheckInverseTrack();
            GetCheckPoint();
            GetIdGround();
            CollisionHit();
            CheckVacuum();
            GetGears();
            SetGearHUD();
            SetRPMHUD();
            Speedmeter();
            RetireCheck();
            RespawPlayer();
        }        
    }


    public void ResetAll()
    {
        velocity = Vector3.zero;
        timer = 0.0f;
        activateTimerCarGrassState = 0;
        isGuardRailColliderTimerStart = false;
        timerGuardRail = 0.0f;
        isGuardrailLeft = 0;
        steeringIndex = 0.0f;
        steeringIndexInc = 0.0f;
        currentGear = 0;
        wayPointLookAtId = 0;
        wayPointLookAtIndex = 0;
        turningState = TurningStateEnum.init;
        SteeringReset();
        timerTurning = 0.0f;
        lastTrackID = 0;
        timerChangeTyreAnimation = 0.0f;
        for(int i = 0; i < checkPointArray.Length; i++)
            checkPointArray[i] = false;        
        timeLap = 0.0f;
        timeLastLap = 0.0f;
        timeBestLap = 0.0f;
        FormatLap(timeLap, chronometer);
        FormatLap(timeLastLap, chronometerLastLap);
        FormatLap(timeBestLap, chronometerBestLap);
        vacuumState = VacuumStateEnum.init;
        enumRacingStatus = 0;
        gridLightBoard.SetActive(true);
        foreach (GameObject gridLightRedGameObject in gridLightRed)
        {
            gridLightRedGameObject.gameObject.GetComponent<Renderer>().material = gridLightRedMaterialReset;
        }
        foreach (GameObject gridLightGreenGameObject in gridLightGreen)
        {
            gridLightGreenGameObject.gameObject.GetComponent<Renderer>().material = gridLightGreenMaterialReset;
        }
        speed = 0.0f;
        steeringIndexInc = 0.0f;
        steering.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        leftTyre.gameObject.GetComponent<Renderer>().material = tyreMaterial[1];
        leftTyreHighlight.gameObject.GetComponent<Renderer>().material = tyreMaterialHighlight[1];
        rightTyre.gameObject.GetComponent<Renderer>().material = tyreMaterial[1];
        rightTyreHighlight.gameObject.GetComponent<Renderer>().material = tyreMaterialHighlight[1];
        laps = 0;
    }


    private void SetToPoint()
    {        
        if (tyres[0].GetComponentsInChildren<Tyre>()[0].idGround < wayPointLookAtId + 2 && tyres[0].GetComponentsInChildren<Tyre>()[0].idGround > wayPointLookAtId - 2)
        {            
            wayPointLookAtIndex++;
            if (wayPointLookAtIndex >= wayPointObjectList.Count)
                wayPointLookAtIndex = 0;
            wayPointLookAtId = wayPointObjectList[wayPointLookAtIndex].GetSetId;
        }
    }
    private void InitTyre()
    {
        leftTyreHighlight.SetActive(false);
        rightTyreHighlight.SetActive(false);
        leftTyre.gameObject.GetComponent<Renderer>().material = tyreMaterial[1];
        leftTyreHighlight.gameObject.GetComponent<Renderer>().material = tyreMaterialHighlight[1];
        rightTyre.gameObject.GetComponent<Renderer>().material = tyreMaterial[1];
        rightTyreHighlight.gameObject.GetComponent<Renderer>().material = tyreMaterialHighlight[1];
    }
    
    private void ChangeGridBoardLights()
    {
        try
        {            
            if (enumRacingStatus == 1)
            {
                foreach (GameObject gridLightRedGameObject in gridLightRed)
                {

                    gridLightRedGameObject.gameObject.GetComponent<Renderer>().material = gridLightRedMaterial;
                }
            }
            if (enumRacingStatus == 2)
            {
                foreach (GameObject gridLightGreenGameObject in gridLightGreen)
                {
                    gridLightGreenGameObject.gameObject.GetComponent<Renderer>().material = gridLightGreenMaterial;
                }
            }
            if (enumRacingStatus == 3)
            {
                gridLightBoard.SetActive(false);
            }
        }
        catch (Exception ex)
        {

        }

    }
    private void Control()
    {
        try
        {           
            if (!isGuardRailColliderTimerStart)
            {
                if (Controls.KeyCodeDownACCELL())
                {
                    if (speed < speedCurrent || speed == 0.0f)
                    {
                        speed += speedIncreaseArray[currentGear];                        
                    }
                    this.transform.Translate(Vector3.forward * speed);                    
                }
                else
                {
                    if (Controls.KeyCodeDownBREAK())
                    {
                        if (speed > 0.0f)
                        {
                            speed -= speedBrake;
                            this.transform.Translate(Vector3.forward * speed);
                        }
                        else
                            speed = 0.0f;
                    }
                    else
                    {
                        if (speed > 0.0f)
                        {
                            speed -= speedDecrease;
                            this.transform.Translate(Vector3.forward * speed);                            
                        }
                    }
                }
                
                if (Controls.KeyCodeDownLEFT())
                {
                    try
                    {
                        if (speed > 0.0f)
                        {
                            this.transform.RotateAround(this.transform.position, Vector3.up, -handleIncrease * Time.deltaTime);
                            if (turningState == TurningStateEnum.init)
                                turningState = TurningStateEnum.turning;
                            Steering(true);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    
                }
                else if (Controls.KeyCodeDownRIGHT())
                {                    
                    if (speed > 0.0f)
                    {
                        this.transform.RotateAround(this.transform.position, Vector3.up, handleIncrease * Time.deltaTime);
                        if (turningState == TurningStateEnum.init)
                            turningState = TurningStateEnum.turning;
                        Steering(false);
                    }
                }
                else
                {
                    if (turningState == TurningStateEnum.turning)
                        turningState = TurningStateEnum.finishTurning;
                    SteeringReset();
                }

                Quaternion q = this.transform.rotation;
                q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
                this.transform.rotation = q;
            }
            ManageTyreCollision();
            TyreChangeSpeed();
        }
        catch (Exception ex)
        {
        }        
    }    

    private void Steering(bool isRight)
    {
        try
        {
            if (steeringIndexInc < 30.0f && steeringIndexInc > -30.0f)
            {
                steeringIndex = isRight ? 2.0f : -2.0f;                
                steering.transform.Rotate(0.0f, 0.0f, steeringIndex);
                steeringIndexInc += isRight ? 2.0f : -2.0f;

                if (steeringIndexInc < 5.0f && steeringIndexInc > -5.0f)
                {
                    leftTyre.gameObject.GetComponent<Renderer>().material = tyreMaterial[1];
                    leftTyreHighlight.gameObject.GetComponent<Renderer>().material = tyreMaterialHighlight[1];
                    rightTyre.gameObject.GetComponent<Renderer>().material = tyreMaterial[1];
                    rightTyreHighlight.gameObject.GetComponent<Renderer>().material = tyreMaterialHighlight[1];
                }
                else if (steeringIndexInc >= 5.0f)
                {
                    leftTyre.gameObject.GetComponent<Renderer>().material = tyreMaterial[0];
                    leftTyreHighlight.gameObject.GetComponent<Renderer>().material = tyreMaterialHighlight[0];
                    rightTyre.gameObject.GetComponent<Renderer>().material = tyreMaterial[2];
                    rightTyreHighlight.gameObject.GetComponent<Renderer>().material = tyreMaterialHighlight[2];
                }
                else if (steeringIndexInc <= -5.0f)
                {
                    leftTyre.gameObject.GetComponent<Renderer>().material = tyreMaterial[2];
                    leftTyreHighlight.gameObject.GetComponent<Renderer>().material = tyreMaterialHighlight[2];
                    rightTyre.gameObject.GetComponent<Renderer>().material = tyreMaterial[0];
                    rightTyreHighlight.gameObject.GetComponent<Renderer>().material = tyreMaterialHighlight[0];
                }
            }
        }
        catch { }

    }
    private void SteeringReset()
    {
        try
        {
            if (steeringIndexInc > 0.0f)
            {
                steeringIndexInc += -2.0f;
                steering.transform.Rotate(0.0f, 0.0f, -2.0f);
            }
            else if (steeringIndexInc < 0.0f)
            {
                steeringIndexInc += 2.0f;
                steering.transform.Rotate(0.0f, 0.0f, 2.0f);
            }

            if (steeringIndexInc < 5.0f && steeringIndexInc > -5.0f)
            {
                leftTyre.gameObject.GetComponent<Renderer>().material = tyreMaterial[1];
                leftTyreHighlight.gameObject.GetComponent<Renderer>().material = tyreMaterialHighlight[1];
                rightTyre.gameObject.GetComponent<Renderer>().material = tyreMaterial[1];
                rightTyreHighlight.gameObject.GetComponent<Renderer>().material = tyreMaterialHighlight[1];
            }
        }
        catch (Exception ex)
        {
        }

    }
    private void ManageTyreCollision()
    {
        try
        {
            Tyre tyre = null;
            for (int i = 0; i < tyres.Length; i++)
            {
                if (tyres[i].GetCollision() == 2)
                {                    
                    speedCurrent = 0.0f;
                    speed = 0.0f;                    
                    tyre = tyres[i];
                    tyres[i].SetCollision(0);
                    isGuardRailColliderTimerStart = true;                    
                    break;
                }
                else if (tyres[i].GetCollision() == 1)
                {                    
                    speedCurrent = 0.0125f;
                    if (speed > 0.0f && speed > speedCurrent)
                    {
                        speed -= 0.002f;
                        activateTimerCarGrassState = 1;
                        HandleLimit();
                    }
                    break;
                }
                else
                {
                    if (activateTimerCarGrassState == 1)
                        activateTimerCarGrassState = 2;
                    SpeedPercent();
                    speedCurrent = speedMax;                    
                }
            }
            if (tyre != null)
            {               
                GuardrailOffSet(tyre);
            }            
        }
        catch (Exception ex)
        { }
    }
    private void TyreChangeSpeed()
    {
        try
        {
            if (speed > 0.0f)
            {
                timerChangeTyreAnimation += 1.0f * Time.deltaTime;
                if (speed < 0.01f)
                {

                    if (timerChangeTyreAnimation > 0.2f)
                    {
                        leftTyreHighlight.SetActive(!leftTyreHighlight.activeSelf);
                        rightTyreHighlight.SetActive(!rightTyreHighlight.activeSelf);
                        timerChangeTyreAnimation = 0.0f;
                    }
                }
                else if (speed >= 0.01f && speed < 0.02f)
                {

                    if (timerChangeTyreAnimation > 0.15f)
                    {
                        leftTyreHighlight.SetActive(!leftTyreHighlight.activeSelf);
                        rightTyreHighlight.SetActive(!rightTyreHighlight.activeSelf);
                        timerChangeTyreAnimation = 0.0f;
                    }
                }
                else if (speed >= 0.02f)
                {

                    if (timerChangeTyreAnimation > 0.075f)
                    {
                        leftTyreHighlight.SetActive(!leftTyreHighlight.activeSelf);
                        rightTyreHighlight.SetActive(!rightTyreHighlight.activeSelf);
                        timerChangeTyreAnimation = 0.0f;
                    }
                }
            }
            else
            {
                leftTyreHighlight.SetActive(false);
                rightTyreHighlight.SetActive(false);
            }
        }
        catch { }

    }
    private void HandleLimit()
    {
        if (handleIncrease > handleIncreaseMin)
            handleIncrease -= 2.5f;
        if (handleIncrease < handleIncreaseMin)
            handleIncrease = handleIncreaseMin;
    }
    private void SpeedPercent()
    {
        float speedPerc = (speed * 100.0f) / speedCurrent;
        HandlePercent(speedPerc);
    }
    private void TimeCarGuardrail()
    {        
        if (isGuardRailColliderTimerStart)
        {            
            timerGuardRail += 1.0f * Time.deltaTime;
            //if ((int)tyre.enumTyreSide == 0)
            //    this.transform.Translate(Vector3.left * -0.005f);
            //else if ((int)tyre.enumTyreSide == 1)
            //    this.transform.Translate(Vector3.left * 0.005f);            
            //LookToPointMoveGuardrail();
            if (timerGuardRail > 0.1f)
            {
                timerGuardRail = 0.0f;
                isGuardRailColliderTimerStart = false;
            }
        }
    }
    private void GuardrailOffSet(Tyre tyre)
    {
        if ((int)tyre.enumTyreSide == 0)
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z - guardrailOffSet);
        else if ((int)tyre.enumTyreSide == 1)
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z + guardrailOffSet);
        else if ((int)tyre.enumTyreSide == 2)
            this.transform.localPosition = new Vector3(this.transform.localPosition.x - guardrailOffSet, this.transform.localPosition.y, this.transform.localPosition.z);
        LookToPointMoveGuardrail();
        isGuardRailColliderTimerStart = false;
    }
    private void HandlePercent(float speedPerc)
    {
        float handlePerc = ((handleIncreaseMax - handleIncreaseMin) * speedPerc) / 100.0f;
        handleIncrease = handleIncreaseMin + (handleIncreaseMax - handlePerc);
        if (handleIncrease < handleIncreaseMin)
            handleIncrease = handleIncreaseMin;
    }
    private void LookToPointMoveGuardrail()
    {
        //Definir qual referência está mais próxima do player
        List<Vector3> wayPointDist = new List<Vector3>();

        //int index = wayPointLookAtIndex == 0 ? 0 : wayPointLookAtIndex - 1;
        foreach (WayPoint wPoint in wayPointObjectList[wayPointLookAtIndex].GetSetWayPointList)
            wayPointDist.Add(wPoint.transform.position);        
        //Look At referência              
        var lookPos = wayPointDist[1] - this.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100.0f);
        transform.rotation = rotation;
        //this.transform.position = new Vector3(wayPointDist[1].x, wayPointDist[1].y, wayPointDist[1].z);
    }
    private void Timer()
    {
        timeLap += Time.deltaTime;
        //TimeSpan t = TimeSpan.FromSeconds(timeLap);
        //string timeFormat = string.Format("{1:D2}:{2:D2}.{3:D3}",
        //        t.Hours,
        //        t.Minutes,
        //        t.Seconds,
        //        t.Milliseconds);
        //chronometer.text = timeFormat;
        FormatLap(timeLap, chronometer);
    }
    private void CheckInverseTrack()
    {
        if (tyres[0].GetComponentsInChildren<Tyre>()[0].idGround != lastTrackID)
        {
            if (tyres[0].GetComponentsInChildren<Tyre>()[0].idGround < lastTrackID && tyres[0].GetComponentsInChildren<Tyre>()[0].idGround != 0)
            {
                if (System.Math.Abs(tyres[0].GetComponentsInChildren<Tyre>()[0].idGround - lastTrackID) > 5)
                    LookToPointMoveReverse();
            }
            else
            {
                lastTrackID = tyres[0].GetComponentsInChildren<Tyre>()[0].idGround;
            }
        }
    }
    private void LookToPointMoveReverse()
    {
        //Definir qual referência está mais próxima do player
        List<Vector3> wayPointDist = new List<Vector3>();
        foreach (WayPoint wPoint in wayPointObjectList[wayPointLookAtIndex].GetSetWayPointList)
            wayPointDist.Add(wPoint.transform.position);
        //Look At referência                    
        var lookPos = wayPointDist[1] - this.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
    }
    private void GetCheckPoint()
    {
        foreach (var tyre in tyres)
        {
            if (tyre.GetCheckPointID() > -1)
            {
                checkPointArray[tyre.GetCheckPointID()] = true;
                CheckIfLapIsFinished();
            }
        }                
    }
    private void CheckIfLapIsFinished()
    {
        foreach (var tyre in tyres)
        {
            if (tyre.GetCheckPointID() == 0 && checkPointArray.Where(x => x == true).Count() == 3)
            {
                laps++;
                timeLastLap = timeLap;
                FormatLap(timeLastLap, chronometerLastLap);
                if (laps == 2)
                    timeBestLap = timeLastLap;
                else
                    timeBestLap = timeLap < timeBestLap ? timeLap : timeBestLap;
                FormatLap(timeBestLap, chronometerBestLap);
                for (int i = 0; i < checkPointArray.Count(); i++)
                {
                    checkPointArray[i] = false;
                    timeLap = 0.0f;
                }
            }
            if (tyre.GetCheckPointID() == 0 && idGround == 0 && laps == 0)
            {
                laps = 1;
            }
        }        
    }
    private void FormatLap(float timeL, Text chronometerText)
    {
        TimeSpan t = TimeSpan.FromSeconds(timeL);
        string timeFormat = string.Format("{1:D2}:{2:D2}.{3:D3}",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);        
        chronometerText.text = timeFormat;
    }
    private void GetIdGround()
    {
        idGround = tyres[0].GetComponentsInChildren<Tyre>()[0].idGround;
        timeDiff[idGround] = raceManager.raceTime;

        if(idGround > 0 && idGround < 20)
            checkPointArray[0] = true;
    }
    private void CollisionHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.collider.tag == "Car")
            {
                CarCollision(hit);
            }
        }
    }
    private void CarCollision(RaycastHit hit)
    {
        if (hit.distance < 1.25f)
        {
            speed = speed / 1.5f;
            currentGear = 0;
        }
    }
    private void SetRPMHUD()
    {
        float perc = ((speed - gears[currentGear]) * 100.0f) / (gearsIncreaseProv[currentGear] - gears[currentGear]);
        float inc = perc > 0.0f ? (perc * 90.0f) / 100.0f : 0.0f;        
        Quaternion rotation = Quaternion.Euler(0, 0, inc);        
        RPMPointer.transform.rotation = rotation;
    }
    private void GetGears()
    {
        for (int i = 0; i < gears.Length; i++)
        {
            if (i < gears.Length - 1)
            {
                if (speed > gears[i] && speed < gears[i + 1])
                    currentGear = i;
            }
            else
            {
                if (speed > gears[i])
                    currentGear = i;
            }
        }
    }
    private void SetGearHUD()
    {
        gear.text = (currentGear + 1).ToString();
    }
    private void Speedmeter()
    {
        float speedCalc = (speed * speedmeterMax) / speedMaxInit;
        speedMeter.text = ((int)speedCalc).ToString();
    }
    private void InitGears()
    {
        for (int i = 0; i < 6; i++)
        {
            gears[i] = 0.005f * (i * 1);
        }

        for (int i = 0; i < gearsIncreaseProv.Length - 1; i++)
        {
            gearsIncreaseProv[i] = gears[i + 1];
        }
        gearsIncreaseProv[gearsIncreaseProv.Length - 1] = speedMaxInit;

    }
    private void GetWayPoints()
    {
        if (wayPoints == null)
            wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");

        wayPointObjectList = new List<WayPointObject>();
        var queryLastNames = from wayPoint in wayPoints group wayPoint by wayPoint.GetComponent<WayPoint>().GetSettrackID;
        int count = 0;
        int idWayPoint = 0;
        bool isLookToRedirect = false;
        foreach (var query1 in queryLastNames)
        {
            WayPointObject wayPointObject = new WayPointObject();
            wayPointObjectList.Add(wayPointObject);
            wayPointObjectList[count].GetSetWayPointList = new List<WayPoint>();
            foreach (var query2 in query1)
            {
                WayPoint obj = query2.GetComponent<WayPoint>();

                wayPointObjectList[count].GetSetWayPointList.Add(obj);
                idWayPoint = obj.GetSettrackID;
                isLookToRedirect = obj.GetIsLookToRedirect;
            }
            wayPointObjectList[count].GetSetId = idWayPoint;
            wayPointObjectList[count].GetSetIsLookToRedirect = isLookToRedirect;
            count++;
        }
        wayPointObjectList = wayPointObjectList.OrderBy(a => a.GetSetId).ToList();
        wayPointLookAtId = wayPointObjectList[0].GetSetId;
        wayPointLookAtIndex = 0;
    }
    private void CheckVacuum()
    {
        if ((enumRacingStatus == 2 || enumRacingStatus == 3))
        {
            if (playerVacuum.carCollide && speed > 0.005f)
            {
                //speedMax = vacuumAdd;
                vacuumState = VacuumStateEnum.inside;
                speed += speedIncrease;
            }
            else
            {
                //speedMax = speedMaxInit;
                if (vacuumState == VacuumStateEnum.inside)
                {
                    vacuumState = VacuumStateEnum.outside;                    
                }                    
            }
        }        
        VacuumReset();
    }
    private void VacuumReset()
    {
        if (vacuumState == VacuumStateEnum.outside && speed > speedMax)
        {
            speed -= speedDecrease;
        }
        else if (vacuumState == VacuumStateEnum.outside && speed <= speedMax)
        {
            vacuumState = VacuumStateEnum.init;
        }
    }

    public void EnableCheckeredFlag(bool enable)
    {
        checkeredFlag.SetActive(enable);
    }
    public void RetireCheck()
    {
        if (this.transform.position.y < -5.0f)
        {
            raceStatus = Car.RaceStatusEnum.retired;
            this.gameObject.SetActive(false);
        }
    }
    public void RespawPlayer()
    {
        if (this.transform.position.y < 0.0f || this.transform.position.y > 0.01f)
        {
            this.transform.position = wayPointObjectList[wayPointLookAtIndex].GetSetWayPointList[1].transform.position;
            this.transform.rotation = wayPointObjectList[wayPointLookAtIndex].GetSetWayPointList[1].transform.rotation;            
        }
        if (this.transform.rotation.x > 0.1f || this.transform.rotation.x < -0.1f)
        {            
            this.transform.rotation = new Quaternion(0.0f, this.transform.rotation.y, this.transform.rotation.z, 0.0f);            
        }
    }
}
