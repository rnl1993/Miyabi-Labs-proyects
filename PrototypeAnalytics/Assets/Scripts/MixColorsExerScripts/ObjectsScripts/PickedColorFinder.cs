using UnityEngine;

public class PickedColorFinder : MonoBehaviour {

    private InstructionsScript GetInstructions;

	private void OnEnable()
	{
        GetInstructions = FindObjectOfType<InstructionsScript>();
	}

	private void Update()
	{
	
        if(GetInstructions.ColorAllowed == false){

            Destroy(this.gameObject);
        }
	}

}
