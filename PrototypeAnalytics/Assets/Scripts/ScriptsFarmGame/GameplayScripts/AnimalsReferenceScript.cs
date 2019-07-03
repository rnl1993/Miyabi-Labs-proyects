using UnityEngine;

public class AnimalsReferenceScript : MonoBehaviour {

    // Variables
         public int animalType;
    public ScriptableObject_Animal_Script Animal_Script;

	// Functions

	void Start () {
        
        animalType = Animal_Script.animalType;
        this.gameObject.GetComponent<Renderer>().material.mainTexture = Animal_Script.AnimalTexture;

	}
	
}
