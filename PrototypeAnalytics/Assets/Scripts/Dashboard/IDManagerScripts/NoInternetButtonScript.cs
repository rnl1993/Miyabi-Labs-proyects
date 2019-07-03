using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoInternetButtonScript : MonoBehaviour {

    public GameObject NoInternetCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseCanvas(){

        NoInternetCanvas.gameObject.SetActive(false);
    }
}
