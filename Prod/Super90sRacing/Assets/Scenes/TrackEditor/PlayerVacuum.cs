using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVacuum : MonoBehaviour
{
    [SerializeField]
    public bool carCollide = false;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.collider.tag == "Car")            
                if (hit.distance < 1.5f)                
                    carCollide = true;                
                else
                    carCollide = false;            
            else            
                carCollide = false;            
        }
    }
}
