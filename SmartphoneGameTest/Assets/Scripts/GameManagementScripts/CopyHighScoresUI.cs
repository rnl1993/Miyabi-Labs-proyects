using UnityEngine;
using UnityEngine.UI;

public class CopyHighScoresUI : MonoBehaviour {

    // Sliders

    public Text Copy_HS1_text;
    public Text Copy_HS2_text;
    public Text Copy_HS3_text;
    public Text Copy_HS4_text;
    public Text Copy_HS5_text;

    // Texts

    public Slider Copy_HS1_Slider;
    public Slider Copy_HS2_Slider;
    public Slider Copy_HS3_Slider;
    public Slider Copy_HS4_Slider;
    public Slider Copy_HS5_Slider;

    // Private Variables

    float maxValue;

    // Scripts References

    HighScoresScript highScores;

	// Funtions

	void Start () {

        maxValue = 101;

        Copy_HS1_Slider.gameObject.SetActive(false);
        Copy_HS2_Slider.gameObject.SetActive(false);
        Copy_HS3_Slider.gameObject.SetActive(false);
        Copy_HS4_Slider.gameObject.SetActive(false);
        Copy_HS5_Slider.gameObject.SetActive(false);

        Copy_HS1_text.gameObject.SetActive(false);
        Copy_HS2_text.gameObject.SetActive(false);
        Copy_HS3_text.gameObject.SetActive(false);
        Copy_HS4_text.gameObject.SetActive(false);
        Copy_HS5_text.gameObject.SetActive(false);

        highScores = FindObjectOfType<HighScoresScript>();
		
	}

    public void CopyCanvasValues(){

        if(highScores.HS1_Slider.gameObject.activeInHierarchy == true){

            Copy_HS1_Slider.gameObject.SetActive(true);
            Copy_HS1_Slider.maxValue = maxValue;
            Copy_HS1_Slider.value = highScores.HS1_Slider.value;
            Copy_HS1_text.gameObject.SetActive(true);
            Copy_HS1_text.text = highScores.HS1_text.text;

        }

        if (highScores.HS2_Slider.gameObject.activeInHierarchy == true){

            Copy_HS2_Slider.gameObject.SetActive(true);
            Copy_HS2_Slider.maxValue = maxValue;
            Copy_HS2_Slider.value = highScores.HS2_Slider.value;
            Copy_HS2_text.gameObject.SetActive(true);
            Copy_HS2_text.text = highScores.HS2_text.text;

        }

        if (highScores.HS3_Slider.gameObject.activeInHierarchy == true)
        {

            Copy_HS3_Slider.gameObject.SetActive(true);
            Copy_HS3_Slider.maxValue = maxValue;
            Copy_HS3_Slider.value = highScores.HS3_Slider.value;
            Copy_HS3_text.gameObject.SetActive(true);
            Copy_HS3_text.text = highScores.HS3_text.text;
        }

        if (highScores.HS4_Slider.gameObject.activeInHierarchy == true)
        {

            Copy_HS4_Slider.gameObject.SetActive(true);
            Copy_HS4_Slider.maxValue = maxValue;
            Copy_HS4_Slider.value = highScores.HS4_Slider.value;
            Copy_HS4_text.gameObject.SetActive(true);
            Copy_HS4_text.text = highScores.HS4_text.text;

        }

        if (highScores.HS5_Slider.gameObject.activeInHierarchy == true)
        {

            Copy_HS5_Slider.gameObject.SetActive(true);
            Copy_HS5_Slider.maxValue = maxValue;
            Copy_HS5_Slider.value = highScores.HS5_Slider.value;
            Copy_HS5_text.gameObject.SetActive(true);
            Copy_HS5_text.text = highScores.HS5_text.text;

        }

    }
	
}
