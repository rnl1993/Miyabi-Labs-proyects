using System;
using UnityEngine;

public class Metric : MonoBehaviour, IComparable<Metric>{

    // Public variables.
    // Este int se va usar para poder ordenar la manera en la que se va a ir escribiendo en el archivo.
    public int priority;

    // Private variables.

    /* Este es el string que se va a mandar escribir en el documento de texto, la idea es que el script
      “WriteScript” va a agarrar este string que ya va haber sido procesado por el script de cada métrica. */
    [HideInInspector] public string StringToWriteOnFile;

    // Este es el string que se va a mandar escribir en el documento de texto cuando se acaba el tiempo del ejercicio.
    [HideInInspector] public string FinalWriteOnFile;

    // Esta función crea el string que se va a pasar al archivo de texto en cada ronda de instrucciones.
    public void CopyPerInstructionMetrics(string MetricName, float MetricValue, string Format){

        StringToWriteOnFile = "";
        StringToWriteOnFile = MetricName + MetricValue.ToString() + Format;
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función crea el string que se va a pasar al archivo de texto al final de ejercicio, Este string tiene la cuenta final de todas las métricas.
    public void FinalLogWrite(string MetricName, float FinalMetricValue, string Format){

        FinalWriteOnFile = MetricName + FinalMetricValue.ToString() + Format;
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función es nativa del “IComparable<Metric>”, y me va a ayudar a poder ordenar los scripts dentro de una lista.
    public int CompareTo(Metric other){

        if (this.priority > other.priority){

            return 1;
        }

        else if (this.priority < other.priority){

            return -1;
        }

        else{

            return 0;
        }
    }
}