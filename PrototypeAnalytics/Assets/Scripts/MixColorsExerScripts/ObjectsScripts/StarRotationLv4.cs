using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotationLv4 : MonoBehaviour {

    private InstructionsScript GetInstructions;

	// Use this for initialization
	void Start () {
	
        GetInstructions = FindObjectOfType<InstructionsScript>();
        StartCoroutine(lateStart());

	}

    IEnumerator lateStart(){

        yield return new WaitForSeconds(0.2f);

        if ((GetInstructions.Level == 4) && GetComponent<KeepSize>() != null){
            
            this.transform.Rotate(90, -90, 0);
           
        }
    }

}
