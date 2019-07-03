using System.Collections;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TutorialScript : MonoBehaviour {

    // pulic variables.
    public Sprite[] GetImages; // Este arreglo contiene las imagenes del tutorial.
    public AudioClip[] AudioInstructions;

    public GameObject button; // El botón que va a repetir el tutorial.
    public GameObject Player;
    public GameObject Dashboard; // El dashboard que se va a prender y apagar.

    // private variables.
    private int ImagePosition; // La posición actual del arreglo de imagenes del tutorial.
    private int InstruccionPosition;
    private static bool stopTutorialAnimation;

    private Image CurrentImage; // La imagen que se va a ver del tutorial.
    private AudioSource GetAudio;

    // Funtions
    void Start()
    {
        CurrentImage = GetComponent<Image>();
        GetAudio = GetComponent<AudioSource>();
        CurrentImage.sprite = GetImages[ImagePosition];
        Player.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        Dashboard.gameObject.SetActive(false);

        Invoke("CallTutorial", 1);
    }

    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKeyDown(KeyCode.A)){

            SkipTutorial();
        }
    }

    public void CallTutorial()
    {

        StartCoroutine("Tutorial");
    }

    IEnumerator Tutorial()
    {
        if (ImagePosition < GetImages.Length && stopTutorialAnimation == false)
        {
            CurrentImage.sprite = GetImages[ImagePosition];
            ImagePosition++;

            GetAudio.clip = AudioInstructions[InstruccionPosition];
            GetAudio.Play();
        }

        if (ImagePosition != GetImages.Length && stopTutorialAnimation == false)
        {
            Invoke("CallTutorial", AudioInstructions[InstruccionPosition].length);
            InstruccionPosition++;
        }

        if (ImagePosition == GetImages.Length)
        {
            yield return new WaitForSeconds(2);

            Dashboard.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
            Player.gameObject.SetActive(true);
            ImagePosition = 0;
            InstruccionPosition = 0;
            yield return new WaitForSeconds(0.1f);
            this.gameObject.SetActive(false);
        }
    }

    public void TutorialButton(){

        Player.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        Dashboard.gameObject.SetActive(false);
        Invoke("CallTutorial", 0.1f);
    }

    void SkipTutorial(){

        stopTutorialAnimation = true;
        StopCoroutine("Tutorial");
        CancelInvoke("CallTutorial");
        ImagePosition = 0;
        InstruccionPosition = 0;
        Player.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        Dashboard.gameObject.SetActive(true);
        print("Estoy presionando el boton para saltarme el tutorial"); 
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        stopTutorialAnimation = false;
    }
}