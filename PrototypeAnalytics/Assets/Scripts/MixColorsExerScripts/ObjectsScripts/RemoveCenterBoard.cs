using UnityEngine;

public class RemoveCenterBoard : MonoBehaviour {

    private InstructionsScript GetInstructions;

    private void Start(){

        GetInstructions = FindObjectOfType<InstructionsScript>();

        if (GetInstructions.Level != 1){
            foreach (Transform child in transform){
                Destroy(child.gameObject);
            }
        }
    }
}
