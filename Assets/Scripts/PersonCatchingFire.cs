using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonCatchingFire : MonoBehaviour
{
    private GameObject Person;
    private GameObject Fire;
    // Start is called before the first frame update
    void Start()
    {
        Person = GameObject.Find("ManOnFire");
        Person.SetActive(false);
        Fire = Person.transform.Find("PS_Parent").gameObject;
        Fire.SetActive(true);
        Person.GetComponent<Animator>().Play("ManOnFire");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
