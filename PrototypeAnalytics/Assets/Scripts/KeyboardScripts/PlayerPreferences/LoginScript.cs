using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour {

    public InputField Mail;
    public InputField password;
    public Button EnterButton;

	// Use this for initialization
	void Start () {
        
        Mail.gameObject.SetActive(false);
        password.gameObject.SetActive(false);
        EnterButton.gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowFields(){

        Mail.gameObject.SetActive(true);
        password.gameObject.SetActive(true);
        EnterButton.gameObject.SetActive(true);
        this.gameObject.SetActive(false);

    }
}
