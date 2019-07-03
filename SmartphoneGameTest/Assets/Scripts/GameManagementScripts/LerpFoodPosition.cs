using UnityEngine;

public class LerpFoodPosition : MonoBehaviour {

    //Variables

    public GameObject FoodContainers;
    public GameObject starManager;
    public GameObject orientation;
    public int speed;

	// Functions
	
	void Update () {

        float step = Time.deltaTime * speed;

         FoodContainers.transform.position = Vector3.Lerp(FoodContainers.transform.position, this.transform.position, step);
        FoodContainers.transform.LookAt(orientation.transform);

        starManager.transform.position = Vector3.Lerp(starManager.transform.position, this.transform.position, step);
        starManager.transform.LookAt(orientation.transform);
       
	}
}
