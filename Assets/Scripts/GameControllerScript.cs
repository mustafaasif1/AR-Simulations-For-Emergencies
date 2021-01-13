using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{

    public GameObject FireExtinguisher;
    public GameObject FireExtinguisherRotated;
    public GameObject fire;
    public GameObject SimulateFireCanvas;
    public GameObject ResetCanvas;
    public GameObject Camera;

    public Button SimulateFire;
    public Button Reset;
    public Button DropPin;
    public Button LiftAndRotateExtinguisherTowardsFire;
    public Button AimPipeTowardsFire;
    public Button ExtinguishFireButton;
    

    public Scene StartingScene;

    void Start()
    {
        StartingScene = SceneManager.GetActiveScene();

        fire.SetActive(false);
        DropPin.gameObject.SetActive(false);
        LiftAndRotateExtinguisherTowardsFire.gameObject.SetActive(false);
        ExtinguishFireButton.gameObject.SetActive(false);
        AimPipeTowardsFire.gameObject.SetActive(false);
        
        ExtinguishFireButton.onClick.AddListener(ExtinguishFireClick);
        LiftAndRotateExtinguisherTowardsFire.onClick.AddListener(SimulateLifting);
        SimulateFire.onClick.AddListener(SimulateFireClick);
        Reset.onClick.AddListener(ResetClick);
        DropPin.onClick.AddListener(DropPinClick);
        AimPipeTowardsFire.onClick.AddListener(AimPipeTowardsFireClick);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetButtonDown("1Key"))
        // {
        //     Camera.GetComponent<Animator>().Play("Camera Zoom in Fire Extinguisher");
        // }
    }

    void SimulateFireClick(){
    	fire.SetActive(true);
    	SimulateFireCanvas.SetActive(false);
    	ResetCanvas.SetActive(true);
        DropPin.gameObject.SetActive(true);
    }

    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);
    }

    void DropPinClick()
    {
        FireExtinguisher.GetComponent<Animator>().Play("Fire Extinguisher clip dropping on the floor");
        DropPin.gameObject.SetActive(false);
        LiftAndRotateExtinguisherTowardsFire.gameObject.SetActive(true);
    }

    void SimulateLifting()
    {
        Destroy (FireExtinguisher.transform.Find("polySurface7").gameObject);
        Destroy (FireExtinguisher.transform.Find("polySurface8").gameObject);
        Destroy (FireExtinguisher.transform.Find("polySurface11").gameObject);
        Destroy (FireExtinguisher.transform.Find("Glass_Part").gameObject);
        // FireExtinguisher.transform.position = new Vector3(FireExtinguisher.transform.position.x, FireExtinguisher.transform.position.y + 1, FireExtinguisher.transform.position.z);
        FireExtinguisher.GetComponent<Animator>().Play("Fire Extinguisher Lifting and Facing the Fire");
        LiftAndRotateExtinguisherTowardsFire.gameObject.SetActive(false);
        AimPipeTowardsFire.gameObject.SetActive(true);
    }

    void AimPipeTowardsFireClick()
    {
        FireExtinguisher.SetActive(false);
        FireExtinguisherRotated.SetActive(true);
        AimPipeTowardsFire.gameObject.SetActive(false);
        ExtinguishFireButton.gameObject.SetActive(true);
    }

    void ExtinguishFireClick()
    {
        FireExtinguisherRotated.GetComponent<Animator>().Play("Rotated Pipe Handle Pressed");
        ExtinguishFireButton.gameObject.SetActive(false);
        
    }  
}
