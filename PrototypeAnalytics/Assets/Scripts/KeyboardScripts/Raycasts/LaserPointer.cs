using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserPointer : MonoBehaviour {

    // Variables

	private LineRenderer lr;

	// Functions
	void Start () {

		lr = GetComponent<LineRenderer> ();
        lr.SetPosition(1, new Vector3(0, 0, 50));
	}
	
	void Update () {

		RaycastHit hit;

		if(Physics.Raycast(transform.position, transform.forward, out hit)){

            // hace lo que dice su nombre, crea una linea de mesh para que funcione como un laser

			if (hit.collider) {

				lr.SetPosition (1, new Vector3 (0, 0, hit.distance));
			} 

			else {

				lr.SetPosition (1, new Vector3(0,0, 5000));
			}
		}
	}
}