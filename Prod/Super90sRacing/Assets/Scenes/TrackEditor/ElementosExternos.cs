using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ElementosExternos : MonoBehaviour
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public string materialName;
    public int id;
    void Update()
    {
        pos = this.transform.position;
        rot = this.transform.rotation;
        scale = this.transform.localScale;
        materialName = this.GetComponent<Renderer>().sharedMaterial.name;
    }
}