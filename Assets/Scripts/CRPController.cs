using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CRPController : MonoBehaviour
{
    public GameObject Canvas;

    public Button Shout;
    public Button Reset;
    
    public Scene StartingScene;

    public TextMeshProUGUI message;

    // Start is called before the first frame update
    void Start()
    {
        message.text = "Step 1: The person is lying on the floor. Please the button to shout for help!";
        StartingScene = SceneManager.GetActiveScene();
        Reset.onClick.AddListener(ResetClick);
        Shout.onClick.AddListener(ShoutClick); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetClick(){
    	SceneManager.LoadScene(StartingScene.name);
    }

    void ShoutClick(){
    	Debug.Log ("Hello");
    }
}
