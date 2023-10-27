using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeDefinePitStrategy : MonoBehaviour
{
    GameObject cube1;
    GameObject cube2;
    GameObject cube3;
    List<GameObject> cubes = new List<GameObject>();
    List<GameObject> cubes1 = new List<GameObject>();
    void Start()
    {
        cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube1.transform.position = new Vector3(-2.0f, 0.0f, 0.0f);
        cube1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);        

        cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube2.transform.position = new Vector3(12.0f, 0.0f, 0.0f);
        cube2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);        

        cube3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube3.transform.position = new Vector3(5.0f, 10.0f, 0.0f);
        cube3.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
        Vector2 Point_1 = new Vector2(cube1.transform.position.x, cube1.transform.position.y);
        Vector2 Point_2 = new Vector2(cube3.transform.position.x, cube3.transform.position.y);
        float angle = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x) * 180 / Mathf.PI;
        float angleRadians = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x);
        
        float tangent = Mathf.Tan(angleRadians);
        int inc1 = 0;
        for (int i = 0; i < 14; i++)
        {                        
            if (cube1.transform.position.x + ((float)i * 0.5f) >= 0.0f)
            {                
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubes.Add(cube);
                float opposite = tangent * ((float)i * 0.5f);
                cubes[inc1].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                cubes[inc1].transform.position = new Vector3(cube1.transform.position.x + ((float)i * 0.5f), opposite, 0.0f);
                inc1++;
            }            
        }
        
        Vector2 Point_3 = new Vector2(cube2.transform.position.x, cube2.transform.position.y);
        Vector2 Point_4 = new Vector2(cube3.transform.position.x, cube3.transform.position.y);
        float angle1 = Mathf.Atan2(Point_4.y - Point_3.y, Point_4.x - Point_3.x) * 180 / Mathf.PI;
        float angleRadians1 = Mathf.Atan2(Point_4.y - Point_3.y, Point_4.x - Point_3.x);
        
        float tangent1 = Mathf.Tan(angleRadians);
        int inc2 = 0;
        for (int i = 0; i < 14; i++)
        {
            if (cube2.transform.position.x - ((float)i * 0.5f) <= 10.0f)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubes1.Add(cube);
                float opposite = tangent1 * ((float)i * 0.5f);
                cubes1[inc2].transform.position = new Vector3(cube2.transform.position.x - ((float)i * 0.5f), opposite, 0.0f);
                cubes1[inc2].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                inc2++;
            }                
        }
        
        cube1.SetActive(false);
        cube2.SetActive(false);
        
    }

    void Update()
    {
        Vector2 Point_1 = new Vector2(cube1.transform.position.x, cube1.transform.position.y);
        Vector2 Point_2 = new Vector2(cube3.transform.position.x, cube3.transform.position.y);
        float angle = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x) * 180 / Mathf.PI;
        float angleRadians = Mathf.Atan2(Point_2.y - Point_1.y, Point_2.x - Point_1.x);

        float tangent = Mathf.Tan(angleRadians);
        int inc1 = 0;
        for (int i = 0; i < 14; i++)
        {
            if (cube1.transform.position.x + ((float)i * 0.5f) >= 0.0f)
            {                
                float opposite = tangent * ((float)i * 0.5f);
                cubes[inc1].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                cubes[inc1].transform.position = new Vector3(cube1.transform.position.x + ((float)i * 0.5f), opposite, 0.0f);
                inc1++;
            }
        }

        Vector2 Point_3 = new Vector2(cube2.transform.position.x, cube2.transform.position.y);
        Vector2 Point_4 = new Vector2(cube3.transform.position.x, cube3.transform.position.y);
        float angle1 = Mathf.Atan2(Point_4.y - Point_3.y, Point_4.x - Point_3.x) * 180 / Mathf.PI;
        float angleRadians1 = Mathf.Atan2(Point_4.y - Point_3.y, Point_4.x - Point_3.x);

        float tangent1 = Mathf.Tan(angleRadians);
        int inc2 = 0;
        for (int i = 0; i < 14; i++)
        {
            if (cube2.transform.position.x - ((float)i * 0.5f) <= 10.0f)
            {                
                float opposite = tangent1 * ((float)i * 0.5f);
                cubes1[inc2].transform.position = new Vector3(cube2.transform.position.x - ((float)i * 0.5f), opposite, 0.0f);
                cubes1[inc2].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                inc2++;
            }
        }

    }
}
