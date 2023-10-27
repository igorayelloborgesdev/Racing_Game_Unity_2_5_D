using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RacingEditor
{
#if (UNITY_EDITOR)
    #region Editor
    [MenuItem("Racing Tools/Road/Load Track Line")]
    private static void LoadTrackLine()
    {
        GetTrackGenerator().GetComponent<RacingTrackGenerator>().CreateTrack();
    }
    [MenuItem("Racing Tools/Street/Load Street Track Line")]
    private static void LoadStreetTrackLine()
    {        
        GetTrackGenerator().GetComponent<RacingTrackGenerator>().CreateTrackStreet();
    }
    [MenuItem("Racing Tools/Street/Change Street Track Texture")]
    private static void ChangeTrackTextureStreet()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        List<GameObject> someList = new List<GameObject>(selectedObjs);
        someList = someList.OrderBy(x => x.GetComponent<Asphalt>().id).ToList();
        for (int i = 0; i < someList.Count; i++)
        {
            trackGenerator.GetComponent<RacingTrackGenerator>().ChangeTextureStreet(someList[i], (i % 2));
        }
    }
    [MenuItem("Racing Tools/Road/Change Track Texture")]
    private static void ChangeTrackTexture()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        List<GameObject> someList = new List<GameObject>(selectedObjs);
        someList = someList.OrderBy(x => x.GetComponent<Asphalt>().id).ToList();
        for (int i = 0; i < someList.Count; i++)
        {
            trackGenerator.GetComponent<RacingTrackGenerator>().ChangeTexture(someList[i]);
        }
    }
    [MenuItem("Racing Tools/Change Track Grid")]
    private static void ChangeTrackGrid()
    {

        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        List<GameObject> someList = new List<GameObject>(selectedObjs);
        someList = someList.OrderBy(x => x.GetComponent<Asphalt>().id).ToList();
        trackGenerator.GetComponent<RacingTrackGenerator>().ChangeTextureGrid(someList, someList[0].GetComponent<Asphalt>().id, someList[someList.Count - 1].GetComponent<Asphalt>().id);
    }
    [MenuItem("Racing Tools/Add Directional Board Left")]
    private static void AddDirectionalBoardLeft()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        List<GameObject> someList = new List<GameObject>(selectedObjs);
        someList = someList.OrderBy(x => x.GetComponent<Asphalt>().id).ToList();
        trackGenerator.GetComponent<RacingTrackGenerator>().AddDirectionalBoardLeft(someList[0].GetComponent<Asphalt>().id, someList[someList.Count - 1].GetComponent<Asphalt>().id);
    }
    [MenuItem("Racing Tools/Add Directional Board Right")]
    private static void AddDirectionalBoardRight()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        List<GameObject> someList = new List<GameObject>(selectedObjs);
        someList = someList.OrderBy(x => x.GetComponent<Asphalt>().id).ToList();
        trackGenerator.GetComponent<RacingTrackGenerator>().AddDirectionalBoardRight(someList[0].GetComponent<Asphalt>().id, someList[someList.Count - 1].GetComponent<Asphalt>().id);
    }    
    [MenuItem("Racing Tools/Add Sponsor Center")]
    private static void AddSponsorCenter()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        int id = new List<GameObject>(selectedObjs).FirstOrDefault().GetComponent<Asphalt>().id;
        trackGenerator.GetComponent<RacingTrackGenerator>().AddSponsorCenter(id);
    }
    [MenuItem("Racing Tools/Road/Add Sponsor Left")]
    private static void AddSponsorLeft()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        int id = new List<GameObject>(selectedObjs).FirstOrDefault().GetComponent<Grass>().id;
        trackGenerator.GetComponent<RacingTrackGenerator>().AddSponsorLeft(id);
    }
    [MenuItem("Racing Tools/Road/Add Sponsor Right")]
    private static void AddSponsorRight()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        int id = new List<GameObject>(selectedObjs).FirstOrDefault().GetComponent<Grass>().id;
        trackGenerator.GetComponent<RacingTrackGenerator>().AddSponsorRight(id);
    }    
    [MenuItem("Racing Tools/Street/ Add Street Sponsor Left")]
    private static void AddStreetSponsorLeft()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        int id = new List<GameObject>(selectedObjs).FirstOrDefault().GetComponent<Grass>().id;
        trackGenerator.GetComponent<RacingTrackGenerator>().AddSponsorStreetLeft(id);
    }
    [MenuItem("Racing Tools/Street/Add Street Sponsor Right")]
    private static void AddStreetSponsorRight()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        int id = new List<GameObject>(selectedObjs).FirstOrDefault().GetComponent<Grass>().id;
        trackGenerator.GetComponent<RacingTrackGenerator>().AddSponsorStreetRight(id);
    }    
    [MenuItem("Racing Tools/Way Point 1 %&a")]
    private static void WayPoint1()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        List<GameObject> someList = new List<GameObject>(selectedObjs);
        someList = someList.OrderBy(x => x.GetComponent<Asphalt>().id).ToList();
        trackGenerator.GetComponent<RacingTrackGenerator>().WayPoint(someList, someList[0].GetComponent<Asphalt>().id, someList[someList.Count - 1].GetComponent<Asphalt>().id, new int[] { 1, 2, 3 });
    }
    [MenuItem("Racing Tools/Way Point 2 %&s")]
    private static void WayPoint2()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        List<GameObject> someList = new List<GameObject>(selectedObjs);
        someList = someList.OrderBy(x => x.GetComponent<Asphalt>().id).ToList();
        trackGenerator.GetComponent<RacingTrackGenerator>().WayPoint(someList, someList[0].GetComponent<Asphalt>().id, someList[someList.Count - 1].GetComponent<Asphalt>().id, new int[] { 3, 1, 2 });
    }
    [MenuItem("Racing Tools/Way Point 3 %&d")]
    private static void WayPoint3()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] selectedObjs = Selection.gameObjects;
        List<GameObject> someList = new List<GameObject>(selectedObjs);
        someList = someList.OrderBy(x => x.GetComponent<Asphalt>().id).ToList();
        trackGenerator.GetComponent<RacingTrackGenerator>().WayPoint(someList, someList[0].GetComponent<Asphalt>().id, someList[someList.Count - 1].GetComponent<Asphalt>().id, new int[] { 2, 3, 1 });
    }
    [MenuItem("Racing Tools/Way Point Hide Show %h")]
    private static void WayPointHideShow()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("WayPoint");
        foreach (GameObject trackGenerator in trackGeneratorList)
            trackGenerator.GetComponent<MeshRenderer>().enabled = !trackGenerator.GetComponent<MeshRenderer>().enabled;
    }
    [MenuItem("Racing Tools/Save Track")]
    private static void SaveTrack()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        GameObject trackGenerator = trackGeneratorList[0];
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>().ToArray();
        trackGenerator.GetComponent<RacingTrackGenerator>().Save(allObjects);
    }
    #endregion
#endif
    #region Build
    private static GameObject GetTrackGenerator()
    {
        GameObject[] trackGeneratorList = GameObject.FindGameObjectsWithTag("trackGenerator");
        return trackGeneratorList[0];
    }
    #endregion
}
