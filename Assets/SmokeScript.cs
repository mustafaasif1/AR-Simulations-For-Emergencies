using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour
{
    public int collisions;
    public static bool colliding;
    public int frames;
    public int curr_frames;

    void Start(){
        collisions = 0;
        colliding = false;
        frames = 0;
        curr_frames = 0;

    }

    void LateUpdate(){
        if (colliding){
            curr_frames = frames;
        }
        colliding = false;
    }

    void Update(){
        frames += 1;
        if(!colliding){
            if ((frames - curr_frames)>5){
                collisions = 0;
                }
            }
    }


    void OnParticleCollision (GameObject other) {
    
    if(other.gameObject.name == "FireBase"){
       collisions += 1;
       colliding = true;
    }
    Debug.Log(collisions+"times hit");
    if(collisions > 120){
        GameObject.Find("PS_Parent").GetComponent<ParticleSystem>().Stop();
    }

    }
}
