using UnityEngine;
using UnityEngine.UI;

public class NumPadScript : MonoBehaviour {

    [HideInInspector] public int buttonValue;

    [HideInInspector] public Text ButtonValueText;

	// Use this for initialization
	void OnEnable() {

        ButtonValueText = this.GetComponentInChildren<Text>();
        if(int.TryParse(ButtonValueText.text, out buttonValue)){

        }
	}
}
