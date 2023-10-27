using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilMonoBehavior : MonoBehaviour
{
    #region Method
    public static GameObject InstantiateMonoBehavior(GameObject gameObj)
    {
        return Instantiate(gameObj, Vector3.zero, Quaternion.identity);
    }
    #endregion
}
