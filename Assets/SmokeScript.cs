using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour
{
    void OnParticleCollision (GameObject other) {

       Debug.Log("Hittings" + other.gameObject.name);
       
    }
}
