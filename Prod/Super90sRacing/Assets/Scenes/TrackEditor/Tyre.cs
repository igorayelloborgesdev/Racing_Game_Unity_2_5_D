using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyre : MonoBehaviour
{
    private int collisionID = 0;
    private int guardrailID = 0;
    public int GetSetIsGuardrailLeft
    {
        get
        {
            return guardrailID;
        }
        set
        {
            guardrailID = value;
        }
    }
    public int idGround;
    public float[] speed;
    public bool isSpeedChange;
    private int checkPointID = -1;
    public bool collisionTrackPit = false;

    public enum EnumTyreSide
    {
        Left = 0,
        Right = 1,
        Front = 2,
        Back = 3        
    }
    public EnumTyreSide enumTyreSide = EnumTyreSide.Left;

    private void OnTriggerEnter(Collider collider)
    {        
        ManageCollider(collider);
    }

    private void ManageCollider(Collider collider)
    {
        if (collider.tag == "asphalt")
            SetCollision(0, collider);
        else if (collider.tag == "grass")
            SetCollision(1, collider);
        else if (collider.tag == "guardrail")
        {            
            SetCollision(2, collider);            
            var hitPoint = collider.ClosestPoint(this.transform.position);
            var playerPosition = collider.bounds.center;
            var dir = hitPoint - playerPosition;
            var angle = Vector2.SignedAngle(transform.right, dir);                     
        }
    }

    private void SetCollision(int id, Collider collider)
    {        
        collisionID = id;
        if (id == 0 || id == 1)
            GetAsphaltGrassID(id, collider);
    }
    public void SetCollision(int id)
    {
        collisionID = id;        
    }
    public int GetCollision()
    {
        return collisionID;
    }    

    public int GetCheckPointID()
    {
        return checkPointID;
    }

    public void GetAsphaltGrassID(int id, Collider collider)
    {
        if (id == 0)
        {
            if (collider.GetComponent<Asphalt>() != null)
            {
                idGround = collider.GetComponent<Asphalt>().id;
                isSpeedChange = collider.GetComponent<Asphalt>().isSpeedChange;
                speed = collider.GetComponent<Asphalt>().speedArray;                
                GetCheckPointCollision(collider);
            }
        }
        else if (id == 1)
        {
            if (collider.GetComponent<Grass>() != null)
            {
                idGround = collider.GetComponent<Grass>().id;
                GetCheckPointCollision(collider);
            }
        }     
    }

    private void GetCheckPointCollision(Collider collider)
    {
        if (collider.GetComponent<Checkpoint>() != null)
        {
            checkPointID = collider.GetComponent<Checkpoint>().id;
        }
        else
            checkPointID = -1;
    }

}
