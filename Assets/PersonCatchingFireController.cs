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
    public TextMeshProUGUI message;

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
        message.text = "In this scenario, we will learn the actions needed to take if your clothes catch fire. Press the button to simulate the scenario";

    }

    // Update is called once per frame
    void Update()
    {
        Fire.transform.position = Person.transform.Find("mixamorig:Hips").gameObject.transform.position;
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
            StartCoroutine(WaitForFire());
            ActionButton.onClick.AddListener(Faller);
            ActionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Fall Down";
            message.text = "It is very important not to panic and run. Running fans the flames and increases the fire. Drop to the floor";
        }
    }

    void Faller()
    {
        if (letsRoll)
        {
            letsRoll = false;
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
        }
        if (letsRoll && rolledTimes % 2 == 1)
        {
            letsRoll = false;
            Person.GetComponent<Animator>().Play("Rolling Person Opposite");
            StartCoroutine(WaitForRoll());
            rolledTimes++;
        }
        if (rolledTimes > 8)
        {

            ActionButton.onClick.RemoveListener(Roller);
            ActionButton.onClick.AddListener(Stander);
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
        }

    }
}
