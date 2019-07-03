using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseNoInternetMenu : MonoBehaviour {

    // variables.
    public GameObject NoInternetCanvas;

    // Functions.
    public void CloseNoInternetMenuButton(){

        // Esta funcion es para cerrar el menu de ids sin hacer cambios.
        NoInternetCanvas.gameObject.SetActive(false);
    }

    void OnDisable(){

        RaycastDashBoard.LevelsBoxCollidersOff = false;
    }
}
