using System;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReactionTime : MonoBehaviour{

    // Este bool es para evitar que se mida el tiempo de reacción más de una vez por ronda de instrucciones.
    private bool IsCounted;

    private readonly List<float> ReactionsTimesList = new List<float>();
    // Este es el nombre de la métrica que se va a escribir.
    private string MetricName;

    // El string del tiempo de reacción promedio del niño.
    private string AverageTOR;
    // Aqui registro el momento en el que el niño selecciono el primer objeto de cada ronda de instrucción.
    private DateTime ButtonPress;
    // Este es el tiempo de reacción del niño.
    private TimeSpan TimeToReact;
    // Este es el valor de la métrica por ronda de instrucción que se va a escribir.
    private float MetricValuePerInstruction;
    private float AverageTimeOfReaction;
    // Uso este Double para redondear el tiempo que sale en cada ronda de instrucción a 2 digitos.
    private Double RoundTime;
    // Uso este Double para redondear el tiempo que sale en el resultado final a 2 digitos.
    private Double AverageRoundUp;

    // Script references.
    private InstructionsScript GetInstructionScript;
    // Una referencia a mi propio código para poder accesar a sus funciones.
    private Metric MetricScript;

    // Use this for initialization
    void Start(){

        // Este script es a donde voy a enviar las métricas procesadas.
        MetricScript = GetComponent<Metric>();
        /* Busco el script de las instrucciones, para poder comparar el objeto que pasa 
           el raycast con el nombre del objeto que esta pidiendo las instrucciones. */
        GetInstructionScript = FindObjectOfType<InstructionsScript>();

        if (GetInstructionScript.EnglishM == false){
            MetricName = "Tiempo de reacción ";
            AverageTOR = "Promedio del tiempo de reacción";
        }
        else{
            MetricName = "Reaction Time ";
            AverageTOR = "Average Reaction Time";
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    public void ReactionTimePerInstruction(){

        CountMetricPerInstruction();
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función cuenta cuántas veces el niño cumplió las condiciones de esta métrica. 
    void CountMetricPerInstruction(){

        if (IsCounted == false){

            ButtonPress = DateTime.Now;
            // Aquí saco el tiempo de reacción del niño.
            TimeToReact = ButtonPress - InstructionsScript.InstructionTime;
            // Aquí convierto “TimeToReact” de un “TimeSpan” a un float, para poderlo pasar a la función que crea el string que se manda al writer.
            MetricValuePerInstruction = float.Parse(TimeToReact.TotalSeconds.ToString());
            ReactionsTimesList.Add(MetricValuePerInstruction);
            // Cambio el bool a true para que no se registre más de un tiempo de reacción.
            IsCounted = true;
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función procesa la información de cada ronda de instrucciones.
    public void CopyCountMetricPerInstruction(){

        RoundTime = MetricValuePerInstruction;

        if (GetInstructionScript.EnglishM == false){
            // Aquí Genero el string de la métrica que se va a mandar escribir en el documento de texto.
            MetricScript.CopyPerInstructionMetrics(MetricName + " = ", (float)Math.Round(RoundTime, 2), " segundos" + "\n\n");
        }

        if (GetInstructionScript.EnglishM == true){
            MetricScript.CopyPerInstructionMetrics(MetricName + " = ", (float)Math.Round(RoundTime, 2), " seconds" + "\n\n");
        }

        MetricValuePerInstruction = 0;
        IsCounted = false;
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función lo que hace es crear la ultima entrada de métricas para el archivo de texto con los resultados finales.
    public void EndGameMetricValues(){

        // Aquí saco el tiempo promedio de reacción.
        AverageTimeOfReaction = ReactionsTimesList.Average();
        // Aqui redondeo el valor del MaxTime a 2 digitos.
        AverageRoundUp = AverageTimeOfReaction;

        if (GetInstructionScript.EnglishM == false){
            // Aqui utilizo normal la función que ya habia creado para mandar a escribir al archivo.
            MetricScript.FinalLogWrite(AverageTOR + " = ", (float)Math.Round(AverageRoundUp, 2), " segundos" + "\n\n");
        }

        else{
            MetricScript.FinalLogWrite(AverageTOR + " = ", (float)Math.Round(AverageRoundUp, 2), " seconds" + "\n\n");
        }
    }
}