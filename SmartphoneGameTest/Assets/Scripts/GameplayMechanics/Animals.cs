using UnityEngine;

public class Animals : MonoBehaviour {

    //Variables

    public Animals_SO_Script animals_SO;
    public int animalType;

    private Texture Materialtexture;

	//Funtions

	void Start () {

        animalType = animals_SO.animalType;
        Materialtexture = this.gameObject.GetComponent<Renderer>().material.mainTexture = animals_SO.AnimalTexture;
		
	}
	
	
	void Update () {
		
	}
}
