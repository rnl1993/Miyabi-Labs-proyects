using UnityEngine;
using System.Collections;

public class ObjToPaint : MonoBehaviour{
    // public variables
     /*  El bool "colored" lo uso en el script de "InstructionsScript" para marcar que ya pinte esta figura.
        (Como cada figura tiene su propia instancia del script este bool es independiente de las demas figuras). */
        [HideInInspector] public bool colored;
          /*  El bool "isCounted" tambien trabaja en el script "InstructionsScript". 
        La primera vez que se pinta una figura el bool cambia a true, y de ahi
        no se resetea a false hasta que el billboard sea pintado por completo. */
       [HideInInspector] public bool isCounted;
   
    /*  Como su nombre lo dice, este bool se utiliza para llevar un control de cuando el niño contesto 
        correctamente una vez y evitar que haya respuestas correctas repetidas. */
        [HideInInspector] public bool IsPaintedCorrectly;
    // necesito el Renderer publico para que el raycast pueda pintar a la figura.
        [HideInInspector] public Renderer[] rend;
    // Este color es el color con el que vamos a pintar la figura. El color viene desde el raycast.
        [HideInInspector] public Color ColorToUse;

     public ObjsNames_SO objsNames;
    // Estos strings son para el raycast
    public string ObjectName; // Nombre del objeto.
    public string ObjectSize; // Tamaño del objeto.
    public string ObjectPositions; // Posición del objeto.
    //
    public int ObjectAlreadyCounted;

    // Estos 2 vectores y bool son para determinar y mantener la diferencia de tamaños en los objectos, cuando se juega en nivel 3 o mayor.
     [HideInInspector] public Vector3 ObjectIsBigVector;
     [HideInInspector] public Vector3 ObjectIsSmallVector;
     [HideInInspector] public bool IsBig;

    // private variables

    private Vector3 CompareSize;
    // Este int es para determinar que figuras van a ser grandes o no. 
    private int WillItBeBig;

    // script reference
    private InstructionsScript GetInstructions;
    private RaycastPalleteScript raycastPallete;

    // Funtions _______________________________________________________________________________________

    void Start(){
        
        // Las referencias de los scripts y demás componentes.
        raycastPallete = FindObjectOfType<RaycastPalleteScript>();
        GetInstructions = FindObjectOfType<InstructionsScript>();
        rend = GetComponentsInChildren<Renderer>();

        StartCoroutine(LateStart(0.01f));

    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    void Update(){
        
        DisableName();
        IsPaintedCorrectly &= GetInstructions.resetObjName != true;
        if (GetInstructions.Level == 5)
            SideManagement();
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Aqui pinto los objetos seleccionados 
    public void Paint(){
        
        ColorToUse = raycastPallete.Raycolor;

        for (int i = 0; i < this.transform.childCount; i++){
            rend[i].material.color = ColorToUse;
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________
     
    /* Esta función apaga el "Nombre" del objeto que pinto el niño en caso de que haya hecho la instrucción de manera correcta, 
       para evitar que obtenga más de una respuesta correcta pintando la misma figura. */

    public void DisableName(){

        if (IsPaintedCorrectly == true){
            //ObjectName = "";
            ObjectAlreadyCounted = 2;
        }

        else{
            
            //ObjectName = objsNames.Name;
            ObjectAlreadyCounted = 0;
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    /* Esta función nada más checa que la figura se haya pintado. Despues de que es pintada la figura la primera vez,
    ya no puede ser contada como respuesta correcta o incorrecta. Esto es para que el niño pueda seguir pintando la
    misma figura si asi lo desean. Esta función tambien me va a ayudar a checar cuando el niño haya acabado un billboard. */

    public void Notwhite() { colored = true; }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    public void SizeManagement(){
       
        // Aqui establezco un Vector3 de escala 2 para comparar la escala del objeto actual; 
        // Si la escala del objecto es igual al Vector3 se considera como un objeto grande.
        CompareSize = new Vector3(2, 2, 2);

        /* Para asegurarnos de que haya por lo menos una pieza de cada tipo (Grande y pequeña) hay ciertas figuras que van a mantener su escala siempre.
           Estas figuras tienen un script extra, en este if checo que solo podamos variar al azar la escala de las figuras que no tengan este script ("KeepSize"). */
       
        if (this.GetComponent<KeepSize>() == null){
            
            if (WillItBeBig >= 50){
                
                ObjectIsBigVector = this.transform.localScale * 2;
                this.transform.localScale = ObjectIsBigVector;

                IsBig = true;
            }

            else{
                ObjectIsSmallVector = this.transform.localScale;

                IsBig = false;
            }
        }

        if(GetInstructions.EnglishM == false){
            // Aqui el objeto se le asigna el "string value" de "Grande".
            if (this.transform.localScale.x >= CompareSize.x)
            { ObjectSize = objsNames.BigSize; }
            // Aqui el objeto se le asigna el "string value" de "Pequeño".
            else { ObjectSize = objsNames.normalSize + "Pequeños"; }
        }

        if (GetInstructions.EnglishM == true) {

            // Aqui el objeto se le asigna el "string value" de "Grande".
            if (this.transform.localScale.x >= CompareSize.x)
            { ObjectSize = "(Big)"; }
            // Aqui el objeto se le asigna el "string value" de "Pequeño".
            else { ObjectSize = objsNames.normalSize + "(Small)"; }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Aqui les designo su "posición" a los objetos.
    public void SideManagement(){

        if(GetInstructions.EnglishM == false){

            if (this.transform.position.x < 0){
                ObjectPositions = "Izquierdo";
            }

            if (this.transform.position.x > 0){
                ObjectPositions = "Derecho";
            }
        }

        if(GetInstructions.EnglishM == true) {

            if (this.transform.position.x < 0){
                ObjectPositions = "Left";
            }

            if (this.transform.position.x > 0){
                ObjectPositions = "Right";
            }
        }

    }

    IEnumerator LateStart(float waitTime){
        
        yield return new WaitForSeconds(waitTime);

        if(GetInstructions.EnglishM == false){
            // Le asigno su "Nombre" al objeto para que lo pueda detectar el raycast.
            ObjectName = objsNames.Name;
        }

        if(GetInstructions.EnglishM == true){

            ObjectName = objsNames.EnglishName;
        }

        // Estos bools se comunican con el script de "InstructionsScript" para marcar si pintó las figuras correctas el niño. 
        IsPaintedCorrectly = false;
        if (GetInstructions.Level > 2){
            
            // Si "WillItBeBig" resulta mayor a 50, esta figura va ser del doble de tamaño.
            WillItBeBig = Random.Range(1, 101);
        }

        yield return new WaitForSeconds(waitTime);
        SizeManagement();
    }
}