using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypeWriteEffectScript : MonoBehaviour {

    public string FullText; // La primera parte de la explicación del tutorial.
    public string FullTextSecondPart; // la segunda parte de la explicación del tutorial.
    public string TutorialFullText; // Este es el texto introductorio.

    public int ExplanationTextDelay; // este es int para darle ritmo a como van a apareciendo los textos.
    public float WriteSpeed; // La velocidad a la que se escribe el texto en la pantalla.
    public float LerpTime;
    public float LerpTime2;

    public Text OpeningText;

    public GameObject Control; // Este es el modelo del control. 
    public GameObject FirstTextfinalposition; // Esta es la posición final del texto de bienvenida. 
    public Image BackgroundInstructionsPanel; // Estas son las instrucciones del tutorial.

    public AudioClip[] InstructionsClips; // Este es el arreglo donde guardo los audios de las instrucciones para irlos llamando cuando sean necesarios. 

    private string FirstCurrentText = "";         // Este es el texto que tiene el efecto de typewriter. Este es el texto introductorio.
    private string CurrentText = "";             // Este es el texto que se va a ir escribiendo.
    private bool TextAnimation;                 // Este bool activa la animación del primer texto.
    private bool FirstInstructionAudioStarted; // Este bool es para iniciar el conteo de la duración del audio de la instrucción.
    private bool SecondInstructionAudio;      // Este bool es para iniciar iniciar el audio de la segunda parte de la instrucciones.
    private bool thirdInstructionAudio;      // Este bool es para iniciar el 3er audio de las instrucciones.
    private bool FourthInstructionAudio;    // Este bool es para iniciar el 4to audio de las instrucciones.
    private float InstructionsTimer;       // este float es para contar la duración del audio de las instrucciones. 
    private int InstructionPosition;      // Este int es para ir recorriendo el arreglo de audios de las instrucciones.

    private AudioSource GetAudio; //
    private CanvasGroup GetCanvasGroup; //
    private TutorialCubeScript [] GetTutorialCube; // 

	// Use this for initialization
	void Start () {

        // 
        GetTutorialCube = FindObjectsOfType<TutorialCubeScript>();
        GetAudio = GetComponent<AudioSource>();
        // Aqui Apago los objetos que al principio no tienen que ser visibles.
        Control.gameObject.SetActive(false);

        GetCanvasGroup = BackgroundInstructionsPanel.GetComponent<CanvasGroup>();
        GetCanvasGroup.alpha = 0;
        FirstInstructionAudioStarted = false;
        // Este bool mantiene la animación del primer texto apagara hasta que sea necesaria.
        TextAnimation = false;

        StartCoroutine(FirstText());
	}

    //* _________________________________________________________________________________________________________________________________________________________________________________________________

    // _________________________________________________________________________________________________________________________________________________________________________________________________

    private void FixedUpdate()
    {
        // Despues de que el texto de presentación se termine de escribir, le aplico una pequeña animación para bajar este texto y crecerlo un poco.
        if(TextAnimation == true){

            // Aqui reposiciono el texto hacia abajo.
            OpeningText.transform.position = Vector3.LerpUnclamped(OpeningText.transform.position, FirstTextfinalposition.transform.position, LerpTime * Time.deltaTime);

            if(OpeningText.transform.localScale.x < 0.18f){

                // Aqui hago el texto un poco más grande.
                OpeningText.transform.localScale = Vector3.LerpUnclamped(OpeningText.transform.localScale, OpeningText.transform.localScale * 2, LerpTime2 * Time.deltaTime);
            }
        }

        // Este “if” cuenta la duración del primer audio, asi puedo saber cuando termino y puedo mandar a llamar la siguiente instrucción.
        if(FirstInstructionAudioStarted == true){ 

            InstructionsTimer += Time.deltaTime;
            if (InstructionsTimer >= InstructionsClips[InstructionPosition].length)
            {
                SecondInstructionAudio = true;
                FirstInstructionAudioStarted = false;
                InstructionsTimer = 0;
            }
        }

        // lo mismo que el “if” anterior.
        if(SecondInstructionAudio == true){

            InstructionsTimer += Time.deltaTime;
            if(InstructionsTimer >= InstructionsClips[InstructionPosition].length + 1){ // Este +1 es solo porque el audio de prueba que estoy usando esta mal editado y se escucha feo. Tambien en un futuro puede que sirva para mantener mejor tempo. 
                
                thirdInstructionAudio = true;
                SecondInstructionAudio = false;
                InstructionsTimer = 0;
            }
        }

        if(thirdInstructionAudio == true){

            InstructionsTimer += Time.deltaTime;
            if(InstructionsTimer >= InstructionsClips[InstructionPosition].length){

                thirdInstructionAudio = false;
                FourthInstructionAudio = true;
                InstructionsTimer = 0;
            }
        }
    }

    // Aqui escribo el texto de presentación.
    IEnumerator FirstText(){

        for (int i =0; i < TutorialFullText.Length; i++){
            
            FirstCurrentText = TutorialFullText.Substring(0, i + 1); // Aqui voy escribiendo el texto de introducción. 
            OpeningText.text = FirstCurrentText;
            yield return new WaitForSeconds(WriteSpeed); // Este “yield return new WaitForSeconds” ayuda a marcar el ritmo de escritura del texto.
        }

        yield return new WaitForSeconds(0.8f); // Aqui le doy un poco de tiempo al texto en el centro antes de animarlo. 

        TextAnimation = true; // Este bool activa la animación del texto de introducción para pocisionarlo abajo. 

        yield return new WaitForSeconds(ExplanationTextDelay); // Este “yield return new WaitForSeconds” evita que el texto que va a explicar las dinamicas, aparezca al mismo tiempo que corre la animación del texto anterior.   

        // Aqui empiezo la corrutina que va a actualizar el segundo texto donde le vamos a explicar el niño como funciona el juego.
        StartCoroutine(ShowText());
    }

    //* _________________________________________________________________________________________________________________________________________________________________________________________________

    // _________________________________________________________________________________________________________________________________________________________________________________________________

    IEnumerator ShowText(){

        // audios
        GetAudio.clip = InstructionsClips[InstructionPosition]; // Este es el audio que va a introducir el juego, todavia no se exactamente que vamos a decir, pero este es solo un test de la logica por el momento.
        GetAudio.Play();
        FirstInstructionAudioStarted = true; // Aqui prendo un bool para poder saber cuando se acabe el audio de la primera instrucción, para sabe cuando iniciar el 2do audio de la instrucción.  

        // Todo este “for” es para ir escribiendo el texto que explica como ir jugando el juego.
        // __________________________________________________
        for (int i = 0; i < FullText.Length; i++){
            
            CurrentText = FullText.Substring(0, i + 1);
            this.GetComponent<Text>().text = CurrentText;
            yield return new WaitForSeconds(WriteSpeed);
        }
        //* ______________________________________________________

        yield return new WaitUntil(() => SecondInstructionAudio == true); // Este "WaitUntil" es para esperar hasta que se acabe el audio de la primera explicacion sobre las instrucciones. 

        this.GetComponent<Text>().text = "";  // Aqui reseteo el texto de las explicaciones.
        InstructionPosition++;               // Aqui cambio el track de las instrucciones.
        yield return new WaitForSeconds(1); // Aqui le doy un poco de tiempo a las instrucciones para que no aparezcan de golpe.

        StartCoroutine(FadeInInstructions()); // Aqui Hago un FadeIn del texto de las instrucciones junto con su panel de fondo. 

        yield return new WaitForSeconds(0.2f);

        GetAudio.clip = InstructionsClips[InstructionPosition]; // Este es el audio que va a introducir el juego, todavia no se exactamente que vamos a decir, pero este es solo un test de la logica por el momento.
        GetAudio.Play();

        // Aqui escribo el segundo texto de instrucciones. El primer y segundo texto se va a ir intercalando ida y vuelta, los textos obviamente se va a ir actualizando conforme sea necesario.
        // __________________________________________________
        for (int i = 0; i < FullTextSecondPart.Length; i++)
        {
            CurrentText = FullTextSecondPart.Substring(0, i + 1);
            this.GetComponent<Text>().text = CurrentText;
            yield return new WaitForSeconds(WriteSpeed);
        }
        //* ______________________________________________________

        yield return new WaitUntil(() => thirdInstructionAudio == true);

        this.GetComponent<Text>().text = "";// Aqui reseteo el texto de las explicaciones.
        InstructionPosition++;

        GetAudio.clip = InstructionsClips[InstructionPosition]; // Este es el audio que va a introducir el juego, todavia no se exactamente que vamos a decir, pero este es solo un test de la logica por el momento.
        GetAudio.Play();

        FullTextSecondPart = "";
        FullText = "Deberás identificar cual es el color y el objeto que te pide la instrucción y seleccionarlos como se muestra en la siguiente animación.";

        ActivateColors(); // Aqui instancio la paleta de colores para la demostracion de como seleccionar colores.
        ActivateObject(); // Aqui muestro el objeto para ser pintado.

        for (int i = 0; i < FullText.Length; i++) { // Todo este “for” es para ir escribiendo el texto que explica como ir jugando el juego.

            CurrentText = FullText.Substring(0, i + 1);
            this.GetComponent<Text>().text = CurrentText;
            yield return new WaitForSeconds(WriteSpeed);
        }

        InstructionPosition++;   
        yield return new WaitUntil(() => FourthInstructionAudio == true);

        StartCoroutine(LastTextAnimation());

        Control.gameObject.SetActive(true); // Aqui activo el control para la demostración de animaciones.

        yield return new WaitForSeconds(3.5f);

        FindObjectOfType<ColorPickedTutorialScript>().gameObject.GetComponent<TutorialCubeScript>().RecolorCube();

        yield return new WaitForSeconds(8);

        ControlSecondAnimation(); // Aqui simulo la selección del objeto para pintarlo.  

        yield return new WaitForSeconds(1.5f);

        FindObjectOfType<DummyObjectTutorialScript>().gameObject.GetComponent<TutorialCubeScript>().RecolorCube(); // Aqui lo pinto.

    }

    //* _________________________________________________________________________________________________________________________________________________________________________________________________

    // _________________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función instancia la paleta de colores.
    void ActivateColors(){

        FindObjectOfType<TutorialColorPalette>().TurnOnPallete();
    }

    // Aqui prendo la figura que se va a “Pintar”.
    void ActivateObject(){

        StartCoroutine(FindObjectOfType<DummyObjectTutorialScript>().gameObject.GetComponent<TutorialCubeScript>().FadeIn());
        StartCoroutine(FindObjectOfType<ColorPickedTutorialScript>().gameObject.GetComponent<TutorialCubeScript>().FadeIn());
    }

    // Aqui hago aparecer el control para la explicación de como jugar.
    void ActivateControl(){
        // Al activar el control, la primera animación tambien se manda a llamar.
        Control.gameObject.SetActive(true);
    }

    // Aqui mando a llamar la segunda animación del control.
    void ControlSecondAnimation(){

        FindObjectOfType<TutorialControlAnimation>().SecondAnimation();
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________________

    IEnumerator FadeInInstructions(){

        while(GetCanvasGroup.alpha < 1){

            GetCanvasGroup.alpha += Time.deltaTime / 2;
            yield return null;
        }

        yield return null;
    }

    //* _________________________________________________________________________________________________________________________________________________________________________________________________

    IEnumerator LastTextAnimation(){

        this.GetComponent<Text>().text = "";
        GetAudio.clip = InstructionsClips[InstructionPosition]; // Este es el audio que va a introducir el juego, todavia no se exactamente que vamos a decir, pero este es solo un test de la logica por el momento.
        GetAudio.Play();
        FullTextSecondPart = "Apunta con tu laser al color que se te indica en la instrucción y seleccionalo utilizando el gatillo, despues de seleccionar el color, apunta al objeto que quieras pintar y seleccionalo de la misma manera.";
        for (int i = 0; i < FullTextSecondPart.Length; i++)
        {
            CurrentText = FullTextSecondPart.Substring(0, i + 1);
            this.GetComponent<Text>().text = CurrentText;
            yield return new WaitForSeconds(WriteSpeed);
        }

        yield return new WaitForSeconds(5);

        this.GetComponent<Text>().text = "";
        FullTextSecondPart = "Este es el final del tutorial. El juego comenzará a continuación.";
        for (int i = 0; i < FullTextSecondPart.Length; i++)
        {
            CurrentText = FullTextSecondPart.Substring(0, i + 1);
            this.GetComponent<Text>().text = CurrentText;
            yield return new WaitForSeconds(WriteSpeed);
        }

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("ColorObjectsExercises");

    }
}
