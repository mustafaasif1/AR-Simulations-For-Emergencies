using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using GoogleARCore;

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
    
    void Start()
    {
        StartingScene = SceneManager.GetActiveScene();
        Person = GameObject.Find("ManOnFire");
        Fire = Person.transform.Find("PS_Parent").gameObject;
        ActionButton.gameObject.SetActive(true);
        Reset.onClick.AddListener(ResetClick);
        ActionButton.onClick.AddListener(onFire);
        rolledTimes = 0;
        letsRoll = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire.transform.position = Person.transform.Find("mixamorig:Hips").gameObject.transform.position;
    }


    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);

    }
    
    void onFire(){
        if(letsRoll){
            letsRoll = false;
            Person.GetComponent<Animator>().Play("In Agony");
            Fire.SetActive(true);
            ActionButton.onClick.RemoveListener(onFire);
            StartCoroutine(WaitForFire());
            ActionButton.onClick.AddListener(Faller);
            ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Fall Down";
        }
    }

    void Faller(){
        if(letsRoll){
            letsRoll = false;
            StartCoroutine(WaitForFire());
            Person.GetComponent<Animator>().Play("Falling Person");
            ActionButton.onClick.RemoveListener(Faller);
            ActionButton.onClick.AddListener(Roller);
            ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Roll";
        }
    }

    IEnumerator WaitForFire(){
    yield return new WaitForSeconds(3.0f);
    letsRoll = true;
    }

    IEnumerator WaitForRoll(){
    yield return new WaitForSeconds(1.2f);
    letsRoll = true;
    }

    void Roller(){
        if(letsRoll && rolledTimes%2 == 0){
            letsRoll = false;
            Person.GetComponent<Animator>().Play("Rolling Person");
            StartCoroutine(WaitForRoll());
            rolledTimes ++;
        }
        if(letsRoll && rolledTimes%2 == 1){
            letsRoll = false;
            Person.GetComponent<Animator>().Play("Rolling Person Opposite");
            StartCoroutine(WaitForRoll());
            rolledTimes ++;
        }
        if (rolledTimes > 8){
            
            ActionButton.onClick.RemoveListener(Roller);
            ActionButton.onClick.AddListener(Stander);
            ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stand Up";
            
        }   

    }

    void Stander(){
        if(letsRoll){
            Person.GetComponent<Animator>().Play("Standing Person");
            ActionButton.onClick.RemoveListener(Stander);
        }
            
    }
}
