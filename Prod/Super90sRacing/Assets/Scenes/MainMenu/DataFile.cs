using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System;
using Newtonsoft.Json;

/// <summary>
/// Data file.
/// </summary>
public static class DataFile
{
    #region methods	
    public static T GetData<T>(string dataName)
    {
        try
        {
            var dfs = Resources.Load<TextAsset>(dataName);
            if (dfs != null)
            {
                T obj = JsonUtility.FromJson<T>(dfs.text);
                return obj;
            }
            else
            {
                return default(T);
            }
        }
        catch(Exception ex)
        {
            return default(T);
        }
    }
    public static void SaveData(object obj, string pathName)
    {
        try
        {
            string dataAsJson = JsonUtility.ToJson(obj);
            string filePath = Application.dataPath + pathName;
            File.WriteAllText(filePath, dataAsJson);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public static T GetDataS<T>(string dataName)
    {
        try
        {

            string filePath = Application.persistentDataPath + dataName;
            var dfs = File.ReadAllText(filePath);
            if (dfs != null)
            {
                T obj = JsonConvert.DeserializeObject<T>(dfs);
                //T obj = JsonUtility.FromJson<T>(dfs);
                return obj;
            }
            else
            {
                return default(T);
            }
        }
        catch(Exception ex)
        {
            return default(T);
        }
    }
    public static T GetDataSResources<T>(string dataName)
    {
        try
        {

            var dfs = Resources.Load<TextAsset>(dataName).ToString();
            if (dfs != null)
            {
                T obj = JsonConvert.DeserializeObject<T>(dfs);
                //T obj = JsonUtility.FromJson<T>(dfs);
                return obj;
            }
            else
            {
                return default(T);
            }
        }
        catch (Exception ex)
        {
            var exMsg = ex.Message;
            return default(T);
        }
    }
    public static JSONResult SaveDataS(object obj, string pathName, string fileName)
    {
        try
        {
            string dataAsJson = JsonConvert.SerializeObject(obj);
            string filePath = Application.persistentDataPath + "/" + pathName;
            var destinationDirectory = new DirectoryInfo(filePath);
            if (!destinationDirectory.Exists)
                destinationDirectory.Create();
            filePath = filePath + "/" + fileName;
            File.WriteAllText(filePath, dataAsJson);
            return new JSONResult() { IsOK = true, message = Language.GetLanguage[General.GetSetConfig.languageID][106] };
        }
        catch (Exception ex)
        {
            var exMsg = ex.Message;
            return new JSONResult() { IsOK = false, message = Language.GetLanguage[General.GetSetConfig.languageID][107] };
        }
    }
    public static void SaveDataS(object obj, string pathfileName)
    {
        try
        {
            string dataAsJson = JsonConvert.SerializeObject(obj);
            string filePath = Application.dataPath + pathfileName;
            var destinationDirectory = new DirectoryInfo(filePath);
            File.WriteAllText(filePath, dataAsJson);
        }
        catch (Exception ex)
        {
            var exMsg = ex.Message;
        }
    }
    public static bool DeleteDataS(string pathName)
    {
        try
        {
            string filePath = Application.persistentDataPath + "/" + pathName;
            FileInfo fi2 = new FileInfo(filePath);
            fi2.Delete();
            return true;
        }
        catch (Exception ex)
        {
            var exMsg = ex.Message;
            return false;
        }
    }
    #endregion
}