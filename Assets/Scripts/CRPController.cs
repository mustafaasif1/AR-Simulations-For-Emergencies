using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CRPController : MonoBehaviour
{
    public GameObject Canvas;

    public Button Shout;
    public Button Reset;
    public GameObject Hands;
    public Scene StartingScene;

    public TextMeshProUGUI message;
    public bool handsTogether;
    public int counter;
    public Button Mover;
    public bool firstTime;

    // Start is called before the first frame update
    void Start()
    {
        message.text = "Step 1: The person is lying on the floor. Tap on the hands to bring them together in order to perform CPR!";
        StartingScene = SceneManager.GetActiveScene();
        Reset.onClick.AddListener(ResetClick);
        Shout.onClick.AddListener(ShoutClick);
        Mover.onClick.AddListener(HandsMover);
        Mover.gameObject.SetActive(false);
        handsTogether = false;
        firstTime = false;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Hands = GameObject.Find("Hands");
        if (Input.GetMouseButtonDown (0)) 
            {
                Debug.Log("Press");
                RaycastHit hitInfo = new RaycastHit ();
                if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) 
                {
                    Debug.Log ("Object Hit is " + hitInfo.collider.gameObject.name);
                    if (hitInfo.collider.gameObject.name == "HandMesh" & !handsTogether){
                        Hands.GetComponent<Animator>().Play("Put Hands Together");
                        handsTogether = true;
                        message.text = "Well Done! Now tap on the button to beign the CPR";
                        Mover.gameObject.SetActive(true);
                    }

                }
            }
    }

    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);
    }

    void ShoutClick(){
    	Debug.Log ("Hello");
    }
    void HandsMover(){
        if (!firstTime){
            firstTime = true;
            message.text = "Excellent! Now keep on tapping the same button to compress the chest 30 times in 20 seconds";
        }
        if (firstTime){
            counter ++;
            message.text = "                           " + counter;

        }
        Hands.GetComponent<Animator>().SetTrigger("Active");
        
            
    }
}
