using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Camera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("1Key"))
        {
            Camera.GetComponent<Animator>().Play("Camera Zoom in Fire Extinguisher");
        }
    }
}
