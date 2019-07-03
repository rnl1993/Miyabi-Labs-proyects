using UnityEngine;

public class KeepSize : MonoBehaviour {

    private InstructionsScript GetInstructions;

    private void Start()
    {
        GetInstructions = FindObjectOfType<InstructionsScript>();
    }

    private void Update()
    {
        if (GetInstructions.Level < 4 ){

            Destroy(this.gameObject);
        }
    }
}
