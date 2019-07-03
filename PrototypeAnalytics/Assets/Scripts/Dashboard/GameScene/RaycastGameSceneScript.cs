using UnityEngine;

public class RaycastGameSceneScript : MonoBehaviour
{

    // public variables

    public float speed;
    public bool touching;
    public RaycastHit hit;
    public LayerMask layerMask;
    public GameObject GrabbedObject;
    public GameObject GrabberPosition;

    // private variables

    private float maxDistance = 500;

    // Use this for initialization
    void Start()
    {
        
        touching = false;

    }

    void Update()
    {

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask))
        {
            
            if (hit.collider.GetComponent<Cube1Script>() != null || hit.collider.GetComponent<Cube2Script>() != null || hit.collider.GetComponent<Cube3Script>() != null || hit.collider.GetComponent<Cube4Script>() != null)
            {

                GrabbedObject = hit.collider.gameObject;

                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {
                    
                    float step = Time.deltaTime * speed;

                    GrabbedObject.transform.position = Vector3.Lerp(GrabbedObject.transform.position,GrabberPosition.transform.position, step);
                }

            }

        }

        else
        {
            touching = false;
        }
    }
}