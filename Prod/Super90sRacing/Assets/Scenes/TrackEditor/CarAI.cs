using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarAI : Car
{
    [SerializeField]
    private GameObject front;
    [SerializeField]
    private GameObject back;
    [SerializeField]
    private Renderer helmetMaterial;
    [SerializeField]
    private Material frontMaterial;
    [SerializeField]
    private Material backMaterial;
    public float[] gears = new float[6];
    public float[] gearsIncreaseProv = new float[6];
    public float[] gearsIncrease = new float[6];
    public int currentGear = 0;
    GameObject[] wayPoints;
    public List<WayPointObject> wayPointObjectList;
    public int wayPointLookAtId = 0;
    public int wayPointLookAtIndex = 0;
    private float cornerSpeed = 0.0f;
    [SerializeField]
    public Tyre[] tyres;
    private WayPoint.WayPointPosition WayPointPosition = new WayPoint.WayPointPosition();
    public enum OvertakingStatus
    {
        Init = 0,
        Detect = 1,
        Change = 2
    }
    OvertakingStatus overtakingStatus = OvertakingStatus.Init;
    private enum VacuumStateEnum { init, inside, outside };
    private VacuumStateEnum vacuumState = VacuumStateEnum.init;
    private bool isBraking = false;
    float speedBrake = 0.00075f;
    private Vector3 wayDirection = Vector3.zero;
    bool isGuardRailColliderTimerStart = false;
    int activateTimerCarGrassState = 0;
    public Car carPlayer;
    private bool isChangeDirection = false;
    private bool[] checkPointArray = { false, false, false };
    private float guardrailOffSet = 0.005f;    


    void Start()
    {
        InitGears();
        cornerSpeed = speedCurrent;
        GetWayPoints();
        SetToPoint();
        WayPointPosition = WayPoint.WayPointPosition.first;
        overtakingStatus = OvertakingStatus.Init;        
    }
    void Update()
    {
        if (!raceManager.GetShowPauseMenu)
        {
            if (enumRacingStatus == 2 || enumRacingStatus == 3)
            {
                Controls();
            }
            SetToPoint();
            GetIdGround();
            OvertakingHit();
            ChecksIfCarOvertaking();
            //CheckVacuum();
            GetGears();
            GetCheckPoint();
            ResetXPosition();
            RetireCheck();
            RespawPlayer();            
        }        
    }
    void OnTriggerStay(Collider other)
    {
        TriggerCollision(other);        
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerCollision(other);
    }

    private void TriggerCollision(Collider other)
    {       
        if (other.tag == "Car")
        {
            CarCollisionDirection(false);
        }        
        if (other.tag == "grass" || other.tag == "guardrail")
        {            
            //if (speed > 0.005f)
            //    speed = speed * 0.9f;
            //else
            //    speed = 0.005f;

            //speed = speed * 0.9f;
            //ChangeDirection();//<-
            //CarCollisionDirectionGrassGuardrail(other.gameObject.transform.position);
        }        
    }

    public void SetGraph(int id)
    {        
        var spriteFront = Resources.Load<Sprite>("carFront" + id.ToString());
        Material frontMaterialNew = new Material(frontMaterial);
        frontMaterialNew.mainTexture = spriteFront.texture;
        front.gameObject.GetComponent<Renderer>().material = frontMaterialNew;

        var spriteBack = Resources.Load<Sprite>("carBack" + id.ToString());
        Material backMaterialNew = new Material(backMaterial);
        backMaterialNew.mainTexture = spriteBack.texture;        
        back.gameObject.GetComponent<Renderer>().material = backMaterialNew;        
    }
    public void SetHelmetColor(Color color)
    {
        helmetMaterial.material.color = color;        
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
    private void Controls()
    {
        
        if (tyres[0].GetComponentsInChildren<Tyre>()[0].isSpeedChange && tyres[0].GetComponentsInChildren<Tyre>()[0].speed[(int)WayPointPosition - 1] != speed)
        {

            if (tyres[0].GetComponentsInChildren<Tyre>()[0].speed[(int)WayPointPosition - 1].Equals(0.0f))
            {
                if (tyres[0].GetComponentsInChildren<Tyre>()[0].speed[0] < speed)
                {
                    isBraking = true;
                    cornerSpeed = tyres[0].GetComponentsInChildren<Tyre>()[0].speed[0];
                }
                if (tyres[0].GetComponentsInChildren<Tyre>()[0].speed[0] >= speed)
                {
                    isBraking = false;
                    cornerSpeed = tyres[0].GetComponentsInChildren<Tyre>()[0].speed[0];
                }
            }
            else
            {
                if (tyres[0].GetComponentsInChildren<Tyre>()[0].speed[(int)WayPointPosition - 1] < speed)
                {
                    isBraking = true;
                    cornerSpeed = tyres[0].GetComponentsInChildren<Tyre>()[0].speed[(int)WayPointPosition - 1];
                }
                if (tyres[0].GetComponentsInChildren<Tyre>()[0].speed[(int)WayPointPosition - 1] >= speed)
                {
                    isBraking = false;
                    cornerSpeed = tyres[0].GetComponentsInChildren<Tyre>()[0].speed[(int)WayPointPosition - 1];
                }
            }
        }        
        if (!isBraking)
            Accel();
        else
            Brake();

        LookToPointMove();
        ManageTyreCollision();
    }
    private void Accel()
    {
        if (speed < cornerSpeed || speed == 0.0f)
        {
            speed += speedIncreaseArray[currentGear];
        }
        this.transform.Translate(Vector3.forward * speed);
    }
    private void Brake()
    {        
        if (speed > 0.0f && speed > cornerSpeed)
        {     
            speed -= speedBrake;
            this.transform.Translate(Vector3.forward * speed);
        }
        else
            isBraking = false;
    }    
    private void ManageTyreCollision()
    {
        try
        {
            //Tyre tyre = null;
            //for (int i = 0; i < tyres.Length; i++)
            //{
            //    if (tyres[i].GetCollision() == 2)
            //    {
            //        speedCurrent = 0.0f;
            //        speed = 0.0f;
            //        tyre = tyres[i];
            //        tyres[i].SetCollision(0);
            //        isGuardRailColliderTimerStart = true;
            //        break;
            //    }
            //    else if (tyres[i].GetCollision() == 1)
            //    {
            //        speedCurrent = 0.0125f;
            //        if (speed > 0.0f && speed > speedCurrent)
            //        {
            //            speed -= 0.002f;
            //            activateTimerCarGrassState = 1;
            //            HandleLimit();
            //        }
            //        break;
            //    }
            //    else
            //    {
            //        if (activateTimerCarGrassState == 1)
            //            activateTimerCarGrassState = 2;
            //        SpeedPercent();
            //        speedCurrent = speedMax;
            //    }                
            //}
            //if (tyre != null)
            //{
            //    GuardrailOffSet(tyre);
            //}
            SpeedPercent();
            speedCurrent = speedMax;
        }
        catch (Exception ex)
        { }
    }
    private void LookToPointMove()
    {
        try
        {
            wayDirection = wayPointObjectList[wayPointLookAtIndex].GetSetWayPointList.Where(x => x.wayPointPosition == WayPointPosition).FirstOrDefault().transform.position;
            Vector3 way = wayDirection;
            var lookPos = way - this.transform.position;
            lookPos.y = 0;
            Vector3 enemyDirectionLocal = this.transform.InverseTransformPoint(way);

            int handle = tyres[0].GetComponentsInChildren<Tyre>()[0].speed[(int)WayPointPosition - 1] > 0.025f || tyres[0].GetComponentsInChildren<Tyre>()[0].speed[(int)WayPointPosition - 1] < 0.001f ? 1 : 7;


            Vector3 relativePos = way - this.transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 3 * Time.deltaTime);


            //this.transform.LookAt(way);//<-

            //if (enemyDirectionLocal.x < 0)
            //{
            //    this.transform.RotateAround(this.transform.position, Vector3.up, (-1.0f * handle) * Time.deltaTime);
            //    this.transform.RotateAround(this.transform.position, Vector3.up, -handleIncrease * Time.deltaTime);
            //    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 0.0f);
            //}
            //else if (enemyDirectionLocal.x > 0)
            //{
            //    this.transform.RotateAround(this.transform.position, Vector3.up, (1.0f * handle) * Time.deltaTime);
            //    this.transform.RotateAround(this.transform.position, Vector3.up, handleIncrease * Time.deltaTime);
            //    this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 0.0f);
            //}
        }
        catch (Exception ex)
        {
        }        
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
        foreach (WayPoint wPoint in wayPointObjectList[wayPointLookAtIndex].GetSetWayPointList)
            wayPointDist.Add(wPoint.transform.position);
        //Look At referência              
        var lookPos = wayPointDist[1] - this.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100.0f);
        transform.rotation = rotation;
        this.transform.position = new Vector3(wayPointDist[1].x, wayPointDist[1].y, wayPointDist[1].z);
    }
    private void GetIdGround()
    {
        idGround = tyres[0].GetComponentsInChildren<Tyre>()[0].idGround;
        timeDiff[idGround] = raceManager.raceTime;

        if (idGround > 0 && idGround < 20)
            checkPointArray[0] = true;
    }
    private void OvertakingHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {            
            if (hit.collider.tag == "Car")
            {                
                if (hit.distance < 1.5f)                    
                {
                    if (hit.collider.gameObject.GetComponentInParent<Car>() != null)
                    {
                        carPlayer = hit.collider.gameObject.GetComponentInParent<Car>();             
                    }                        
                    //ChangeDirection();
                    overtakingStatus = OvertakingStatus.Detect;                 
                }
                CarCollision(hit);
            }
            else
            {
                if (overtakingStatus == OvertakingStatus.Detect)
                {
                    overtakingStatus = OvertakingStatus.Change;
                    isChangeDirection = true;
                }
            }
        }
    }

    private void LookToPointOvertake()
    {
        //Definir qual referência está mais próxima do player
        List<Vector3> wayPointDist = new List<Vector3>();
        foreach (WayPoint wPoint in wayPointObjectList[wayPointLookAtIndex].GetSetWayPointList)
            wayPointDist.Add(wPoint.transform.position);
        //Look At referência
        var lookPos = wayPointDist[1] - this.transform.position;
        lookPos.y = 0;        
        transform.rotation = Quaternion.LookRotation(lookPos);
    }

    private void ChangeDirection()
    {
        if (WayPointPosition == WayPoint.WayPointPosition.first)
            WayPointPosition = WayPoint.WayPointPosition.second;
        else if (WayPointPosition == WayPoint.WayPointPosition.second)
            WayPointPosition = WayPoint.WayPointPosition.third;
        else if (WayPointPosition == WayPoint.WayPointPosition.third)
            WayPointPosition = WayPoint.WayPointPosition.first;
    }
    private void CarCollision(RaycastHit hit)
    {
        if (hit.distance < 1.25f)
        {            
            CarCollisionDirection(true);
            //ChangeDirection();
            if (hit.collider.tag == "Car")
                speed = hit.collider.GetComponentInParent<Car>().speed / 1.5f;
        }
    }
    private void CarCollisionDirection(bool canMove)
    {        
        float DotResult = Vector3.Dot(transform.forward, wayDirection);
        if (canMove)
        {
            //float directionChange = 0.00001f;
            //float directionChange = 0.001f;
            //if (DotResult > 0.0f)
            //{
            //    this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.position.z + directionChange);
            //}
            //else
            //{
            //    this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.position.z - directionChange);
            //}
            ChangeDirection();
        }                
    }
    private void ChecksIfCarOvertaking()
    {
        if (isChangeDirection)
        {
            if (tyres[0].GetComponentsInChildren<Tyre>()[0].idGround > 5 && carPlayer.idGround > 5)
            {
                int diff = Math.Abs(tyres[0].GetComponentsInChildren<Tyre>()[0].idGround - carPlayer.idGround);
                if (diff > 10)
                {
                    WayPointPosition = WayPoint.WayPointPosition.first;
                    overtakingStatus = OvertakingStatus.Init;
                    isChangeDirection = false;
                }
            }
        }
    }
    private void CheckVacuum()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.collider.tag == "Car")
            {
                if (hit.distance < 1.5f)
                {
                    speedMax = vacuumAdd;
                    vacuumState = VacuumStateEnum.inside;
                    speed += speedIncrease;
                }
            }
            else
            {
                speedMax = speedMaxInit;
                if (vacuumState == VacuumStateEnum.inside)
                    vacuumState = VacuumStateEnum.outside;
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
    private void GetCheckPoint()
    {
        if (tyres[0].GetCheckPointID() > -1)
        {
            checkPointArray[tyres[0].GetCheckPointID()] = true;
            CheckIfLapIsFinished();
        }
    }
    private void CheckIfLapIsFinished()
    {
        if (tyres[0].GetCheckPointID() == 0 && checkPointArray.Where(x => x == true).Count() == 3)
        {
            laps++;
            for (int i = 0; i < checkPointArray.Count(); i++)
            {
                checkPointArray[i] = false;
            }
        }
        if (tyres[0].GetCheckPointID() == 0 && idGround == 0 && laps == 0)
        {
            laps = 1;
        }        
    }    
    private void CarCollisionDirectionGrassGuardrail(Vector3 colliderPosition)
    {
        float DotResult = Vector3.Dot(transform.forward, colliderPosition);
        if (DotResult > 0.0f)
        {
            this.transform.RotateAround(this.transform.position, Vector3.up, (100.0f) * Time.deltaTime);
        }
        else
        {
            this.transform.RotateAround(this.transform.position, Vector3.up, -(100.0f) * Time.deltaTime);
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
