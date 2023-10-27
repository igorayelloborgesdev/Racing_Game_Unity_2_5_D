using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RacingTrackGenerator : MonoBehaviour
{
    #region Serialized field
    [SerializeField]
    private GameObject asphalt;
    [SerializeField]
    private Material material1;
    [SerializeField]
    private Material material2;
    [SerializeField]
    private Material materialGrass1;
    [SerializeField]
    private Material materialGrass2;
    [SerializeField]
    private Material materialGuardRail;
    [SerializeField]
    private Material material1asphalt;
    [SerializeField]
    private Material material2asphalt;
    [SerializeField]
    private Material material2asphaltStreet;
    [SerializeField]
    private Material material1RoadAsphalt;
    [SerializeField]
    private Material material2RoadAsphalt;
    [SerializeField]
    private Material material1grid;
    [SerializeField]
    private Material material2grid;
    [SerializeField]
    private Material asfalto1_Tunel;
    [SerializeField]
    private Material asfalto2_Tunel;
    [SerializeField]
    private Material asfalto1_1Tunel;
    [SerializeField]
    private Material asfalto1_2Tunel;
    [SerializeField]
    private Material grass1_Tunel;
    [SerializeField]
    private Material grass2_Tunel;
    [SerializeField]
    private Material guardrail_Tunel;
    [SerializeField]
    private Material tunel_Wall;
    [SerializeField]
    private Material roof1;
    [SerializeField]
    private Material roof2;
    [SerializeField]
    private Material pilarMaterial;
    [SerializeField]
    private Material tunelMaterial;
    [SerializeField]
    private Material boardLeftMaterial;
    [SerializeField]
    private Material boardRightMaterial;
    [SerializeField]
    private Material asfalto1Rua;
    [SerializeField]
    private Material asfalto2Rua;
    [SerializeField]
    private Material grass1Rua;
    [SerializeField]
    private Material grass2Rua;
    [SerializeField]
    private Material pitBoardLeft;
    [SerializeField]
    private Material pitBoardRight;
    [SerializeField]
    private Material treeMaterial;
    [SerializeField]
    private Material sponsorMaterial;
    [SerializeField]
    private Material sponsorMaterialCenter;
    [SerializeField]
    private Material finishLine1Material;
    [SerializeField]
    private Material finishLine2Material;
    [SerializeField]
    private GameObject[] prefabs;
    [SerializeField]
    private Material[] prefabsMaterial;
    #endregion
    #region Variables
    private float guardRailHeight = 0.015f;
    private int difficultID = 0;
    private float[] difficultArray = { -0.002f, 0.000f, 0.003f };//<-
    #endregion
    #region Methods
    public void CreateTrack()
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOMesh = new TrackSegmentDTO();
        trackSegmentDTOMesh.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTO.trackVertexDTOArray)
        {
            for (int i = 0; i < trackVertexDTO.pointsArray.Count; i += 2)
            {
                Vector3 diference = trackVertexDTO.pointsArray[i + 1] - trackVertexDTO.pointsArray[i];
                float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;
                float hyp = 0.075f;
                float co = Mathf.Sin(angleRadians) * hyp;
                float ca = Mathf.Cos(angleRadians) * hyp;
                Vector3 point1 = Vector3.zero;
                if (trackVertexDTO.pointsArray[i].z > trackVertexDTO.pointsArray[i + 1].z)
                    point1 = new Vector3(trackVertexDTO.pointsArray[i].x + (ca), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (-1.0f * co));
                else
                    point1 = new Vector3(trackVertexDTO.pointsArray[i].x + (ca), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (co));

                Vector3 point2 = Vector3.zero;
                if (trackVertexDTO.pointsArray[i].z > trackVertexDTO.pointsArray[i + 1].z)
                    point2 = new Vector3(trackVertexDTO.pointsArray[i].x + (-1.0f * ca), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (co));
                else
                    point2 = new Vector3(trackVertexDTO.pointsArray[i].x + (-1.0f * ca), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (-1.0f * co));

                float hyp1 = 0.2f;
                float co1 = Mathf.Sin(angleRadians) * hyp1;
                float ca1 = Mathf.Cos(angleRadians) * hyp1;
                Vector3 point1grass = Vector3.zero;
                if (trackVertexDTO.pointsArray[i].z > trackVertexDTO.pointsArray[i + 1].z)
                    point1grass = new Vector3(trackVertexDTO.pointsArray[i].x + (ca1), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (-1.0f * co1));
                else
                    point1grass = new Vector3(trackVertexDTO.pointsArray[i].x + (ca1), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (co1));
                Vector3 point2grass = Vector3.zero;
                if (trackVertexDTO.pointsArray[i].z > trackVertexDTO.pointsArray[i + 1].z)
                    point2grass = new Vector3(trackVertexDTO.pointsArray[i].x + (-1.0f * ca1), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (co1));
                else
                    point2grass = new Vector3(trackVertexDTO.pointsArray[i].x + (-1.0f * ca1), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (-1.0f * co1));

                if (index != 0)
                {
                    trackSegmentDTO1.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTO1.trackVertexDTOArray[trackSegmentDTO1.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTO1.trackVertexDTOArray[trackSegmentDTO1.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint1);//0                    
                    trackSegmentDTO1.trackVertexDTOArray[trackSegmentDTO1.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint2);//1                        
                    trackSegmentDTO1.trackVertexDTOArray[trackSegmentDTO1.trackVertexDTOArray.Count - 1].pointsArray.Add(point1);//2                    
                    trackSegmentDTO1.trackVertexDTOArray[trackSegmentDTO1.trackVertexDTOArray.Count - 1].pointsArray.Add(point2);//3

                    trackSegmentDTOGrass1.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTOGrass1.trackVertexDTOArray[trackSegmentDTOGrass1.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTOGrass1.trackVertexDTOArray[trackSegmentDTOGrass1.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint2);//0                    
                    trackSegmentDTOGrass1.trackVertexDTOArray[trackSegmentDTOGrass1.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint2Grass);//1                        
                    trackSegmentDTOGrass1.trackVertexDTOArray[trackSegmentDTOGrass1.trackVertexDTOArray.Count - 1].pointsArray.Add(point2);//2                    
                    trackSegmentDTOGrass1.trackVertexDTOArray[trackSegmentDTOGrass1.trackVertexDTOArray.Count - 1].pointsArray.Add(point2grass);//3

                    trackSegmentDTOGrass2.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTOGrass2.trackVertexDTOArray[trackSegmentDTOGrass2.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTOGrass2.trackVertexDTOArray[trackSegmentDTOGrass2.trackVertexDTOArray.Count - 1].pointsArray.Add(point1);//0                    
                    trackSegmentDTOGrass2.trackVertexDTOArray[trackSegmentDTOGrass2.trackVertexDTOArray.Count - 1].pointsArray.Add(point1grass);//1                        
                    trackSegmentDTOGrass2.trackVertexDTOArray[trackSegmentDTOGrass2.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint1);//2                    
                    trackSegmentDTOGrass2.trackVertexDTOArray[trackSegmentDTOGrass2.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint1Grass);//3

                    trackSegmentDTOGuardRail1.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray.Add(point2grass);//0                                        
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint2Grass);//2                    
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray.Add(new Vector3(point2grass.x, point2grass.y + guardRailHeight, point2grass.z));//1                        
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray.Add(new Vector3(initPoint2Grass.x, initPoint2Grass.y + guardRailHeight, initPoint2Grass.z));//3

                    trackSegmentDTOGuardRail2.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint1Grass);//2                    
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray.Add(point1grass);//0                                        
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray.Add(new Vector3(initPoint1Grass.x, initPoint1Grass.y + guardRailHeight, initPoint1Grass.z));//3
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray.Add(new Vector3(point1grass.x, point1grass.y + guardRailHeight, point1grass.z));//1                        




                    trackSegmentDTOMesh.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTOMesh.trackVertexDTOArray[trackSegmentDTOMesh.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTOMesh.trackVertexDTOArray[trackSegmentDTOMesh.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint2Grass);//2                    
                    trackSegmentDTOMesh.trackVertexDTOArray[trackSegmentDTOMesh.trackVertexDTOArray.Count - 1].pointsArray.Add(point2grass);//0                                        
                    trackSegmentDTOMesh.trackVertexDTOArray[trackSegmentDTOMesh.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint1Grass);//3
                    trackSegmentDTOMesh.trackVertexDTOArray[trackSegmentDTOMesh.trackVertexDTOArray.Count - 1].pointsArray.Add(point1grass);//1                        

                }

                initPoint1 = point1;
                initPoint2 = point2;

                initPoint1Grass = point1grass;
                initPoint2Grass = point2grass;

            }
            index++;
        }


        List<List<Vector3>> coordenadas1 = new List<List<Vector3>>();
        List<int> triangles1 = new List<int>();


        List<Vector3> coordenadasAsfalto = new List<Vector3>();
        List<int> trianglesPos = new List<int>();
        int materialIndex = 0;
        //Asphalt
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTO1.trackVertexDTOArray)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            //DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            //Mesh mesh = new Mesh();
            //mesh.vertices = plane.GetComponent<MeshFilter>().mesh.vertices;
            //mesh.triangles = plane.GetComponent<MeshFilter>().mesh.triangles;

            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);//2
            vertices[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);//0
            vertices[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);//1
            vertices[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);//3

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            //plane.AddComponent<MeshCollider>();
            //plane.GetComponent<MeshCollider>().convex = true;
            //plane.GetComponent<MeshCollider>().isTrigger = true;

            plane.GetComponent<Renderer>().material = materialIndex % 2 == 0 ? material1asphalt : material2asphalt;

            plane.AddComponent<Asphalt>();
            plane.GetComponent<Asphalt>().id = materialIndex;
            plane.GetComponent<Asphalt>().objName = "asphalt";
            plane.GetComponent<Asphalt>().className = "Asphalt";
            plane.GetComponent<Asphalt>().tagName = "asphalt";
            plane.GetComponent<Asphalt>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Asphalt>().pointsArray = new Vector3[4];
            plane.GetComponent<Asphalt>().pointsArray[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            plane.GetComponent<Asphalt>().pointsArray[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            plane.GetComponent<Asphalt>().pointsArray[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            plane.GetComponent<Asphalt>().pointsArray[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            plane.name = "asphalt";
            plane.tag = "asphalt";

            materialIndex++;

            coordenadasAsfalto.Add(plane.GetComponent<Asphalt>().pointsArray[2]);
            coordenadasAsfalto.Add(plane.GetComponent<Asphalt>().pointsArray[3]);
            coordenadasAsfalto.Add(plane.GetComponent<Asphalt>().pointsArray[1]);
            coordenadasAsfalto.Add(plane.GetComponent<Asphalt>().pointsArray[0]);
            trianglesPos.Add(0 + ((materialIndex - 1) * 4));
            trianglesPos.Add(1 + ((materialIndex - 1) * 4));
            trianglesPos.Add(2 + ((materialIndex - 1) * 4));
            trianglesPos.Add(2 + ((materialIndex - 1) * 4));
            trianglesPos.Add(1 + ((materialIndex - 1) * 4));
            trianglesPos.Add(3 + ((materialIndex - 1) * 4));
        }

        List<Vector3> coordenadasGrass1 = new List<Vector3>();
        List<int> trianglesPosGrass1 = new List<int>();
        int materialIndex1 = 0;

        materialGrass2.mainTexture = Resources.Load("grass3_2", typeof(Texture2D)) as Texture;
        materialGrass1.mainTexture = Resources.Load("grass3_1", typeof(Texture2D)) as Texture;
        
        //Grass
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTOGrass1.trackVertexDTOArray)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            //DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            vertices[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            vertices[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            vertices[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            //plane.AddComponent<MeshCollider>();
            //plane.GetComponent<MeshCollider>().convex = true;
            //plane.GetComponent<MeshCollider>().isTrigger = true;

            plane.GetComponent<Renderer>().material = materialIndex1 % 2 == 0 ? materialGrass2 : materialGrass1;
            
            plane.AddComponent<Grass>();
            plane.GetComponent<Grass>().id = materialIndex1;
            plane.GetComponent<Grass>().isLeft = true;

            plane.GetComponent<Grass>().objName = "grass";
            plane.GetComponent<Grass>().className = "grass";
            plane.GetComponent<Grass>().tagName = "grass";
            plane.GetComponent<Grass>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Grass>().pointsArray = new Vector3[4];
            plane.GetComponent<Grass>().pointsArray[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            plane.GetComponent<Grass>().pointsArray[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            plane.GetComponent<Grass>().pointsArray[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            plane.GetComponent<Grass>().pointsArray[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            plane.name = "grass";
            plane.tag = "grass";

            materialIndex1++;

            coordenadasGrass1.Add(plane.GetComponent<Grass>().pointsArray[2]);
            coordenadasGrass1.Add(plane.GetComponent<Grass>().pointsArray[3]);
            coordenadasGrass1.Add(plane.GetComponent<Grass>().pointsArray[1]);
            coordenadasGrass1.Add(plane.GetComponent<Grass>().pointsArray[0]);
            trianglesPosGrass1.Add(0 + ((materialIndex1 - 1) * 4));
            trianglesPosGrass1.Add(1 + ((materialIndex1 - 1) * 4));
            trianglesPosGrass1.Add(2 + ((materialIndex1 - 1) * 4));
            trianglesPosGrass1.Add(2 + ((materialIndex1 - 1) * 4));
            trianglesPosGrass1.Add(1 + ((materialIndex1 - 1) * 4));
            trianglesPosGrass1.Add(3 + ((materialIndex1 - 1) * 4));

        }
        List<Vector3> coordenadasGrass2 = new List<Vector3>();
        List<int> trianglesPosGrass2 = new List<int>();
        int materialIndex2 = 0;
        //Grass
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTOGrass2.trackVertexDTOArray)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            //DestroyImmediate(plane.GetComponent<MeshCollider>());            
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            vertices[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            vertices[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            vertices[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            //plane.AddComponent<MeshCollider>();
            //plane.GetComponent<MeshCollider>().convex = true;
            //plane.GetComponent<MeshCollider>().isTrigger = true;

            plane.GetComponent<Renderer>().material = materialIndex2 % 2 == 0 ? materialGrass2 : materialGrass1;

            plane.AddComponent<Grass>();
            plane.GetComponent<Grass>().id = materialIndex2;
            plane.GetComponent<Grass>().isLeft = false;

            plane.GetComponent<Grass>().objName = "grass";
            plane.GetComponent<Grass>().className = "grass";
            plane.GetComponent<Grass>().tagName = "grass";
            plane.GetComponent<Grass>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Grass>().pointsArray = new Vector3[4];
            plane.GetComponent<Grass>().pointsArray[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            plane.GetComponent<Grass>().pointsArray[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            plane.GetComponent<Grass>().pointsArray[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            plane.GetComponent<Grass>().pointsArray[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            plane.name = "grass";
            plane.tag = "grass";

            materialIndex2++;

            coordenadasGrass2.Add(plane.GetComponent<Grass>().pointsArray[2]);
            coordenadasGrass2.Add(plane.GetComponent<Grass>().pointsArray[3]);
            coordenadasGrass2.Add(plane.GetComponent<Grass>().pointsArray[1]);
            coordenadasGrass2.Add(plane.GetComponent<Grass>().pointsArray[0]);
            trianglesPosGrass2.Add(0 + ((materialIndex2 - 1) * 4));
            trianglesPosGrass2.Add(1 + ((materialIndex2 - 1) * 4));
            trianglesPosGrass2.Add(2 + ((materialIndex2 - 1) * 4));
            trianglesPosGrass2.Add(2 + ((materialIndex2 - 1) * 4));
            trianglesPosGrass2.Add(1 + ((materialIndex2 - 1) * 4));
            trianglesPosGrass2.Add(3 + ((materialIndex2 - 1) * 4));

        }

        int materialIndex3 = 0;
        //Guardrail
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTOGuardRail1.trackVertexDTOArray)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            vertices[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            vertices[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            vertices[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<BoxCollider>();
            plane.GetComponent<BoxCollider>().isTrigger = true;

            plane.GetComponent<Renderer>().material = materialGuardRail;

            plane.AddComponent<Guardrail>();
            plane.GetComponent<Guardrail>().id = materialIndex3;
            plane.GetComponent<Guardrail>().isLeft = true;

            plane.GetComponent<Guardrail>().objName = "guardrail";
            plane.GetComponent<Guardrail>().className = "guardrail";
            plane.GetComponent<Guardrail>().tagName = "guardrail";
            plane.GetComponent<Guardrail>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Guardrail>().pointsArray = new Vector3[4];
            plane.GetComponent<Guardrail>().pointsArray[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            plane.GetComponent<Guardrail>().pointsArray[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            plane.GetComponent<Guardrail>().pointsArray[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            plane.GetComponent<Guardrail>().pointsArray[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            plane.name = "guardrail";
            plane.tag = "guardrail";

            materialIndex3++;
        }

        int materialIndex4 = 0;
        //Guardrail
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTOGuardRail2.trackVertexDTOArray)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            vertices[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            vertices[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            vertices[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<BoxCollider>();
            plane.GetComponent<BoxCollider>().isTrigger = true;

            plane.GetComponent<Renderer>().material = materialGuardRail;

            plane.AddComponent<Guardrail>();
            plane.GetComponent<Guardrail>().id = materialIndex4;
            plane.GetComponent<Guardrail>().isLeft = false;

            plane.GetComponent<Guardrail>().objName = "guardrail";
            plane.GetComponent<Guardrail>().className = "guardrail";
            plane.GetComponent<Guardrail>().tagName = "guardrail";
            plane.GetComponent<Guardrail>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Guardrail>().pointsArray = new Vector3[4];
            plane.GetComponent<Guardrail>().pointsArray[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            plane.GetComponent<Guardrail>().pointsArray[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            plane.GetComponent<Guardrail>().pointsArray[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            plane.GetComponent<Guardrail>().pointsArray[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            plane.name = "guardrail";
            plane.tag = "guardrail";

            materialIndex4++;
        }

        GameObject gameObjectFinal = new GameObject();
        gameObjectFinal.name = "meshCoordenadas";
        gameObjectFinal.tag = "meshCoordinates";
        List<Vector3> coordenadasLista = new List<Vector3>();
        int counter = 0;
        foreach (TrackVertexDTO coordenada in trackSegmentDTOMesh.trackVertexDTOArray)
        {
            foreach (Vector3 coord in coordenada.pointsArray)
            {
                coordenadasLista.Add(coord);
            }
            triangles1.Add(0 + ((counter) * 4));
            triangles1.Add(1 + ((counter) * 4));
            triangles1.Add(2 + ((counter) * 4));
            triangles1.Add(2 + ((counter) * 4));
            triangles1.Add(1 + ((counter) * 4));
            triangles1.Add(3 + ((counter) * 4));
            counter++;
        }

        gameObjectFinal.AddComponent<MeshCoordenadas>();
        gameObjectFinal.GetComponent<MeshCoordenadas>().coordenadasMesh = coordenadasLista.ToArray();
        gameObjectFinal.GetComponent<MeshCoordenadas>().trianglesPos = triangles1.ToArray();
        GameObject objGrass2 = new GameObject();
        objGrass2.AddComponent<MeshFilter>();
        objGrass2.AddComponent<MeshRenderer>();
        Mesh mesh2Grass = objGrass2.GetComponent<MeshFilter>().mesh;
        mesh2Grass.vertices = gameObjectFinal.GetComponent<MeshCoordenadas>().coordenadasMesh;
        mesh2Grass.triangles = gameObjectFinal.GetComponent<MeshCoordenadas>().trianglesPos;
        objGrass2.AddComponent<MeshCollider>();
        objGrass2.name = "meshCollider";
        DestroyImmediate(objGrass2.GetComponent<MeshRenderer>());

    }
    public void CreateTrackStreet()
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOMesh = new TrackSegmentDTO();
        trackSegmentDTOMesh.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTO.trackVertexDTOArray)
        {
            for (int i = 0; i < trackVertexDTO.pointsArray.Count; i += 2)
            {
                Vector3 diference = trackVertexDTO.pointsArray[i + 1] - trackVertexDTO.pointsArray[i];
                float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;
                float hyp = 0.075f;
                float co = Mathf.Sin(angleRadians) * hyp;
                float ca = Mathf.Cos(angleRadians) * hyp;
                Vector3 point1 = Vector3.zero;
                if (trackVertexDTO.pointsArray[i].z > trackVertexDTO.pointsArray[i + 1].z)
                    point1 = new Vector3(trackVertexDTO.pointsArray[i].x + (ca), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (-1.0f * co));
                else
                    point1 = new Vector3(trackVertexDTO.pointsArray[i].x + (ca), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (co));

                Vector3 point2 = Vector3.zero;
                if (trackVertexDTO.pointsArray[i].z > trackVertexDTO.pointsArray[i + 1].z)
                    point2 = new Vector3(trackVertexDTO.pointsArray[i].x + (-1.0f * ca), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (co));
                else
                    point2 = new Vector3(trackVertexDTO.pointsArray[i].x + (-1.0f * ca), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (-1.0f * co));

                float hyp1 = 0.2f;
                float co1 = Mathf.Sin(angleRadians) * hyp1;
                float ca1 = Mathf.Cos(angleRadians) * hyp1;
                Vector3 point1grass = Vector3.zero;
                if (trackVertexDTO.pointsArray[i].z > trackVertexDTO.pointsArray[i + 1].z)
                    point1grass = new Vector3(trackVertexDTO.pointsArray[i].x + (ca1), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (-1.0f * co1));
                else
                    point1grass = new Vector3(trackVertexDTO.pointsArray[i].x + (ca1), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (co1));
                Vector3 point2grass = Vector3.zero;
                if (trackVertexDTO.pointsArray[i].z > trackVertexDTO.pointsArray[i + 1].z)
                    point2grass = new Vector3(trackVertexDTO.pointsArray[i].x + (-1.0f * ca1), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (co1));
                else
                    point2grass = new Vector3(trackVertexDTO.pointsArray[i].x + (-1.0f * ca1), trackVertexDTO.pointsArray[i].y, trackVertexDTO.pointsArray[i].z + (-1.0f * co1));

                if (index != 0)
                {
                    trackSegmentDTO1.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTO1.trackVertexDTOArray[trackSegmentDTO1.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTO1.trackVertexDTOArray[trackSegmentDTO1.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint1);//0                    
                    trackSegmentDTO1.trackVertexDTOArray[trackSegmentDTO1.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint2);//1                        
                    trackSegmentDTO1.trackVertexDTOArray[trackSegmentDTO1.trackVertexDTOArray.Count - 1].pointsArray.Add(point1);//2                    
                    trackSegmentDTO1.trackVertexDTOArray[trackSegmentDTO1.trackVertexDTOArray.Count - 1].pointsArray.Add(point2);//3
                    
                    trackSegmentDTOGrass1.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTOGrass1.trackVertexDTOArray[trackSegmentDTOGrass1.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTOGrass1.trackVertexDTOArray[trackSegmentDTOGrass1.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint2);//0                    
                    trackSegmentDTOGrass1.trackVertexDTOArray[trackSegmentDTOGrass1.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint2Grass);//1                        
                    trackSegmentDTOGrass1.trackVertexDTOArray[trackSegmentDTOGrass1.trackVertexDTOArray.Count - 1].pointsArray.Add(point2);//2                    
                    trackSegmentDTOGrass1.trackVertexDTOArray[trackSegmentDTOGrass1.trackVertexDTOArray.Count - 1].pointsArray.Add(point2grass);//3

                    trackSegmentDTOGrass2.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTOGrass2.trackVertexDTOArray[trackSegmentDTOGrass2.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTOGrass2.trackVertexDTOArray[trackSegmentDTOGrass2.trackVertexDTOArray.Count - 1].pointsArray.Add(point1);//0                    
                    trackSegmentDTOGrass2.trackVertexDTOArray[trackSegmentDTOGrass2.trackVertexDTOArray.Count - 1].pointsArray.Add(point1grass);//1                        
                    trackSegmentDTOGrass2.trackVertexDTOArray[trackSegmentDTOGrass2.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint1);//2                    
                    trackSegmentDTOGrass2.trackVertexDTOArray[trackSegmentDTOGrass2.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint1Grass);//3

                    trackSegmentDTOGuardRail2.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint1);//0                    
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray.Add(point1);//1                                    
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray.Add(new Vector3(initPoint1.x, initPoint1.y + guardRailHeight, initPoint1.z));//2
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray.Add(new Vector3(point1.x, point1.y + guardRailHeight, point1.z));//3      

                    trackSegmentDTOGuardRail1.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray.Add(point2);//1
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint2);//0
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray.Add(new Vector3(point2.x, point2.y + guardRailHeight, point2.z));//3
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray.Add(new Vector3(initPoint2.x, initPoint2.y + guardRailHeight, initPoint2.z));//2                    

                    trackSegmentDTOMesh.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTOMesh.trackVertexDTOArray[trackSegmentDTOMesh.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTOMesh.trackVertexDTOArray[trackSegmentDTOMesh.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint2Grass);//2                    
                    trackSegmentDTOMesh.trackVertexDTOArray[trackSegmentDTOMesh.trackVertexDTOArray.Count - 1].pointsArray.Add(point2grass);//0                                        
                    trackSegmentDTOMesh.trackVertexDTOArray[trackSegmentDTOMesh.trackVertexDTOArray.Count - 1].pointsArray.Add(initPoint1Grass);//3
                    trackSegmentDTOMesh.trackVertexDTOArray[trackSegmentDTOMesh.trackVertexDTOArray.Count - 1].pointsArray.Add(point1grass);//1         

                }

                initPoint1 = point1;
                initPoint2 = point2;

                initPoint1Grass = point1grass;
                initPoint2Grass = point2grass;

            }
            index++;
        }

        int materialIndex = 0;
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTO1.trackVertexDTOArray)
        {
            try
            {
                GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
                DestroyImmediate(plane.GetComponent<MeshCollider>());
                Mesh mesh = plane.GetComponent<MeshFilter>().mesh;

                Vector3[] vertices = mesh.vertices;

                vertices[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);//2
                vertices[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);//0
                vertices[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);//1
                vertices[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);//3

                mesh.vertices = vertices;
                mesh.RecalculateBounds();
                plane.AddComponent<MeshCollider>();

                plane.GetComponent<Renderer>().material = materialIndex % 2 == 0 ? material1 : asfalto1Rua;

                plane.AddComponent<Asphalt>();
                plane.GetComponent<Asphalt>().id = materialIndex;
                plane.GetComponent<Asphalt>().objName = "asphalt";
                plane.GetComponent<Asphalt>().className = "Asphalt";
                plane.GetComponent<Asphalt>().tagName = "asphalt";
                plane.GetComponent<Asphalt>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
                plane.GetComponent<Asphalt>().pointsArray = new Vector3[4];
                plane.GetComponent<Asphalt>().pointsArray[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
                plane.GetComponent<Asphalt>().pointsArray[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
                plane.GetComponent<Asphalt>().pointsArray[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
                plane.GetComponent<Asphalt>().pointsArray[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

                plane.name = "asphalt";
                plane.tag = "asphalt";

                materialIndex++;
            }
            catch (Exception ex) { }

        }

        int materialIndex1 = 0;
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTOGrass1.trackVertexDTOArray)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            vertices[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            vertices[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            vertices[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();

            plane.GetComponent<Renderer>().material = materialIndex1 % 2 == 0 ? grass1Rua : grass2Rua;

            plane.AddComponent<Grass>();
            plane.GetComponent<Grass>().id = materialIndex1;
            plane.GetComponent<Grass>().isLeft = true;

            plane.GetComponent<Grass>().objName = "grass";
            plane.GetComponent<Grass>().className = "grass";
            plane.GetComponent<Grass>().tagName = "grass";
            plane.GetComponent<Grass>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Grass>().pointsArray = new Vector3[4];
            plane.GetComponent<Grass>().pointsArray[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            plane.GetComponent<Grass>().pointsArray[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            plane.GetComponent<Grass>().pointsArray[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            plane.GetComponent<Grass>().pointsArray[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            plane.name = "grass";
            plane.tag = "grass";

            materialIndex1++;
        }

        int materialIndex2 = 0;
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTOGrass2.trackVertexDTOArray)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            vertices[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            vertices[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            vertices[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();

            plane.GetComponent<Renderer>().material = materialIndex2 % 2 == 0 ? grass1Rua : grass2Rua;

            plane.AddComponent<Grass>();
            plane.GetComponent<Grass>().id = materialIndex2;
            plane.GetComponent<Grass>().isLeft = false;

            plane.GetComponent<Grass>().objName = "grass";
            plane.GetComponent<Grass>().className = "grass";
            plane.GetComponent<Grass>().tagName = "grass";
            plane.GetComponent<Grass>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Grass>().pointsArray = new Vector3[4];
            plane.GetComponent<Grass>().pointsArray[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            plane.GetComponent<Grass>().pointsArray[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            plane.GetComponent<Grass>().pointsArray[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            plane.GetComponent<Grass>().pointsArray[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            plane.name = "grass";
            plane.tag = "grass";

            materialIndex2++;
        }
        int materialIndex3 = 0;
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTOGuardRail1.trackVertexDTOArray)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            vertices[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            vertices[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            vertices[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();

            plane.GetComponent<Renderer>().material = materialGuardRail;

            plane.AddComponent<Guardrail>();
            plane.GetComponent<Guardrail>().id = materialIndex3;
            plane.GetComponent<Guardrail>().isLeft = true;

            plane.GetComponent<Guardrail>().objName = "guardrail";
            plane.GetComponent<Guardrail>().className = "guardrail";
            plane.GetComponent<Guardrail>().tagName = "guardrail";
            plane.GetComponent<Guardrail>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Guardrail>().pointsArray = new Vector3[4];
            plane.GetComponent<Guardrail>().pointsArray[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            plane.GetComponent<Guardrail>().pointsArray[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            plane.GetComponent<Guardrail>().pointsArray[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            plane.GetComponent<Guardrail>().pointsArray[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            plane.name = "guardrail";
            plane.tag = "guardrail";

            materialIndex3++;
        }

        int materialIndex4 = 0;
        foreach (TrackVertexDTO trackVertexDTO in trackSegmentDTOGuardRail2.trackVertexDTOArray)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            vertices[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            vertices[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            vertices[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();

            plane.GetComponent<Renderer>().material = materialGuardRail;

            plane.AddComponent<Guardrail>();
            plane.GetComponent<Guardrail>().id = materialIndex4;
            plane.GetComponent<Guardrail>().isLeft = false;

            plane.GetComponent<Guardrail>().objName = "guardrail";
            plane.GetComponent<Guardrail>().className = "guardrail";
            plane.GetComponent<Guardrail>().tagName = "guardrail";
            plane.GetComponent<Guardrail>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Guardrail>().pointsArray = new Vector3[4];
            plane.GetComponent<Guardrail>().pointsArray[0] = new Vector3(trackVertexDTO.pointsArray[2].x, trackVertexDTO.pointsArray[2].y, trackVertexDTO.pointsArray[2].z);
            plane.GetComponent<Guardrail>().pointsArray[1] = new Vector3(trackVertexDTO.pointsArray[0].x, trackVertexDTO.pointsArray[0].y, trackVertexDTO.pointsArray[0].z);
            plane.GetComponent<Guardrail>().pointsArray[2] = new Vector3(trackVertexDTO.pointsArray[1].x, trackVertexDTO.pointsArray[1].y, trackVertexDTO.pointsArray[1].z);
            plane.GetComponent<Guardrail>().pointsArray[3] = new Vector3(trackVertexDTO.pointsArray[3].x, trackVertexDTO.pointsArray[3].y, trackVertexDTO.pointsArray[3].z);

            plane.name = "guardrail";
            plane.tag = "guardrail";

            materialIndex4++;
        }
        List<List<Vector3>> coordenadas1 = new List<List<Vector3>>();
        List<int> triangles1 = new List<int>();

        GameObject gameObjectFinal = new GameObject();
        gameObjectFinal.name = "meshCoordenadas";
        gameObjectFinal.tag = "meshCoordinates";
        List<Vector3> coordenadasLista = new List<Vector3>();
        int counter = 0;
        //foreach (TrackVertexDTO coordenada in trackSegmentDTO1.trackVertexDTOArray)
        foreach (TrackVertexDTO coordenada in trackSegmentDTOMesh.trackVertexDTOArray)                
        {
            foreach (Vector3 coord in coordenada.pointsArray)
            {
                coordenadasLista.Add(coord);
            }
            triangles1.Add(0 + ((counter) * 4));
            triangles1.Add(1 + ((counter) * 4));
            triangles1.Add(2 + ((counter) * 4));
            triangles1.Add(2 + ((counter) * 4));
            triangles1.Add(1 + ((counter) * 4));
            triangles1.Add(3 + ((counter) * 4));
            counter++;
        }

        gameObjectFinal.AddComponent<MeshCoordenadas>();
        gameObjectFinal.GetComponent<MeshCoordenadas>().coordenadasMesh = coordenadasLista.ToArray();
        gameObjectFinal.GetComponent<MeshCoordenadas>().trianglesPos = triangles1.ToArray();
        GameObject objGrass2 = new GameObject();
        objGrass2.AddComponent<MeshFilter>();
        objGrass2.AddComponent<MeshRenderer>();
        Mesh mesh2Grass = objGrass2.GetComponent<MeshFilter>().mesh;
        mesh2Grass.vertices = gameObjectFinal.GetComponent<MeshCoordenadas>().coordenadasMesh;
        mesh2Grass.triangles = gameObjectFinal.GetComponent<MeshCoordenadas>().trianglesPos;
        objGrass2.AddComponent<MeshCollider>();
        objGrass2.name = "meshCollider";
        DestroyImmediate(objGrass2.GetComponent<MeshRenderer>());

    }
    public TrackSegmentDTO LoadTrackJson(int id = 0)
    {
        string fileName = "TrackLine_" + id.ToString();
        //string filePath = Resources.Load<TextAsset>(fileName).ToString();
        string dfs = Resources.Load<TextAsset>(fileName).ToString();
        //var dfs = File.ReadAllText(filePath);
        TrackSegmentDTO obj = JsonConvert.DeserializeObject<TrackSegmentDTO>(dfs);
        return obj;
    }
    #endregion

    #region Street
    public void ChangeTextureStreet(GameObject obj, int id)
    {
        obj.GetComponent<Renderer>().sharedMaterial = obj.GetComponent<Renderer>().material.mainTexture.name == "asfalto" ? material2asphaltStreet : asfalto2Rua;
        obj.GetComponent<Asphalt>().materialName = obj.GetComponent<Renderer>().sharedMaterial.name;
    }
    #endregion
    public void ChangeTexture(GameObject obj)
    {
        obj.GetComponent<Renderer>().material = obj.GetComponent<Renderer>().sharedMaterial.name == "asfalto1" ? material1RoadAsphalt : material2RoadAsphalt;
        obj.GetComponent<Asphalt>().materialName = obj.GetComponent<Renderer>().sharedMaterial.name;
    }


    public void ChangeTextureGrid(List<GameObject> obj, int idInit, int idEnd)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int i = idInit; i < idEnd + 1; i++)
        {
            for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[i].pointsArray.Count; j += 2)
            {
                Vector3 diference = trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j];
                float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;
                float hyp = 0.0325f;
                float co = Mathf.Sin(angleRadians) * hyp;
                float ca = Mathf.Cos(angleRadians) * hyp;
                Vector3 point1 = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point1 = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co));
                else
                    point1 = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co));

                Vector3 point2 = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point2 = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co));
                else
                    point2 = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co));

                trackSegmentDTOGuardRail1.trackVertexDTOArray.Add(new TrackVertexDTO());
                trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray.Add(point1);
                trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].angle = Vector3.Angle(diference, Vector3.right);

                trackSegmentDTOGuardRail2.trackVertexDTOArray.Add(new TrackVertexDTO());
                trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray.Add(point2);
                trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].angle = Vector3.Angle(diference, Vector3.right);

                initPoint1 = point1;
            }
            index++;
        }

        for (int i = 0; i < obj.Count(); i++)
        {
            if (i % 2 == 0)
            {
                if (i % 4 == 0)
                {
                    obj[i].GetComponent<Renderer>().sharedMaterial = material1grid;
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                    cube.transform.position = new Vector3(trackSegmentDTOGuardRail1.trackVertexDTOArray[i].pointsArray[0].x, trackSegmentDTOGuardRail1.trackVertexDTOArray[i].pointsArray[0].y + 0.001f, trackSegmentDTOGuardRail1.trackVertexDTOArray[i].pointsArray[0].z);
                    cube.transform.rotation = Quaternion.Euler(cube.transform.rotation.x, trackSegmentDTOGuardRail1.trackVertexDTOArray[i].angle, cube.transform.rotation.z);
                    cube.GetComponent<MeshRenderer>().enabled = false;
                    cube.name = "grid";
                    cube.tag = "grid";

                    cube.AddComponent<Grid>().id = (obj.Count() - 1) - i;
                    cube.GetComponent<Grid>().objName = "grid";
                    cube.GetComponent<Grid>().tagName = "grid";
                    cube.GetComponent<Grid>().localScale = cube.transform.localScale;
                    cube.GetComponent<Grid>().rotation = new Vector4(cube.transform.rotation.x, cube.transform.rotation.y, cube.transform.rotation.z, cube.transform.rotation.w);
                    cube.GetComponent<Grid>().points = cube.transform.position;
                }
                else
                {
                    obj[i].GetComponent<Renderer>().sharedMaterial = material2grid;
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                    cube.transform.position = new Vector3(trackSegmentDTOGuardRail2.trackVertexDTOArray[i].pointsArray[0].x, trackSegmentDTOGuardRail2.trackVertexDTOArray[i].pointsArray[0].y + 0.001f, trackSegmentDTOGuardRail2.trackVertexDTOArray[i].pointsArray[0].z);
                    cube.transform.rotation = Quaternion.Euler(cube.transform.rotation.x, trackSegmentDTOGuardRail2.trackVertexDTOArray[i].angle, cube.transform.rotation.z);
                    cube.GetComponent<MeshRenderer>().enabled = false;
                    cube.name = "grid";
                    cube.tag = "grid";

                    cube.AddComponent<Grid>().id = (obj.Count() - 1) - i;
                    cube.GetComponent<Grid>().objName = "grid";
                    cube.GetComponent<Grid>().tagName = "grid";
                    cube.GetComponent<Grid>().localScale = cube.transform.localScale;
                    cube.GetComponent<Grid>().rotation = new Vector4(cube.transform.rotation.x, cube.transform.rotation.y, cube.transform.rotation.z, cube.transform.rotation.w);
                    cube.GetComponent<Grid>().points = cube.transform.position;
                    cube.GetComponent<BoxCollider>().isTrigger = true;
                }
            }

        }

    }

    public void AddDirectionalBoardLeft(int idInit, int idEnd)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        float tunelHeight = 0.0625f;

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int i = idInit; i < idEnd + 2; i++)
        {
            for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[i].pointsArray.Count; j += 2)
            {

                Vector3 diference = trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j];
                float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

                float hyp1 = 0.135f;
                float co1 = Mathf.Sin(angleRadians) * hyp1;
                float ca1 = Mathf.Cos(angleRadians) * hyp1;
                Vector3 point1grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co1));
                else
                    point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co1));
                Vector3 point2grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co1));
                else
                    point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co1));

                float hyp2 = 0.09f;
                float co2 = Mathf.Sin(angleRadians) * hyp2;
                float ca2 = Mathf.Cos(angleRadians) * hyp2;
                Vector3 point3grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co2));
                else
                    point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co2));
                Vector3 point4grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co2));
                else
                    point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co2));

                if (i % 3 == 0)
                {

                    GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
                    DestroyImmediate(plane.GetComponent<MeshCollider>());
                    Mesh mesh = plane.GetComponent<MeshFilter>().mesh;

                    Vector3[] vertices = mesh.vertices;

                    vertices[0] = new Vector3(point2grass.x, point2grass.y + tunelHeight, point2grass.z);
                    vertices[1] = point2grass;
                    vertices[2] = point4grass;
                    vertices[3] = new Vector3(point4grass.x, point4grass.y + tunelHeight, point4grass.z);

                    mesh.vertices = vertices;
                    mesh.RecalculateBounds();
                    plane.AddComponent<MeshCollider>();
                    plane.GetComponent<MeshCollider>().convex = true;
                    plane.GetComponent<MeshCollider>().isTrigger = true;
                    plane.GetComponent<Renderer>().material = boardRightMaterial;
                    plane.AddComponent<BoardDirectional>();
                    plane.GetComponent<BoardDirectional>().pointsArray = new Vector3[4];
                    plane.GetComponent<BoardDirectional>().id = 0;
                    plane.GetComponent<BoardDirectional>().objName = "boardDirectional";
                    plane.GetComponent<BoardDirectional>().className = "boardDirectional";
                    plane.GetComponent<BoardDirectional>().tagName = "boardDirectional";
                    plane.GetComponent<BoardDirectional>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
                    plane.GetComponent<BoardDirectional>().pointsArray[0] = new Vector3(point2grass.x, point2grass.y + tunelHeight, point2grass.z);
                    plane.GetComponent<BoardDirectional>().pointsArray[1] = point2grass;
                    plane.GetComponent<BoardDirectional>().pointsArray[2] = point4grass;
                    plane.GetComponent<BoardDirectional>().pointsArray[3] = new Vector3(point4grass.x, point4grass.y + tunelHeight, point4grass.z);
                    plane.name = "boardDirectional";
                    plane.tag = "boardDirectional";

                    //--------------------------------------------

                    GameObject plane1 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
                    DestroyImmediate(plane1.GetComponent<MeshCollider>());
                    Mesh mesh1 = plane1.GetComponent<MeshFilter>().mesh;

                    Vector3[] vertices1 = mesh1.vertices;

                    vertices1[0] = new Vector3(point4grass.x, point4grass.y + tunelHeight, point4grass.z);
                    vertices1[1] = point4grass;
                    vertices1[2] = point2grass;
                    vertices1[3] = new Vector3(point2grass.x, point2grass.y + tunelHeight, point2grass.z);

                    mesh1.vertices = vertices1;
                    mesh1.RecalculateBounds();
                    plane1.AddComponent<MeshCollider>();
                    plane1.GetComponent<MeshCollider>().convex = true;
                    plane1.GetComponent<MeshCollider>().isTrigger = true;
                    plane1.GetComponent<Renderer>().material = boardLeftMaterial;
                    plane1.AddComponent<BoardDirectional>();
                    plane1.GetComponent<BoardDirectional>().pointsArray = new Vector3[4];
                    plane1.GetComponent<BoardDirectional>().id = 0;
                    plane1.GetComponent<BoardDirectional>().objName = "boardDirectional";
                    plane1.GetComponent<BoardDirectional>().className = "boardDirectional";
                    plane1.GetComponent<BoardDirectional>().tagName = "boardDirectional";
                    plane1.GetComponent<BoardDirectional>().materialName = plane1.GetComponent<Renderer>().sharedMaterial.name;
                    plane1.GetComponent<BoardDirectional>().pointsArray[0] = new Vector3(point4grass.x, point4grass.y + tunelHeight, point4grass.z);
                    plane1.GetComponent<BoardDirectional>().pointsArray[1] = point4grass;
                    plane1.GetComponent<BoardDirectional>().pointsArray[2] = point2grass;
                    plane1.GetComponent<BoardDirectional>().pointsArray[3] = new Vector3(point2grass.x, point2grass.y + tunelHeight, point2grass.z);
                    plane1.name = "boardDirectional";
                    plane1.tag = "boardDirectional";


                }
            }
        }

    }
    public void AddDirectionalBoardRight(int idInit, int idEnd)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        float tunelHeight = 0.0625f;

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int i = idInit; i < idEnd + 2; i++)
        {
            for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[i].pointsArray.Count; j += 2)
            {

                Vector3 diference = trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j];
                float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

                float hyp1 = 0.135f;
                float co1 = Mathf.Sin(angleRadians) * hyp1;
                float ca1 = Mathf.Cos(angleRadians) * hyp1;
                Vector3 point1grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co1));
                else
                    point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co1));
                Vector3 point2grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co1));
                else
                    point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co1));

                float hyp2 = 0.09f;
                float co2 = Mathf.Sin(angleRadians) * hyp2;
                float ca2 = Mathf.Cos(angleRadians) * hyp2;
                Vector3 point3grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co2));
                else
                    point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co2));
                Vector3 point4grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co2));
                else
                    point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co2));

                if (i % 3 == 0)
                {

                    //--------------------------------------------

                    GameObject plane2 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
                    DestroyImmediate(plane2.GetComponent<MeshCollider>());
                    Mesh mesh2 = plane2.GetComponent<MeshFilter>().mesh;

                    Vector3[] vertices2 = mesh2.vertices;

                    vertices2[0] = new Vector3(point1grass.x, point1grass.y + tunelHeight, point1grass.z);
                    vertices2[1] = point1grass;
                    vertices2[2] = point3grass;
                    vertices2[3] = new Vector3(point3grass.x, point3grass.y + tunelHeight, point3grass.z);

                    mesh2.vertices = vertices2;
                    mesh2.RecalculateBounds();
                    plane2.AddComponent<MeshCollider>();
                    plane2.GetComponent<MeshCollider>().convex = true;
                    plane2.GetComponent<MeshCollider>().isTrigger = true;
                    plane2.GetComponent<Renderer>().material = boardRightMaterial;
                    plane2.AddComponent<BoardDirectional>();
                    plane2.GetComponent<BoardDirectional>().pointsArray = new Vector3[4];
                    plane2.GetComponent<BoardDirectional>().id = 0;
                    plane2.GetComponent<BoardDirectional>().objName = "boardDirectional";
                    plane2.GetComponent<BoardDirectional>().className = "boardDirectional";
                    plane2.GetComponent<BoardDirectional>().tagName = "boardDirectional";
                    plane2.GetComponent<BoardDirectional>().materialName = plane2.GetComponent<Renderer>().sharedMaterial.name;
                    plane2.GetComponent<BoardDirectional>().pointsArray[0] = new Vector3(point1grass.x, point1grass.y + tunelHeight, point1grass.z);
                    plane2.GetComponent<BoardDirectional>().pointsArray[1] = point1grass;
                    plane2.GetComponent<BoardDirectional>().pointsArray[2] = point3grass;
                    plane2.GetComponent<BoardDirectional>().pointsArray[3] = new Vector3(point3grass.x, point3grass.y + tunelHeight, point3grass.z);
                    plane2.name = "boardDirectional";
                    plane2.tag = "boardDirectional";

                    //--------------------------------------------

                    GameObject plane3 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
                    DestroyImmediate(plane3.GetComponent<MeshCollider>());
                    Mesh mesh3 = plane3.GetComponent<MeshFilter>().mesh;

                    Vector3[] vertices3 = mesh3.vertices;

                    vertices3[0] = new Vector3(point3grass.x, point3grass.y + tunelHeight, point3grass.z);
                    vertices3[1] = point3grass;
                    vertices3[2] = point1grass;
                    vertices3[3] = new Vector3(point1grass.x, point1grass.y + tunelHeight, point1grass.z);

                    mesh3.vertices = vertices3;
                    mesh3.RecalculateBounds();
                    plane3.AddComponent<MeshCollider>();
                    plane3.GetComponent<MeshCollider>().convex = true;
                    plane3.GetComponent<MeshCollider>().isTrigger = true;
                    plane3.GetComponent<Renderer>().material = boardLeftMaterial;
                    plane3.AddComponent<BoardDirectional>();
                    plane3.GetComponent<BoardDirectional>().pointsArray = new Vector3[4];
                    plane3.GetComponent<BoardDirectional>().id = 0;
                    plane3.GetComponent<BoardDirectional>().objName = "boardDirectional";
                    plane3.GetComponent<BoardDirectional>().className = "boardDirectional";
                    plane3.GetComponent<BoardDirectional>().tagName = "boardDirectional";
                    plane3.GetComponent<BoardDirectional>().materialName = plane3.GetComponent<Renderer>().sharedMaterial.name;
                    plane3.GetComponent<BoardDirectional>().pointsArray[0] = new Vector3(point3grass.x, point3grass.y + tunelHeight, point3grass.z);
                    plane3.GetComponent<BoardDirectional>().pointsArray[1] = point3grass;
                    plane3.GetComponent<BoardDirectional>().pointsArray[2] = point1grass;
                    plane3.GetComponent<BoardDirectional>().pointsArray[3] = new Vector3(point1grass.x, point1grass.y + tunelHeight, point1grass.z);
                    plane3.name = "boardDirectional";
                    plane3.tag = "boardDirectional";

                    //--------------------------------------------

                }
            }
        }

    }

    public void AddTreeLeft(int id)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        float treeHeight = 0.1f;

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[id].pointsArray.Count; j += 2)
        {

            Vector3 diference = trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j];
            float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

            float hyp1 = 0.2f;
            float co1 = Mathf.Sin(angleRadians) * hyp1;
            float ca1 = Mathf.Cos(angleRadians) * hyp1;
            Vector3 point1grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));
            else
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            Vector3 point2grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            else
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));

            float hyp2 = 0.3f;
            float co2 = Mathf.Sin(angleRadians) * hyp2;
            float ca2 = Mathf.Cos(angleRadians) * hyp2;
            Vector3 point3grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));
            else
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            Vector3 point4grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            else
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));


            //--------------------------------------------

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);
            vertices[1] = point2grass;
            vertices[2] = point4grass;
            vertices[3] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();

            plane.AddComponent<MeshCollider>();
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;
            plane.GetComponent<Renderer>().material = treeMaterial;
            plane.AddComponent<Tree>();
            plane.GetComponent<Tree>().pointsArray = new Vector3[4];
            plane.GetComponent<Tree>().id = 0;
            plane.GetComponent<Tree>().objName = "tree";
            plane.GetComponent<Tree>().className = "tree";
            plane.GetComponent<Tree>().tagName = "tree";
            plane.GetComponent<Tree>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Tree>().pointsArray[0] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);
            plane.GetComponent<Tree>().pointsArray[1] = point2grass;
            plane.GetComponent<Tree>().pointsArray[2] = point4grass;
            plane.GetComponent<Tree>().pointsArray[3] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);
            plane.name = "tree";
            plane.tag = "tree";

            //--------------------------------------------

            GameObject plane1 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane1.GetComponent<MeshCollider>());
            Mesh mesh1 = plane1.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices1 = mesh1.vertices;

            vertices1[0] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);
            vertices1[1] = point4grass;
            vertices1[2] = point2grass;
            vertices1[3] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);

            mesh1.vertices = vertices1;
            mesh1.RecalculateBounds();

            plane1.AddComponent<MeshCollider>();
            plane1.GetComponent<MeshCollider>().convex = true;
            plane1.GetComponent<MeshCollider>().isTrigger = true;
            plane1.GetComponent<Renderer>().material = treeMaterial;
            plane1.AddComponent<Tree>();
            plane1.GetComponent<Tree>().pointsArray = new Vector3[4];
            plane1.GetComponent<Tree>().id = 0;
            plane1.GetComponent<Tree>().objName = "tree";
            plane1.GetComponent<Tree>().className = "tree";
            plane1.GetComponent<Tree>().tagName = "tree";
            plane1.GetComponent<Tree>().materialName = plane1.GetComponent<Renderer>().sharedMaterial.name;
            plane1.GetComponent<Tree>().pointsArray[0] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);
            plane1.GetComponent<Tree>().pointsArray[1] = point4grass;
            plane1.GetComponent<Tree>().pointsArray[2] = point2grass;
            plane1.GetComponent<Tree>().pointsArray[3] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);
            plane1.name = "tree";
            plane1.tag = "tree";
        }


    }
    public void AddTreeRight(int id)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        float treeHeight = 0.1f;

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[id].pointsArray.Count; j += 2)
        {

            Vector3 diference = trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j];
            float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

            float hyp1 = 0.2f;
            float co1 = Mathf.Sin(angleRadians) * hyp1;
            float ca1 = Mathf.Cos(angleRadians) * hyp1;
            Vector3 point1grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));
            else
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            Vector3 point2grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            else
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));

            float hyp2 = 0.3f;
            float co2 = Mathf.Sin(angleRadians) * hyp2;
            float ca2 = Mathf.Cos(angleRadians) * hyp2;
            Vector3 point3grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));
            else
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            Vector3 point4grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            else
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));


            //--------------------------------------------

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);
            vertices[1] = point1grass;
            vertices[2] = point3grass;
            vertices[3] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();

            plane.AddComponent<MeshCollider>();
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;
            plane.GetComponent<Renderer>().material = treeMaterial;
            plane.AddComponent<Tree>();
            plane.GetComponent<Tree>().pointsArray = new Vector3[4];
            plane.GetComponent<Tree>().id = 0;
            plane.GetComponent<Tree>().objName = "tree";
            plane.GetComponent<Tree>().className = "tree";
            plane.GetComponent<Tree>().tagName = "tree";
            plane.GetComponent<Tree>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Tree>().pointsArray[0] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);
            plane.GetComponent<Tree>().pointsArray[1] = point1grass;
            plane.GetComponent<Tree>().pointsArray[2] = point3grass;
            plane.GetComponent<Tree>().pointsArray[3] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);
            plane.name = "tree";
            plane.tag = "tree";

            //--------------------------------------------

            GameObject plane1 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane1.GetComponent<MeshCollider>());
            Mesh mesh1 = plane1.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices1 = mesh1.vertices;

            vertices1[0] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);
            vertices1[1] = point3grass;
            vertices1[2] = point1grass;
            vertices1[3] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);

            mesh1.vertices = vertices1;
            mesh1.RecalculateBounds();

            plane1.AddComponent<MeshCollider>();
            plane1.GetComponent<MeshCollider>().convex = true;
            plane1.GetComponent<MeshCollider>().isTrigger = true;
            plane1.GetComponent<Renderer>().material = treeMaterial;
            plane1.AddComponent<Tree>();
            plane1.GetComponent<Tree>().pointsArray = new Vector3[4];
            plane1.GetComponent<Tree>().id = 0;
            plane1.GetComponent<Tree>().objName = "tree";
            plane1.GetComponent<Tree>().className = "tree";
            plane1.GetComponent<Tree>().tagName = "tree";
            plane1.GetComponent<Tree>().materialName = plane1.GetComponent<Renderer>().sharedMaterial.name;
            plane1.GetComponent<Tree>().pointsArray[0] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);
            plane1.GetComponent<Tree>().pointsArray[1] = point3grass;
            plane1.GetComponent<Tree>().pointsArray[2] = point1grass;
            plane1.GetComponent<Tree>().pointsArray[3] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);
            plane1.name = "tree";
            plane1.tag = "tree";
        }


    }
    public void AddSponsorLeft(int id)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        float treeHeight = 0.1f;

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[id].pointsArray.Count; j += 2)
        {

            Vector3 diference = trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j];
            float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

            float hyp1 = 0.2f;
            float co1 = Mathf.Sin(angleRadians) * hyp1;
            float ca1 = Mathf.Cos(angleRadians) * hyp1;
            Vector3 point1grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));
            else
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            Vector3 point2grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            else
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));

            float hyp2 = 0.3f;
            float co2 = Mathf.Sin(angleRadians) * hyp2;
            float ca2 = Mathf.Cos(angleRadians) * hyp2;
            Vector3 point3grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));
            else
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            Vector3 point4grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            else
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));


            //--------------------------------------------

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);
            vertices[1] = point2grass;
            vertices[2] = point4grass;
            vertices[3] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();

            plane.AddComponent<MeshCollider>();
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;
            plane.GetComponent<Renderer>().material = sponsorMaterial;
            plane.AddComponent<Sponsor>();
            plane.GetComponent<Sponsor>().pointsArray = new Vector3[4];
            plane.GetComponent<Sponsor>().id = 0;
            plane.GetComponent<Sponsor>().objName = "sponsor";
            plane.GetComponent<Sponsor>().className = "sponsor";
            plane.GetComponent<Sponsor>().tagName = "sponsor";
            plane.GetComponent<Sponsor>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Sponsor>().pointsArray[0] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);
            plane.GetComponent<Sponsor>().pointsArray[1] = point2grass;
            plane.GetComponent<Sponsor>().pointsArray[2] = point4grass;
            plane.GetComponent<Sponsor>().pointsArray[3] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);
            plane.name = "sponsor";
            plane.tag = "sponsor";

            //--------------------------------------------

            GameObject plane1 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane1.GetComponent<MeshCollider>());
            Mesh mesh1 = plane1.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices1 = mesh1.vertices;

            vertices1[0] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);
            vertices1[1] = point4grass;
            vertices1[2] = point2grass;
            vertices1[3] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);

            mesh1.vertices = vertices1;
            mesh1.RecalculateBounds();

            plane1.AddComponent<MeshCollider>();
            plane1.GetComponent<MeshCollider>().convex = true;
            plane1.GetComponent<MeshCollider>().isTrigger = true;
            plane1.GetComponent<Renderer>().material = sponsorMaterial;
            plane1.AddComponent<Sponsor>();
            plane1.GetComponent<Sponsor>().pointsArray = new Vector3[4];
            plane1.GetComponent<Sponsor>().id = 0;
            plane1.GetComponent<Sponsor>().objName = "sponsor";
            plane1.GetComponent<Sponsor>().className = "sponsor";
            plane1.GetComponent<Sponsor>().tagName = "sponsor";
            plane1.GetComponent<Sponsor>().materialName = plane1.GetComponent<Renderer>().sharedMaterial.name;
            plane1.GetComponent<Sponsor>().pointsArray[0] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);
            plane1.GetComponent<Sponsor>().pointsArray[1] = point4grass;
            plane1.GetComponent<Sponsor>().pointsArray[2] = point2grass;
            plane1.GetComponent<Sponsor>().pointsArray[3] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);
            plane1.name = "sponsor";
            plane1.tag = "sponsor";

        }


    }
    public void AddSponsorRight(int id)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        float treeHeight = 0.1f;

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[id].pointsArray.Count; j += 2)
        {

            Vector3 diference = trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j];
            float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

            float hyp1 = 0.2f;
            float co1 = Mathf.Sin(angleRadians) * hyp1;
            float ca1 = Mathf.Cos(angleRadians) * hyp1;
            Vector3 point1grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));
            else
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            Vector3 point2grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            else
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));

            float hyp2 = 0.3f;
            float co2 = Mathf.Sin(angleRadians) * hyp2;
            float ca2 = Mathf.Cos(angleRadians) * hyp2;
            Vector3 point3grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));
            else
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            Vector3 point4grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            else
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));


            //--------------------------------------------

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);
            vertices[1] = point1grass;
            vertices[2] = point3grass;
            vertices[3] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;
            plane.GetComponent<Renderer>().material = sponsorMaterial;
            plane.AddComponent<Sponsor>();
            plane.GetComponent<Sponsor>().pointsArray = new Vector3[4];
            plane.GetComponent<Sponsor>().id = 0;
            plane.GetComponent<Sponsor>().objName = "sponsor";
            plane.GetComponent<Sponsor>().className = "sponsor";
            plane.GetComponent<Sponsor>().tagName = "sponsor";
            plane.GetComponent<Sponsor>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Sponsor>().pointsArray[0] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);
            plane.GetComponent<Sponsor>().pointsArray[1] = point1grass;
            plane.GetComponent<Sponsor>().pointsArray[2] = point3grass;
            plane.GetComponent<Sponsor>().pointsArray[3] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);
            plane.name = "sponsor";
            plane.tag = "sponsor";

            //--------------------------------------------

            GameObject plane1 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane1.GetComponent<MeshCollider>());
            Mesh mesh1 = plane1.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices1 = mesh1.vertices;

            vertices1[0] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);
            vertices1[1] = point3grass;
            vertices1[2] = point1grass;
            vertices1[3] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);

            mesh1.vertices = vertices1;
            mesh1.RecalculateBounds();
            plane1.AddComponent<MeshCollider>();
            plane1.GetComponent<MeshCollider>().convex = true;
            plane1.GetComponent<MeshCollider>().isTrigger = true;
            plane1.GetComponent<Renderer>().material = sponsorMaterial;
            plane1.AddComponent<Sponsor>();
            plane1.GetComponent<Sponsor>().pointsArray = new Vector3[4];
            plane1.GetComponent<Sponsor>().id = 0;
            plane1.GetComponent<Sponsor>().objName = "sponsor";
            plane1.GetComponent<Sponsor>().className = "sponsor";
            plane1.GetComponent<Sponsor>().tagName = "sponsor";
            plane1.GetComponent<Sponsor>().materialName = plane1.GetComponent<Renderer>().sharedMaterial.name;
            plane1.GetComponent<Sponsor>().pointsArray[0] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);
            plane1.GetComponent<Sponsor>().pointsArray[1] = point3grass;
            plane1.GetComponent<Sponsor>().pointsArray[2] = point1grass;
            plane1.GetComponent<Sponsor>().pointsArray[3] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);
            plane1.name = "sponsor";
            plane1.tag = "sponsor";

        }


    }
    public void AddSponsorCenter(int id)
    {
        int index = 0;
        float tunelHeight2 = 0.08f;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[id].pointsArray.Count; j += 2)
        {

            Vector3 diference = trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j];
            float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

            float hyp1 = 0.075f;
            float co1 = Mathf.Sin(angleRadians) * hyp1;
            float ca1 = Mathf.Cos(angleRadians) * hyp1;
            Vector3 point1grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));
            else
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            Vector3 point2grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            else
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));

            float hyp2 = 0.075f;
            float co2 = Mathf.Sin(angleRadians) * hyp2;
            float ca2 = Mathf.Cos(angleRadians) * hyp2;
            Vector3 point3grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));
            else
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            Vector3 point4grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            else
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));



            //--------------------------------------------

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(point1grass.x, point1grass.y + tunelHeight2, point1grass.z);
            vertices[1] = point1grass;
            vertices[2] = point2grass;
            vertices[3] = new Vector3(point2grass.x, point2grass.y + tunelHeight2, point2grass.z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;
            plane.GetComponent<Renderer>().material = sponsorMaterialCenter;
            plane.AddComponent<Sponsor>();
            plane.GetComponent<Sponsor>().pointsArray = new Vector3[4];
            plane.GetComponent<Sponsor>().id = 0;
            plane.GetComponent<Sponsor>().objName = "sponsor";
            plane.GetComponent<Sponsor>().className = "sponsor";
            plane.GetComponent<Sponsor>().tagName = "sponsor";
            plane.GetComponent<Sponsor>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Sponsor>().pointsArray[0] = new Vector3(point1grass.x, point1grass.y + tunelHeight2, point1grass.z);
            plane.GetComponent<Sponsor>().pointsArray[1] = point1grass;
            plane.GetComponent<Sponsor>().pointsArray[2] = point2grass;
            plane.GetComponent<Sponsor>().pointsArray[3] = new Vector3(point2grass.x, point2grass.y + tunelHeight2, point2grass.z);
            plane.name = "sponsor";
            plane.tag = "sponsor";

            //--------------------------------------------

            GameObject plane1 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane1.GetComponent<MeshCollider>());
            Mesh mesh1 = plane1.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices1 = mesh1.vertices;

            vertices1[0] = new Vector3(point2grass.x, point2grass.y + tunelHeight2, point2grass.z);
            vertices1[1] = point2grass;
            vertices1[2] = point1grass;
            vertices1[3] = new Vector3(point1grass.x, point1grass.y + tunelHeight2, point1grass.z);

            mesh1.vertices = vertices1;
            mesh1.RecalculateBounds();
            plane1.AddComponent<MeshCollider>();
            plane1.GetComponent<MeshCollider>().convex = true;
            plane1.GetComponent<MeshCollider>().isTrigger = true;
            plane1.GetComponent<Renderer>().material = sponsorMaterialCenter;
            plane1.AddComponent<Sponsor>();
            plane1.GetComponent<Sponsor>().pointsArray = new Vector3[4];
            plane1.GetComponent<Sponsor>().id = 0;
            plane1.GetComponent<Sponsor>().objName = "sponsor";
            plane1.GetComponent<Sponsor>().className = "sponsor";
            plane1.GetComponent<Sponsor>().tagName = "sponsor";
            plane1.GetComponent<Sponsor>().materialName = plane1.GetComponent<Renderer>().sharedMaterial.name;
            plane1.GetComponent<Sponsor>().pointsArray[0] = new Vector3(point2grass.x, point2grass.y + tunelHeight2, point2grass.z);
            plane1.GetComponent<Sponsor>().pointsArray[1] = point2grass;
            plane1.GetComponent<Sponsor>().pointsArray[2] = point1grass;
            plane1.GetComponent<Sponsor>().pointsArray[3] = new Vector3(point1grass.x, point1grass.y + tunelHeight2, point1grass.z);
            plane1.name = "sponsor";
            plane1.tag = "sponsor";

        }


    }

    public void AddTreeStreetLeft(int id)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        float treeHeight = 0.1f;

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[id].pointsArray.Count; j += 2)
        {

            Vector3 diference = trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j];
            float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

            float hyp1 = 0.1f;
            float co1 = Mathf.Sin(angleRadians) * hyp1;
            float ca1 = Mathf.Cos(angleRadians) * hyp1;
            Vector3 point1grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));
            else
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            Vector3 point2grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            else
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));

            float hyp2 = 0.2f;
            float co2 = Mathf.Sin(angleRadians) * hyp2;
            float ca2 = Mathf.Cos(angleRadians) * hyp2;
            Vector3 point3grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));
            else
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            Vector3 point4grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            else
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));


            //--------------------------------------------

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);
            vertices[1] = point2grass;
            vertices[2] = point4grass;
            vertices[3] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();

            plane.GetComponent<Renderer>().material = treeMaterial;

            plane.AddComponent<MeshCollider>();
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;
            plane.GetComponent<Renderer>().material = treeMaterial;
            plane.AddComponent<Tree>();
            plane.GetComponent<Tree>().pointsArray = new Vector3[4];
            plane.GetComponent<Tree>().id = 0;
            plane.GetComponent<Tree>().objName = "tree";
            plane.GetComponent<Tree>().className = "tree";
            plane.GetComponent<Tree>().tagName = "tree";
            plane.GetComponent<Tree>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Tree>().pointsArray[0] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);
            plane.GetComponent<Tree>().pointsArray[1] = point2grass;
            plane.GetComponent<Tree>().pointsArray[2] = point4grass;
            plane.GetComponent<Tree>().pointsArray[3] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);
            plane.name = "tree";
            plane.tag = "tree";

            //--------------------------------------------

            GameObject plane1 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane1.GetComponent<MeshCollider>());
            Mesh mesh1 = plane1.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices1 = mesh1.vertices;

            vertices1[0] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);
            vertices1[1] = point4grass;
            vertices1[2] = point2grass;
            vertices1[3] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);

            mesh1.vertices = vertices1;
            mesh1.RecalculateBounds();
            plane1.AddComponent<MeshCollider>();

            plane1.GetComponent<Renderer>().material = treeMaterial;

            plane1.AddComponent<MeshCollider>();
            plane1.GetComponent<MeshCollider>().convex = true;
            plane1.GetComponent<MeshCollider>().isTrigger = true;
            plane1.GetComponent<Renderer>().material = treeMaterial;
            plane1.AddComponent<Tree>();
            plane1.GetComponent<Tree>().pointsArray = new Vector3[4];
            plane1.GetComponent<Tree>().id = 0;
            plane1.GetComponent<Tree>().objName = "tree";
            plane1.GetComponent<Tree>().className = "tree";
            plane1.GetComponent<Tree>().tagName = "tree";
            plane1.GetComponent<Tree>().materialName = plane1.GetComponent<Renderer>().sharedMaterial.name;
            plane1.GetComponent<Tree>().pointsArray[0] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);
            plane1.GetComponent<Tree>().pointsArray[1] = point4grass;
            plane1.GetComponent<Tree>().pointsArray[2] = point2grass;
            plane1.GetComponent<Tree>().pointsArray[3] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);
            plane1.name = "tree";
            plane1.tag = "tree";

        }


    }
    public void AddTreeStreetRight(int id)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        float treeHeight = 0.1f;

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[id].pointsArray.Count; j += 2)
        {

            Vector3 diference = trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j];
            float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

            float hyp1 = 0.1f;
            float co1 = Mathf.Sin(angleRadians) * hyp1;
            float ca1 = Mathf.Cos(angleRadians) * hyp1;
            Vector3 point1grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));
            else
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            Vector3 point2grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            else
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));

            float hyp2 = 0.2f;
            float co2 = Mathf.Sin(angleRadians) * hyp2;
            float ca2 = Mathf.Cos(angleRadians) * hyp2;
            Vector3 point3grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));
            else
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            Vector3 point4grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            else
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));


            //--------------------------------------------

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);
            vertices[1] = point1grass;
            vertices[2] = point3grass;
            vertices[3] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.GetComponent<Renderer>().material = treeMaterial;

            plane.AddComponent<MeshCollider>();
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;
            plane.GetComponent<Renderer>().material = treeMaterial;
            plane.AddComponent<Tree>();
            plane.GetComponent<Tree>().pointsArray = new Vector3[4];
            plane.GetComponent<Tree>().id = 0;
            plane.GetComponent<Tree>().objName = "tree";
            plane.GetComponent<Tree>().className = "tree";
            plane.GetComponent<Tree>().tagName = "tree";
            plane.GetComponent<Tree>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Tree>().pointsArray[0] = vertices[0];
            plane.GetComponent<Tree>().pointsArray[1] = vertices[1];
            plane.GetComponent<Tree>().pointsArray[2] = vertices[2];
            plane.GetComponent<Tree>().pointsArray[3] = vertices[3];
            plane.name = "tree";
            plane.tag = "tree";

            //--------------------------------------------

            GameObject plane1 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane1.GetComponent<MeshCollider>());
            Mesh mesh1 = plane1.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices1 = mesh1.vertices;

            vertices1[0] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);
            vertices1[1] = point3grass;
            vertices1[2] = point1grass;
            vertices1[3] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);

            mesh1.vertices = vertices1;
            mesh1.RecalculateBounds();
            plane1.GetComponent<Renderer>().material = treeMaterial;

            plane1.AddComponent<MeshCollider>();
            plane1.GetComponent<MeshCollider>().convex = true;
            plane1.GetComponent<MeshCollider>().isTrigger = true;
            plane1.GetComponent<Renderer>().material = treeMaterial;
            plane1.AddComponent<Tree>();
            plane1.GetComponent<Tree>().pointsArray = new Vector3[4];
            plane1.GetComponent<Tree>().id = 0;
            plane1.GetComponent<Tree>().objName = "tree";
            plane1.GetComponent<Tree>().className = "tree";
            plane1.GetComponent<Tree>().tagName = "tree";
            plane1.GetComponent<Tree>().materialName = plane1.GetComponent<Renderer>().sharedMaterial.name;
            plane1.GetComponent<Tree>().pointsArray[0] = vertices1[0];
            plane1.GetComponent<Tree>().pointsArray[1] = vertices1[1];
            plane1.GetComponent<Tree>().pointsArray[2] = vertices1[2];
            plane1.GetComponent<Tree>().pointsArray[3] = vertices1[3];
            plane1.name = "tree";
            plane1.tag = "tree";

        }


    }

    public void AddSponsorStreetLeft(int id)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        float treeHeight = 0.1f;

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[id].pointsArray.Count; j += 2)
        {

            Vector3 diference = trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j];
            float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

            float hyp1 = 0.1f;
            float co1 = Mathf.Sin(angleRadians) * hyp1;
            float ca1 = Mathf.Cos(angleRadians) * hyp1;
            Vector3 point1grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));
            else
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            Vector3 point2grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            else
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));

            float hyp2 = 0.2f;
            float co2 = Mathf.Sin(angleRadians) * hyp2;
            float ca2 = Mathf.Cos(angleRadians) * hyp2;
            Vector3 point3grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));
            else
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            Vector3 point4grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            else
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));


            //--------------------------------------------

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);
            vertices[1] = point2grass;
            vertices[2] = point4grass;
            vertices[3] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();

            plane.GetComponent<Renderer>().material = sponsorMaterial;

            plane.AddComponent<MeshCollider>();
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;
            plane.GetComponent<Renderer>().material = sponsorMaterial;
            plane.AddComponent<Sponsor>();
            plane.GetComponent<Sponsor>().pointsArray = new Vector3[4];
            plane.GetComponent<Sponsor>().id = 0;
            plane.GetComponent<Sponsor>().objName = "sponsor";
            plane.GetComponent<Sponsor>().className = "sponsor";
            plane.GetComponent<Sponsor>().tagName = "sponsor";
            plane.GetComponent<Sponsor>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Sponsor>().pointsArray[0] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);
            plane.GetComponent<Sponsor>().pointsArray[1] = point2grass;
            plane.GetComponent<Sponsor>().pointsArray[2] = point4grass;
            plane.GetComponent<Sponsor>().pointsArray[3] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);
            plane.name = "sponsor";
            plane.tag = "sponsor";

            //--------------------------------------------

            GameObject plane1 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane1.GetComponent<MeshCollider>());
            Mesh mesh1 = plane1.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices1 = mesh1.vertices;

            vertices1[0] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);
            vertices1[1] = point4grass;
            vertices1[2] = point2grass;
            vertices1[3] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);

            mesh1.vertices = vertices1;
            mesh1.RecalculateBounds();
            plane1.AddComponent<MeshCollider>();

            plane1.GetComponent<Renderer>().material = sponsorMaterial;

            plane1.AddComponent<MeshCollider>();
            plane1.GetComponent<MeshCollider>().convex = true;
            plane1.GetComponent<MeshCollider>().isTrigger = true;
            plane1.GetComponent<Renderer>().material = sponsorMaterial;
            plane1.AddComponent<Sponsor>();
            plane1.GetComponent<Sponsor>().pointsArray = new Vector3[4];
            plane1.GetComponent<Sponsor>().id = 0;
            plane1.GetComponent<Sponsor>().objName = "sponsor";
            plane1.GetComponent<Sponsor>().className = "sponsor";
            plane1.GetComponent<Sponsor>().tagName = "sponsor";
            plane1.GetComponent<Sponsor>().materialName = plane1.GetComponent<Renderer>().sharedMaterial.name;
            plane1.GetComponent<Sponsor>().pointsArray[0] = new Vector3(point4grass.x, point4grass.y + treeHeight, point4grass.z);
            plane1.GetComponent<Sponsor>().pointsArray[1] = point4grass;
            plane1.GetComponent<Sponsor>().pointsArray[2] = point2grass;
            plane1.GetComponent<Sponsor>().pointsArray[3] = new Vector3(point2grass.x, point2grass.y + treeHeight, point2grass.z);
            plane1.name = "sponsor";
            plane1.tag = "sponsor";

        }


    }
    public void AddSponsorStreetRight(int id)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        float treeHeight = 0.1f;

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[id].pointsArray.Count; j += 2)
        {

            Vector3 diference = trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j];
            float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

            float hyp1 = 0.1f;
            float co1 = Mathf.Sin(angleRadians) * hyp1;
            float ca1 = Mathf.Cos(angleRadians) * hyp1;
            Vector3 point1grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));
            else
                point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            Vector3 point2grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co1));
            else
                point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co1));

            float hyp2 = 0.2f;
            float co2 = Mathf.Sin(angleRadians) * hyp2;
            float ca2 = Mathf.Cos(angleRadians) * hyp2;
            Vector3 point3grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));
            else
                point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            Vector3 point4grass = Vector3.zero;
            if (trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j + 1].z)
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (co2));
            else
                point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[id].pointsArray[j].z + (-1.0f * co2));


            //--------------------------------------------

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices = mesh.vertices;

            vertices[0] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);
            vertices[1] = point1grass;
            vertices[2] = point3grass;
            vertices[3] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();

            plane.GetComponent<Renderer>().material = sponsorMaterial;

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;
            plane.GetComponent<Renderer>().material = sponsorMaterial;
            plane.AddComponent<Sponsor>();
            plane.GetComponent<Sponsor>().pointsArray = new Vector3[4];
            plane.GetComponent<Sponsor>().id = 0;
            plane.GetComponent<Sponsor>().objName = "sponsor";
            plane.GetComponent<Sponsor>().className = "sponsor";
            plane.GetComponent<Sponsor>().tagName = "sponsor";
            plane.GetComponent<Sponsor>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
            plane.GetComponent<Sponsor>().pointsArray[0] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);
            plane.GetComponent<Sponsor>().pointsArray[1] = point1grass;
            plane.GetComponent<Sponsor>().pointsArray[2] = point3grass;
            plane.GetComponent<Sponsor>().pointsArray[3] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);
            plane.name = "sponsor";
            plane.tag = "sponsor";

            //--------------------------------------------

            GameObject plane1 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane1.GetComponent<MeshCollider>());
            Mesh mesh1 = plane1.GetComponent<MeshFilter>().mesh;

            Vector3[] vertices1 = mesh1.vertices;

            vertices1[0] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);
            vertices1[1] = point3grass;
            vertices1[2] = point1grass;
            vertices1[3] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);

            mesh1.vertices = vertices1;
            mesh1.RecalculateBounds();
            plane1.AddComponent<MeshCollider>();

            plane1.GetComponent<Renderer>().material = sponsorMaterial;

            mesh1.vertices = vertices1;
            mesh1.RecalculateBounds();
            plane1.AddComponent<MeshCollider>();
            plane1.GetComponent<MeshCollider>().convex = true;
            plane1.GetComponent<MeshCollider>().isTrigger = true;
            plane1.GetComponent<Renderer>().material = sponsorMaterial;
            plane1.AddComponent<Sponsor>();
            plane1.GetComponent<Sponsor>().pointsArray = new Vector3[4];
            plane1.GetComponent<Sponsor>().id = 0;
            plane1.GetComponent<Sponsor>().objName = "sponsor";
            plane1.GetComponent<Sponsor>().className = "sponsor";
            plane1.GetComponent<Sponsor>().tagName = "sponsor";
            plane1.GetComponent<Sponsor>().materialName = plane1.GetComponent<Renderer>().sharedMaterial.name;
            plane1.GetComponent<Sponsor>().pointsArray[0] = new Vector3(point3grass.x, point3grass.y + treeHeight, point3grass.z);
            plane1.GetComponent<Sponsor>().pointsArray[1] = point3grass;
            plane1.GetComponent<Sponsor>().pointsArray[2] = point1grass;
            plane1.GetComponent<Sponsor>().pointsArray[3] = new Vector3(point1grass.x, point1grass.y + treeHeight, point1grass.z);
            plane1.name = "sponsor";
            plane1.tag = "sponsor";

        }


    }

    public void AddPitBoardLeft(int idInit, int idEnd)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        float tunelHeight = 0.0625f;

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int i = idInit; i < idEnd + 2; i++)
        {
            for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[i].pointsArray.Count; j += 2)
            {

                Vector3 diference = trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j];
                float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

                float hyp1 = 0.135f;
                float co1 = Mathf.Sin(angleRadians) * hyp1;
                float ca1 = Mathf.Cos(angleRadians) * hyp1;
                Vector3 point1grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co1));
                else
                    point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co1));
                Vector3 point2grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co1));
                else
                    point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co1));

                float hyp2 = 0.09f;
                float co2 = Mathf.Sin(angleRadians) * hyp2;
                float ca2 = Mathf.Cos(angleRadians) * hyp2;
                Vector3 point3grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co2));
                else
                    point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co2));
                Vector3 point4grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co2));
                else
                    point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co2));

                if (i % 3 == 0)
                {

                    GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
                    DestroyImmediate(plane.GetComponent<MeshCollider>());
                    Mesh mesh = plane.GetComponent<MeshFilter>().mesh;

                    Vector3[] vertices = mesh.vertices;

                    vertices[0] = new Vector3(point2grass.x, point2grass.y + tunelHeight, point2grass.z);
                    vertices[1] = point2grass;
                    vertices[2] = point4grass;
                    vertices[3] = new Vector3(point4grass.x, point4grass.y + tunelHeight, point4grass.z);

                    mesh.vertices = vertices;
                    mesh.RecalculateBounds();
                    plane.AddComponent<MeshCollider>();
                    plane.GetComponent<MeshCollider>().convex = true;
                    plane.GetComponent<MeshCollider>().isTrigger = true;

                    plane.GetComponent<Renderer>().material = pitBoardRight;

                    plane.AddComponent<Board>();
                    plane.GetComponent<Board>().pointsArray = new Vector3[4];
                    plane.GetComponent<Board>().id = 0;
                    plane.GetComponent<Board>().objName = "board";
                    plane.GetComponent<Board>().className = "board";
                    plane.GetComponent<Board>().tagName = "board";
                    plane.GetComponent<Board>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
                    plane.GetComponent<Board>().pointsArray[0] = new Vector3(point2grass.x, point2grass.y + tunelHeight, point2grass.z);
                    plane.GetComponent<Board>().pointsArray[1] = point2grass;
                    plane.GetComponent<Board>().pointsArray[2] = point4grass;
                    plane.GetComponent<Board>().pointsArray[3] = new Vector3(point4grass.x, point4grass.y + tunelHeight, point4grass.z);
                    plane.name = "board";
                    plane.tag = "board";

                    //--------------------------------------------

                    GameObject plane1 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
                    DestroyImmediate(plane1.GetComponent<MeshCollider>());
                    Mesh mesh1 = plane1.GetComponent<MeshFilter>().mesh;

                    Vector3[] vertices1 = mesh1.vertices;

                    vertices1[0] = new Vector3(point4grass.x, point4grass.y + tunelHeight, point4grass.z);
                    vertices1[1] = point4grass;
                    vertices1[2] = point2grass;
                    vertices1[3] = new Vector3(point2grass.x, point2grass.y + tunelHeight, point2grass.z);

                    mesh1.vertices = vertices1;
                    mesh1.RecalculateBounds();
                    plane1.AddComponent<MeshCollider>();
                    plane1.GetComponent<MeshCollider>().convex = true;
                    plane1.GetComponent<MeshCollider>().isTrigger = true;

                    plane1.GetComponent<Renderer>().material = pitBoardLeft;

                    plane1.AddComponent<Board>();
                    plane1.GetComponent<Board>().pointsArray = new Vector3[4];
                    plane1.GetComponent<Board>().id = 0;
                    plane1.GetComponent<Board>().objName = "board";
                    plane1.GetComponent<Board>().className = "board";
                    plane1.GetComponent<Board>().tagName = "board";
                    plane1.GetComponent<Board>().materialName = plane.GetComponent<Renderer>().sharedMaterial.name;
                    plane1.GetComponent<Board>().pointsArray[0] = new Vector3(point4grass.x, point4grass.y + tunelHeight, point4grass.z);
                    plane1.GetComponent<Board>().pointsArray[1] = point4grass;
                    plane1.GetComponent<Board>().pointsArray[2] = point2grass;
                    plane1.GetComponent<Board>().pointsArray[3] = new Vector3(point2grass.x, point2grass.y + tunelHeight, point2grass.z);
                    plane1.name = "board";
                    plane1.tag = "board";

                }
            }
        }

    }
    public void AddPitBoardRight(int idInit, int idEnd)
    {
        int index = 0;
        Vector3 initPoint1 = Vector3.zero;
        Vector3 initPoint2 = Vector3.zero;

        Vector3 initPoint1Grass = Vector3.zero;
        Vector3 initPoint2Grass = Vector3.zero;

        TrackSegmentDTO trackSegmentDTO1 = new TrackSegmentDTO();
        trackSegmentDTO1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass1 = new TrackSegmentDTO();
        trackSegmentDTOGrass1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGrass2 = new TrackSegmentDTO();
        trackSegmentDTOGrass2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        float tunelHeight = 0.0625f;

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        for (int i = idInit; i < idEnd + 2; i++)
        {
            for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[i].pointsArray.Count; j += 2)
            {

                Vector3 diference = trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j];
                float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

                float hyp1 = 0.135f;
                float co1 = Mathf.Sin(angleRadians) * hyp1;
                float ca1 = Mathf.Cos(angleRadians) * hyp1;
                Vector3 point1grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co1));
                else
                    point1grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co1));
                Vector3 point2grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co1));
                else
                    point2grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca1), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co1));

                float hyp2 = 0.09f;
                float co2 = Mathf.Sin(angleRadians) * hyp2;
                float ca2 = Mathf.Cos(angleRadians) * hyp2;
                Vector3 point3grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co2));
                else
                    point3grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co2));
                Vector3 point4grass = Vector3.zero;
                if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                    point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co2));
                else
                    point4grass = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca2), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co2));

                if (i % 3 == 0)
                {

                    //--------------------------------------------

                    GameObject plane2 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
                    DestroyImmediate(plane2.GetComponent<MeshCollider>());
                    Mesh mesh2 = plane2.GetComponent<MeshFilter>().mesh;

                    Vector3[] vertices2 = mesh2.vertices;

                    vertices2[0] = new Vector3(point1grass.x, point1grass.y + tunelHeight, point1grass.z);
                    vertices2[1] = point1grass;
                    vertices2[2] = point3grass;
                    vertices2[3] = new Vector3(point3grass.x, point3grass.y + tunelHeight, point3grass.z);

                    mesh2.vertices = vertices2;
                    mesh2.RecalculateBounds();
                    plane2.AddComponent<MeshCollider>();
                    plane2.GetComponent<MeshCollider>().convex = true;
                    plane2.GetComponent<MeshCollider>().isTrigger = true;

                    plane2.GetComponent<Renderer>().material = pitBoardRight;

                    plane2.AddComponent<Board>();
                    plane2.GetComponent<Board>().pointsArray = new Vector3[4];
                    plane2.GetComponent<Board>().id = 0;
                    plane2.GetComponent<Board>().objName = "board";
                    plane2.GetComponent<Board>().className = "board";
                    plane2.GetComponent<Board>().tagName = "board";
                    plane2.GetComponent<Board>().materialName = plane2.GetComponent<Renderer>().sharedMaterial.name;
                    plane2.GetComponent<Board>().pointsArray[0] = new Vector3(point1grass.x, point1grass.y + tunelHeight, point1grass.z);
                    plane2.GetComponent<Board>().pointsArray[1] = point1grass;
                    plane2.GetComponent<Board>().pointsArray[2] = point3grass;
                    plane2.GetComponent<Board>().pointsArray[3] = new Vector3(point3grass.x, point3grass.y + tunelHeight, point3grass.z);
                    plane2.name = "board";
                    plane2.tag = "board";

                    //--------------------------------------------

                    GameObject plane3 = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
                    DestroyImmediate(plane3.GetComponent<MeshCollider>());
                    Mesh mesh3 = plane3.GetComponent<MeshFilter>().mesh;

                    Vector3[] vertices3 = mesh3.vertices;

                    vertices3[0] = new Vector3(point3grass.x, point3grass.y + tunelHeight, point3grass.z);
                    vertices3[1] = point3grass;
                    vertices3[2] = point1grass;
                    vertices3[3] = new Vector3(point1grass.x, point1grass.y + tunelHeight, point1grass.z);

                    mesh3.vertices = vertices3;
                    mesh3.RecalculateBounds();
                    plane3.AddComponent<MeshCollider>();
                    plane3.GetComponent<MeshCollider>().convex = true;
                    plane3.GetComponent<MeshCollider>().isTrigger = true;

                    plane3.GetComponent<Renderer>().material = pitBoardLeft;

                    plane3.AddComponent<Board>();
                    plane3.GetComponent<Board>().pointsArray = new Vector3[4];
                    plane3.GetComponent<Board>().id = 0;
                    plane3.GetComponent<Board>().objName = "board";
                    plane3.GetComponent<Board>().className = "board";
                    plane3.GetComponent<Board>().tagName = "board";
                    plane3.GetComponent<Board>().materialName = plane3.GetComponent<Renderer>().sharedMaterial.name;
                    plane3.GetComponent<Board>().pointsArray[0] = new Vector3(point3grass.x, point3grass.y + tunelHeight, point3grass.z);
                    plane3.GetComponent<Board>().pointsArray[1] = point3grass;
                    plane3.GetComponent<Board>().pointsArray[2] = point1grass;
                    plane3.GetComponent<Board>().pointsArray[3] = new Vector3(point1grass.x, point1grass.y + tunelHeight, point1grass.z);
                    plane3.name = "board";
                    plane3.tag = "board";

                    //--------------------------------------------

                }
            }
        }

    }

    public void WayPoint(List<GameObject> obj, int idInit, int idEnd, int[] wayPointSequence)
    {

        int index = 0;
        Vector3 initPoint1 = Vector3.zero;

        TrackSegmentDTO trackSegmentDTOGuardRail1 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail1.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTOGuardRail2 = new TrackSegmentDTO();
        trackSegmentDTOGuardRail2.trackVertexDTOArray = new List<TrackVertexDTO>();

        TrackSegmentDTO trackSegmentDTO = LoadTrackJson();

        float[] hyp = new float[] { 0.04f, 0.0f, -0.04f };
        for (int a = 0; a < hyp.Length; a++)
        {
            for (int i = idInit; i < idEnd + 1; i++)
            {
                for (int j = 0; j < trackSegmentDTO.trackVertexDTOArray[i].pointsArray.Count; j += 2)
                {
                    Vector3 diference = trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1] - trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j];
                    float angleRadians = (Vector3.Angle(diference, Vector3.right) * Mathf.PI) / 180;

                    float co = Mathf.Sin(angleRadians) * hyp[a];
                    float ca = Mathf.Cos(angleRadians) * hyp[a];
                    Vector3 point1 = Vector3.zero;
                    if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                        point1 = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co));
                    else
                        point1 = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (ca), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co));

                    Vector3 point2 = Vector3.zero;
                    if (trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z > trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j + 1].z)
                        point2 = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (co));
                    else
                        point2 = new Vector3(trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].x + (-1.0f * ca), trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].y, trackSegmentDTO.trackVertexDTOArray[i].pointsArray[j].z + (-1.0f * co));

                    trackSegmentDTOGuardRail1.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].pointsArray.Add(point1);
                    trackSegmentDTOGuardRail1.trackVertexDTOArray[trackSegmentDTOGuardRail1.trackVertexDTOArray.Count - 1].angle = Vector3.Angle(diference, Vector3.right);

                    trackSegmentDTOGuardRail2.trackVertexDTOArray.Add(new TrackVertexDTO());
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray = new List<Vector3>();
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].pointsArray.Add(point2);
                    trackSegmentDTOGuardRail2.trackVertexDTOArray[trackSegmentDTOGuardRail2.trackVertexDTOArray.Count - 1].angle = Vector3.Angle(diference, Vector3.right);
                }
                index++;
            }

            for (int b = 0; b < obj.Count(); b++)
            {                
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                cube.transform.position = new Vector3(trackSegmentDTOGuardRail1.trackVertexDTOArray[a].pointsArray[0].x, trackSegmentDTOGuardRail1.trackVertexDTOArray[a].pointsArray[0].y + 0.001f, trackSegmentDTOGuardRail1.trackVertexDTOArray[a].pointsArray[0].z);
                cube.transform.rotation = Quaternion.Euler(cube.transform.rotation.x, trackSegmentDTOGuardRail1.trackVertexDTOArray[a].angle, cube.transform.rotation.z);
                //cube.GetComponent<MeshRenderer>().enabled = false;
                cube.name = "WayPoint";
                cube.tag = "WayPoint";
                cube.AddComponent<WayPoint>();
                cube.GetComponent<WayPoint>().GetSettrackID = obj[b].GetComponent<Asphalt>().id;
                cube.GetComponent<WayPoint>().SetWayPoint(wayPointSequence[a]);
                cube.GetComponent<BoxCollider>().isTrigger = true;                
            }
        }
    }
    public void Save(GameObject[] allObjects)
    {
        
        Track track = new Track();
        track.asphaltList = new List<AsphaltDTO>();
        track.grassList = new List<GrassDTO>();
        track.guardrailList = new List<GuardrailDTO>();
        track.gridList = new List<GridDTO>();
        track.boardList = new List<BoardDTO>();
        track.treeList = new List<TreeDTO>();
        track.sponsorList = new List<SponsorDTO>();
        track.boardDirectionalList = new List<BoardDirectionalDTO>();
        track.tunelWallList = new List<TunelWallDTO>();
        track.objectReferencedList = new List<ElementosExternosDTO>();
        track.objWayPointList = new List<WayPointDTO>();

        //Asphalt
        GameObject[] objAsphalt = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(a => a.tag == "asphalt").ToArray();
        foreach (GameObject obj in objAsphalt)
        {
            Asphalt asphaltNewObj = obj.GetComponent<Asphalt>();
            AsphaltDTO asphaltNew = new AsphaltDTO();
            asphaltNew.id = asphaltNewObj.id;
            asphaltNew.objName = asphaltNewObj.objName;
            asphaltNew.className = asphaltNewObj.className;
            asphaltNew.materialName = obj.GetComponent<Renderer>().sharedMaterial.name;

            asphaltNew.tagName = asphaltNewObj.tagName;
            asphaltNew.pointsArray = asphaltNewObj.pointsArray;            
            asphaltNew.speedArray = asphaltNewObj.speedArray;

            asphaltNew.isSpeedChange = asphaltNewObj.isSpeedChange;

            Checkpoint checkpointNewObj = obj.GetComponent<Checkpoint>();
            if (checkpointNewObj != null)
                asphaltNew.checkpointNewObj = new CheckpointDTO() { id = checkpointNewObj.id };

            track.asphaltList.Add(asphaltNew);

        }

        //Grass
        GameObject[] objGrass = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(a => a.tag == "grass").ToArray();
        foreach (GameObject obj in objGrass)
        {
            Grass grassNewObj = obj.GetComponent<Grass>();
            GrassDTO grassNew = new GrassDTO();
            grassNew.id = grassNewObj.id;
            grassNew.objName = grassNewObj.objName;
            grassNew.className = grassNewObj.className;
            grassNew.materialName = obj.GetComponent<Renderer>().sharedMaterial.name;

            grassNew.tagName = grassNewObj.tagName;
            grassNew.pointsArray = grassNewObj.pointsArray;

            Checkpoint checkpointNewObj = obj.GetComponent<Checkpoint>();
            if (checkpointNewObj != null)
                grassNew.checkpointNewObj = new CheckpointDTO() { id = checkpointNewObj.id };

            track.grassList.Add(grassNew);
        }

        //Guardrail
        GameObject[] objGuardrail = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(a => a.tag == "guardrail").ToArray();
        foreach (GameObject obj in objGuardrail)
        {
            Guardrail guardrailNewObj = obj.GetComponent<Guardrail>();
            GuardrailDTO guardrailNew = new GuardrailDTO();
            guardrailNew.id = guardrailNewObj.id;
            guardrailNew.objName = guardrailNewObj.objName;
            guardrailNew.className = guardrailNewObj.className;
            guardrailNew.materialName = obj.GetComponent<Renderer>().sharedMaterial.name;
            guardrailNew.tagName = guardrailNewObj.tagName;
            guardrailNew.pointsArray = guardrailNewObj.pointsArray;
            track.guardrailList.Add(guardrailNew);
        }

        //Grid
        GameObject[] objGrid = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(a => a.tag == "grid").ToArray();
        foreach (GameObject obj in objGrid)
        {
            Grid gridNewObj = obj.GetComponent<Grid>();
            GridDTO gridNew = new GridDTO();
            gridNew.id = gridNewObj.id;
            gridNew.objName = gridNewObj.objName;
            gridNew.tagName = gridNewObj.tagName;
            gridNew.points = gridNewObj.points;
            gridNew.localScale = gridNewObj.localScale;
            gridNew.rotation = new Vector4(gridNewObj.rotation.x, gridNewObj.rotation.y, gridNewObj.rotation.z, gridNewObj.rotation.w);
            track.gridList.Add(gridNew);
        }

        //Board
        GameObject[] objBoard = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(a => a.tag == "board").ToArray();
        foreach (GameObject obj in objBoard)
        {
            Board boardNewObj = obj.GetComponent<Board>();
            BoardDTO boardNew = new BoardDTO();
            boardNew.id = boardNewObj.id;
            boardNew.objName = boardNewObj.objName;
            boardNew.tagName = boardNewObj.tagName;
            boardNew.materialName = boardNewObj.GetComponent<Renderer>().sharedMaterial.name;
            boardNew.className = boardNewObj.className;
            boardNew.pointsArray = boardNewObj.pointsArray;
            track.boardList.Add(boardNew);
        }

        //Tree
        GameObject[] objTree = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(a => a.tag == "tree").ToArray();
        foreach (GameObject obj in objTree)
        {
            Tree treeNewObj = obj.GetComponent<Tree>();
            TreeDTO treeNew = new TreeDTO();
            treeNew.id = treeNewObj.id;
            treeNew.objName = treeNewObj.objName;
            treeNew.tagName = treeNewObj.tagName;
            treeNew.materialName = treeNewObj.GetComponent<Renderer>().sharedMaterial.name;
            treeNew.className = treeNewObj.className;
            treeNew.pointsArray = treeNewObj.pointsArray;
            track.treeList.Add(treeNew);
        }

        //Sponsor
        GameObject[] objSponsor = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(a => a.tag == "sponsor").ToArray();
        foreach (GameObject obj in objSponsor)
        {
            Sponsor sponsorNewObj = obj.GetComponent<Sponsor>();
            SponsorDTO sponsorNew = new SponsorDTO();
            sponsorNew.id = sponsorNewObj.id;
            sponsorNew.objName = sponsorNewObj.objName;
            sponsorNew.tagName = sponsorNewObj.tagName;
            sponsorNew.materialName = sponsorNewObj.GetComponent<Renderer>().sharedMaterial.name;
            sponsorNew.className = sponsorNewObj.className;
            sponsorNew.pointsArray = sponsorNewObj.pointsArray;
            track.sponsorList.Add(sponsorNew);
        }

        //board Directional
        GameObject[] objboardDirectional = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(a => a.tag == "boardDirectional").ToArray();
        foreach (GameObject obj in objboardDirectional)
        {
            BoardDirectional boardDirectionalNewObj = obj.GetComponent<BoardDirectional>();
            BoardDirectionalDTO boardDirectionalNew = new BoardDirectionalDTO();
            boardDirectionalNew.id = boardDirectionalNewObj.id;
            boardDirectionalNew.objName = boardDirectionalNewObj.objName;
            boardDirectionalNew.tagName = boardDirectionalNewObj.tagName;
            boardDirectionalNew.materialName = boardDirectionalNewObj.GetComponent<Renderer>().sharedMaterial.name;
            boardDirectionalNew.className = boardDirectionalNewObj.className;
            boardDirectionalNew.pointsArray = boardDirectionalNewObj.pointsArray;
            track.boardDirectionalList.Add(boardDirectionalNew);
        }

        //Tunel
        GameObject[] objTunel = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(a => a.tag == "tuneWall").ToArray();
        foreach (GameObject obj in objTunel)
        {
            TunelWall tunelNewObj = obj.GetComponent<TunelWall>();
            TunelWallDTO tunellNew = new TunelWallDTO();
            tunellNew.id = tunelNewObj.id;
            tunellNew.objName = tunelNewObj.objName;
            tunellNew.tagName = tunelNewObj.tagName;
            tunellNew.materialName = tunelNewObj.GetComponent<Renderer>().sharedMaterial.name;
            tunellNew.className = tunelNewObj.className;
            tunellNew.pointsArray = tunelNewObj.pointsArray;
            track.tunelWallList.Add(tunellNew);
        }

        //objectReferenced
        GameObject[] objectReferenced = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(a => a.tag == "objectReferenced").ToArray();
        foreach (GameObject obj in objectReferenced)
        {
            ElementosExternos objRefNewObj = obj.GetComponent<ElementosExternos>();
            ElementosExternosDTO objNew = new ElementosExternosDTO();
            objNew.id = objRefNewObj.id;
            objNew.pos = objRefNewObj.pos;
            objNew.rot = new Vector4(objRefNewObj.rot.x, objRefNewObj.rot.y, objRefNewObj.rot.z, objRefNewObj.rot.w);
            objNew.scale = objRefNewObj.scale;
            objNew.materialName = objRefNewObj.GetComponent<Renderer>().sharedMaterial.name;
            track.objectReferencedList.Add(objNew);
        }

        //WayPoint
        GameObject[] objWayPointList = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(a => a.tag == "WayPoint").ToArray();
        foreach (GameObject obj in objWayPointList)
        {
            WayPoint objRefNewObj = obj.GetComponent<WayPoint>();
            WayPointDTO objNew = new WayPointDTO();
            objNew.trackID = objRefNewObj.GetSettrackID;
            objNew.wayPointPosition = (int)objRefNewObj.wayPointPosition;
            objNew.isLookToRedirect = objRefNewObj.GetIsLookToRedirect;
            objNew.tagName = obj.tag;
            objNew.pointsArray = obj.transform.position;
            objNew.rotationArray = new Vector4(obj.transform.rotation.x, obj.transform.rotation.y, obj.transform.rotation.z, obj.transform.rotation.w);

            track.objWayPointList.Add(objNew);
        }        

        //MeshCoordenadas
        GameObject objMeshCoordinates = UnityEngine.Object.FindObjectsOfType<GameObject>().Where(a => a.tag == "meshCoordinates").First();
        MeshCoordenadasDTO meshCoordenadasDTO = new MeshCoordenadasDTO();
        meshCoordenadasDTO.coordenadasMesh = new List<Vector3>();
        meshCoordenadasDTO.trianglesPos = new List<int>();
        foreach (Vector3 coordenadasMesh in objMeshCoordinates.GetComponent<MeshCoordenadas>().coordenadasMesh)
        {
            meshCoordenadasDTO.coordenadasMesh.Add(coordenadasMesh);
        }
        foreach (int trianglesPos in objMeshCoordinates.GetComponent<MeshCoordenadas>().trianglesPos)
        {
            meshCoordenadasDTO.trianglesPos.Add(trianglesPos);
        }
        track.meshCoordenadasDTO = meshCoordenadasDTO;

        string dataAsJson = JsonConvert.SerializeObject(track);
        string filePath = "D:/IGOR/track_0.json";
        File.WriteAllText(filePath, dataAsJson);
        Debug.Log("Track Saved");
    }
    public void LoadTrack(string trackName, int trackId = 0)
    {
        Track track = LoadTrackFinalJson(trackName);
        LoadCreatedTrack(track, trackId);
    }
    public void LoadCreatedTrack(Track track, int trackId = 0)
    {
        difficultID = General.GetSetConfig.difficultID;
#if (UNITY_EDITOR)
        List<Material> materialsList = GenerateMaterialList();
#else
        List<Material> materialsList = GetAllMaterials();
#endif

        //Asphalt
        foreach (AsphaltDTO block in track.asphaltList)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            //Mesh mesh = plane.GetComponent<MeshFilter>().sharedMesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = block.pointsArray[0];//2
            vertices[1] = block.pointsArray[1];//0
            vertices[2] = block.pointsArray[2];//1
            vertices[3] = block.pointsArray[3];//3

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();

            plane.GetComponent<Renderer>().material = materialsList.Where(x => x.name == block.materialName).FirstOrDefault();

            plane.name = block.objName;
            plane.AddComponent<Asphalt>();
            plane.GetComponent<Asphalt>().id = block.id;
            plane.GetComponent<Asphalt>().objName = block.objName;
            plane.GetComponent<Asphalt>().className = block.className;
            plane.GetComponent<Asphalt>().tagName = block.tagName;
            plane.GetComponent<Asphalt>().materialName = block.materialName;
            plane.GetComponent<Asphalt>().pointsArray = block.pointsArray;

            plane.GetComponent<Asphalt>().isSpeedChange = block.isSpeedChange;

            if (block.speedArray[0] > 0.0f && block.speedArray[0] < 0.03f)
            {
                for (int i = 0; i < block.speedArray.Length; i++)
                    plane.GetComponent<Asphalt>().speedArray[i] = block.speedArray[i] + difficultArray[difficultID];
            }
            else                
                plane.GetComponent<Asphalt>().speedArray = block.speedArray;            

            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;

            if (block.checkpointNewObj != null)
            {
                plane.AddComponent<Checkpoint>();
                plane.GetComponent<Checkpoint>().id = block.checkpointNewObj.id;
            }

            plane.name = plane.GetComponent<Asphalt>().objName;
            plane.tag = plane.GetComponent<Asphalt>().tagName;
        }
        //Grass
        foreach (GrassDTO block in track.grassList)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            //Mesh mesh = plane.GetComponent<MeshFilter>().sharedMesh;
            //Mesh mesh = new Mesh();
            //mesh.vertices = plane.GetComponent<MeshFilter>().mesh.vertices;
            //mesh.triangles = plane.GetComponent<MeshFilter>().mesh.triangles;

            Vector3[] vertices = mesh.vertices;

            vertices[0] = block.pointsArray[0];//2
            vertices[1] = block.pointsArray[1];//0
            vertices[2] = block.pointsArray[2];//1
            vertices[3] = block.pointsArray[3];//3

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();

            plane.GetComponent<Renderer>().material = materialsList.Where(x => x.name == block.materialName).FirstOrDefault();

            plane.name = block.objName;
            plane.AddComponent<Grass>();
            plane.GetComponent<Grass>().id = block.id;
            plane.GetComponent<Grass>().objName = block.objName;
            plane.GetComponent<Grass>().className = block.className;
            plane.GetComponent<Grass>().tagName = block.tagName;
            plane.GetComponent<Grass>().materialName = block.materialName;
            plane.GetComponent<Grass>().pointsArray = block.pointsArray;
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;

            if (block.checkpointNewObj != null)
            {
                plane.AddComponent<Checkpoint>();
                plane.GetComponent<Checkpoint>().id = block.checkpointNewObj.id;
            }

            plane.name = plane.GetComponent<Grass>().objName;
            plane.tag = plane.GetComponent<Grass>().tagName;
        }
        //Guardrail
        foreach (GuardrailDTO block in track.guardrailList)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            //Mesh mesh = plane.GetComponent<MeshFilter>().sharedMesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = block.pointsArray[0];//2
            vertices[1] = block.pointsArray[1];//0
            vertices[2] = block.pointsArray[2];//1
            vertices[3] = block.pointsArray[3];//3

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            //plane.AddComponent<BoxCollider>();
            plane.AddComponent<MeshCollider>();

            plane.GetComponent<Renderer>().material = materialsList.Where(x => x.name == block.materialName).FirstOrDefault();

            plane.name = block.objName;
            plane.AddComponent<Guardrail>();
            plane.GetComponent<Guardrail>().id = block.id;
            plane.GetComponent<Guardrail>().objName = block.objName;
            plane.GetComponent<Guardrail>().className = block.className;
            plane.GetComponent<Guardrail>().tagName = block.tagName;
            plane.GetComponent<Guardrail>().materialName = block.materialName;
            plane.GetComponent<Guardrail>().pointsArray = block.pointsArray;
            //plane.GetComponent<MeshCollider>().convex = true;
            //plane.GetComponent<BoxCollider>().isTrigger = true;

            plane.name = plane.GetComponent<Guardrail>().objName;
            plane.tag = plane.GetComponent<Guardrail>().tagName;
        }
        //Grid
        foreach (GridDTO block in track.gridList)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<Grid>();
            cube.GetComponent<Grid>().id = block.id;
            cube.GetComponent<Grid>().objName = block.objName;
            cube.GetComponent<Grid>().tagName = block.tagName;
            cube.GetComponent<Grid>().points = block.points;
            cube.GetComponent<Grid>().localScale = block.localScale;
            cube.GetComponent<Grid>().rotation = block.rotation;

            cube.name = cube.GetComponent<Grid>().objName;
            cube.tag = cube.GetComponent<Grid>().tagName;
            cube.transform.position = cube.GetComponent<Grid>().points;
            cube.transform.localScale = cube.GetComponent<Grid>().localScale;
            cube.transform.rotation = new Quaternion(cube.GetComponent<Grid>().rotation.x, cube.GetComponent<Grid>().rotation.y, cube.GetComponent<Grid>().rotation.z, cube.GetComponent<Grid>().rotation.w);

            cube.GetComponent<MeshRenderer>().enabled = false;
            DestroyImmediate(cube.GetComponent<BoxCollider>());
        }
        //Board
        foreach (BoardDTO block in track.boardList)
        {
            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = block.pointsArray[0];//2
            vertices[1] = block.pointsArray[1];//0
            vertices[2] = block.pointsArray[2];//1
            vertices[3] = block.pointsArray[3];//3

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;

            plane.GetComponent<Renderer>().material = materialsList.Where(x => x.name == block.materialName).FirstOrDefault();

            plane.name = block.objName;
            plane.AddComponent<Board>();
            plane.GetComponent<Board>().id = block.id;
            plane.GetComponent<Board>().objName = block.objName;
            plane.GetComponent<Board>().className = block.className;
            plane.GetComponent<Board>().tagName = block.tagName;
            plane.GetComponent<Board>().materialName = block.materialName;
            plane.GetComponent<Board>().pointsArray = block.pointsArray;
            plane.name = plane.GetComponent<Board>().objName;
            plane.tag = plane.GetComponent<Board>().tagName;
        }
        //Tree
        foreach (TreeDTO block in track.treeList)
        {

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = block.pointsArray[0];//2
            vertices[1] = block.pointsArray[1];//0
            vertices[2] = block.pointsArray[2];//1
            vertices[3] = block.pointsArray[3];//3

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;

            plane.GetComponent<Renderer>().material = materialsList.Where(x => x.name == block.materialName).FirstOrDefault();

            plane.name = block.objName;
            plane.AddComponent<Tree>();
            plane.GetComponent<Tree>().id = block.id;
            plane.GetComponent<Tree>().objName = block.objName;
            plane.GetComponent<Tree>().className = block.className;
            plane.GetComponent<Tree>().tagName = block.tagName;
            plane.GetComponent<Tree>().materialName = block.materialName;
            plane.GetComponent<Tree>().pointsArray = block.pointsArray;
            plane.name = plane.GetComponent<Tree>().objName;
            plane.tag = plane.GetComponent<Tree>().tagName;
        }
        //Sponsor
        foreach (SponsorDTO block in track.sponsorList)
        {

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = block.pointsArray[0];//2
            vertices[1] = block.pointsArray[1];//0
            vertices[2] = block.pointsArray[2];//1
            vertices[3] = block.pointsArray[3];//3

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            //plane.AddComponent<MeshCollider>();
            //plane.GetComponent<MeshCollider>().convex = true;
            //plane.GetComponent<MeshCollider>().isTrigger = true;

            plane.GetComponent<Renderer>().material = materialsList.Where(x => x.name == block.materialName).FirstOrDefault();

            plane.name = block.objName;
            plane.AddComponent<Sponsor>();
            plane.GetComponent<Sponsor>().id = block.id;
            plane.GetComponent<Sponsor>().objName = block.objName;
            plane.GetComponent<Sponsor>().className = block.className;
            plane.GetComponent<Sponsor>().tagName = block.tagName;
            plane.GetComponent<Sponsor>().materialName = block.materialName;
            plane.GetComponent<Sponsor>().pointsArray = block.pointsArray;
            plane.name = plane.GetComponent<Sponsor>().objName;
            plane.tag = plane.GetComponent<Sponsor>().tagName;
        }
        //board Directional
        foreach (BoardDirectionalDTO block in track.boardDirectionalList)
        {

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = block.pointsArray[0];//2
            vertices[1] = block.pointsArray[1];//0
            vertices[2] = block.pointsArray[2];//1
            vertices[3] = block.pointsArray[3];//3

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            //plane.AddComponent<MeshCollider>();            
            //plane.GetComponent<MeshCollider>().convex = false;
            //plane.GetComponent<MeshCollider>().isTrigger = true;

            plane.GetComponent<Renderer>().material = materialsList.Where(x => x.name == block.materialName).FirstOrDefault();

            plane.name = block.objName;
            plane.AddComponent<BoardDirectional>();
            plane.GetComponent<BoardDirectional>().id = block.id;
            plane.GetComponent<BoardDirectional>().objName = block.objName;
            plane.GetComponent<BoardDirectional>().className = block.className;
            plane.GetComponent<BoardDirectional>().tagName = block.tagName;
            plane.GetComponent<BoardDirectional>().materialName = block.materialName;
            plane.GetComponent<BoardDirectional>().pointsArray = block.pointsArray;
            plane.name = plane.GetComponent<BoardDirectional>().objName;
            plane.tag = plane.GetComponent<BoardDirectional>().tagName;
        }
        //Tunel
        foreach (TunelWallDTO block in track.tunelWallList)
        {

            GameObject plane = Instantiate(asphalt, Vector3.zero, Quaternion.identity);
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            Mesh mesh = plane.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            vertices[0] = block.pointsArray[0];//2
            vertices[1] = block.pointsArray[1];//0
            vertices[2] = block.pointsArray[2];//1
            vertices[3] = block.pointsArray[3];//3

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            plane.AddComponent<MeshCollider>();
            plane.GetComponent<MeshCollider>().convex = true;
            plane.GetComponent<MeshCollider>().isTrigger = true;

            plane.GetComponent<Renderer>().material = materialsList.Where(x => x.name == block.materialName).FirstOrDefault();

            plane.name = block.objName;
            plane.AddComponent<TunelWall>();
            plane.GetComponent<TunelWall>().id = block.id;
            plane.GetComponent<TunelWall>().objName = block.objName;
            plane.GetComponent<TunelWall>().className = block.className;
            plane.GetComponent<TunelWall>().tagName = block.tagName;
            plane.GetComponent<TunelWall>().materialName = block.materialName;
            plane.GetComponent<TunelWall>().pointsArray = block.pointsArray;
            plane.name = plane.GetComponent<TunelWall>().objName;
            plane.tag = plane.GetComponent<TunelWall>().tagName;
        }

        //objectReferenced
        foreach (ElementosExternosDTO block in track.objectReferencedList)
        {
            GameObject plane = Instantiate(prefabs[block.id], block.pos, new Quaternion(block.rot.x, block.rot.y, block.rot.z, block.rot.w));
            DestroyImmediate(plane.GetComponent<MeshCollider>());
            plane.transform.localScale = block.scale;
            DestroyImmediate(plane.GetComponent<ElementosExternos>());
            plane.GetComponent<Renderer>().material = prefabsMaterial.Where(x => x.name == block.materialName).FirstOrDefault();
            if (block.materialName == "bg")
            {
                string textureName = "BG_" + trackId.ToString();
                plane.GetComponent<Renderer>().material.mainTexture = Resources.Load(textureName, typeof(Texture2D)) as Texture;            
            }
            
        }
        //WayPoint
        foreach (WayPointDTO block in track.objWayPointList)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<WayPoint>();
            cube.GetComponent<WayPoint>().GetSettrackID = block.trackID;
            cube.GetComponent<WayPoint>().wayPointPosition = (WayPoint.WayPointPosition)block.wayPointPosition;
            cube.GetComponent<WayPoint>().GetIsLookToRedirect = block.isLookToRedirect;
            cube.GetComponent<WayPoint>().name = "WayPoint";
            cube.GetComponent<WayPoint>().tag = block.tagName;
            cube.GetComponent<WayPoint>().transform.position = block.pointsArray;
            cube.GetComponent<WayPoint>().transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            cube.GetComponent<WayPoint>().transform.localRotation = new Quaternion(block.rotationArray.x, block.rotationArray.y, block.rotationArray.z, block.rotationArray.w);

            cube.GetComponent<MeshRenderer>().enabled = false;
            DestroyImmediate(cube.GetComponent<BoxCollider>());
        }       

        //MeshCoordinates
        //track.meshCoordenadasDTO.coordenadasMesh
        GameObject gameObjectFinal = new GameObject();
        gameObjectFinal.name = "meshCoordenadasGrass";
        List<Vector3> coordenadasLista = new List<Vector3>();

        foreach (Vector3 coordenada in track.meshCoordenadasDTO.coordenadasMesh)
        {
            coordenadasLista.Add(coordenada);
        }

        List<int> triangles1 = new List<int>();
        foreach (int coordenadatrianglesPos in track.meshCoordenadasDTO.trianglesPos)
        {
            triangles1.Add(coordenadatrianglesPos);
        }

        gameObjectFinal.AddComponent<MeshCoordenadas>();
        gameObjectFinal.GetComponent<MeshCoordenadas>().coordenadasMesh = coordenadasLista.ToArray();
        gameObjectFinal.GetComponent<MeshCoordenadas>().trianglesPos = triangles1.ToArray();
        GameObject objGrass2 = new GameObject();
        objGrass2.AddComponent<MeshFilter>();
        objGrass2.AddComponent<MeshRenderer>();
        Mesh mesh2Grass = objGrass2.GetComponent<MeshFilter>().mesh;
        mesh2Grass.vertices = gameObjectFinal.GetComponent<MeshCoordenadas>().coordenadasMesh;
        mesh2Grass.triangles = gameObjectFinal.GetComponent<MeshCoordenadas>().trianglesPos;
        objGrass2.AddComponent<MeshCollider>();
        objGrass2.name = "meshColliderGrass";
        DestroyImmediate(objGrass2.GetComponent<MeshRenderer>());




    }
