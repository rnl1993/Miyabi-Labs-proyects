using UnityEngine;
using UnityEngine.UI;

public class RaycastDashBoard : MonoBehaviour {

    // public variables.

    // El canvas de cuando si hay internet.
    public GameObject IDCanvas;
    // El canvas de cuando no hay internet.
    public GameObject NoInternetCanvas;
    // el input field del menu de ID's de aqui copio el valor para los mails de los niños.
    public InputField inputFieldID;
    // Este es el texto que aparece cuando no tienes internet y quieres abrir el menu de los ids
    public Text NoInternettext;

    public LayerMask layerMask;
    //
    [HideInInspector] public static bool LevelsBoxCollidersOff;

    // private variables.
    private RaycastHit hit;
    private readonly float maxDistance = 500;
    private float IDMenuTimer;

	// Use this for initialization
	void Start () {
        IDCanvas.gameObject.SetActive(false);
        NoInternetCanvas.gameObject.SetActive(false);
    }

    //______________________________________________________________________________________________________________________

	void Update () {

        // Este if es el que va a activar el menu del ID para marcar si quieres guardar el registro del niño.
        if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey(KeyCode.Space)){

            IDMenuTimer += Time.deltaTime;

            // despues de 5 segundos se va a activar el menu de ID.
            if(IDMenuTimer > 5.0f){

                if (Application.internetReachability == NetworkReachability.NotReachable){

                    // Aqui borro el ID de los niños, porque no puedes mandar mails, entonces para evitar problemas
                    PlayerPrefs.DeleteKey(IDScript.SaveID);
                    // apago los colliders de los niveles para que no haya problema a la hora de cerrar el menu de que no hay internet.
                    LevelsBoxCollidersOff = true;
                    // activo el canvas que te indica que no hay internet.
                    NoInternetCanvas.gameObject.SetActive(true);
                    NoInternettext.text = "No estas conectado a internet, no vas a poder guardar ni recibir el registro de esta sesión.";

                }
                else{

                    LevelsBoxCollidersOff = true;
                    IDCanvas.gameObject.SetActive(true);
                }
            }
        } //__________________________________________________________________________
        // Cuando levantas el botón, el contador que activa el menu de IDs se resetea a 0.
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyUp(KeyCode.Space)){

            IDMenuTimer = 0;
        } //__________________________________________________________________________

        Vector3 direction = transform.TransformDirection(Vector3.forward);

		if (Physics.Raycast (transform.position, direction, out hit, maxDistance, layerMask)){

			if (hit.collider != null){

                // Este “if” es el que activa la flechita de que aceptas los terminos y condiciones.
                if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && hit.collider.GetComponent<ToggleScript>() != null || hit.collider.GetComponent<ToggleScript>() != null && Input.GetKeyDown(KeyCode.A)){

                    hit.collider.GetComponent<ToggleScript>().ToSAccepted();
                }

                // Este “if” es el que apaga el canvas de los ToS.
                if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && hit.collider.GetComponent<AcceptButtonScript>() != null || Input.GetKeyDown(KeyCode.A) && hit.collider.GetComponent<AcceptButtonScript>() != null) {

                    hit.collider.GetComponent<AcceptButtonScript>().MailFinishedSending();
                }

                // Este “if” es el que escribe los numeros de los IDs en el menu de IDs escondido.
                if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && hit.collider.GetComponent<NumPadScript>() != null || Input.GetKeyDown(KeyCode.A) && hit.collider.GetComponent<NumPadScript>() != null){

                    inputFieldID.text += hit.collider.GetComponent<NumPadScript>().ButtonValueText.text;
                }

                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && hit.collider.GetComponent<DelButtonIDKeyboard>() != null || Input.GetKeyDown(KeyCode.A) && hit.collider.GetComponent<DelButtonIDKeyboard>() != null) {
                    hit.collider.GetComponent<DelButtonIDKeyboard>().DelNumber();
                }

                // este “if” Guarda y cierra el menu de IDs
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && hit.collider.GetComponent<SaveIDMenuScript>() != null || Input.GetKeyDown(KeyCode.A) && hit.collider.GetComponent<SaveIDMenuScript>() != null){

                    hit.collider.GetComponent<SaveIDMenuScript>().SaveID();
                }

                // Este “if” borra el ID que esta guardado, pero no cierra el menu, despues decido con Joseb si asi se queda esto o si los 3 botones cierran el menu.
                if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && hit.collider.GetComponent<DeleteIDScript>() != null || Input.GetKeyDown(KeyCode.A) && hit.collider.GetComponent<DeleteIDScript>() != null){

                    hit.collider.GetComponent<DeleteIDScript>().DeleteID();
                }

                // Este “if” Cierra el menu de IDs, pero sin borrar o cambiar la informacion que ya esta guardada.
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && hit.collider.GetComponent<CloseIDMenuScript>() != null || Input.GetKeyDown(KeyCode.A) && hit.collider.GetComponent<CloseIDMenuScript>() != null){

                    hit.collider.GetComponent<CloseIDMenuScript>().CloseIDMenu();
                }

                // Aqui cierro el Canva del mensaje de cuando no tienes internet.
                if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && hit.collider.GetComponent<CloseNoInternetMenu>() != null || Input.GetKeyDown(KeyCode.A) && hit.collider.GetComponent<CloseNoInternetMenu>() != null){

                    hit.collider.GetComponent<CloseNoInternetMenu>().CloseNoInternetMenuButton();
                }

                if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && hit.collider.GetComponent<AdsScript>() != null || Input.GetKeyDown(KeyCode.A) && hit.collider.GetComponent<AdsScript>() != null){

                    hit.collider.GetComponent<AdsScript>().Ads();
                }

                // Este “if” hace que los botones cambien de color cuando los apuntas. 
                if (hit.collider.GetComponent<GameSceneScript> () != null || hit.collider.GetComponent<ExerciseSceneScript>() != null || hit.collider.GetComponent<Returnkeyboard>() != null) {

                    hit.collider.gameObject.GetComponent<Renderer>().material.color = hit.collider.gameObject.GetComponent<Renderer>().material.color/2;

                    // Este “if” Cambia la escena al juego.
                    if (OVRInput.GetDown (OVRInput.Button.PrimaryIndexTrigger) && hit.collider.GetComponent<GameSceneScript>() != null || Input.GetKeyDown(KeyCode.A) && hit.collider.GetComponent<GameSceneScript>() != null) {

                        hit.collider.GetComponent<GameSceneScript>().ChangeScene();
					}

                    // Este “if” Cambia la escena al ejercicio. 
                    if (OVRInput.GetDown (OVRInput.Button.PrimaryIndexTrigger) && hit.collider.GetComponent<ExerciseSceneScript>() != null || Input.GetKeyDown(KeyCode.A) && hit.collider.GetComponent<ExerciseSceneScript>() != null) {

                        hit.collider.GetComponent<ExerciseSceneScript>().ChangeScene();
                    }
                }
            }
		}
	}
}