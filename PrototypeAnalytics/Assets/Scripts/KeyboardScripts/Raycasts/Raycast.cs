using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour {

    // public variables

    public LayerMask layerMask;
    public InputField MailField;
    public InputField PasswordField;
    public InputField activeField;

    // private variables.
    private int maxDistance = 1000;
    private RaycastHit hit;


	// Funtions
	void Start () {
        if (activeField != null){

            activeField = MailField;
        }
    }

	void Update () {

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        // un raycast normal

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask)){

            // aqui escribo las letras cuando las apuntas y presionas el gatillo de oculus al mismo tiempo:

            if(hit.collider.GetComponent<KeySample>() != null && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)|| (hit.collider.GetComponent<KeySample>() != null && Input.GetKeyDown(KeyCode.A))){

                var KeyName = hit.collider.GetComponent<KeySample>();

                    activeField.text += KeyName.NameText.text;
            }

            // lo mismo que arriba, pero para llamar las funciones especiales de las teclas que no son letras o simbolos.

            if (hit.collider.GetComponent<SpecialKeysSample>() != null && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || (hit.collider.GetComponent<SpecialKeysSample>() != null && Input.GetKeyDown(KeyCode.A))){

                var SkeyValue = hit.collider.GetComponent<SpecialKeysSample>();

                SkeyValue.ALlFunctions(SkeyValue.FunctionValue);
            }
        }
	}
}