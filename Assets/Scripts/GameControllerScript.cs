using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{

    public GameObject FireExtinguisher;
    public GameObject fire;
    public GameObject SimulateFireCanvas;
    public GameObject ResetCanvas;
    public GameObject Camera;

    public Button SimulateFire;
    public Button Reset;
    public Button DropPin;

    public Scene StartingScene;

    void Start()
    {
        StartingScene = SceneManager.GetActiveScene();

        fire.SetActive(false);
        ResetCanvas.SetActive(false);

        SimulateFire.onClick.AddListener(SimulateFireClick);
        Reset.onClick.AddListener(ResetClick);
        DropPin.onClick.AddListener(DropPinClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("1Key"))
        {
            Camera.GetComponent<Animator>().Play("Camera Zoom in Fire Extinguisher");
        }
    }

    void SimulateFireClick(){
    	fire.SetActive(true);
    	SimulateFireCanvas.SetActive(false);
    	ResetCanvas.SetActive(true);
    }

    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);
    }

    void DropPinClick()
    {
        FireExtinguisher.GetComponent<Animator>().Play("Fire Extinguisher clip dropping on the floor");
    }
}
