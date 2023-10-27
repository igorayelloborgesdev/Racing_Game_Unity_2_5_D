using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Controls {
	#region Events
	public static bool KeyCodePAUSE()
	{		
		if (Input.GetKeyDown ((KeyCode)General.GetSetConfig.controlsKeycode[4]))
			return true;
		return false;
	}
    public static bool KeyCodeLEFT()
    {        
        if (General.GetSetConfig.controlID == 0)
        {
            if (Input.GetKeyDown((KeyCode)General.GetSetConfig.controlsKeycode[0]))
                return true;
            return false;
        }
        else
        {
            int horizontalInput = (int)Input.GetAxis("Horizontal");            
            if (horizontalInput == -1)
                return true;
            return false;
        }
    }
    public static bool KeyCodeRIGHT()
    {        
        if (General.GetSetConfig.controlID == 0)
        {
            if (Input.GetKeyDown((KeyCode)General.GetSetConfig.controlsKeycode[1]))
                return true;
            return false;
        }
        else
        {            
            int horizontalInput = (int)Input.GetAxis("Horizontal");            
            if (horizontalInput == 1)
                return true;
            return false;
        }
    }    
    public static bool KeyCodeACCELL()
    {        
        if (Input.GetKeyDown((KeyCode)General.GetSetConfig.controlsKeycode[3]))
            return true;
        return false;        
    }
    public static bool KeyCodeDownACCELL()
    {        
        if (Input.GetKey((KeyCode)General.GetSetConfig.controlsKeycode[3]))
            return true;
        return false;
    }
    public static bool KeyCodeUpACCELL()
    {     
        if (Input.GetKeyUp((KeyCode)General.GetSetConfig.controlsKeycode[3]))
            return true;
        return false;
    }
    public static bool KeyCodeBREAK()
    {
        if (Input.GetKeyDown((KeyCode)General.GetSetConfig.controlsKeycode[2]))
            return true;
        return false;
    }
    public static bool KeyCodeDownBREAK()
    {
        if (Input.GetKey((KeyCode)General.GetSetConfig.controlsKeycode[2]))
            return true;
        return false;
    }
    public static bool KeyCodeUpBREAK()
    {
        if (Input.GetKeyUp((KeyCode)General.GetSetConfig.controlsKeycode[2]))
            return true;
        return false;
    }
    public static bool KeyCodeDownLEFT()
    {     
        if (General.GetSetConfig.controlID == 0)
        {
            if (Input.GetKey((KeyCode)General.GetSetConfig.controlsKeycode[0]))
                return true;
            return false;
        }
        else
        {
            int horizontalInput = (int)Input.GetAxis("Horizontal");
            if (horizontalInput == -1)
                return true;            
            return false;
        }
    }
    public static bool KeyCodeDownRIGHT()
    {        
        if (General.GetSetConfig.controlID == 0)
        {            
            if (Input.GetKey((KeyCode)General.GetSetConfig.controlsKeycode[1]))
                return true;
            return false;
        }
        else
        {
            int horizontalInput = (int)Input.GetAxis("Horizontal");
            if (horizontalInput == 1)
                return true;
            return false;
        }    
    }
    public static int KeyCodePosition()
    {        
        if (General.GetSetConfig.controlID == 0)
        {
            if (Input.GetKey((KeyCode)General.GetSetConfig.controlsKeycode[0]))
                return 2;
            if (Input.GetKey((KeyCode)General.GetSetConfig.controlsKeycode[1]))
                return 0;            
            return -1;
        }
        else
        {
            int verticalInput = (int)Input.GetAxisRaw("Vertical");
            int horizontalInput = (int)Input.GetAxis("Horizontal");            
            if (horizontalInput == -1)
                return 0;
            if (horizontalInput == 1)
                return 2;
            return -1;
        }        
    }
    public static KeyCode GetKeyDownAssign()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {            
            if (Input.GetKeyDown(kcode))
                return kcode;
        }
        return KeyCode.None;
    }
	#endregion
}