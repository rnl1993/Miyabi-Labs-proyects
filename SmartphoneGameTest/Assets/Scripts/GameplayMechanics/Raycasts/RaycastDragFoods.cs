using UnityEngine;

public class RaycastDragFoods : MonoBehaviour {

    //Variables

    public Color HoverColor1 = new Color(0.5f, 0.5f, 0.5f, 1);
    public GameObject GrabbedObject;
    public GameObject ObjectGrabber;
    public LayerMask layerMask;
    public int speed;

    private float maxDistance = 500;
    private RaycastHit hit;

	// Funtions
	
	void Update () {

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask)){

            if(hit.collider.GetComponent<Food>() != null){

                GrabbedObject = hit.collider.gameObject;

                if(GvrControllerInput.ClickButton){
                    
                    float step = Time.deltaTime * speed;

                    GrabbedObject.transform.position = Vector3.Lerp(GrabbedObject.transform.position, ObjectGrabber.transform.position, step);
                }
            }
        }
	}
}
