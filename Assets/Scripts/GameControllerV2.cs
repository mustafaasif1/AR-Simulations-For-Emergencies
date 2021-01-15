using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerV2 : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject FireExtinguisher;
    // public GameObject Camera;
    
    public Button Reset;

    public Scene StartingScene;
    
    // Start is called before the first frame update
    void Start()
    {
        StartingScene = SceneManager.GetActiveScene();
        Reset.onClick.AddListener(ResetClick);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         Debug.Log(hit.transform.name);
        //         if (hit.transform.name == "Fire Extinguisher")
        //         {
        //             Debug.Log(1);
        //         }
        //     }
        // }  
    }

    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);
    }
}
