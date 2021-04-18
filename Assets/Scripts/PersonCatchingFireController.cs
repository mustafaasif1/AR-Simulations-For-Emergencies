using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using GoogleARCore;
using System;
using GoogleARCore.Examples.Common;

public class PersonCatchingFireController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Person;
    private GameObject Fire;
    public Button ActionButton;
    public Button Reset;
    public Scene StartingScene;
    public int rolledTimes;
    public bool letsRoll;
    public TextMeshProUGUI message;
    public int maxRoll;
    public static bool initDone;
    public bool figured;
    public static bool gameOn;
    public GameObject GameObjectVerticalPlanePrefab;
    public GameObject GameObjectHorizontalPlanePrefab;
    public GameObject Cam;
    
    

    private const float _prefabRotation = 180.0f;

    void Start()
    {
        StartingScene = SceneManager.GetActiveScene();
        Reset.onClick.AddListener(ResetClick);
        ActionButton.onClick.AddListener(onFire);
        ActionButton.gameObject.SetActive(false);
        rolledTimes = 0;
        letsRoll = true;
        maxRoll = 9;
        figured = false;
        initDone = false;
        gameOn = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (figured){
            Person = GameObject.Find("ManOnFire");
            Fire = Person.transform.Find("PS_Parent").gameObject;
            ActionButton.gameObject.SetActive(true);
            message.text = "In this scenario, we will learn the actions needed to take if your clothes catch fire. Press the button to simulate the scenario";
        
            figured = false;
            gameOn = true;
        
        }
        if(gameOn){
            Fire.transform.position = Person.transform.Find("mixamorig:Hips").gameObject.transform.position;
        }

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

    }


    void ResetClick()
    {
        SceneManager.LoadScene(StartingScene.name);
        message.text = "In this scenario, we will learn the actions needed to take if your clothes catch fire. Press the button to simulate the scenario";


    }

    void onFire()
    {
        if (letsRoll)
        {
            letsRoll = false;
            Person.GetComponent<Animator>().Play("In Agony");
            Fire.SetActive(true);
            ActionButton.onClick.RemoveListener(onFire);
            ActionButton.gameObject.SetActive(false);
            StartCoroutine(WaitForFire());
            ActionButton.onClick.AddListener(Faller);
            ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Drop to the Floor";
            message.text = "It is very important not to panic and run. Running fans the flames and increases the fire. Drop to the floor";
        }
    }

    void Faller()
    {
        if (letsRoll)
        {
            letsRoll = false;
            ActionButton.gameObject.SetActive(false);
            StartCoroutine(WaitForFire());
            Person.GetComponent<Animator>().Play("Falling Person");
            ActionButton.onClick.RemoveListener(Faller);
            ActionButton.onClick.AddListener(Roller);
            ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Roll";
            message.text = "Roll backwards and forwards on the flame to smother the fire. By rolling on the flames you starve them of oxygen and put out the fire.";
        }
    }

    IEnumerator WaitForFire()
    {
        yield return new WaitForSeconds(3.0f);
        letsRoll = true;
        ActionButton.gameObject.SetActive(true);
    }

    IEnumerator WaitForRoll()
    {
        yield return new WaitForSeconds(1.2f);
        letsRoll = true;
    }

    void Roller()
    {
        if (letsRoll && rolledTimes % 2 == 0)
        {
            letsRoll = false;
            Person.GetComponent<Animator>().Play("Rolling Person");
            StartCoroutine(WaitForRoll());
            rolledTimes++;
            message.text = rolledTimes.ToString();
        
        }
        if (letsRoll && rolledTimes % 2 == 1)
        {
            letsRoll = false;
            Person.GetComponent<Animator>().Play("Rolling Person Opposite");
            StartCoroutine(WaitForRoll());
            rolledTimes++;
            message.text = rolledTimes.ToString();
        
        }
        if (rolledTimes > maxRoll)
        {

            ActionButton.onClick.RemoveListener(Roller);
            ActionButton.onClick.AddListener(Stander);
            Fire.GetComponentInChildren<ParticleSystem>().Stop();
            ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stand Up";
            message.text = "Stand Up. Immediately treat the area with cool running water (e.g. from a cold tap) for 20 minutes. Only apply the water to the burned area. Consult a doctor.";

        }
        

    }

    void Stander()
    {
        if (letsRoll)
        {
            Person.GetComponent<Animator>().Play("Standing Person");
            ActionButton.onClick.RemoveListener(Stander);
            ActionButton.gameObject.SetActive(false);
        }

    }
}
