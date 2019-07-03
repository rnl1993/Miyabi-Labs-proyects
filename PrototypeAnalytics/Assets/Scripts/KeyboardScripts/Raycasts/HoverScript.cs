using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HoverScript : MonoBehaviour {

    // Variables

    public LayerMask layerMask;
    [HideInInspector] public GameObject tempOG;
    public float speed;
    [HideInInspector] public bool big;

    private int maxDistance = 1;
    private float RayDelay;
    private RaycastHit hit;
    private Vector3 tempScale;
    private Vector3 tempScale2;
    private bool Stop;
    private AudioSource As;

	// Functions

	void Start () {

        big = false;
        Stop = false;

        As = GetComponent<AudioSource>();
        RayDelay = 1;
	}
	
	void FixedUpdate () {

        RayDelay -= Time.deltaTime;
        if(RayDelay <=0){

            maxDistance = 500;
            RayDelay = 0;
        }

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        // un raycast normal

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask))
        {

            hit.collider.GetComponent<HoverTargetScript>();

            // En este if hago creecer la tecla que este apuntado hasta que alcance cierto limite

            if (Stop == false)
            {
                big = true;
                tempOG = hit.collider.gameObject;
                tempScale = tempOG.transform.localScale;
                tempScale.x += speed;
                tempScale.y += speed;
                tempScale.z += speed;

                tempScale2 = new Vector3(tempScale.x, tempScale.y, tempScale.z);

                tempOG.transform.localScale = tempScale2;

                hit.collider.transform.localScale = tempOG.transform.localScale;

                // una vez que llega a dicho limite deja de creecer y corre un sonido

                if (tempScale.x >  (tempScale.y - 0.01f))
                {
                    Stop = true;
                    As.Play();
                }
            }
        }
        else
        {
            // cuando dejas de apuntar a la tecla, regresa a su tamaño original 

            if (tempOG != null)
            {
                big = false;
                Stop = false;
            }
        }
	}
}
