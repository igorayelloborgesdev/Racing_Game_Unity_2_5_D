using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField]
    private int trackID = 0;
    public int GetSettrackID
    {
        get
        {
            return trackID;
        }
        set
        {
            trackID = value;
        }
    }

    public enum WayPointPosition
    {
        first = 1,
        second = 2,
        third = 3
    }
    public WayPointPosition wayPointPosition = WayPointPosition.first;
    [SerializeField]
    private bool isLookToRedirect;
    public bool GetIsLookToRedirect
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
    public void SetWayPoint(int id)
    {
        wayPointPosition = (WayPointPosition)id;
    }
}
