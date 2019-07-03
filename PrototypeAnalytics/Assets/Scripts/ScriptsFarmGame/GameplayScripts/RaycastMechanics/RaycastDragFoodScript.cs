using UnityEngine;

public class RaycastDragFoodScript : MonoBehaviour {

    public Color HoverColor1 = new Color(0.5f, 0.5f, 0.5f, 1);
    public GameObject GrabbedObject;
    public GameObject ObjectGrabber;
    public LayerMask layerMask;
    public bool touching;
    public int speed;

    // private variables

    private float maxDistance = 500;
    private RaycastHit hit;

	// Use this for initialization
	void Start () {

        touching = false;

	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask))
        {

            if (hit.collider.GetComponent<FoodReferenceScript>() != null)
            {

                touching = true;
                GrabbedObject = hit.collider.gameObject;

                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {

                    float step = Time.deltaTime * speed;

                    GrabbedObject.transform.position = Vector3.Lerp(GrabbedObject.transform.position, ObjectGrabber.transform.position, step);
                    GrabbedObject.GetComponent<FoodReferenceScript>().ChangeColor();

                }

            }

        }

        else{

            touching = false;

        }
		
	}
}
