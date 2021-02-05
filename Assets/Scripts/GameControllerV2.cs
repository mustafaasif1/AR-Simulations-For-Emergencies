using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControllerV2 : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject FireExtinguisher;
    public GameObject Dustbin;
    public GameObject Cam;
    public GameObject FireAlarmHandle;
    public GameObject AlarmSound;    

    public Button Reset;
    
    public Scene StartingScene;

    public TextMeshProUGUI message;
    public TextMeshProUGUI TimeCount;

    public int TimeRemaining;
    public float TimeTrack;


    bool PinIsRemoved = false;
    bool ExtinguisherInFrontOfCamera = false;
    bool aimed = false;
    bool firing = false;
    bool fireAlarmActivated = false;
    public static bool finishgame = false;
    public static bool on;
    public static bool putout;
    // Start is called before the first frame update
    void Start()
    {
        message.text = "Your dust bin has just caught fire and you have to put it out. Find the fire alarm and tap on the handle to activate it.";
        StartingScene = SceneManager.GetActiveScene();
        Reset.onClick.AddListener(ResetClick);
        FireExtinguisher.transform.Find("polySurface10").gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        AlarmSound.SetActive(false); 
        TimeRemaining = 120;
        TimeTrack = 120;
        TimeCount.text = TimeRemaining.ToString();
        on = true; 
        putout = false; 
        PinIsRemoved = false;
        ExtinguisherInFrontOfCamera = false;
        aimed = false;
        fireAlarmActivated = false;
        finishgame = false; 
    }

    // Update is called once per frame
    void FixedUpdate(){
        TimeTrack -= 1/50f;
        if (!finishgame) {
            TimeRemaining = (int)TimeTrack;
            int minutes = TimeRemaining/60;
            int seconds = TimeRemaining%60;
            if(on && !finishgame && seconds >= 10) TimeCount.text = minutes.ToString() + ":" + seconds.ToString();
            else if (on && !finishgame) TimeCount.text = minutes.ToString() + ":0" + seconds.ToString();
            else TimeCount.text = "0:00";
            if(TimeRemaining == 0) {
                on = false;
                AlarmSound.SetActive(false);
                Destroy(FireExtinguisher.transform.Find("polySurface36").gameObject.GetComponent<Collider>());
                Destroy(FireExtinguisher.transform.Find("polySurface37").gameObject.GetComponent<Collider>());
                if(firing){
                    FireExtinguisher.GetComponent<Animator>().Play("UnPressing Handle");
                    FireExtinguisher.transform.Find("Smoke").gameObject.SetActive(false);
                    firing = false;
                }
            }
        }
    }
    void Update()
    {
        if(on && !finishgame){
            if (Input.GetMouseButtonDown (0)) 
            {
                Debug.Log("Press");
                RaycastHit hitInfo = new RaycastHit ();
                if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) 
                {
                    Debug.Log ("Object Hit is " + hitInfo.collider.gameObject.name);
                    if (hitInfo.collider.name == "Circle.001" && !fireAlarmActivated){
                        FireAlarmHandle.GetComponent<Animator>().Play("Pull Handle");
                        fireAlarmActivated = true;
                        AlarmSound.SetActive(true);
                        message.text = "You have now activated the fire alarm. Now find the fire extinguisher and tap on it to hold it.";
                    }
                    if (hitInfo.collider.gameObject.name == "Fire Extinguisher" && fireAlarmActivated)
                    {
                        FireExtinguisher.transform.parent = Cam.transform;
                        FireExtinguisher.transform.position = new Vector3(Cam.transform.position.x - 1.5f, Cam.transform.position.y - 2.5f ,Cam.transform.position.z - 9f);
                        // FireExtinguisher.GetComponent<Animator>().Play("Bring Extinguisher to Camera");
                        ExtinguisherInFrontOfCamera = true;
                        FireExtinguisher.layer = LayerMask.NameToLayer("Ignore Raycast");
                        message.text = "Now tap on the pin to pull it out. This will break the tamper seal";
                    }

                    if (hitInfo.collider.name == "pinMesh" & ExtinguisherInFrontOfCamera)
                    {
                        FireExtinguisher.GetComponent<Animator>().Play("Remove the Pin");
                        PinIsRemoved = true;
                        FireExtinguisher.transform.Find("polySurface10").gameObject.layer = LayerMask.NameToLayer("Default");
                        message.text = "Now tap on the pipe and take aim. Point the nozzle at the base of the fire";
            
                    }

                    if (hitInfo.collider.gameObject.name == "polySurface36" || hitInfo.collider.gameObject.name == "polySurface37"){

                    }
                    else if (hitInfo.collider.gameObject.name == "PipeCube" & PinIsRemoved & !aimed)
                    {
                        Debug.Log("here");
                        Destroy (FireExtinguisher.transform.Find("polySurface14").gameObject);
                        FireExtinguisher.GetComponent<Animator>().Play("Point Pipe Towards Fire");
                        aimed = true;
                        Destroy(FireExtinguisher.transform.Find("polySurface10").gameObject.GetComponent<Collider>());
                        Destroy(FireExtinguisher.GetComponent<Collider>());
                        message.text = "Now move towards the dustbin and keep pressing the handle to release the extinguishing agent until the fire is put out";
                    }

                } 
            }        
            else if (Input.GetMouseButton(0) && aimed){
                RaycastHit hitInfo = new RaycastHit ();
                if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) {
                    if ((hitInfo.collider.gameObject.name == "polySurface36" | hitInfo.collider.gameObject.name == "polySurface37") && aimed)
                    {
                        // Debug.Log("Fireeee");
                        FireExtinguisher.GetComponent<Animator>().Play("Pressing Handle");
                        FireExtinguisher.transform.Find("Smoke").gameObject.SetActive(true);
                        firing = true;
                    }
                    
                }
            
                
            }

            else if (Input.GetMouseButtonUp(0) && aimed){
                if (firing){
                    FireExtinguisher.GetComponent<Animator>().Play("UnPressing Handle");
                    FireExtinguisher.transform.Find("Smoke").gameObject.SetActive(false);
                    firing = false;
                    if(putout) {
                        AlarmSound.SetActive(false);
                        message.text = "Congratulations! The fire has been put out";
                        finishgame = true;
                    }
                }


            }
        }
        else if (!finishgame){
            message.text = "You have run out of time. Click on Reset to try again";
        }

        // if (isFiring) 
        // {
        //     Debug.Log("Fire in the hole");
        // }
    }

    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);
    }

    // public void pointerDown()
    // {
    //     isFiring = true;
    // }

    // public void pointerUp()
    // {
    //     isFiring = false;
    // }


}
