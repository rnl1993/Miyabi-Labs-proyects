using UnityEngine;
using System.Collections;

public class RaycastPalleteScript : MonoBehaviour {

    // public variables

    public LayerMask layerMask;
    public Color Raycolor;
    // Este Color solo se utilizara para el ejercicio de combinar colores;
    public Color Raycolor2;
    // En esta ocación si es necesario que sea "public" el RaycastHit porque lo utiliza "InstructionsScript"
    public RaycastHit hit;

    // Estos strings general la oración que se va a comparar con la oración creada por las instrucciones _______________________________________________________________________________________

    // Este es un int que utilizo para llevar control de los colores que haz seleccionado cuando tienes que combinar colores (no hace nada en los niveles donde no combinas colores).
    public int ColorsPicked; 
    // Este string va a salvar el valor del tamaño de la figura que escojiste para comparar.
    public string ChoseSize;
    // Este string va a salvar el valor del color con la que vas a pintar la figura para comparar.
    public string choseColor;
    // Solo se va a utilizar este color cuando se juegue en nivel 4 o mayor.
    public string choseColor2;
    // El nombre de la figura que escojiste pintar.
    public string ChoseFigure;
    // EL lado de la figura que escogiste.
    public string ChoseSide;

    // private variables
    private float RayDelay;
    private float maxDistance = 1;

    // script references
    private FlusherScript GetFlusherScript;
    private InstructionsScript instructions;
    private PickedColorFinder [] GetInstantiatePointer;
    private BillboardMapsManagerScript GetBillboardMapsManagerScript;

	// Functions.

