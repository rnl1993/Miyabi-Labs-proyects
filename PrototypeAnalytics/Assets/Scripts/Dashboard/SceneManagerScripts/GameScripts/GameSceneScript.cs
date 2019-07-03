using UnityEngine;

public class GameSceneScript : MonoBehaviour {

    // public variables
    [HideInInspector] public Renderer rend;

    // private variables
    private Color OgColor;

    // Script Reference.
    private LoadingScreenControl GetLoadingScreen;

	// Functions
	void Start () {

        // Aqui guardo el color original del botón, antes de que sea tocado por el raycast.
		rend = GetComponent<Renderer>();

        // Aqui guardo el color original del botón, antes de que sea tocado por el raycast.
        OgColor = rend.material.color;

        // Las referencias de los otros scripts que voy a usar.
        GetLoadingScreen = FindObjectOfType<LoadingScreenControl>();

	}

    private void Update(){
        
        ChangeColor();
    }

     void ChangeColor(){

        rend.material.GetColor("_Color");
        rend.material.SetColor("_Color", OgColor);
	}

	public void ChangeScene(){

        GetLoadingScreen.LoadScreenExample(3 - 1);
	}
}