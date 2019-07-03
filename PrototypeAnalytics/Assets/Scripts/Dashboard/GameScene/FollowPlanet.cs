using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlanet : MonoBehaviour {

    public GameObject Planet;
    public float Speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position = Vector3.Lerp(this.transform.position, Planet.transform.position, Speed);
        //transform.LookAt(Planet.transform.position);
		
	}
}