	void Start () {

        // Empiezas sin colores seleccionados.
        ColorsPicked = 0;
        // Por default el segundo color no tiene valor, cuando estas en el nivel >=4, a la hora de seleccionar tu segundo color este valor va a cambiar.
        choseColor2 = "";
        // Al inicio de cualquier juego, el valor default de tu color es negro.
        choseColor = "Negro";
        // Un tiempo de delay del raycast por cuestiones tecnicas.
        RayDelay = 0.5f;
        // Busco las referencias de los scripts.
        GetFlusherScript = FindObjectOfType<FlusherScript>();
        instructions = FindObjectOfType<InstructionsScript>();
        GetBillboardMapsManagerScript = FindObjectOfType<BillboardMapsManagerScript>();

    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    void FixedUpdate () {

        RayDelay -= Time.deltaTime;
        if (RayDelay <= 0){
            maxDistance = 500;
            RayDelay = 0;
        }
        // vector para el raycast.
        Vector3 direction = transform.TransformDirection(Vector3.forward);
       
        // Aquí inicia el raycast.
        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask)){

            if(hit.collider != null){

                // Este if es para interactuar con el color de la paleta de colores.
                if (hit.collider.GetComponent<ColorPalleteScript>() != null && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && instructions.ColorAllowed == true || hit.collider.GetComponent<ColorPalleteScript>() != null && Input.GetKeyDown(KeyCode.A)){
                    
                    // Este if es para seleccionar un color cuando todavia no combinamos colores.
                    if (instructions.Level < 4){

                        var Palletecolor = hit.collider.GetComponent<ColorPalleteScript>();
                        choseColor = Palletecolor.ColorName;
                        Raycolor = Palletecolor.color;

                    }

                    // Este if es para poder combinar colores cuando el nivel en el que estas jugando es 4 o mayor.
                    if (ColorsPicked < 1 && instructions.Level >= 4){
                        
                        StartCoroutine(InstantiatePointers());
                    }
                }

                // Este if solo se utiliza cuando estamos jugando en el nivel 4 o mayor. 
                if (hit.collider.GetComponent<ColorPalleteScript>() != null &&  ColorsPicked >= 1 && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && instructions.ColorAllowed == true|| hit.collider.GetComponent<ColorPalleteScript>() != null && Input.GetKeyDown(KeyCode.A) && ColorsPicked >= 1){
                    StartCoroutine(InstantiateSecondPointers());

                }

                // Este if es para escojer el objecto que vas a pintar.
                if(hit.collider.GetComponent<ObjToPaint>() != null && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || hit.collider.GetComponent<ObjToPaint>() != null && Input.GetKeyDown(KeyCode.A)){

                    var toPaint = hit.collider.GetComponent<ObjToPaint>();

                    if(instructions.Level == 1 || instructions.Level == 2 || instructions.Level == 3){

                        ChoseFigure = toPaint.ObjectName;
                        ChoseSize = toPaint.ObjectSize;
                        GetBillboardMapsManagerScript.SubstractObjFromMap(ChoseFigure, ChoseSize, ChoseSide);
                        toPaint.Paint();
                        GetFlusherScript.CheckObjectMetrics();
                    }

                    if(instructions.Level== 4 || instructions.Level == 5){

                        // Aqui igualo este string con el string del objeto que se acaba de pintar, para poder accesar a el más fácil.
                        ChoseFigure = toPaint.ObjectName; // El objeto a pintar.
                        // Aqui igualo este string con el string del objeto que se acaba de pintar, para poder accesar a el más fácil.
                        ChoseSize = toPaint.ObjectSize; // El tamaño del objeto a pintar.
                        // Como solo en el nivel 5 se checan los lados, va adentro de este if.
                        if (instructions.Level == 5){
                            ChoseSide = toPaint.ObjectPositions;
                        }
                        GetBillboardMapsManagerScript.SubstractObjFromMap(ChoseFigure, ChoseSize, ChoseSide);
                        toPaint.Paint();
                        GetFlusherScript.CheckObjectMetrics();
                    }
                }
            }
        }
	}

    // _________________________________________________________________________________________________________________________________________________________________________________________

    IEnumerator InstantiatePointers(){
        
        var Palletecolor = hit.collider.GetComponent<ColorPalleteScript>();

        Raycolor = Palletecolor.color;
        choseColor = Palletecolor.ColorName;
        // Instancio el cubo que marca que color haz elegido, en los niveles donde tienes que escoger mas de 1.
        Palletecolor.transform.GetComponentInChildren<InstantiatePointerScript>().Pointer();

        yield return new WaitForSeconds(0.1f);

        // Este contador me ayuda a saber si ya selecciono el primer color. 
        ColorsPicked++;
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    IEnumerator InstantiateSecondPointers(){

        if(ColorsPicked >= 2){

            ColorsPicked = 0;

            var Palletecolor = hit.collider.GetComponent<ColorPalleteScript>();

            GetInstantiatePointer = FindObjectsOfType<PickedColorFinder>();

            for (int i = 0; i < GetInstantiatePointer.Length; i++){

                Destroy(GetInstantiatePointer[i].gameObject);
            }

            Raycolor = Palletecolor.color;
            Raycolor2 = Color.black;
            choseColor = Palletecolor.ColorName;
            choseColor2 = "Negro";
            Palletecolor.transform.GetComponentInChildren<InstantiatePointerScript>().Pointer();
        }
        
        if(ColorsPicked == 1){

            var Palletecolor = hit.collider.GetComponent<ColorPalleteScript>();
            //
            choseColor2 = Palletecolor.ColorName;
            //
            Raycolor2 = Palletecolor.color;
            Palletecolor.transform.GetComponentInChildren<InstantiatePointerScript>().Pointer();
            // La combinación de los 2 colores seleccionados.
            Raycolor = Raycolor + Raycolor2;

            if(Raycolor.r > 1 || Raycolor.g > 1 || Raycolor.b > 1){

                Raycolor = Raycolor / 2.5f;
            }

            if(Raycolor.r == 1 && Raycolor.g == 1 && Raycolor.b == 1){

                Raycolor = Raycolor * 0.5f;
            }
        }

        yield return new WaitForSeconds(0.1f);

        // Aqui el valor de “ColorsPicked” es igual a 2; cuando el niño vuelva a escojer un color, los colores se resetean y el color 1 se vuelve esa proxima selección de color.
        ColorsPicked++;
    }
}