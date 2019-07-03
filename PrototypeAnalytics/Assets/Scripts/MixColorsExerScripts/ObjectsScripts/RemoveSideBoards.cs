using UnityEngine;

public class RemoveSideBoards : MonoBehaviour {

    private InstructionsScript GetInstructions;

    private void Update()
    {
        GetInstructions = FindObjectOfType<InstructionsScript>();

        if (GetInstructions.Level == 1)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }           
        } 
    }
}
