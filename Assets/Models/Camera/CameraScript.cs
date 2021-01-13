using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Camera;

    // Update is called once per frame
    void Update()
    {
        float speed = 3.0f;
        var move = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
        transform.position += move * speed * Time.deltaTime;
    }

}
