using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private float rotate;
    private void Start()
    {
        rotate = 0.1f;
    }
    public void Rotate(int dam)
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(-rotate, rotate));
        rotate *= 1 + dam * 1f / 700f;
    }
}
