using UnityEngine;
using UnityEngine.UI;

public class SaveIDMenuScript : MonoBehaviour {

    public GameObject CanvasID;

    private InputField inputFieldID;
    private string IDInt;

    void Start(){

        inputFieldID = CanvasID.GetComponentInChildren<InputField>();
    }

    public void SaveID(){

        IDInt = inputFieldID.text;
        PlayerPrefs.SetString(IDScript.SaveID, IDInt);
        CanvasID.GetComponentInChildren<InputField>().text = "";
        CanvasID.gameObject.SetActive(false);
    }

    void OnDisable(){

        RaycastDashBoard.LevelsBoxCollidersOff = false;
    }
}
