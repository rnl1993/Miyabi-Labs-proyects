using UnityEngine;
using UnityEngine.UI;
                 
public class TestTexts : MonoBehaviour {

    public Text testText;

	// Functions
	void Start () {

        if (Application.platform == RuntimePlatform.Android){

            testText.text = "Estoy en un Android";
        }

        else if (Application.platform == RuntimePlatform.IPhonePlayer){

            testText.text = "Estoy en un IOS";
        }
		
	}
	
}
