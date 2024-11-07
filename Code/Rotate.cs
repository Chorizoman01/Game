using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    private float rotZ;
    public float RotationSpeed;
    public bool Clockwise;


    // This was used to anmate the circles so they roll
    void Update()
    {
        if(Clockwise == false)
        {
            rotZ += Time.deltaTime * RotationSpeed;
        }
        else
        {
            rotZ += -Time.deltaTime * RotationSpeed;
        }

        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }
}
