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
    public GameObject C02;

    public Button SimulateFire;
    public Button Reset;
    public Button DropPin;
    public Button LiftAndRotateExtinguisherTowardsFire;
    public Button AimPipeTowardsFire;
    public Button ExtinguishFireButton;
    public float fireOver;
    public Text txt;
    

    public Scene StartingScene;

    void Start()
    {
        StartingScene = SceneManager.GetActiveScene();
        fireOver = 0;
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
       
        if (fireOver > 0){
            if ((Time.time - fireOver) > 1){
            
            Animator anim  = FireExtinguisherRotated.GetComponent<Animator>();
            anim.speed = 3;
            anim.Play("Rotated Pipe Handle UnPressed");
            
            C02.GetComponent<ParticleSystem>().Stop();
            fireOver = 0;
            }
            
        }
        
    }

    void SimulateFireClick(){
    	fire.SetActive(true);
    	SimulateFireCanvas.SetActive(false);
    	ResetCanvas.SetActive(true);
        DropPin.gameObject.SetActive(true);
        txt.text = "Remove the pin holding the flow of gas from the Fire Extinguisher to start using it by pressing the button on the right";
    }

    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);
    }

    void DropPinClick()
    {
        FireExtinguisher.GetComponent<Animator>().Play("Fire Extinguisher clip dropping on the floor");
        DropPin.gameObject.SetActive(false);
        LiftAndRotateExtinguisherTowardsFire.gameObject.SetActive(true);
        txt.text = "In order to lift the Fire Extinguisher above to the level of the fire press the button on the right";
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
        txt.text = "To point the pipe towards the fire, press the button on the right";
    }

    void AimPipeTowardsFireClick()
    {
        FireExtinguisher.SetActive(false);
        FireExtinguisherRotated.SetActive(true);
        AimPipeTowardsFire.gameObject.SetActive(false);
        ExtinguishFireButton.gameObject.SetActive(true);
        txt.text = "Clench the lever of the fire extinguisher in order to extinguish the fire by using the button on the right";
    }

    void ExtinguishFireClick()
    {
        Animator anim  = FireExtinguisherRotated.GetComponent<Animator>();
        anim.speed = 3;
        anim.Play("Rotated Pipe Handle Pressed");
        ExtinguishFireButton.gameObject.SetActive(false); 
        C02.SetActive(true);
        Time.timeScale = 0.4f;
        fire.GetComponent<ParticleSystem>().Stop();
        fireOver = Time.time;
        txt.text = "The tutorial ends here, remember safely let go of the lever";
        
        
        
        
    }  
}
