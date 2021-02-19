using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using TMPro;
using GoogleARCore.Examples.HelloAR;
using GoogleARCore.Examples.Common;

public class DetectedPlaneVisualizer1 : MonoBehaviour
{
    public GameObject TrackedPlanePrefab;
    private List<DetectedPlane> _newPlanes = new List<DetectedPlane>();
    public GameObject setUp;
    public bool active;
    public TextMeshProUGUI message;
    public GameObject Cam;
    public bool doneTouch;
    
    void Start(){
        active = false;
        doneTouch = false;
        message.text = "Scanning for Planes ...";

    }

    // Update is called once per frame
    void Update()
    {
        Session.GetTrackables<DetectedPlane>(_newPlanes, TrackableQueryFilter.New);

        if (!active && _newPlanes.Count > 0){
            message.text = "Tap on the mesh to spawn the Scenarios";
            active = true;
        }
        if(!doneTouch){
        if (Input.touchCount >= 1){
            Touch touch = Input.GetTouch(0);
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.Default;
            if(touch.phase == TouchPhase.Began){
                if(Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)){
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                    Instantiate(setUp, anchor.transform.position, anchor.transform.rotation, anchor.transform  );
                    GameControllerV2.figured = true;
                    doneTouch = true;
                }
            }
        }
        }


        

        // Iterate over planes found in this frame and instantiate corresponding GameObjects to visualize them.
        foreach (var curPlane in _newPlanes)
        {
            // Instantiate a plane visualization prefab and set it to track the new plane. The transform is set to
            // the origin with an identity rotation since the mesh for our prefab is updated in Unity World
            // coordinates.
            var planeObject = Instantiate(TrackedPlanePrefab, Vector3.zero, Quaternion.identity, transform);
            planeObject.GetComponent<DetectedPlaneVisualizer>().Initialize(curPlane);

            // Apply a random color and grid rotation.
            planeObject.GetComponent<Renderer>().material.SetColor("_GridColor", new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)));
            planeObject.GetComponent<Renderer>().material.SetFloat("_UvRotation", Random.Range(0.0f, 360.0f));
        }
    }
}
