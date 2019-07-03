using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class VRdevicesManagerScript : MonoBehaviour {
    
    // public variables.
    public Image Daydream;
    public Image Cardboard;
    public Button playButton;
    public Image CloseInstructionsButton;
    public Text Instruction;
    [HideInInspector] public string VrDevice;

    public static string VR = "VR";

    // script References

    private static VRdevicesManagerScript instance;

    // Funtions.

    void Start()
    {
        if (Cardboard != null && Daydream != null && CloseInstructionsButton != null) {
            
            Cardboard.gameObject.SetActive(false);
            Daydream.gameObject.SetActive(false);
            CloseInstructionsButton.gameObject.SetActive(false);
            Instruction.gameObject.SetActive(false);
        }
    }

    public void VrOptions()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer){

            VrDevice = "Cardboard";
            PlayerPrefs.SetString(VR, VrDevice);
            SceneManager.LoadScene("GameTestScene_1");
        }

        else if (Cardboard != null || Daydream != null) {

            Cardboard.gameObject.SetActive(true);
            Daydream.gameObject.SetActive(true);
            CloseInstructionsButton.gameObject.SetActive(true);
            Instruction.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);

            Instruction.text = "Escoje el sistema que vas a utilizar. ";
        }
    }

    public void ReturnPlaybutton(){

        playButton.gameObject.SetActive(true);
        Cardboard.gameObject.SetActive(false);
        Daydream.gameObject.SetActive(false);
        CloseInstructionsButton.gameObject.SetActive(false);
        Instruction.gameObject.SetActive(false);

    }

    // Esta funcion inicia la corrutina del daydream.

    public void GameSceneButtonDaydream()
    {
        StartCoroutine(SceneButtonDaydreamCoroutine());     } 
    //  Esta funcion inicia la corrutina del Cardboard.
     public void GameSceneButtonCardboard()
    {         StartCoroutine(GameSceneButtonCardboardCoroutine());     }

    //
    IEnumerator SceneButtonDaydreamCoroutine(){

        VrDevice = "Daydream";
        PlayerPrefs.SetString(VR, VrDevice);

        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("GameTestScene_1");

    }

    //
    IEnumerator GameSceneButtonCardboardCoroutine(){

        VrDevice = "Cardboard";
        PlayerPrefs.SetString(VR, VrDevice);

        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("GameTestScene_1");

    }
 }
