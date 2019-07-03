using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamesScenesManager : MonoBehaviour {

    // public variables

    [HideInInspector] public float timer;
    [HideInInspector] public int minTime;
    [HideInInspector] public Renderer rend;
    [HideInInspector] public GameObject button;
    [HideInInspector] public GameObject NoGame;
    public Text RemainingTimeText;

    //public GameTextures_SO GetGameTextures;
    //public bool game;
    //[HideInInspector] public bool switchGame;
   
    // private variables

    /*
    private int value;
    private int GameIsOn;
    public static int remainingTime;
    private Double roundedTime;
    private Texture[] GameTextures;
    private static GamesScenesManager instance;
    private static float changeGameTime;
    */

    // dateTime variables

    //private DateTime FirstDayOfPlay;
    //private TimeSpan EndedPeriod;

    // script References

    private LoadingScreenControl loadingScreen;
    /*
    private GamesScenesManager gamesScenesManager;

    private GameSceneScript gameSceneScript;
    private findText FindText;
    */

    //*

    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // esta es una simple función de singleton.  

    /*
    void Awake(){

        KeysGames.GamesBoolValue = PlayerPrefs.GetInt(KeysGames.CheckGamesBool, 0);

        // Este "if" es un singleton para este GameObject. 
        if(instance != null){

            Destroy(gameObject);
        }

        else{

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
*/

    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    private void Start(){
        
        //changeGameTime = 1000000;
        loadingScreen = FindObjectOfType<LoadingScreenControl>();

    }

    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    private void FixedUpdate(){

        /*
        if(SceneManager.GetActiveScene().name == "DashBoard"){
        
            //ButtonManager();
            //ChangeExperience();
        }
        */
        /*
        if(game == true && SceneManager.GetActiveScene().name == "DashBoard"){

            changeGameTime -= Time.deltaTime;

            if(changeGameTime <= 0){
                
                switchGame = !switchGame;
                changeGameTime = 10;
            }
        }
        */
    }

    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    /*
    void ButtonManager(){

        // Este “if” se asegura que cuando regremos al “dashboard” despues de jugar un juego o hacer un ejercicio, que el script si encuentre button que este scipt necesita y manipula

        if (FindObjectOfType<GameSceneScript>() != null){
            
            if (button == null && SceneManager.GetActiveScene().name == "DashBoard"){
                button = FindObjectOfType<GameSceneScript>().gameObject;
            }
        }

        // Lo mismo que el “if” de arriba, solo que este busca el letrero de “No game”, para prenderlo o apagarlo, como sea necesario

        if (FindObjectOfType<NoGameScript>() != null){

            if (NoGame == null && SceneManager.GetActiveScene().name == "DashBoard"){
                NoGame = FindObjectOfType<NoGameScript>().gameObject;
            }
        }

        // Este “if” busca el renderer del button para poder cambiarle la textura desde este script

        if (rend == null && SceneManager.GetActiveScene().name == "DashBoard"){
            
            if (button != null){
                rend = button.GetComponent<Renderer>();
            }
            else {

                button = null;
                rend = null;
            }
        }

        // Este “if” busca el texto que te dice cuanto tiempo te queda de juego. 

        if (RemainingTimeText == null && SceneManager.GetActiveScene().name == "DashBoard" && FindObjectOfType<findText>() !=null){
            
            FindText = FindObjectOfType<findText>();
            RemainingTimeText = FindText.GetComponentInChildren<Text>();
            RemainingTimeText.text = "Tiempo restante para jugar: " + remainingTime;
        }

        // Aqui solo imprimo de manera constante el tiempo que te queda de juego en el texto que recupero el “if” de arriba, No puedo poner esto en el Start o Awake, porque por alguna razón, al parecer al codigo no le da tiempo de buscar todos estos “if” en el start.

        if (RemainingTimeText != null){
            RemainingTimeText.text = "Tiempo restante para jugar: " + remainingTime;
        }
        //*

        // Este “if” se asegura que por si alguna razón, tu tiempo de juego fuera menor a 0, no se imprima como numeros negativos, sino que se iguale a 0
        // En teoria este “if” no es necesario, gracias al “if” que sigue en el codigo, pero este es solo una redundacia que quiero tener por seguridad.

        if (remainingTime <= 0){
            
            remainingTime = 0;
            PlayerPrefs.SetInt(KeysGames.KeyGameTimeRemaining, remainingTime);
            remainingTime = PlayerPrefs.GetInt(KeysGames.KeyGameTimeRemaining);
        }

        // Este “if” evita que entres a algún juego que tenga un “costo” de tiempo mayor al tiempo de juego que tienes actualmente.

        if (remainingTime <= minTime || remainingTime <= 0){
            
            PlayerPrefs.SetInt(KeysGames.KeyGamesEnable, 0);

            if (button != null){
                button.gameObject.SetActive(false);
            }
        }

        // Este “if” checa el valor ”PlayerPrefs.GetInt(KeysGames.KeyGamesEnable)“, un playerpref que activa o desactiva el “button” que te permite accesar a los juegos. 
        // Este if en especifico lo apaga porque checa si ”PlayerPrefs.GetInt(KeysGames.KeyGamesEnable)“ es igual a 0.

        if (game == false){
            
            if(button != null){

            button.gameObject.SetActive(false);

            }

            if (NoGame != null){
            NoGame.gameObject.SetActive(true);
            }
        }

        // La contraparte del “if” de arriba, aqui ”PlayerPrefs.GetInt(KeysGames.KeyGamesEnable)“ tiene que ser igual a 1,
        // por lo que si entras, el “button” de juegos esta prendido, tambien apaga el quad que tiene le mensaje de “No game”.
        if (game == true){
            
            if (button != null){
                button.gameObject.SetActive(true);
            }

            if (NoGame != null){
                NoGame.gameObject.SetActive(false);
            }
        }
    }
    */

    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función checa que textura tiene el “button” por el momento, y dependiendo de cual sea, es la escena de juego que va a cargar.


    public void ChangeScene(){
        
    /*
        if (rend.material.mainTexture == GetGameTextures.Game1){
            loadingScreen.LoadScreenExample(4);
        }

        else if (rend.material.mainTexture == GetGameTextures.Game2){
            loadingScreen.LoadScreenExample(5);
        }
    */
    }


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función checa que textura deberia de tener actualmente el “button” y la cambia a como debe de ser el caso. Tambien asigna el “costo” de tiempo que tiene la escena que abriria cada textura.
    // Nota: por el momento el valor del costo de las escenas que vamos a cargar lo tengo que asignar de manera manual en esta función, todavia no se como podria agarrar ese valor desde escenas que todavia no se han cargado.

/*
    void ChangeExperience(){

        if(switchGame == false){
            if (rend != null){
                
                rend.material.mainTexture = GetGameTextures.Game1;
                minTime = 1;
            }
        }

        if (switchGame == true){
            if (rend != null){
                
                rend.material.mainTexture = GetGameTextures.Game2;
                minTime = 1;
            }
        }
    }
*/
}