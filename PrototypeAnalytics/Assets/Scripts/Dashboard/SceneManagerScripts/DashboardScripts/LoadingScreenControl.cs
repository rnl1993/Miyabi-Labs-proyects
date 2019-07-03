using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreenControl : MonoBehaviour{

    // public variables.
    public Slider slider;
    public GameObject loadingScreenObj;
    public GameObject Dashboard;

    // private variables.
    private AsyncOperation async;


    // _________________________________________________________________________________________________

    void Start(){
        
        // Aqui apago el canvas que tiene el la barra de carga del loading Screen.
        loadingScreenObj.SetActive(false);
    }

    // _________________________________________________________________________________________________

    // Esta función es la que manda a cargar las demás escenas, y funciona de la siguiente manera;
    // EL numero que utilices para llamar esta función, es el numero de escena que va a cargar.

    public void LoadScreenExample(int LV){

        // Aqui prendo el Canvas de la barra de progreso.
        loadingScreenObj.SetActive(true);

        // Aqui apago los demás objetos de la escena para que solo se vea la barra de progreso con el texto.
        Dashboard.SetActive(false);

        // Aqui mando a llamar la corrutina que hace el proceso de cargar la escena que mandé a llamar.
        StartCoroutine(LoadingScreen(LV));
    }

    // _________________________________________________________________________________________________

    // Esta Corrutina es la que manda a cargar la escena que quieres abrir. 
    IEnumerator LoadingScreen(int lvl){
        
        async = SceneManager.LoadSceneAsync(lvl); // esta linea de codigo es la que carga la escena a abrir.
        async.allowSceneActivation = false; 

        while (async.isDone == false){
            // Aqui es donde visualmente se muestra el progreso de carga de la escena que estas abriendo.
            slider.value = async.progress;

            if (async.progress == 0.9f){

                slider.value = 1f;
                async.allowSceneActivation = true;

            }
            // Este yield return nulo es para que la barra de progreso se pueda ver llena.
            // Si se quisiera dejar ver este efecto más, nada mas se le agrega duración a este yield.
            yield return null;
        }
    }
}