using UnityEngine;
using UnityEngine.UI;

public class ChangeColorScript : MonoBehaviour {
    
    public Color OtherColor = new Color(0.2953008f, 0.7291772f, 0.7924528f, 1);
    public Color OtherColor2 = new Color(0, 0.206121f, 0.6886792f, 1);
    public Image Artwork;
    public LayerMask layerMask;
    public bool colorswitch;

    private int maxDistance = 1000;
    private RaycastHit hit;

	// Functions
	void Start () {

        colorswitch = false;
	}
	
	void Update () {

        Vector3 direction = transform.TransformDirection(Vector3.forward);

        // un raycast normal

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask)){

           // aqui cambio de color a las teclas, busco a los 2 tipos, normales y especiales

            if (hit.collider.GetComponent<KeySample>() != null || hit.collider.GetComponent<SpecialKeysSample>() != null){

                colorswitch = true;
                hit.collider.gameObject.GetComponent<Image>().color = OtherColor;
                // aqui intento cambiar de nuevo de color a otro mas obscuro (rojo por el momento solo para que no haya ninguna duda que si cambio de color)

                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {
                    colorswitch = true;
                    hit.collider.gameObject.GetComponent<Image>().color = OtherColor2;
                }
            }
        }

        else
        {
            colorswitch = false;
        }
	}
}
