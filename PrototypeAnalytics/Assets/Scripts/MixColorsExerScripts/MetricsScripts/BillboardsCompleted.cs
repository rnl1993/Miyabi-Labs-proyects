using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class BillboardsCompleted : MonoBehaviour {

    public AudioClip BillboardLleno;
    public GameObject BillboardCenter;

    // Private variables

    private int BillboardPoints; // Este int es para contar las figuras pintadas, independientemente de si fue de manera correcta o incorrecta. Se retea a 0 cada que llenas un billboard
    // Este int es para contar cuantas veces a completado un Billboard el niño
    private int RoundsCompleted;
    // Este bool es para asegurame de que el chequeo del billboard termino, antes de continuar con otras operaciones.
    private bool CheckBillboardFinished;
    //
    private string MetricName;
    // Este diccionario es para llevar el registro de las figuras que estan pintadas en el nivel 1, ya que solo en el nivel 1 hay un solo Billboard.
    private readonly Dictionary<int, bool> BillboardFiguresColored = new Dictionary<int, bool>();

    // Script References

    private Metric MetricScript;
    private AudioSource GetAudio;
    private ObjToPaint[] FiguresArray;
    private RearrangeObjectScript[] MoveObjects;
    private InstructionsScript GetInstructionsScript;
    private RaycastPalleteScript GetRaycastPalleteScript;
    private BillboardMapsManagerScript GetBillboardMapsManagerScript;


    // Use this for initialization
    void Start () {

        MetricScript = GetComponent<Metric>();
        GetAudio = GetComponent<AudioSource>();
        MoveObjects = FindObjectsOfType<RearrangeObjectScript>();
        GetInstructionsScript = FindObjectOfType<InstructionsScript>();
        GetRaycastPalleteScript = FindObjectOfType<RaycastPalleteScript>();
        GetBillboardMapsManagerScript = FindObjectOfType<BillboardMapsManagerScript>();

        MetricName = GetInstructionsScript.EnglishM == false ? "Billboards terminados " : "Billboards Completed ";
        StartCoroutine(LateStart());
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    IEnumerator LateStart(){

        yield return new WaitForSeconds(0.1f);
        FiguresArray = FindObjectsOfType<ObjToPaint>();
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    public void CheckIfBillboardIsCompleted(){

        StartCoroutine(BillboardCompleted());
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    IEnumerator BillboardCompleted(){          // Este bool es para asegurarme de que esta corrutina se haya realizado por completo antes de continuar con las tareas de las funciones que la llaman.         CheckBillboardFinished = false;         // Esta linea hace referencia al objeto que estamos pintando. Ademas de llamar una funció que me ayuda a saber que objetos ya estan pintados y cuales no.         GetRaycastPalleteScript.hit.collider.GetComponent<ObjToPaint>().Notwhite();         for (int i = 0; i < FiguresArray.Length; i++){              BillboardFiguresColored[i] = FiguresArray[i].colored;              if (BillboardFiguresColored[i] == true && FiguresArray[i].isCounted == false){                  FiguresArray[i].isCounted = true;                 BillboardPoints++;                 yield return new WaitForSeconds(0.1f);                 if (BillboardPoints == FiguresArray.Length){                      // Esta funció vuelve a llenar el “Map” de objetos.                     GetBillboardMapsManagerScript.SizeManager();                     // Aqui reactivo la instrucció, en caso de que se haya desactivado porque solo quedaba una opciones para las instrucciones.                     if (GetInstructionsScript.Level >= 4) { GetInstructionsScript.SecondInstructionOff = false; }                     // Aqui reseteo el contador de puntos del billboard.                      //Se tiene que resetear porque este funciona cuando su valor es igual al numero de objetos en cada billboard.                     BillboardPoints = 0;                     // El numero de billboards que se an pintando por completo.                     RoundsCompleted++;                     // Aqui manejo el audio para cuando llena el billboard.                      GetAudio.clip = BillboardLleno;                     GetAudio.Play();                     if (GetInstructionsScript.Level == 1){                          if (GetInstructionsScript.EnglishM == false)                             GetInstructionsScript.InstructionsText.text = "pintaste todas las figuras, espera instrucciones... ";                          if (GetInstructionsScript.EnglishM == true)                             GetInstructionsScript.InstructionsText.text = "You painted all the objects, wait for instructions...";                     }                     // En esta parte, para el nivel 1, le agrego el script de “RearrangeObjectScript” cuando el niñ completa su primer billboard.                     // De esta manera, cada que termine un billboard, las figuras se van a poder reacomodar.                     if (BillboardCenter.GetComponent<RearrangeObjectScript>() == null){                          MoveObjects[0] = BillboardCenter.AddComponent<RearrangeObjectScript>();
                        MoveObjects[0].ChangePositions();                     }                      MoveObjects[0].ChangePositions();                      yield return new WaitForSeconds(0.2f);                      for (int j = 0; j < FiguresArray.Length; j++){                          FiguresArray[j].colored = false;                         FiguresArray[j].isCounted = false;                         for (int x = 0; x < FiguresArray[j].transform.childCount; x++){                              // cuando todas las figuras estan pintadas. Las figuras vuelven a ser blancas                             FiguresArray[j].rend[x].material.color = Color.white;                         }                     }                 }             }         }         // Aqui marco como terminada esta corrutina.         CheckBillboardFinished = true;     }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    public void EndGameMetricValues(){

            MetricScript.FinalLogWrite(MetricName + " = ", RoundsCompleted, "\n\n");
    }
}
