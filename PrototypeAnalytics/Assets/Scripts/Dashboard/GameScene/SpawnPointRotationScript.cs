using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointRotationScript : MonoBehaviour {

    public GameObject RotationPoint;
    public int Rotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(RotationPoint != null){

            this.gameObject.transform.RotateAround(RotationPoint.transform.position, Vector3.forward, Rotation * Time.deltaTime);

        }
		
	}
}
