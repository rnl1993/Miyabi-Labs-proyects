using UnityEngine;

public class ExerciseSceneScript : MonoBehaviour {

    // Public variables.
    [HideInInspector]public Renderer rend;
    public int DificultyLevel; 

    // Private variables
    private Color OgColor;

    // Script references
    private LoadingScreenControl GetLoadingScreen;

	// Functions
	void Start (){

        // El renderer del objeto, para poder cambiarlo de color
        rend = GetComponent<Renderer>();

        // Aqui guardo el color original del botón, antes de que sea tocado por el raycast.
        OgColor = rend.material.color;

		// Las referencias de los otros scripts que voy a usar.
        GetLoadingScreen = FindObjectOfType<LoadingScreenControl>();
	}

    void Update(){

        // aqui constantemente mando a resetear el color del botón, para cuando el raycast ya no lo este tocando.
        ChangeColor();
    }

    void ChangeColor(){

        // esta función lo unico que hace es hacer que el color del objeto regrese a ser el color que guartde en start. 
        rend.material.GetColor("_Color");
        rend.material.SetColor("_Color", OgColor);
    }

	public void ChangeScene(){

        PlayerPrefs.SetInt("ColorObjectsExercisesDificultyLevel", DificultyLevel);
        GetLoadingScreen.LoadScreenExample(2 - 1);
	}
}