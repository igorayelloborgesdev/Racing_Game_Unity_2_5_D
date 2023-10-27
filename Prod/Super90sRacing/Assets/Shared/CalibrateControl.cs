using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class CalibrateControl
{
    #region Serialized field
    private Button[] buttonInput;
    private Color selected;
    private Color unSelected;
    #endregion
    #region Variables
    private static int inputID = -1;
    public static int GetSetinputID
    {
        get
        {
            return inputID;
        }
        set
        {
            inputID = value;
        }
    }
    private KeyCode[] keyCodes = new KeyCode[5];
    private const string pathNameConfig = "/Resources/config.json";
    #endregion
    #region Constructor
    public CalibrateControl(Button[] buttonInput, Color selected, Color unSelected)
    {
        this.buttonInput = buttonInput;
        this.selected = selected;
        this.unSelected = unSelected;
    }
    #endregion
    #region Methods
    public void Init()
    {
        try
        {
            for (int i = 0; i < General.GetSetConfig.controlsKeycode.Length; i++)
            {
                keyCodes[i] = (KeyCode)General.GetSetConfig.controlsKeycode[i];
                buttonInput[i].GetComponentInChildren<Text>().text = keyCodes[i].ToString();
            }
            SetDirectionalDefalt();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    private void SetColor(int id)
    {
        try
        {
            if (General.GetSetConfig.controlID == 0 || (General.GetSetConfig.controlID == 1 && inputID > 1))
            {
                for (int i = 0; i < buttonInput.Length; i++)
                {
                    buttonInput[i].GetComponent<Image>().color = unSelected;
                    buttonInput[i].GetComponentInChildren<Text>().color = selected;
                }
                buttonInput[id].GetComponent<Image>().color = selected;
                buttonInput[id].GetComponentInChildren<Text>().color = unSelected;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    private void SaveConfig()
    {
        try
        {
            for (int a = 0; a < keyCodes.Length; a++)
            {
                General.GetSetConfig.controlsKeycode[a] = (int)keyCodes[a];
            }
            Options_Model.SaveConfig();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public void CalibrateControls()
    {
        try
        {
            if (inputID != -1)
            {
                if (General.GetSetConfig.controlID == 0 || (General.GetSetConfig.controlID == 1 && inputID > 1))
                {
                    keyCodes[inputID] = Controls.GetKeyDownAssign();

                    if (keyCodes[inputID] != KeyCode.None)
                    {
                        buttonInput[inputID].GetComponentInChildren<Text>().text = keyCodes[inputID].ToString();
                        for (int a = 0; a < keyCodes.Length; a++)
                        {
                            if (a != inputID && keyCodes[a] == keyCodes[inputID])
                            {
                                keyCodes[a] = KeyCode.None;
                                buttonInput[a].GetComponentInChildren<Text>().text = keyCodes[a].ToString();
                            }
                        }
                        buttonInput[inputID].GetComponent<Image>().color = unSelected;
                        buttonInput[inputID].GetComponentInChildren<Text>().color = selected;
                        inputID = -1;
                        SaveConfig();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public void SetDirectionalDefalt()
    {
        try
        {
            if (General.GetSetConfig.controlID == 1)
            {
                //buttonInput[0].GetComponentInChildren<Text>().text = "+ Y";
                //buttonInput[1].GetComponentInChildren<Text>().text = "- Y";
                buttonInput[0].GetComponentInChildren<Text>().text = "- X";
                buttonInput[1].GetComponentInChildren<Text>().text = "+ X";
            }
            else
            {
                for (int i = 0; i < General.GetSetConfig.controlsKeycode.Length; i++)
                {
                    keyCodes[i] = (KeyCode)General.GetSetConfig.controlsKeycode[i];
                    buttonInput[i].GetComponentInChildren<Text>().text = keyCodes[i].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    #endregion
    #region Events
    public void SelectInput(int id)
    {
        try
        {
            inputID = id;
            SetColor(id);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    #endregion
}
