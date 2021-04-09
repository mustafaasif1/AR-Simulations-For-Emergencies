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
    
    void Start()
    {
        StartingScene = SceneManager.GetActiveScene();
        Person = GameObject.Find("ManOnFire");
        Fire = Person.transform.Find("PS_Parent").gameObject;
        ActionButton.gameObject.SetActive(true);
        Reset.onClick.AddListener(ResetClick);
        ActionButton.onClick.AddListener(onFire);
        rolledTimes = 0;
        
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
        Person.GetComponent<Animator>().Play("In Agony");
        Fire.SetActive(true);
        ActionButton.onClick.RemoveListener(onFire);
        
        ActionButton.onClick.AddListener(Faller);
        ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Fall Down";
        
    }

    void Faller(){
        Person.GetComponent<Animator>().Play("Falling Person");
        ActionButton.onClick.RemoveListener(Faller);
        ActionButton.onClick.AddListener(Roller);
        ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Roll";
        
    }
    void Roller(){
        Person.GetComponent<Animator>().Play("Rolling Person");
        rolledTimes ++;
        if (rolledTimes > 3){
            ActionButton.onClick.RemoveListener(Roller);
            ActionButton.onClick.AddListener(Stander);
            ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Stand Up";
        
        }   

    }

    void Stander(){
        Person.GetComponent<Animator>().Play("Standing Person");
        ActionButton.onClick.RemoveListener(Stander);
            
    }
}
