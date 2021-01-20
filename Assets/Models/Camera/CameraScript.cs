using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Camera;

    // Update is called once per frame
    void Update()
    {
        if(GameControllerV2.on){
            float speed = 4.0f;
            var velocidade = 30;

            var move = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
            transform.position += move * speed * Time.deltaTime;

            if (Input.GetKey(KeyCode.N))
            {
                transform.Rotate(Vector3.up * velocidade * Time.deltaTime); 
            }
            
            if (Input.GetKey(KeyCode.M))
            {
                transform.Rotate(-Vector3.up * velocidade * Time.deltaTime);
            }
        }
    }

    

}
