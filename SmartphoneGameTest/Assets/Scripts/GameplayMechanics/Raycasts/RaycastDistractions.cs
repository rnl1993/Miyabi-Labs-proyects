using UnityEngine;

public class RaycastDistractions : MonoBehaviour {

    // Public Variables

    public LayerMask layerMask;
    // Variables publicas para restar puntos si el niño apunta a la distracción.
    public int negativePoints;
    public float penaltyTime;

    // private Variables

    private float maxDistance = 500;
    private RaycastHit hit;
    private float timer;

    // Script References

    DistractionsScript distractions;

	// Funtions

	void Start () {

        distractions = FindObjectOfType<DistractionsScript>();

	}

	void Update () {
        
        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask)){

            if(hit.collider.gameObject.GetComponent<DistractionsScript>() != null){

                var container = hit.collider.gameObject.GetComponent<DistractionsScript>();
                container.Change = true;
                container.ContactColor();
               
                timer += Time.deltaTime;

                if(timer >= penaltyTime){

                    negativePoints++;
                    timer = 0;

                }
            }
        }

        else{

            if(FindObjectOfType<DistractionsScript>() != null){
                FindObjectOfType<DistractionsScript>().Change = false;
                FindObjectOfType<DistractionsScript>().ChangeColor();
            }
        }
	}
}
