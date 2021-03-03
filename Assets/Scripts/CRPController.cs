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
    public GameObject male;
    public float TimeTrack;
    public TextMeshProUGUI TimeCount;
    public int TimeRemaining = 20;
    public bool gameOn;
    public bool compressionDone;
    public bool mouthToMouth;
    


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
        Hands = GameObject.Find("Hands");
        male = GameObject.Find("Male_1");
        TimeRemaining = 20;
        TimeTrack = 20;
        TimeCount.gameObject.SetActive(false);
        gameOn = true;
        compressionDone = false;
        mouthToMouth = false;
    }


    void FixedUpdate(){
        if (!firstTime){
            TimeTrack = 20;
            TimeRemaining = 20;
        }
        if (firstTime && gameOn && !compressionDone) {
            TimeTrack -= 1/50f;
            TimeRemaining = (int)TimeTrack;
            int minutes = TimeRemaining/60;
            int seconds = TimeRemaining%60;
            if (TimeRemaining < 10){
                TimeCount.color = new Color32(255,0,0,255);
            } else{
                TimeCount.color = new Color32(0, 255, 0,255);
            }
            if(firstTime & seconds >= 10) TimeCount.text = minutes.ToString() + ":" + seconds.ToString();
            else if (firstTime) TimeCount.text = minutes.ToString() + ":0" + seconds.ToString();
            else TimeCount.text = "0:00";
            if(TimeRemaining == 0) {
                message.text = "You failed to compress the chest enough times!";
                gameOn = false;
                Mover.gameObject.SetActive(false);
                }
            
            }

            if(compressionDone){
                TimeCount.gameObject.SetActive(false);
                firstTime = false;
                TimeTrack = 20;
                TimeRemaining = 20;
                compressionDone = false;
                }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOn){
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
                        if (hitInfo.collider.gameObject.name == "FaceMesh" & mouthToMouth){
                            //disable Hands
                            //play animation of giving mouth to mouth
                        }

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

        Hands.GetComponent<Animator>().Play("Compress Hands");
        male.GetComponent<Animator>().Play("Chest shaking");
        if (!firstTime){
            firstTime = true;
            message.text = "Excellent! Now keep on tapping the same button to compress the chest 30 times in 20 seconds";
            TimeCount.gameObject.SetActive(true);
        }
        else if (firstTime){
            counter ++;
            message.text = "                           " + counter;

        }

        if(counter > 29){
            message.text = "Well Done! It is time to breath air into the mouth of the person! Tap on the mouth to do so.";
            compressionDone = true;
            mouthToMouth = true;
            counter = 0;
        }
        
            
    }
}
