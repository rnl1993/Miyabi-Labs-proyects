using UnityEngine;

public class RaycastFoodContainers : MonoBehaviour {

	public LayerMask layerMask;
    public Color HoverColor1 = new Color(0.5f, 0.5f, 0.5f, 1);
    public bool touching;

    private float maxDistance = 500;
    private RaycastHit hit;
    private FoodContainerScript[] foodContainer;

	// Use this for initialization
	void Start () {

		foodContainer = FindObjectsOfType<FoodContainerScript>();
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask))
        {

            if (hit.collider.GetComponent<FoodContainerScript>() != null)
            {
                touching = true;
                var container = hit.collider.gameObject.GetComponent<FoodContainerScript>();

                hit.collider.GetComponent<FoodContainerScript>().ChangeColor();

                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {

                    container.InstantiateFood();

                }

            }

        }

        else{

            touching = false;

        }
		
	}
}
