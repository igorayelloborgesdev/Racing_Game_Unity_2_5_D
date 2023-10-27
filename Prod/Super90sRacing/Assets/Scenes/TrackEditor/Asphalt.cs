using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asphalt : MonoBehaviour
{
    public int id;
    public string objName;
    public string className;
    public string materialName;
    public string tagName;
    public Vector3[] pointsArray;
    public float[] speedArray = new float[3];
    public bool isSpeedChange;
}
