using System;
using UnityEngine;

using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnToDashboard : MonoBehaviour {

    private float waitTime;
    private Manager_Animals animalsManager;

	// Use this for initialization
	void Start () {
        animalsManager = FindObjectOfType<Manager_Animals>();
        waitTime = animalsManager.repeatRate * 3;

	}
	
	// Update is called once per frame
	void Update () {

        if(Mathf.Approximately(animalsManager.repetitions,0.0f))
        {
            StartCoroutine(returnToMenu());

        }
		
	}

    IEnumerator returnToMenu(){

        if (PlayerPrefs.HasKey(KeysForAnalytic.FirstDayOfplay) == false)
        {
            PlayerPrefs.SetString(KeysForAnalytic.FirstDayOfplay, DateTime.Now.ToString());
        }

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene("DashBoardScene");

    }
}
