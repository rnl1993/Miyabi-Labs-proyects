using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnToMainMenuScript : MonoBehaviour {

    // este es el codigo que te regresa al menu desde los exercicios 
    public float TimeRemaining;
    public Text TimerText;

    //private Double roundedTime;

    //private GamesScenesManager gamesScenesManager;

	// Funtions
	void Start () {
        
        //gamesScenesManager = FindObjectOfType<GamesScenesManager>();
	}
	
	// Update is called once per frame
	void Update () {

        TimeRemaining -= Time.deltaTime;
       /* if (TimerText != null){
            
            TimerText.text = "TimeRemaining: " + (roundedTime = Mathf.Round(TimeRemaining));
        }*/

        if (TimeRemaining <= 0){

            /*
            if (GamesScenesManager.remainingTime <= 0){
                
                GamesScenesManager.remainingTime = 30;
            }

            if (gamesScenesManager != null){
                
                gamesScenesManager.game = true;
            }
            */

            SceneManager.LoadScene("NewDashboard");
        }
	}
}
