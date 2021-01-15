using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerV2 : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject FireExtinguisher;
    public GameObject Dustbin;
    public GameObject Cam;
    
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
        if (Input.GetMouseButtonDown (0)) 
        {
            RaycastHit hitInfo = new RaycastHit ();
            if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) 
            {
                Debug.Log ("Object Hit is " + hitInfo.collider.gameObject.name);
                if (hitInfo.collider.gameObject.name == "Fire Extinguisher")
                {
                    FireExtinguisher.transform.parent = Cam.transform;
                    FireExtinguisher.GetComponent<Animator>().Play("Bring Extinguisher to Camera");
                }

            } 
        }
    }

    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);
    }
}
