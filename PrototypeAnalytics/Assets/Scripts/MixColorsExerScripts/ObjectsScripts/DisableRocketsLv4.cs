using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRocketsLv4 : MonoBehaviour {

	private InstructionsScript GetInstructions;

     void Start(){
        GetInstructions = FindObjectOfType<InstructionsScript>();
    }
	
	// Update is called once per frame
	void Update () {
		if(GetInstructions.Level == 4 ){
			Destroy(this.gameObject);
		}
	}
}
