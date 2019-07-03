using UnityEngine;
using UnityEngine.UI;

public class CloseIDMenuScript : MonoBehaviour {

    // variables.
    public GameObject CanvasID;

    // Functions.
    public void CloseIDMenu(){

        CanvasID.GetComponentInChildren<InputField>().text = "";
        // Esta funcion es para cerrar el menu de ids sin hacer cambios.
        CanvasID.gameObject.SetActive(false);
    }

    void OnDisable(){

        RaycastDashBoard.LevelsBoxCollidersOff = false;
    }
}
