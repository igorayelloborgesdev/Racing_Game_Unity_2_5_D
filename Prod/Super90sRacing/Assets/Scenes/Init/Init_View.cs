using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class Init_View : MonoBehaviour {
	#region Variables	
	private Init_Model init_Model;
	#endregion
	#region Behaviors
	// Use this for initialization
	void Start () {
		Init ();
	}
	// Update is called once per frame
	void Update () {
		LoadScene ();
	}
	#endregion
	#region Methods	
	private void Init()
	{
        try
        {            
            init_Model = new Init_Model();
            init_Model.Init();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
		
	}	
	private void LoadScene()
	{
        try
        {
            init_Model.GetSetPeriod += Time.deltaTime;
            if (init_Model.GoToNextScreen(init_Model.GetSetPeriod))
                SceneManager.LoadScene(SceneInfo.GetSceneNames[1], LoadSceneMode.Single);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }        
	}
	#endregion
}