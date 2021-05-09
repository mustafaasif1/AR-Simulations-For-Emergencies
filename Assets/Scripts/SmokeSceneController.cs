﻿using System.Collections;
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

    public GameObject Person;
    public GameObject smoke1;
    public GameObject smoke2;
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
    public GameObject Door;
    public float speed;
    public bool move;
    // Start is called before the first frame update
    void Start()
    {
        StartingScene = SceneManager.GetActiveScene();
        Reset.onClick.AddListener(ResetClick);
        ActionButton.gameObject.SetActive(false);
        crawledTimes = 0;
        letsCrawl = true;
        maxCrawl = 8;
        figured = true;
        initDone = false;
        gameOn = false;
        speed = 1.0f;
        move = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (figured){
            Person = GameObject.Find("Male");
            Door = GameObject.Find("door modern");
            ActionButton.gameObject.SetActive(true);
            ActionButton.onClick.AddListener(onSmoke);
            smoke1 = GameObjectVerticalPlanePrefab.transform.Find("Smoke1").gameObject;
            smoke2 = GameObjectVerticalPlanePrefab.transform.Find("Smoke2").gameObject;
            figured = false;
            gameOn = true;
            message.text = "In this scenario we will learn how to escape a room full of smoke due to Fire. The smoke is dangerous and causes more deaths than the fire itself";
        
        }
        

        
        
    }
    void FixedUpdate(){
        if(move){
            Vector3 movement = new Vector3 (0.01f, 0.0f, 0.0f);
            Person.GetComponent<Rigidbody>().MovePosition(Person.transform.position + movement);


        }
    }

    void ResetClick()
        {
        SceneManager.LoadScene(StartingScene.name); 
    }



    void onSmoke()
        {
            
            smoke1.SetActive(true);
            smoke2.SetActive(true);
            ActionButton.gameObject.SetActive(false);
            Person.GetComponent<Animator>().Play("Idle Dizzy");
            StartCoroutine(WaitForFall());
            ActionButton.onClick.RemoveListener(onSmoke);
            ActionButton.onClick.AddListener(Faller);
            ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Fall to Knees";
            message.text = "Since the smoke rises upwards, the best option is to fall to your knees in order to start crawling";
            
            
    }

     void Faller()
    {
        if (letsCrawl)
        {
            letsCrawl = false;
            ActionButton.gameObject.SetActive(false);
            StartCoroutine(WaitForFall());
            Person.GetComponent<Animator>().Play("Falling to Knees");
            ActionButton.onClick.RemoveListener(Faller);
            ActionButton.onClick.AddListener(Crawler);
            ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Crawl";
            message.text = "Tap on the crawl button in order to crawl towards the door and out of the smoke";
        }
    }

    IEnumerator WaitForFall()
    {
        yield return new WaitForSeconds(2.5f);
        letsCrawl = true;
        ActionButton.gameObject.SetActive(true);
    }

    IEnumerator WaitForCrawl()
    {
        yield return new WaitForSeconds(0.18f);
        move = true;
        yield return new WaitForSeconds(0.8f);
        move = false;
        yield return new WaitForSeconds(0.8f);
        
        letsCrawl = true;
        ActionButton.gameObject.SetActive(true);
    }

    void Crawler()
        {
            if (letsCrawl)
            {
                letsCrawl = false;
                Person.GetComponent<Animator>().Play("Crawling Person");
                StartCoroutine(WaitForCrawl());
                crawledTimes++;
                message.text = "Keep tapping the crawl button to reach the door";
            }

            if (crawledTimes > maxCrawl)
            {

                ActionButton.onClick.RemoveListener(Crawler);
                ActionButton.onClick.AddListener(Stander);
                ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stand Up";
                message.text = "Excellent! Now stand up to figure out a way to open the door";
                
            }
        }

    void Stander(){
        if (letsCrawl)
        {
            Person.GetComponent<Animator>().Play("Standing");
            letsCrawl = false;
            StartCoroutine(WaitForFall());
            ActionButton.onClick.RemoveListener(Stander);
            ActionButton.onClick.AddListener(Kicker);
            ActionButton.gameObject.SetActive(false);
            ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Kick Door";
            message.text = "The door handle is extremely hot. Either kick the door open or use your elbows to open it";
                
        }

    }

    void Kicker(){
     if (letsCrawl)
        {
            Door.GetComponent<Animator>().Play("Door Open");
            Person.GetComponent<Animator>().Play("Kick");
            ActionButton.onClick.RemoveListener(Kicker);
            ActionButton.gameObject.SetActive(false);
            message.text = "Exit the room immediately. Our training ends here. Remember to always get checked by a doctor after you escape from a fire";
        }
    }

}
