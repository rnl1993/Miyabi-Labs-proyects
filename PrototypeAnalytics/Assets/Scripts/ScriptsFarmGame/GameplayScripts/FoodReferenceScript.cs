using UnityEngine;

public class FoodReferenceScript : MonoBehaviour {

    // Variables

    public int foodType;
    public ScriptableObject_Food_Script Food_Script;

    private Texture Materialtexture;
    private Renderer rend;

    private RaycastDragFoodScript raycastDragFood;

	// Function

	void Start () {

        foodType = Food_Script.foodType;
        Materialtexture = this.gameObject.GetComponent<Renderer>().material.mainTexture = Food_Script.FoodTexture;
        rend = GetComponent<Renderer>();
        raycastDragFood = FindObjectOfType<RaycastDragFoodScript>();
	}

    private void Update()
    {

        if(raycastDragFood.touching == false){

            ChangeColorBack();

        }

    }

    public void ChangeColor()
    {

        rend.material.GetColor("_Color");
        rend.material.SetColor("_Color", Color.gray);

    }


    public void ChangeColorBack()
    {

        rend.material.GetColor("_Color");
        rend.material.SetColor("_Color", Color.white);

    }
}
