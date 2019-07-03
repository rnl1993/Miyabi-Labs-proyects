using UnityEngine;

public class RepositionBillboardsScript : MonoBehaviour {

    // public variables.
	public GameObject BB;

    // Script References.
    InstructionsScript GetInstructions;

	void Start () {

        GetInstructions = FindObjectOfType<InstructionsScript>();
       
        if(GetInstructions.Level == 5){
            BB.transform.position = this.gameObject.transform.position;
            BB.transform.rotation = this.gameObject.transform.rotation;
        }
	}
	
	void Update () {


    }
}
