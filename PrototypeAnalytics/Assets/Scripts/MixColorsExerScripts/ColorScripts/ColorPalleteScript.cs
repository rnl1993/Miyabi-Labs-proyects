using UnityEngine;

public class ColorPalleteScript : MonoBehaviour {

    // public variables

    public Color color;
    public string ColorName;
    public ColorPallete_SO colorPallete;

    // private variables

    private Renderer rend;
    private Vector3 OriginalSize;

    // Script references
    private InstructionsScript GetInstructionsScript;

	// Functions

	void Start () {

        GetInstructionsScript = FindObjectOfType<InstructionsScript>();
        rend = GetComponent<Renderer>();
        color = colorPallete.color;
        ColorName = GetInstructionsScript.EnglishM == false ? colorPallete.Name : colorPallete.EnglishName;
        rend.material.color = colorPallete.color;

	}


}
