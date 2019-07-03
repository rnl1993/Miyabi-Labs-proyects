using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExerciseScenesManager : MonoBehaviour {

    // public variables
   
    //public bool hasChangeExercises;
   
    // private variables
    //private int SceneLoaded;

    //private static bool[] scenes = {false,false};

    // variables de dias

    //private TimeSpan EndedPeriod;
    //private DateTime FirstDayOfPlay;
    //private static ExerciseScenesManager instance;

    // Script References

    private LoadingScreenControl loadingScreen;
    //private FindExerciseImage findExerciseImage;

    // Functions

    /*
    void Awake(){
        
        KeysGames.BscenesBoolValue = PlayerPrefs.GetInt(KeysGames.CheckBscenesBool, 0);
        loadingScreen = FindObjectOfType<LoadingScreenControl>();

        // creo un singleton de este objecto para que no se resetie la información cada vez que vayas a un ejercicio y regreses.

        if(instance != null){

            Destroy(gameObject);
        }
        else{
            
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SceneLoaded = 0;
    }
    */

    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función tiene toda la logica para cambiar ejercicios y asegurarse de que antes de poder repetir un nivel, primero tienes que carga cada uno de ellos por lo menos una vez
    public void ChangeSceneExercise(){

        SceneManager.LoadScene("ColorObjectsExercises");

        /*
        // este primer if checa si el numero al azar que se creo concuerda con uno de los espacios "libres" de mi arreglo de bools

        if(scenes[SceneLoaded] == false){

            // aqui cambio ese espacio selecionado a "true", que es como si estuviera ocupado, Lo unico malo de este metodo es que cuando se haga el cambio de escenas de las "A" a las "B", es que los espacios no se resetean, sino solo hasta que se hayan usado todos.
            // Nota: tal vez deba de cambiar ese detalle de vaciar los espacios del arreglo de bools cuando se cambie a que escenas puedes accesar.

            scenes[SceneLoaded] = true;

            if (KeysGames.BscenesBoolValue == 1)
            {
                loadingScreen.LoadScreenExample(SceneManager.GetActiveScene().buildIndex + 2);
            }

            if (KeysGames.BscenesBoolValue == 0)
            {
                loadingScreen.LoadScreenExample(SceneManager.GetActiveScene().buildIndex + 1);
            }

            // Si ya entremos 1 vez a cada escena, regresamos el valor del arreglo de bools a false para que puedas volver a entrar a casa escena.

            if(scenes[0] == true && scenes[1] == true) {

                for (int i = 0; i < scenes.Length; i++){

                    scenes[i] = false;
                }
            }
        }

        else {

            // hago una recursión de esta función cuando el numero que se creó al azar para llamar una escena de ejercicio se repitió.
            ChangeSceneExercise();
        }

        */
    }

}