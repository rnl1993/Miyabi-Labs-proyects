using UnityEngine;
using UnityEngine.UI;


public class RaycastFoodContainers : MonoBehaviour
{

    //Variables

    public LayerMask layerMask;
    public Color HoverColor1 = new Color(0.5f, 0.5f, 0.5f, 1);

    private float maxDistance = 500;
    private RaycastHit hit;
    private FoodContainerScript [] foodContainer;

    // functions

    private void Start()
    {

        foodContainer = FindObjectsOfType<FoodContainerScript>();

    }

    void Update()
    {

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask)){
            
            if(hit.collider.GetComponent<FoodContainerScript>() != null){

                var container = hit.collider.gameObject.GetComponent<FoodContainerScript>();

                hit.collider.GetComponent<FoodContainerScript>().ChangeColor();

                if(GvrControllerInput.ClickButtonDown){

                    container.InstantiateFood();

                }

            }

        }

        else{

            for (int i = 0; i < foodContainer.Length; i++)
            {

                foodContainer[i].ChangeColorBack();

            }
        }

    }

}