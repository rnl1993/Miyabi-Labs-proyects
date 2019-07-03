using UnityEngine;

public class FoodContainerScript : MonoBehaviour {

    // public variables

    public GameObject foodToInstantiate;
    public Food_SO_Script foodContainer;
    [HideInInspector] public Renderer rend;
    public bool change;

    // private variables
    private Texture Materialtexture;

	// Functions

	void Start () {
        
        rend = GetComponent<Renderer>();
        Materialtexture = this.gameObject.GetComponent<Renderer>().material.mainTexture = foodContainer.FoodTexture;
	}
	
    public void ChangeColor(){

        if (change == true)
        {
            rend.material.color = Color.grey;
        }
    }


    public void ChangeColorBack()
    {
        if (change == false)
        {
            rend.material.color = Color.white;
        }
    }

    public void InstantiateFood(){

        Instantiate(foodToInstantiate, this.transform.position, Quaternion.identity);
    }

}