#if (UNITY_EDITOR)
    private List<Material> GenerateMaterialList()
    {        
        List<Material> materialList = new List<Material>();
        Component[] myComponents = this.gameObject.GetComponents(typeof(Component));
        SerializedObject serObj = new SerializedObject(myComponents[1]);
        SerializedProperty serProp = serObj.GetIterator();
        while (serProp.NextVisible(true))
        {
            if (serProp.type == "PPtr<$Material>")
            {
                Material ob = serProp.objectReferenceValue as Material;
                materialList.Add(ob);
            }
        }
        return materialList;
    }
#endif
    private Track LoadTrackFinalJson(string trackName)
    {
        var dfs = Resources.Load<TextAsset>(trackName).ToString();                
        Track obj = JsonConvert.DeserializeObject<Track>(dfs);
        return obj;
    }


    private List<Material> GetAllMaterials()
    {
        var materialList = new List<Material>();
        materialList.Add(material1);
        materialList.Add(material2);
        materialList.Add(materialGrass1);
        materialList.Add(materialGrass2);
        materialList.Add(materialGuardRail);
        materialList.Add(material1asphalt);
        materialList.Add(material2asphalt);
        materialList.Add(material2asphaltStreet);
        materialList.Add(material1RoadAsphalt);
        materialList.Add(material2RoadAsphalt);
        materialList.Add(material1grid);
        materialList.Add(material2grid);
        materialList.Add(asfalto1_Tunel);
        materialList.Add(asfalto2_Tunel);
        materialList.Add(asfalto1_1Tunel);
        materialList.Add(asfalto1_2Tunel);
        materialList.Add(grass1_Tunel);
        materialList.Add(grass2_Tunel);
        materialList.Add(guardrail_Tunel);
        materialList.Add(tunel_Wall);
        materialList.Add(roof1);
        materialList.Add(roof2);
        materialList.Add(pilarMaterial);
        materialList.Add(tunelMaterial);
        materialList.Add(boardLeftMaterial);
        materialList.Add(boardRightMaterial);
        materialList.Add(asfalto1Rua);
        materialList.Add(asfalto2Rua);
        materialList.Add(grass1Rua);
        materialList.Add(grass2Rua);
        materialList.Add(pitBoardLeft);
        materialList.Add(pitBoardRight);
        materialList.Add(treeMaterial);
        materialList.Add(sponsorMaterial);
        materialList.Add(sponsorMaterialCenter);
        materialList.Add(finishLine1Material);
        materialList.Add(finishLine2Material);

        for (int i = 0; i < prefabsMaterial.Length; i++)
            materialList.Add(prefabsMaterial[i]);

        return materialList;

    }

}
