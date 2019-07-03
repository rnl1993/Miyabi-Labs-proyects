using UnityEngine;

public class InputChangeScript : MonoBehaviour {

    public LayerMask layerMask;


    private int maxDistance = 1000;
    private RaycastHit hit;
    private Raycast Keyboardraycast;
    private PasswordScript password;
    private MailScript mail;

	// Use this for initialization
	void Awake () {

        Keyboardraycast = FindObjectOfType<Raycast>();


	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask)){

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.A)){

                if(hit.collider.GetComponent<PasswordScript>() != null){
                    password = FindObjectOfType<PasswordScript>();
                    password.click = true;
                    Keyboardraycast.activeField = Keyboardraycast.PasswordField;

                }

                if(hit.collider.GetComponent<MailScript>() != null){
                    mail = FindObjectOfType<MailScript>();
                    mail.click = true;
                    Keyboardraycast.activeField = Keyboardraycast.MailField;

                }

            }

        }

	}
}
