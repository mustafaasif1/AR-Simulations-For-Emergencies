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
    
    void Start()
    {
        StartingScene = SceneManager.GetActiveScene();
        Person = GameObject.Find("ManOnFire");
        Fire = Person.transform.Find("PS_Parent").gameObject;
        ActionButton.gameObject.SetActive(true);
        Reset.onClick.AddListener(ResetClick);
        ActionButton.onClick.AddListener(Faller);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);
    }
    
    void Faller(){
        Debug.Log("AHHH");
        Person.GetComponent<Animator>().SetBool("FallDown", true);
    }
}
