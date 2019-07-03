using UnityEngine;

public class StarCorrectRotation : MonoBehaviour {

    private InstructionsScript GetInstructions;

	void Start () {

        GetInstructions = FindObjectOfType<InstructionsScript>();
        transform.Rotate(-90, 90, 0);
		
        if(GetInstructions.Level == 5){

            transform.Rotate(0,0,90);
        }

        if (GetInstructions.Level == 5 && GetComponent<KeepSize>() == null){

            transform.Rotate(0, 0, 90);

        }
	}
}
