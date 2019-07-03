using System;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;
using System.Collections;
using System.Net.Security;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

[RequireComponent(typeof(AudioSource))]
public class InstructionsScript : MonoBehaviour{

    // public variables

    // Este “DateTime” es para poder medir el tiempo de reacción del niño, El script que mida esta metrica usará esta información. 
    public static DateTime InstructionTime;
    // Esta variable se deberia de modificar con información dada por la app del celular de padre. Determina el nivel en el que se jugara el ejercicio.
    [HideInInspector] public int Level;
    // Este bool cambia el idioma del juego de pintar figuras.
    [HideInInspector] public bool EnglishM;

    // Esta variable es para prender y apagar el nombre de las figuras que pintan los niños de manera correcta. Es publica porque la usa el script de “ObjToPaint”.
    [HideInInspector] public bool resetObjName;

    // Variables del tiempo.
    public float timer; // la duración del juego
    public int firstRunDelay; // cuanto tiempo hay de espera para la primera instrucción.

    // Esta variable es para determinar cuanto tiempo dura cada instrucción. En un futuro puede que sea private porque se determinaria con info que mandaria la app del celular del padre.  
    public int timeBetweenInstructions;
    // El texto de las instrucciones.
    public Text InstructionsText;
    // A partir del nivel 4 hay 2 instrucciones al mismo tiempo, por lo que este texto solo estara activo a partir del nivel 4.
    public Text Instructions2Text;
    // Los paneles son solo fondos para los textos.
    public Image Panel1;
    public Image Panel2;

    // Los colores que se van agregando en el 4to y 5to nivel. Con esto los puedo prender y apagar cuando sea necesario.
    public GameObject FifthColor;
    public GameObject SixthColor;

    // Los fondos de los colores, para que haya consistencia visual.
    public GameObject FifthColorBackground;
    public GameObject SixthColorBackground;

    // Las posiciones donde van a estar las instrucciones cuando haya más de 1 y/o cuando haya más de un Billboard.
    public GameObject InstructionsPositions;
    public GameObject Instructions2Positions;
    public GameObject InstructionsPositionsLevel1;
    // La posición de la paleta de colores cuando solo hay un billboard.
    public GameObject CenterPalletePosition;
    // Las posiciones de las paletas de colores cuando haya mas de un billboard.
    public GameObject RightPalletePosition;
    // Como en el nivel 5 los billboards estan a los costados, necesito ponerlos en otros lugares. 
    public GameObject RightPalletePositionLevel5;
    // Las paletas de colores para poder posicionarlas en donde van.
    public GameObject RightPallete;
    // Los Billboards, para prenderlos y apagarlos conforme sea necesario.
    public GameObject BillboardCenter;
    public GameObject BillboardRight;
    public GameObject BillboardLeft;
    // Efectos de sonido.
    public AudioClip CambioIstrucciones;
    public AudioClip BillboardLleno;

    // Este bool lo voy a utilizar para controlar la seleccion de colores cuando se llega a la parte de combinar colores.
    [HideInInspector] public bool ColorAllowed;
    //Este bool Apaga la segunda instrucción del nivel 4 y 5 bajo ciertas condiciones. 
    [HideInInspector] public bool SecondInstructionOff;

    // Variables para checar que el niño escogio el color y la figura correctamente.

    [HideInInspector] public string ObjectValue;    // El nombre del objeto. 
    [HideInInspector] public string ColorValue;    // El primer color o que color normal si no estas jugando en el nivel 4 o mayor.
    [HideInInspector] public string ObjectSize;   // Si el objeto es grande o pequeño (solo funciona a partir del nivel 3).
    [HideInInspector] public string ColorValue2; // el color 2, para combinar (solo funciona a partir del nivel 4).
    [HideInInspector] public string ObjectSide; // la posición (Izquierda/Derecha) del objeto (solo funciona a partir del nivel 5).

    // Estas variables son para checar las respuestas de la segunda instrucción que empieza a aparecer a partir del nivel 4.

    [HideInInspector] public string ObjectValue2;      // El nombre del objeto. 
    [HideInInspector] public string ColorValueIn2;    // El primer color de la segunda instrucción.
    [HideInInspector] public string ObjectSize2;     // Si el objeto es grande o pequeño.
    [HideInInspector] public string ColorValue2In2; // el color 2, para combinar.
    [HideInInspector] public string ObjectSide2;   // la posición (Izquierda/Derecha) del objeto (solo funciona a partir del nivel 5).

    // Este int checa cuantas variables de colores existen, para que pueda llevar la cuenta de cuando ya use todos los colores.
    [HideInInspector] public int ColorsAvailable;
    // Este int checa cuantas variables de objetos existen, para que pueda llevar la cuenta de cuando ya use todas las figuras.
    [HideInInspector] public int ObjectsAvailable;

    //* _____________________________

    // private variables

