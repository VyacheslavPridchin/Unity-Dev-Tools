using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public bool X = true, Y = true, Z = true;
    public float speed = 0.1f;

    Vector3 angle;
    private void Start()
    {
        angle = transform.localEulerAngles;
    }

    void Update()
    {
        if (X)
            angle.x += speed;

        if (Y)
            angle.y += speed;

        if (Z)
            angle.z += speed;

        transform.localRotation = Quaternion.Euler(angle);
    }
}
