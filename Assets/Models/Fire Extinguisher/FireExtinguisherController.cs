using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireExtinguisherController : MonoBehaviour
{
    public GameObject FireExtinguisher;
    public Button DropPin;

    void Start()
    {
        DropPin.onClick.AddListener(DropPinClick);
    }

    void DropPinClick()
    {
        FireExtinguisher.GetComponent<Animator>().Play("Fire Extinguisher clip dropping on the floor");  
    }
}
