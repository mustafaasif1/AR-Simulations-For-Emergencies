using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class table : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject fire;
    public GameObject SimulateFireCanvas;
    public GameObject ResetCanvas;

    public Button SimulateFire;
    public Button Reset;

    public Text Message;

    public Scene StartingScene;

    void Start()
    {
        StartingScene = SceneManager.GetActiveScene();

        fire.SetActive(false);
        ResetCanvas.SetActive(false);

        SimulateFire.onClick.AddListener(SimulateFireClick);
        Reset.onClick.AddListener(ResetClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SimulateFireClick(){
    	fire.SetActive(true);
    	SimulateFireCanvas.SetActive(false);
    	ResetCanvas.SetActive(true);
    	Message.text = "Pull the pin of the fire extinguisher, this will break the tamper seal";
    }

    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);
    }
}
