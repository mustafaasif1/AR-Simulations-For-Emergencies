using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using GoogleARCore;
using GoogleARCore.Examples.Common;


public class CRPController : MonoBehaviour{
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
    public static bool gameOn;
    public bool compressionDone;
    public bool mouthToMouth;
    public bool listened;
    public GameObject female;
    public bool allowed;
    public GameObject GameObjectVerticalPlanePrefab;
    public GameObject GameObjectHorizontalPlanePrefab;
    public static bool initDone;
    public GameObject Cam;
    public bool figured;
    private const float _prefabRotation = 0.0f;

    


    // Start is called before the first frame update
    void Start()
    {
        StartingScene = SceneManager.GetActiveScene();
        Reset.onClick.AddListener(ResetClick);
        Shout.onClick.AddListener(ShoutClick);
        Mover.onClick.AddListener(HandsMover);
        Mover.gameObject.SetActive(false);
        handsTogether = false;
        firstTime = false;
        counter = 0;
        TimeRemaining = 20;
        TimeTrack = 20;
        TimeCount.gameObject.SetActive(false);
        gameOn = false;
        compressionDone = false;
        mouthToMouth = false;
        listened = false;
        allowed = true;
        initDone = false;
        figured = false;
    }


    void FixedUpdate(){
        if (!firstTime){
            TimeTrack = 20;
            TimeRemaining = 20;
        }
        if (firstTime && gameOn && !compressionDone) 
        {
            TimeTrack -= 1/50f;
            TimeRemaining = (int)TimeTrack;
            int minutes = TimeRemaining/60;
            int seconds = TimeRemaining%60;
            if (TimeRemaining < 10){
                
                TimeCount.color = new Color32(255,0,0,255);
            } else{
                TimeCount.color = new Color32(0, 255, 0,255);
            }
            if (firstTime & seconds >= 10) TimeCount.text = minutes.ToString() + ":" + seconds.ToString();
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
        if(!initDone){
            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                return;
            }

            // Should not handle input if the player is pointing on UI.
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }

            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            bool foundHit = false;
            
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;
            foundHit = Frame.Raycast(
                touch.position.x, touch.position.y, raycastFilter, out hit);
        

            if (foundHit)
            {
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(Cam.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    message.text = "Hit at back of the current DetectedPlane";
                }
                else
                {
                    // Choose the prefab based on the Trackable that got hit.
                    GameObject prefab;
                    if (hit.Trackable is DetectedPlane)
                    {
                        DetectedPlane detectedPlane = hit.Trackable as DetectedPlane;
                        if (detectedPlane.PlaneType == DetectedPlaneType.Vertical)
                        {
                            prefab = GameObjectVerticalPlanePrefab;
                        }
                        else
                        {
                            prefab = GameObjectHorizontalPlanePrefab;
                        }
                        initDone = true;
                        PlaneDiscoveryGuide.myInitDone = true;
        
                        figured = true;
                        
                    }
                    else{
                        prefab = GameObjectVerticalPlanePrefab;
                        message.text = "Please tap a mesh";
                    }
                    
                    // Instantiate prefab at the hit pose.
                    if (initDone){
                    var gameObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);

                    // Compensate for the hitPose rotation facing away from the raycast (i.e.
                    // camera).
                    gameObject.transform.Rotate(0, _prefabRotation, 0, Space.Self);

                    // Create an anchor to allow ARCore to track the hitpoint as understanding of
                    // the physical world evolves.
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                    // Make game object a child of the anchor.
                    gameObject.transform.parent = anchor.transform;
                    }
                    // Initialize Instant Placement Effect.
                    
                }
            } 
        }



        if (figured){
            
            male = GameObject.Find("Receiving Cpr");
            female = GameObject.Find("Giving Cpr");
            
            message.text = "The person is lying on the floor. Tap on the nose to check for his breath";
            
            figured = false;
            gameOn = true;

        }



        if (gameOn){
            if (Input.GetMouseButtonDown (0)) 
                {
                    Debug.Log("Press");
                    RaycastHit hitInfo = new RaycastHit ();
                    if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) 
                    {
                        Debug.Log ("Object Hit is " + hitInfo.collider.gameObject.name);
                        if (hitInfo.collider.gameObject.name == "HandMesh" & !handsTogether & listened){
                            handsTogether = true;
                            message.text = "Well Done! Now tap on the button to beign the CPR";
                            female.GetComponent<Animator>().Play("Wide Hands 1");
                            Mover.gameObject.SetActive(true); 
                        }
                        if (hitInfo.collider.gameObject.name == "FaceMesh" & !listened){
                            female.GetComponent<Animator>().Play("Checking for breath");
                            male.transform.Find("FaceMesh").gameObject.SetActive(false);
                            female.transform.Find("HandMesh").gameObject.SetActive(true);
                            StartCoroutine(waiter());
                            
                        }
                    }
                }
            }
        }


    IEnumerator waiter(){
        yield return new WaitForSeconds(5);
        listened = true;
        message.text = "We felt no breath on our hand. Now, press on the hands to bring them together and give a CPR";
        female.GetComponent<Animator>().Play("Back to Sitting");
        female.GetComponent<Animator>().SetTrigger("WidenTrig");
    }

    IEnumerator waitForCPR(){
        yield return new WaitForSeconds(0.5f);
        allowed = true;
    }

    IEnumerator waitForWithdraw(){
        yield return new WaitForSeconds(0.5f);
        male.GetComponent<Animator>().Play("Standing Up");
        
    }

    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);
    }

    void ShoutClick(){
    	Debug.Log ("Hello");
    }

    void HandsMover(){
        if (allowed){
            female.GetComponent<Animator>().Play("Compress Hands");
            if (counter<19){
                male.GetComponent<Animator>().Play("Chest Shaking");
            }
        }
        if (!firstTime){
            firstTime = true;
            message.text = "Excellent! Now keep on tapping the same button to compress the chest 20 times in 20 seconds";
            TimeCount.gameObject.SetActive(true);
        }
        else if (firstTime & allowed){
            
            counter ++;
            message.text = counter.ToString();
            allowed = false;
            StartCoroutine(waitForCPR());
            
        }

        if(counter > 19){
            message.text = "Well Done! The Person is Now able to breath again thanks to you!";
            compressionDone = true;
            mouthToMouth = true;
            gameOn = false;
            Mover.gameObject.SetActive(false);
            counter = 0;
            female.GetComponent<Animator>().Play("Hands Withdraw");
            StartCoroutine(waitForWithdraw());
            
       
        }   
    }
}
