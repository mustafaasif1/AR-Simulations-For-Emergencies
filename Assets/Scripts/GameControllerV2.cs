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
                Debug.Log ("Object Hit is " + hitInfo.collider.name);
                if (hitInfo.collider.gameObject.name == "Fire Extinguisher")
                {
                    FireExtinguisher.transform.parent = Cam.transform;
                    FireExtinguisher.transform.position = new Vector3(Cam.transform.position.x - 1.5f, Cam.transform.position.y - 1.6f ,Cam.transform.position.z - 8.5f);
                    // FireExtinguisher.GetComponent<Animator>().Play("Bring Extinguisher to Camera");
                }

                if (hitInfo.collider.name == "polySurface14")
                {
                    Debug.Log ("HELOOOO");
                    FireExtinguisher.GetComponent<Animator>().Play("Remove the Pin");
                    // Destroy (FireExtinguisher.transform.Find("polySurface14").gameObject);
                }

            } 
        }
    }

    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);
    }
}
