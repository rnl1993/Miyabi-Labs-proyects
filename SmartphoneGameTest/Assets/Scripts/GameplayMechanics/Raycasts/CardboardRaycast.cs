using UnityEngine;

public class CardboardRaycast : MonoBehaviour {

    // public variables. 

    // El objeto que va a marcar la posición de la comida que va a agarrar el niño.
    public GameObject gameObjectGrabber;
    // Variables publicas para restar puntos si el niño apunta a la distracción.
    public int negativePoints;
    public float penaltyTime;

    // private variables.
    private GameObject Grabbed;
    // El contador de tiempo de la distracción.
    private float timer;

    // Script references.
    private FoodContainerScript[] foodContainer;
    DistractionsScript distractions;

	// Funtions

	void Start () {
        foodContainer = FindObjectsOfType<FoodContainerScript>();
        distractions = FindObjectOfType<DistractionsScript>();

        if(PlayerPrefs.GetString(VRdevicesManagerScript.VR) == "Daydream"){

            this.gameObject.SetActive(false);
        }
	}

    void Update()
    {
        if (GvrPointerInputModule.CurrentRaycastResult.gameObject != null){

            if (GvrPointerInputModule.CurrentRaycastResult.gameObject.GetComponent<FoodContainerScript>() != null ){

                var container = GvrPointerInputModule.CurrentRaycastResult.gameObject.GetComponent<FoodContainerScript>();
                container.change = true;
                container.ChangeColor();

                if(Input.GetMouseButton(0)){

                    container.InstantiateFood();
                }
            }
            else
            {
                for (int i = 0; i < foodContainer.Length; i++)
                {
                    foodContainer[i].change = false;
                    foodContainer[i].ChangeColorBack();
                }
            }

            if(GvrPointerInputModule.CurrentRaycastResult.gameObject.GetComponent<Food>() != null && Grabbed == null){

                Grabbed = GvrPointerInputModule.CurrentRaycastResult.gameObject;

                if (Input.GetMouseButton(0)){

                    Grabbed.transform.position = gameObjectGrabber.transform.position;
                }
            }
            else { Grabbed = null; }

            if (GvrPointerInputModule.CurrentRaycastResult.gameObject.GetComponent<DistractionsScript>() != null){

                var container = GvrPointerInputModule.CurrentRaycastResult.gameObject.GetComponent<DistractionsScript>();
                container.Change = true;
                container.ContactColor();

                timer += Time.deltaTime;

                if (timer >= penaltyTime)
                {
                    // los puntos que le vamos a rentar al niño por apuntar a la distracción.
                    negativePoints++;
                    timer = 0;
                }
            }

            else
            {
                if (FindObjectOfType<DistractionsScript>() != null)
                {
                    FindObjectOfType<DistractionsScript>().Change = false;
                    FindObjectOfType<DistractionsScript>().ChangeColor();
                }
            }

            // Esta función es solo para la escena de prueba para probar el "Agarrar" un objeto.
            /* 
            if (GvrPointerInputModule.CurrentRaycastResult.gameObject != null)
            {

                if (GvrPointerInputModule.CurrentRaycastResult.gameObject.GetComponent<TestCubeScript>() != null && Input.GetMouseButton(0) && Grabbed == null)
                {
                    Grabbed = GvrPointerInputModule.CurrentRaycastResult.gameObject;
                    Grabbed.transform.position = gameObjectGrabber.transform.position;
                }

                else { Grabbed = null; }
            }*/

        }
    }
}
