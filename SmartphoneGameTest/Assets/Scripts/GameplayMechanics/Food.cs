using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    //Variables

    public Food_SO_Script food_OS;
    public int foodType;


    private Texture Materialtexture;

    //Functions

	void Start () {


        foodType = food_OS.foodType;
        Materialtexture = this.gameObject.GetComponent<Renderer>().material.mainTexture = food_OS.FoodTexture;


	}
	
	
 
}
