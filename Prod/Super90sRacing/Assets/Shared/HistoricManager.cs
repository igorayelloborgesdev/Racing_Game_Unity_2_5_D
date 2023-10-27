using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoricManager : MonoBehaviour
{
    [SerializeField]
    private Historic[] historic = null;
    [SerializeField]
    private Text pos = null;

    public void HideAll()
    {
        foreach (var obj in historic)
        {
            obj.gameObject.SetActive(false);
        }
        pos.gameObject.SetActive(false);
    }
    public void ShowHistoric(int id, int aPos, int teamId, float r, float g, float b)
    {
        historic[id].gameObject.SetActive(true);
        historic[id].SetPositionHistoric(aPos.ToString());
        historic[id].ChangeChassi(teamId);
        historic[id].ChangeCloth(teamId);
        historic[id].ChangeHelmet(r, g, b);
    }
    public void ShowHidePos(bool show)
    {        
        pos.gameObject.SetActive(show);
    }
    public void SetPos(int aPos)
    {
        pos.text = aPos.ToString();
    }
}
