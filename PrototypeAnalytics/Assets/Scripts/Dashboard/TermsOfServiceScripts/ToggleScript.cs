using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour {

    public GameObject GetButton;

    private Toggle GetToggle;

    // Use this for initialization
    void Start () {

        GetToggle = GetComponentInParent<Toggle>();
        GetButton.GetComponent<Collider>().enabled = false;
	}

    public void ToSAccepted(){

        GetToggle.isOn = !GetToggle.isOn;

        if(GetToggle.isOn == true){

            GetButton.GetComponent<Collider>().enabled = true;
        }

        else{
              
            GetButton.GetComponent<Collider>().enabled = false;
        }


    }
}
