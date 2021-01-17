using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour
{
    public int collisions;
     public static bool colliding;

    void Start(){
        collisions = 0;
        colliding = false;
    }

    void LateUpdate(){
        colliding = false;
    }

    void Update(){
        if(!colliding){
            collisions = 0;
            }
    }


    void OnParticleCollision (GameObject other) {
    
    if(other.gameObject.name == "trash_can"){
       collisions += 1;
       colliding = true;
    }

       Debug.Log(collisions + "itni hain");

    }
}
