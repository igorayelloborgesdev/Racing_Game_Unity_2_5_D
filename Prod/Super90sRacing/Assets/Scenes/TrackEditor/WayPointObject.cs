using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointObject : MonoBehaviour
{
    private int id { get; set; }
    public int GetSetId
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }
    private List<WayPoint> wayPointList { get; set; }
    public List<WayPoint> GetSetWayPointList
    {
        get
        {
            return wayPointList;
        }
        set
        {
            wayPointList = value;
        }
    }
    private bool isLookToRedirect { get; set; }
    public bool GetSetIsLookToRedirect
    {
        get
        {
            return isLookToRedirect;
        }
        set
        {
            isLookToRedirect = value;
        }
    }
}
