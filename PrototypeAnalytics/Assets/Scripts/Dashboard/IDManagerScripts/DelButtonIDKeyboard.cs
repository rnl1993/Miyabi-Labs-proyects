using UnityEngine;
using UnityEngine.UI;

public class DelButtonIDKeyboard : MonoBehaviour {

    public InputField InputFieldID;

    private string deleteString;

    // Use this for initialization
    void Start () {
	

	}

   public void DelNumber(){

        if(InputFieldID.text.Length > 0){

            deleteString = InputFieldID.text.Remove(InputFieldID.text.Length - 1);              InputFieldID.text = deleteString; 
        }
    }
}
