using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGraph : MonoBehaviour
{
    #region Variable
    [SerializeField]
    private GameObject[] skillGraph;
    #endregion
    #region Method
    public void SetSkillGraph(int skill)
    {
        for (int i = 0; i < skillGraph.Length; i++)
        {
            skillGraph[i].SetActive(false);
        }
        for (int i = 0; i < skill; i++)
        {
            skillGraph[i].SetActive(true);
        }
    }
    #endregion
}
