using UnityEngine;

public class ObjRotationScript : MonoBehaviour {

    // Public variables
    public int Speed;

    // Script reference
    private InstructionsScript GetInstructions;

	// Functions
	void Start () {

        GetInstructions = FindObjectOfType<InstructionsScript>();
        Speed = 30;
	}
	
	void Update () {

        if (GetInstructions != null)
        {
            if (GetInstructions.Level >= 3)
            {

                this.gameObject.transform.Rotate(Vector3.up * Speed * Time.deltaTime);
            }
        }
	}
}
