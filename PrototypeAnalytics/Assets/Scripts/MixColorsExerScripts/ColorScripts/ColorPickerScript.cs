using UnityEngine;

public class ColorPickerScript : MonoBehaviour {

    private RaycastPalleteScript RaycastPallete;
    private Renderer rend;

	// Use this for initialization
	void Start () {
        
        rend = GetComponent<Renderer>();
        RaycastPallete = FindObjectOfType<RaycastPalleteScript>();

	}
	
	// Update is called once per frame
	void Update () {

        this.rend.material.color = RaycastPallete.Raycolor;

	}
}
