using UnityEngine;

public class RaycastDistractionsScript : MonoBehaviour {

    // Public Variables

    public LayerMask layerMask;
    public int negativePoints;
    public float penaltyTime;
    public bool Contanct;

    // private Variables

    private float maxDistance = 500;
    private RaycastHit hit;
    private float timer;

    // script References

    DistractionScript  distractions;

    // Use this for initialization
    void Start () {
    
        distractions = FindObjectOfType<DistractionScript>();

        Contanct = false;
    }
    
    // Update is called once per frame
    void Update () {

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask))
        {
            
            if (hit.collider.gameObject.GetComponent<DistractionScript>() != null)
            {
                Contanct = true;
                var container = hit.collider.gameObject.GetComponent<DistractionScript>();

                container.ContactColor();

                timer += Time.deltaTime;

                if (timer >= penaltyTime)
                {

                    negativePoints++;
                    timer = 0;

                }

            }

        }

        else{

            Contanct = false;

        }
    }
}