    // Este diccionario guarda todas las instrucciones que puede dictar el juego.
    private Dictionary<int, string> Instructions = new Dictionary<int, string>();

    private readonly Dictionary<int, string> Conjunciones = new Dictionary<int, string>();

    // El hastable para traducir las instrucciones al ingles.
    private readonly Hashtable EnglishTranslation = new Hashtable();
    private readonly Hashtable EnglishTranslationCon = new Hashtable();

    // Nombres de colores para el diccionario de instrucciones.

    private readonly string Red = "Rojo";
    private readonly string Blue = "Azul";
    private readonly string Green = "Verde";
    private readonly string Yellow = "Amarillo";
    // Los colores que se desbloquean en el nivel 4 y 5.
    private readonly string Brown = "Cafe";
    private readonly string Pink = "Rosa";

    // Nombres de objetos para el diccionario de instrucciones.

    private readonly string Cube = "Pinguinos";
    private readonly string Sphere = "Pelotas";
    private readonly string Capsule = "Arboles";
    private readonly string Cylinder = "Peces";
    // Los objetos que se desbloquean en el nivel 4 y 5.
    private readonly string Object5 = "Estrellas";
    private readonly string Object6 = "Cohetes";

    // Tamaño de los objetos para el diccionario de instrucciones.

    private readonly string Normal = "Pequeños";
    private readonly string Big = "Grandes";

    // Lado donde se encuentran objetos para el diccionario de instrucciones.

    private readonly string Left = "Izquierdo";
    private readonly string Right = "Derecho";

    // las llaves para el diccionario de instrucciones.

    // Colores
    private readonly int RedKey = 0;
    private readonly int BlueKey = 1;
    private readonly int Greenkey = 2;
    private readonly int KeyYellow = 3;
    // Estos colores se van desbloqueando de acuerdo de que nivel se este trabajando.
    private readonly int BrownKey = 4;
    private readonly int PinkKey = 5;

    // Objetos.
    private readonly int CubeKey = 6;
    private readonly int SphereKey = 7;
    private readonly int CapsuleKey = 8;
    private readonly int CylinderKey = 9;
    // Estos objetos se van desbloqueando de acuerdo de que nivel se este trabajando.
    private readonly int Object5Key = 10;
    private readonly int Object6Key = 11;

    // Tamaños.
    private readonly int NormalKey = 12;
    private readonly int BigKey = 13;

    // Posiciones.
    private readonly int LeftKey = 14;
    private readonly int RightKey = 15;

    // conjunciones para las instrucciones.
    private readonly int Conjuction0Key = 0;
    private readonly int Conjuction1Key = 1;
    private readonly int Conjuction2Key = 2;
    private readonly int Conjuction3Key = 3;
    private readonly int Conjuction4Key = 4;

    /* Estos strings solo sirven para darlen má formato a las instrucciones del juego. 
       No cumplen ninguna funció mecanica y no tienen ningun valor fuera de su nivel(+). 
       para evitar espacios innesarios en los niveles donde no se utilizan. */
    private string SentenceStart;
    private string ConjuctionSize;
    private string ConjuctionColor;
    private string ConjuctionColor2;
    private string ConjuctionSide;

    // Variables para generar las instrucciones del juego.

    private int RandObj;     // El nombre del objeto.
    private int RandCol;    // El primer color.
    private int RandSize;  // El tamaño al azar del objeto (solo funciona a partir del nivel 3)
    private int RandCol2; // El segundo color al azar (solo funciona a partir del nivel 4).
    private int RandSide;// La posición al azar del objecto (solo funciona a partir del nivel 5)

    // valores para crear las segundas instrucciones.
    private int RandomObjectInstruction2;     // El nombre del objeto de la segunda instrucción.
    private int RandomColorIntruction2;      // El primer color de la segunda instrucción.
    private int RandomSizeInstruction2;     // El tamaño al azar del objeto (solo funciona a partir del nivel 3).
    private int RandomColor2Intruction2;   // El segundo color al azar (solo funciona a partir del nivel 4).
    private int RandomSideInstruction2;   // La posición al azar del objecto (solo funciona a partir del nivel 5).

    // Esta variable es para resetear el(los) color(es) del raycast entre cada instrucción.  
    private Color colorReset;

    // Variables para determinar si el movimiento esta activado.________________________________________________________

    // Variables del movimiento de los objetos.
    private bool MovementeIsOn;
    // Esta variable se va a tener que determinar dependiendo de en que nivel juegue el niño, 
    //por lo que por el momento la modificare manualmente para pruebas, pero despues decidiremos bien como la modificamos.
    private int TurnsToShift;
    // Esta variable es para escoger al azar cual Billboard va a rotar
    private int RandomMove;
    //
    private int InstructionRound;
    // Bool para solo mandar un mail.
    private bool SendedMail;

    // script references

