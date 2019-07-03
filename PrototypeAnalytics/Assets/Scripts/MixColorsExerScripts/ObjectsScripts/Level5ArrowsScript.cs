using UnityEngine;

public class Level5ArrowsScript : MonoBehaviour {

    private InstructionsScript GetInstructions;

	// Use this for initialization
	void Start () {
        
        GetInstructions = FindObjectOfType<InstructionsScript>();

	}
	
	// Update is called once per frame
	void Update () {
		
        if (GetInstructions.Level == 5){

            this.gameObject.SetActive(true);
        }

        if (GetInstructions.Level != 5){

            this.gameObject.SetActive(false);
        }
	}
}
