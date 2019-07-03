using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusController : MonoBehaviour {

	public GameObject rController;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		rController.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
		rController.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

		//Debug.Log(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger));



}

}