    private Left[] leftBoard;
    private Right[] rightBoard;
    private AudioSource GetAudio;
    private FlusherScript GetFlusherScript;
    private RearrangeObjectScript[] MoveObjects;
    private RaycastPalleteScript RaycastPallete;
    private BillboardMapsManagerScript GetBillboardMapsManagerScript;
    private BillboardExerciseWriterScript GetBillboardExerciseWriterScript;

    // Function __________________________________________________________

    void Start(){

        // Aqui busco los componentes que necesito, como los scripts y el audioSource.

        GetAudio = GetComponent<AudioSource>();
        GetFlusherScript = FindObjectOfType<FlusherScript>();
        MoveObjects = FindObjectsOfType<RearrangeObjectScript>();
        RaycastPallete = FindObjectOfType<RaycastPalleteScript>();
        GetBillboardMapsManagerScript = FindObjectOfType<BillboardMapsManagerScript>();
        GetBillboardExerciseWriterScript = FindObjectOfType<BillboardExerciseWriterScript>();

        // AL cambiar este bool, cambias el idioma.
        EnglishM = false;
        // Aqui voy a modificar el valor de "level" manualmente para probar las funciones de "MoveObjectsScript" que rotan la posición de los objectos.
        Level = PlayerPrefs.GetInt("ColorObjectsExercisesDificultyLevel");

        // Similar a la variable de arriba, el verdadero valor de este “int” vendra directamente de la app del papá. 
        //Este int marca cuantas instrucciones tienen que pasar para que las figuras cambien de lugar.
        TurnsToShift = 2;
        // Aqui todavia no pintas nada, por eso el valor es falso.
        resetObjName = false;
        // Al principio del juego me aseguro que el primer color sea negro para el raycast.
        colorReset = Color.black;
        // Aqui llamo la corrutina de LateStart.
        StartCoroutine(LateStart());

        /* Aqui voy a regular el tiempo entre instrucciones en los niveles superiores (Los niveles 4 y 5 van a tener más tiempo porque hay 2 instrucciones en lugar de 1).
           El nivel 1 va a tener 4 segundos menos del total que decidamos sea el tiempo “Standard” de juego, ya que solo tiene un panel ese nivel. */
        if (Level == 1) { timeBetweenInstructions -= 4; }
        if (Level == 2 || Level == 3) { timeBetweenInstructions += 1; }
        // Aqui por decisión unanime, le aumentamos 4 segundos al tiempo total por grupo de instrucciones.
        if (Level == 4) { timeBetweenInstructions = (int)(timeBetweenInstructions * 1.5f) + 4; }

        if (Level == 5) { timeBetweenInstructions = (timeBetweenInstructions * 2); }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    void FixedUpdate(){

        // Este es el tiempo que dura el juego.
        timer -= Time.deltaTime;

        if (timer <= 0){

            timer = 0;

            if (EnglishM == false){

                InstructionsText.text = "Se acabo el tiempo";
            }

            if (EnglishM == true){

                InstructionsText.text = "Time's up";
            }
            // Aqui cambio el texto de las segunda instrucción.
            if (Level >= 4){

                // La version en ingles del final del juego.
                if (EnglishM == false){

                    Instructions2Text.text = "Se acabo el tiempo";
                }
                // La version en ingles del final del juego.
                if (EnglishM == true){

                    Instructions2Text.text = "Time's up";
                }

                CancelInvoke();
                StopCoroutine("InstructionsRoutine");
            }

            if (SendedMail == false){

                // Aqui mando el mail.
                StartCoroutine(SendMail());
            }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    IEnumerator LateStart(){

        // Este LateStart lo utilizo para darle tiempo al juego de que los objetos se instancien, para despues poder encontrar los script de los objetos que se acaban de instanciar.
        yield return new WaitForSeconds(0.01f);

        if (Level != 1){

            /* Si el niño no esta jugando en el nivel 1, aqui busco unos scripts que se agregan automaticamente a los objetos de los billboards, 
             * para que me ayuden a diferenciar cuando el niño acabo alguno de los 2 billboards. Los scripts se agregan dependiendo de la 
             * posicion de los objectos en el mundo, es asi como puedo dicernir que objetos estan a la izquierda y cuales estan a la derecha. */
            leftBoard = FindObjectsOfType<Left>();
            rightBoard = FindObjectsOfType<Right>();
        }
        else { leftBoard = null; rightBoard = null; }

        // Llamo varias funciones aqui.
        // Agarro todas las variables que necesitan los diccionarios y las agrego.
        FillDictionary();
        // Leno el Hastable que esta en Ingles.
        FillTranslation();
        // 
        ActivateTranslation();
        // Esta función modula los colores que aparecen en el nivel, a partir del nivel 4 empezamos a agregar más colores.
        ManageExtraColors();
        // Esta función acomoda todo el UI del juego, dependiendo del nivel que este jugando el niño.
        ArrangeUI();

        // Este "Invoke" llama la función con la corrutina de las instrucciones.
        Invoke("InstructionFunction", firstRunDelay);
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // esta función solo manda a llamar la corrutina que maneja las instrucciones. Tengo que llamarla desde una funcion para poder utilizar la funcion de Invoke.
    public void InstructionFunction(){

        StartCoroutine("InstructionsRoutine");
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    /* Esta es la Corrutina de las instrucciones, en ella hago varias cosas a la vez:
       -Llamo las funciones que generaran las instrucciones.
       -Escribo las instrucciones para el niño.
       -Creo un ciclo recursivo, ya que llamo la función que inicializa esta corrutina. */

    IEnumerator InstructionsRoutine(){

        // Al principio de cada intrucción le regreso a las figuras sus nombres.
        resetObjName = false;
        // Selecciono el objeto(s) para la instrucion(es).
        SelectObjects();
        // Selecciono el color(es) para la instrucion(es).
        RandomColor();
        // Aqui selecciono el color a pintar para la primera instrucción.
        SelectColors();
        // Cuando las instrucciones empiezan a pedir tamaños, esta función los manda llamar.
        CheckSizeToUsed();
        // Esta función selecciona el segundo color para la instrucción (a partir del nivel 4). Tambien se asegura de no repetir el mismo color 2 veces en la misma instrucción.
        CheckRandomColor2();
        // Esta función selecciona el lado del que tiene que pintar las figuras el niño (esta instrucción es exclusiva del nivel 5). 
        CheckInstructionsSides();
        // Aqui checo el “Map” para ver que la instrucción que acabo de escribir sea valida.
        GetBillboardMapsManagerScript.CheckHashtableList();

        if (GetBillboardMapsManagerScript.EscribirRonda == false){

            GetBillboardMapsManagerScript.EscribirRonda = true;
            InstructionFunction();
            yield break;
        }
        // Aqui activo el bool que me permite seleccionar colores 
        ColorAllowed = true;

        InstructionRound++;
        // Aqui escribo las instrucciones.
        InstructionsText.text = Conjunciones[Conjuction0Key] + ObjectValue + Conjunciones[Conjuction2Key] + ObjectSize + Conjunciones[Conjuction1Key] + ColorValue + Conjunciones[Conjuction3Key] + ColorValue2 + Conjunciones[Conjuction4Key] + ObjectSide;
        // Aqui guardo la hora en la que se genero la instrucción para poder sacar el tiempo de reacción.
        InstructionTime = DateTime.Now;
        // Aqui escribo la instruccion 1 en el archivo de texto.
        if (File.Exists(BillboardExerciseWriterScript.BillboardExerciseLogPath)){

            if (EnglishM == false){

                File.AppendAllText(BillboardExerciseWriterScript.BillboardExerciseLogPath, "Ronda De Instrucción = " + InstructionRound + ": " + InstructionsText.text + "\n\n");
            }
            else{
                File.AppendAllText(BillboardExerciseWriterScript.BillboardExerciseLogPath, "instruction Round = " + InstructionRound + ": " + InstructionsText.text + "\n\n");
                print("Instruccion en ingles");
            }
        }
        // Aqui escribo el segundo par de instrucciones. Estas instrucciones solo se activan a partir del nivel 4.
        if (Level >= 4 && SecondInstructionOff == false){
            Instructions2Text.text = Conjunciones[Conjuction0Key] + ObjectValue2 + Conjunciones[Conjuction2Key] + ObjectSize2 + Conjunciones[Conjuction1Key] + ColorValueIn2 + Conjunciones[Conjuction3Key] + ColorValue2In2 + Conjunciones[Conjuction4Key] + ObjectSide2;
            // Aqui escribo la instruccion 2 en el archivo de texto.
            if (File.Exists(BillboardExerciseWriterScript.BillboardExerciseLogPath)){

                if (EnglishM == false){

                    File.AppendAllText(BillboardExerciseWriterScript.BillboardExerciseLogPath, "instrucción 2; Ronda De Instrucción = " + InstructionRound + ": " + Instructions2Text.text + "\n\n");
                }
                else{
                    File.AppendAllText(BillboardExerciseWriterScript.BillboardExerciseLogPath, "instruction 2; instruction Round = " + InstructionRound + ": " + Instructions2Text.text + "\n\n");
                }
            }
        }
        // Apago la instrucción 2 si no se esta jugando algún nivel que la utilice.
        if (SecondInstructionOff == true){

            Instructions2Text.text = "";
        }

        // El int 'timeBetweenInstructions' es el tiempo que va(n) a ser valida(s) la(s) intrución(es) pasada(s).
        yield return new WaitForSeconds(timeBetweenInstructions);

        if (EnglishM == false){

            InstructionsText.text = "cargando instrucciones nuevas... ";
        }

        if (EnglishM == true){

            InstructionsText.text = "Loading instructions";
        }

        if (Level >= 4 && SecondInstructionOff == false){

            if (EnglishM == false){

                Instructions2Text.text = "cargando instrucciones nuevas... ";
            }

            if (EnglishM == true){

                Instructions2Text.text = "Loading instructions";
            }
        }
        /* Esta función hace que las metricas le pasen su información a sus copias del script “Metric”,
         * el cual organizara la información para que el “WriterScript” la pase al documento de texto. */
        GetFlusherScript.FlushPerInstructionsValues();
        GetAudio.clip = CambioIstrucciones;
        GetAudio.Play();
        // Aqui voy a desactivar la habilidad de seleccionar colores, para que el niño no este escogiendo colores mientras se generan nuevas instrucciones. 
        ColorAllowed = false;

        yield return new WaitForSeconds(1);

        // Esta función se encarga de regresar todos los valores a donde tienen que estar para la proxima instrucción.
        AfterInstructionsProcedures();
        // Aqui se manda a escribir las metricas de esta ronda de instruccion(es).
        //if (GetBillboardMapsManagerScript.EscribirRonda == true)
        GetBillboardExerciseWriterScript.WriteToFile();
        // 
        //GetBillboardMapsManagerScript.EscribirRonda = false;
        // Cancelo todas las invocaciones que existan, porque cuando a la instruccion le toma varios intentos encuntrar un valor valido hace muchas llamadas de la misma instrucción.
        CancelInvoke();
        // mando a llamar las instrucciones.
        Invoke("InstructionFunction", 1);
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Solo es una función que crea un numero al azar para escoger un color.
    // Si el nivel es 4 o mayor, se crea un segundo color para la segunda instrucción.
    void RandomColor(){

        RandCol = UnityEngine.Random.Range(0, ColorsAvailable);
        if (Level >= 4){

            RandCol2 = UnityEngine.Random.Range(0, ColorsAvailable);
            if (RandCol2 == RandCol){

                RandomColor();
            }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Aquí le doy su valor a los strings de las instrucciones para los colores. 
    void SelectColors(){

        RandomColor();
        ColorValue = Instructions[RandCol];

        if (Level >= 4){

            ColorValue2 = Instructions[RandomColorIntruction2];
            if (ColorValue2 == ColorValue){

                SelectColors();
            }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Solo es una función que crea un numero al azar para escojer el segundo color, tanto para la primera, como para la segunda instrucción.
    void RandomColor2(){

        // El segundo color de la primera instrucción.
        RandomColorIntruction2 = UnityEngine.Random.Range(0, ColorsAvailable);

        if (Level >= 4){

            // el segundo color de la segunda instrucción.
            RandomColor2Intruction2 = UnityEngine.Random.Range(0, ColorsAvailable);

            if ((RandomColor2Intruction2 == RandomColorIntruction2) || (RandomColor2Intruction2 == RandCol)){

                RandomColor2();
            }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    /* Esta función nada más va a tener que checar que el color1 y el color2 no sean el mismo. 
      Por lo que solo va a comparar que el color que esta función eliga no sea el mismo color de la funcion "CheckColorAlreadyUsed". */

    void CheckRandomColor2(){

        if (timer > 0 && Level >= 4){

            RandomColor2();
            if (RandCol2 != RandCol){

                ColorValueIn2 = Instructions[RandCol2];
                // aqui voy a tratar de asignarle un valor a la instrucción del segundo color a elegir de la segunda instrución.
                ColorValue2In2 = Instructions[RandomColor2Intruction2];
                // Este if se asegura que el primer color de la segunda instrucción no sea el mismo que el segundo Ej: (rojo,rojo).
                if (ColorValue2In2 == ColorValueIn2){

                    CheckRandomColor2();
                }
            }
            // vuelvo a llamar esta función en caso de que los colores se hayan repetido. 
            else { CheckRandomColor2(); }
        }
        // Este es el para cuando no estamos en el nivel 4 o 5. Hace que el valor del segundo color siempre sea nulo.
        else { ColorValue2 = ""; }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Solo es una función que crea un numero al azar para escoger una figura.
    // Si el nivel es 4 o mayor, se selecciona un segundo objecto para pintar para la segunda instrucción.
    void RandomObject(){

        RandObj = UnityEngine.Random.Range(6, (6 + ObjectsAvailable));
        if (Level >= 4){

            // El segundo objeto de la primera instrucción.
            RandomObjectInstruction2 = UnityEngine.Random.Range(6, (6 + ObjectsAvailable));

            // Si el objeto de la instrucción 2 es igual al objeto de la instrucción 1, se repite esta misma función, hasta que los objetos sean diferentes.
            if (RandomObjectInstruction2 == RandObj){

                RandomObject();
            }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    void SelectObjects(){

        RandomObject();
        ObjectValue = Instructions[RandObj];
        if (Level >= 4){

            ObjectValue2 = Instructions[RandomObjectInstruction2];

            if ((ObjectValue2 == ObjectValue) && SecondInstructionOff == false){

                SelectObjects();
            }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Selecciono un tamaño al azar para las instrucciones.
    void RandomSize(){

        RandSize = UnityEngine.Random.Range(12, 14);
        if (Level >= 4){

            RandomSizeInstruction2 = UnityEngine.Random.Range(12, 14);
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Creó un numero al azar para decidir que lado vas a tener que pintar. 
    void RandomPosition(){

        RandSide = UnityEngine.Random.Range(14, 16);

        if (Level == 5){

            RandomSideInstruction2 = UnityEngine.Random.Range(14, 16);
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    /* Esta funcion se encarga de decidir si el niño va a tener que pintar un objeto de tamaño grande o normal, mietras juegue en un nivel inferior a 3, 
     el valor del string de tamaño siempre será = "", cuando juegue en "Level" >= 3 se abrira la posibilidad de que le pidamos diferenciar entre pintar objetos grandes o "normales". */

    void CheckSizeToUsed(){

        if (Level > 2){

            RandomSize();
            ObjectSize = Instructions[RandSize];
        }

        if (Level >= 4){

            ObjectSize2 = Instructions[RandomSizeInstruction2];
        }
    }


    // _________________________________________________________________________________________________________________________________________________________________________________________

    void CheckInstructionsSides(){

        // Para el nivel 5, aquí escojo los lados para las intrucciones en cuanto a que lado tienes que pintar los objetos.
        if (timer > 0 && Level == 5){

            RandomPosition();
            ObjectSide = Instructions[RandSide];
            ObjectSide2 = Instructions[RandomSideInstruction2];
        }
    }

    // ________________________________________________________________________________________________________________________________________________________________________________________

    void ArrangeUI(){

        if (Level == 1){

            // Aqui las instrucciones van a ser posicionadas abajo, porque solo va a haber un panel.
            Panel1.gameObject.transform.position = InstructionsPositionsLevel1.transform.position;

            // Aqui posiciono la paleta de colores en la esquina inferior derecha de la pantalla.
            RightPallete.transform.position = CenterPalletePosition.transform.position;
            RightPallete.transform.rotation = CenterPalletePosition.transform.rotation;

            // Aqui me aseguro que el unico billboard que este activo sea el del centro, y apago los billboards de los lados. 
            BillboardCenter.gameObject.SetActive(true);
            BillboardRight.gameObject.SetActive(false); // Billboard derecho.
            BillboardLeft.gameObject.SetActive(false); // Billboard izquierdo.

        }

        if (Level == 2 || Level == 3 || Level == 4){

            // Aqui posiciono las instrucciones en el centro de la pantalla, porque ahora los billboards entan a los lados.
            Panel1.gameObject.transform.position = InstructionsPositions.transform.position;

            // Aqui posiciono la paleta de colores que va a estar a la derecha del billboard derecho.
            RightPallete.transform.position = RightPalletePosition.transform.position;
            RightPallete.transform.rotation = RightPalletePosition.transform.rotation;

            // Aqui apago el billboard del centro y prendo los billboards de los costados.
            BillboardCenter.gameObject.SetActive(false);
            BillboardRight.gameObject.SetActive(true);
            BillboardLeft.gameObject.SetActive(true);

        }

        if (Level == 5){

            // Aqui posiciono las instrucciones en el centro de la pantalla, porque ahora los billboards entan a los lados.
            Panel1.gameObject.transform.position = InstructionsPositions.transform.position;

            // Aqui posiciono la paleta de colores que va a estar a la derecha del billboard derecho.
            RightPallete.transform.position = RightPalletePositionLevel5.transform.position;
            RightPallete.transform.rotation = RightPalletePositionLevel5.transform.rotation;

            // Aqui apago el billboard del centro y prendo los billboards de los costados.
            BillboardCenter.gameObject.SetActive(false);
            BillboardRight.gameObject.SetActive(true);
            BillboardLeft.gameObject.SetActive(true);

        }

        // Aqui activo las segundas instrucciones a partir del nivel 4. En cualquier otro nivel esto no esta prendido.
        if (Level == 4 || Level == 5){

            Panel2.gameObject.SetActive(true);
            Instructions2Text.gameObject.SetActive(true);
            Panel2.gameObject.transform.position = Instructions2Text.transform.position;
        }
        if (Level < 4){

            Panel2.gameObject.SetActive(false);
            Instructions2Text.gameObject.SetActive(false);
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    void FillDictionary(){

        // Colores
        Instructions.Add(RedKey, Red);
        Instructions.Add(BlueKey, Blue);
        Instructions.Add(Greenkey, Green);
        Instructions.Add(KeyYellow, Yellow);
        Instructions.Add(BrownKey, Brown);
        Instructions.Add(PinkKey, Pink);

        // Figuras
        Instructions.Add(CubeKey, Cube);
        Instructions.Add(SphereKey, Sphere);
        Instructions.Add(CapsuleKey, Capsule);
        Instructions.Add(CylinderKey, Cylinder);
        Instructions.Add(Object5Key, Object5);
        Instructions.Add(Object6Key, Object6);

        // Tamaños
        Instructions.Add(NormalKey, Normal);
        Instructions.Add(BigKey, Big);

        // Direcciones
        Instructions.Add(LeftKey, Left);
        Instructions.Add(RightKey, Right);

        // Aquí activo unas conjunciones para que dependiendo dé las instrucciones, las oraciones tengan sentido. 
        SentenceStart = "Pinta ";
        ConjuctionColor = " de color ";

        // Aquí activo unas conjunciones para que dependiendo dé las instrucciones, las oraciones tengan sentido. 
        if (Level >= 3) { ConjuctionSize = " "; }
        if (Level >= 4) { ConjuctionColor2 = " y "; }
        if (Level == 5) { ConjuctionSide = " del lado "; }

        // Aqui voy a llenar el diccionario de las conjucciones.
        Conjunciones.Add(Conjuction0Key, SentenceStart);
        Conjunciones.Add(Conjuction1Key, ConjuctionColor);

        if (Level < 3){

            Conjunciones.Add(Conjuction2Key, " ");
            Conjunciones.Add(Conjuction3Key, "");
            Conjunciones.Add(Conjuction4Key, "");
        }

        if (Level == 3){

            Conjunciones.Add(Conjuction2Key, ConjuctionSize);
            Conjunciones.Add(Conjuction3Key, " ");
            Conjunciones.Add(Conjuction4Key, " ");
        }

        if (Level == 4){

            Conjunciones.Add(Conjuction2Key, ConjuctionSize);
            Conjunciones.Add(Conjuction3Key, ConjuctionColor2);
            Conjunciones.Add(Conjuction4Key, "");
        }

        if (Level == 5){

            Conjunciones.Add(Conjuction2Key, ConjuctionSize);
            Conjunciones.Add(Conjuction3Key, ConjuctionColor2);
            Conjunciones.Add(Conjuction4Key, ConjuctionSide);
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Aqui traduzco las palabras claves al ingles.
    void FillTranslation(){

        EnglishTranslation.Add("Rojo", "Red");
        EnglishTranslation.Add("Azul", "Blue");
        EnglishTranslation.Add("Verde", "Green");
        EnglishTranslation.Add("Amarillo", "Yellow");
        EnglishTranslation.Add("Cafe", "Brown");
        EnglishTranslation.Add("Rosa", "Pink");

        EnglishTranslation.Add("Pinguinos", "Pinguins");
        EnglishTranslation.Add("Pelotas", "Balls");
        EnglishTranslation.Add("Arboles", "Trees");
        EnglishTranslation.Add("Peces", "Fishes");
        EnglishTranslation.Add("Estrellas", "Stars");
        EnglishTranslation.Add("Cohetes", "Rockets");

        EnglishTranslation.Add("Pequeños", "(Small)");
        EnglishTranslation.Add("Grandes", "(Big)");

        EnglishTranslation.Add("Izquierdo", "Left");
        EnglishTranslation.Add("Derecho", "Right");

        EnglishTranslationCon.Add(SentenceStart, "Paint ");
        EnglishTranslationCon.Add(ConjuctionColor, " with ");

        if (Level >= 3){

            EnglishTranslationCon.Add(ConjuctionSize, " ");
        }

        if (Level >= 4){

            EnglishTranslationCon.Add(ConjuctionColor2, " and ");
        }

        if (Level == 5){

            EnglishTranslationCon.Add(ConjuctionSide, " at your ");
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    void ManageExtraColors(){

        if (Level <= 3){

            // determino cuantos colores estan disponibles para las instrucciones. 
            ColorsAvailable = 4;
            ObjectsAvailable = 4;
            // Aqui apago los colores que todavia no se tienen que mostrar.
            FifthColor.gameObject.SetActive(false);
            FifthColorBackground.gameObject.SetActive(false);

            SixthColor.gameObject.SetActive(false);
            SixthColorBackground.gameObject.SetActive(false);
        }

        // En el nivel 4 solo prendo el color 5 (va a ser Cafe), juntos con su fondo.
        if (Level == 4){

            // determino cuantos colores estan disponibles para las instrucciones.
            ColorsAvailable = 5;
            ObjectsAvailable = 5;

            FifthColor.gameObject.SetActive(true);
            FifthColorBackground.gameObject.SetActive(true);
            // Aqui apago el color que todavia no se tiene que mostrar.
            SixthColor.gameObject.SetActive(false);
            SixthColorBackground.gameObject.SetActive(false);
        }

        // En el nivel 5 prendo los 2 colores extras (el sexto color va a ser Rosa), junto con sus fondos.
        if (Level == 5){
            //
            ColorsAvailable = 6;
            ObjectsAvailable = 6;
            // Aqui prendo el quinto color en el nivel 5
            FifthColor.gameObject.SetActive(true);
            FifthColorBackground.gameObject.SetActive(true);
            // Aqui prendo el color que falta en el nivel 5
            SixthColor.gameObject.SetActive(true);
            SixthColorBackground.gameObject.SetActive(true);
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    void AfterInstructionsProcedures(){

        if (timer > 0){

            /* En este “if” checo y reseteo todo lo necesario antes de mandar llamar nuevas instrucciones:
               - Reseteo los valores del raycast para que el niño no puedan cometer errores injustos para ellos.
               - Mando a llamar la función que rota la posición de las figuras.
               - Le regreso su “nombre” a las figuras que pinto bien en esta ronda de instrucciones. */

            // De-selecciono cualquier objeto que haya escojido el niño.
            RaycastPallete.ChoseFigure = "";

            RaycastPallete.choseColor = "Negro";
            RaycastPallete.choseColor2 = "Negro";
            // Reseteo el primer color a negro.
            RaycastPallete.Raycolor = colorReset;
            // Reseteo el segundo color a negro.
            RaycastPallete.Raycolor2 = colorReset;
            // 
            RaycastPallete.ColorsPicked = 0;
            // Cuando el niño selecciona el color y figura correctamente. esa figura "pierde" su nombre, aqui le "regreso" su nombre a todas las piezas que contesto correctamente.
            resetObjName = true;

            // estos "if"s se encargan de monitorear la rotación de posiciones de las figuras.
            if (Level >= 2){

                MovementeIsOn = true;

                TurnsToShift--;
                if (MovementeIsOn == true){

                    if (TurnsToShift == 0){

                        RandomMove = UnityEngine.Random.Range(0, 2);
                        MoveObjects[RandomMove].ChangePositions();
                        TurnsToShift = 2;
                    }
                }
            }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    void ActivateTranslation(){

        if (EnglishM == true){

            for (int i = 0; i < Instructions.Count; i++){

                Instructions[i] = (string)EnglishTranslation[Instructions[i]];
            }

            for (int i = 0; i < Conjunciones.Count; i++){

                Conjunciones[i] = (string)EnglishTranslationCon[Conjunciones[i]];
            }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    IEnumerator SendMail(){

        SendedMail = true;

        if (PlayerPrefs.HasKey(IDScript.SaveID) == true){

            /* Esta función hace que las metricas le pasen su información a sus copias del script “Metric”,
           el cual organizara la información para que el “WriterScript” la pase al documento de texto. */
            GetFlusherScript.FlushPerInstructionsValues();
            yield return new WaitForSeconds(0.2f);

            // Aqui se manda a escribir las metricas de esta ronda de instruccion(es).
            GetBillboardExerciseWriterScript.WriteToFile();

            yield return new WaitForSeconds(0.2f);

            // Aqui preparo las lineas que se van a escribir con los resultados finales.
            GetFlusherScript.FlushFinalResults();

            yield return new WaitForSeconds(0.1f);

            // El flush con la información final del ejercicio.
            GetBillboardExerciseWriterScript.FinalWrite();

            MailMessage mail = new MailMessage{

                // Este va a ser el mail de nosotros, el “Sender”
                From = new MailAddress("n2500@live.com")
            };
            // Este va a ser el que les vamos a crear a la gente que los compre.
            mail.To.Add("ricardo.narvaez.loyola@hotmail.com");
            // el proposito del correo.
            mail.Subject = "Mail de pruebas con ID (" + PlayerPrefs.GetString(IDScript.SaveID) + ")";
            // Aqui va el cuerpo del mail.
            mail.Body = "Texto de pruebas unicamente, despues se puede cambiar.";

            // En esta parte agrego el archivo de texto en el mail.
            Attachment attachment = new Attachment(BillboardExerciseWriterScript.FinalLogPath);
            mail.Attachments.Add(attachment);

            SmtpClient smtpServer = new SmtpClient{

                Host = "smtp.gmail.com",
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 587,
                // Apago las credenciales default, al parecer no se pueden usar y tienes que usar una “real”.
                UseDefaultCredentials = false,
                // Estas credenciales van a ser vas credenciales del mail sender que vamos a crear.
                Credentials = new NetworkCredential("ricardonarvaezloyola@gmail.com", "12121993") as ICredentialsByHost,
                EnableSsl = true
            };

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyErrors) {

                return true;
            };
            smtpServer.Send(mail);
            PlayerPrefs.DeleteKey(IDScript.SaveID);
        }
    }
}