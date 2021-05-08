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

public class SmokeSceneController : MonoBehaviour
{

    private GameObject Person;
    private GameObject Fire;
    public Button ActionButton;
    public Button Reset;
    public Scene StartingScene;
    public int crawledTimes;
    public bool letsCrawl;
    public TextMeshProUGUI message;
    public int maxCrawl;
    public static bool initDone;
    public bool figured;
    public static bool gameOn;
    public GameObject GameObjectVerticalPlanePrefab;
    public GameObject GameObjectHorizontalPlanePrefab;
    public GameObject Cam;
    // Start is called before the first frame update
    void Start()
    {
        StartingScene = SceneManager.GetActiveScene();
        Reset.onClick.AddListener(ResetClick);
        ActionButton.onClick.AddListener(onSmoke);
        ActionButton.gameObject.SetActive(false);
        crawledTimes = 0;
        letsCrawl = true;
        maxCrawl = 9;
        figured = true;
        initDone = false;
        gameOn = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (figured){
            Person = GameObject.Find("Male");
            ActionButton.gameObject.SetActive(true);
            
            figured = false;
            gameOn = true;
        
        }

        if (gameOn){

        }
        
    }

    void ResetClick()
        {
        SceneManager.LoadScene(StartingScene.name); 
    }



    void onSmoke()
        {
        
    }
}
