using UnityEngine;

public class TutorialColorPalette : MonoBehaviour {

    public GameObject ColorPallete;

	// Use this for initialization
	void Start () {

        if (ColorPallete != null)
        {
            ColorPallete.gameObject.SetActive(false);
        }
	}

    public void TurnOnPallete(){

        ColorPallete.gameObject.SetActive(true);

    }
}
