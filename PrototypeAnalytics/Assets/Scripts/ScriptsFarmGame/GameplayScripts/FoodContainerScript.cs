using UnityEngine;

public class FoodContainerScript : MonoBehaviour {

    // public variables

    public Renderer rend;
    public GameObject foodToInstantiate;
    public ScriptableObject_Food_Script scriptableObject_Food;

    // script references

    private RaycastFoodContainers raycastFoodContainers;

	// Functions
	void Start () {
		
        rend = GetComponent<Renderer>();
        raycastFoodContainers = FindObjectOfType<RaycastFoodContainers>();
        this.gameObject.GetComponent<Renderer>().material.mainTexture = scriptableObject_Food.FoodTexture;
	}

    private void Update()
    {

        if(raycastFoodContainers.touching == false){

            ChangeColorBack();

        }

    }

    public void ChangeColor(){          rend.material.GetColor("_Color");         rend.material.SetColor("_Color", Color.gray);     }      public void ChangeColorBack()     {          rend.material.GetColor("_Color");         rend.material.SetColor("_Color", Color.white);     }      public void InstantiateFood(){          Instantiate(foodToInstantiate, this.transform.position, Quaternion.identity);     }
}
