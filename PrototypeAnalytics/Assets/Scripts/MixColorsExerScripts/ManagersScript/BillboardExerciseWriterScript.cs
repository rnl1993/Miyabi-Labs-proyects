using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class BillboardExerciseWriterScript : MonoBehaviour{

    // Donde se guarda el archivo de texto.
    [HideInInspector] public static string BillboardExerciseLogPath;
    [HideInInspector] public static string FinalLogPath;
    private readonly List<Metric> GetMetricsGameObjects = new List<Metric>();

    // script refences
    private Metric[] GetMetrics;
    private InstructionsScript GetInstructionsScript;

    // El ID del usuario.
    private string UserID;

    void Awake(){

        // Path of the file
        BillboardExerciseLogPath = Application.persistentDataPath + "/BillboardExerciseLog.txt";
        FinalLogPath = Application.persistentDataPath + "/Exercise_Log.txt";

        // Borro el archivo de la sesión anterior al inicio del nuevo ejercicio.
        if (File.Exists(BillboardExerciseLogPath)){

            File.Delete(BillboardExerciseLogPath);
        }

        if(File.Exists(FinalLogPath)){
            File.Delete(FinalLogPath);
        }

        if (PlayerPrefs.HasKey(IDScript.SaveID)){

            CreateText();
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Use this for initialization
    void Start(){

        // Aqui busco todas las copias del script “Metric”
        GetMetrics = FindObjectsOfType<Metric>();
        GetInstructionsScript = FindObjectOfType<InstructionsScript>();
        if (GetInstructionsScript.EnglishM == false){
            File.AppendAllText(BillboardExerciseLogPath, "Nivel del ejercicio: " + GetInstructionsScript.Level + "\n\n");
        }
        else{
            File.AppendAllText(BillboardExerciseLogPath, "Exercise Level: " + GetInstructionsScript.Level + "\n\n");
        }

        /* Aqui agrego todas las copias del script Metric a una lista para poder acomodar dichos scripts
           para que escriban en el orden que yo les diga. */
        for (int i = 0; i < GetMetrics.Length; i++){

            GetMetricsGameObjects.Add(GetMetrics[i]);
        }

        // Aqui acomodo los scripts.
        GetMetricsGameObjects.Sort();
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    void CreateText(){

        //Create file if it doesn't exist
        if (!File.Exists(BillboardExerciseLogPath)){

            File.WriteAllText(FinalLogPath, "Log " + System.DateTime.Now + "\n\n");
            // Nombre del usuario.
            UserID = PlayerPrefs.GetString(IDScript.SaveID) + "\n\n";
            // Aqui agregamos el nombre al archivo
            File.AppendAllText(FinalLogPath, "ID: " + UserID + " ");
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Aqui escribo las metricas al final de cada ronda de instrucciones, mientras aun hay tiempo en el ejercicio.
    // Cuando se acaba el tiempo del ejercicio hay otra función que se encarga de escribir el “Log” final.
    public void WriteToFile(){

        for (int i = 0; i < GetMetricsGameObjects.Count; i++){

            File.AppendAllText(BillboardExerciseLogPath, GetMetricsGameObjects[i].StringToWriteOnFile);
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    /* Esta función va a hacer el ultimo write al archivo de texto, esta función se tiene que llamar cuando el 
       tiempo del ejercicio se terminó. */
    public void FinalWrite(){

        if (GetInstructionsScript.EnglishM == false){
            File.AppendAllText(FinalLogPath, "Resultados finales:" + "\n\n");
        }
        else{
            File.AppendAllText(FinalLogPath, "Final Results:" + "\n\n");
        }


        for (int i = 0; i < GetMetricsGameObjects.Count; i++){

            File.AppendAllText(FinalLogPath, GetMetricsGameObjects[i].FinalWriteOnFile);
        }

        File.AppendAllText(FinalLogPath, "\n" + File.ReadAllText(BillboardExerciseLogPath) + "\n\n");

        File.AppendAllText(FinalLogPath, "\n" + "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - " + "\n\n");

        if(GetInstructionsScript.EnglishM == false){

            File.AppendAllText(FinalLogPath, "Tiempo de reacción: Es el tiempo que le tomo al niño pintar su primer objeto desde el momento que escribió cada instrucción. " + "\n\n");
            File.AppendAllText(FinalLogPath, "Color Correcto: El numero de veces que pintó una figura con el color de la instrucción." + "\n\n");
            File.AppendAllText(FinalLogPath, "Figuras Correctas: El numero de veces que pintó la figura dada en la instrucción." + "\n\n");
            File.AppendAllText(FinalLogPath, "Color Correcto, Figura Incorrecta: la combinación de las 2 métricas. " + "\n\n");
            File.AppendAllText(FinalLogPath, "Figura Correcta, Color Incorrecto: la combinación de las 2 métricas. " + "\n\n");
            File.AppendAllText(FinalLogPath, "Instrucción Correcta: Cuántas veces siguió la instrucción al pie de la letra. " + "\n\n");
            File.AppendAllText(FinalLogPath, "Color Incorrecto: Cada que pinta cualquier objeto con un color que no es el de la instrucción." + "\n\n");
            File.AppendAllText(FinalLogPath, "Figuras Incorrectas: El numero de veces que pintó cualquier figura que no sea la de la instrucción." + "\n\n");
            File.AppendAllText(FinalLogPath, "Instrucción Incorrecta: cuando tanto el objeto, como el color. " + "\n\n");
            File.AppendAllText(FinalLogPath, "Billboards completos: Cuantas veces se pintaron todas las figuras, independientemente de si las figuras se pintaron de manera correcta o no." + "\n\n");
        }
        else{
            File.AppendAllText(FinalLogPath, "Reaction Time: The time it took the kid to select the first object of every instruction. " + "\n\n");
            File.AppendAllText(FinalLogPath, "CorrectColor: The number of times the kid painted an object with the correct color." + "\n\n");
            File.AppendAllText(FinalLogPath, "Correct Objects: The number of times the kid selected the correct object." + "\n\n");
            File.AppendAllText(FinalLogPath, "Correct Color, Incorrect Object: The number of times the kid used the correct color(s), but didn't select the correct object. " + "\n\n");
            File.AppendAllText(FinalLogPath, "Correct Object, Incorrect Color: The number of times the kid selected the correct object, but didn't use the correct color(s). " + "\n\n");
            File.AppendAllText(FinalLogPath, "Correct Instruction: The number of times the kid followed instructions correctly. " + "\n\n");
            File.AppendAllText(FinalLogPath, "Incorrect Color: The number of times the kid painted an object with the incorrect color" + "\n\n");
            File.AppendAllText(FinalLogPath, "Incorrect Object: The number of times the kid selected an incorrect object." + "\n\n");
            File.AppendAllText(FinalLogPath, "Incorrect Instruction: The number of times the kid followed instructions correctly. " + "\n\n");
            File.AppendAllText(FinalLogPath, "Billboards completed: The number of times all the objects in scene were painted, regardless whether they are correct or not. " + "\n\n");
        }
    }
}