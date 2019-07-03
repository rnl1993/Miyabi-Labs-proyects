using System.Collections;
using UnityEngine;

public class TutorialRayScript : MonoBehaviour {
    
	private LineRenderer lr;
    private float distance;
    private float counter;

    // Use this for initialization
    private void OnEnable()
    {
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, new Vector3(0, 0, 0));  
    } 

    void Update()
    {

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {

            // hace lo que dice su nombre, crea una linea de mesh para que funcione como un laser

            if (hit.collider)
            {
                lr.SetPosition(1, new Vector3(0, 0, hit.distance));
            }

            else
            {
                lr.SetPosition(1, new Vector3(0, 0, 0.5f));
            }
        }
    }
}
