using UnityEngine;

public class IncorrectColor : MonoBehaviour {

    // Private variables.

    // Este es el nombre de la metrica que se va a escribir.
    private string MetricName;
    // Este es el valor de la metrica por ronda de instrucción que se va a escribir.
    private int MetricValuePerInstruction;
    // Este valor es la suma total de todas las rondas de instrucciones de esta metrica.
    private int FinalMetricValue;

    // Script references.
    private InstructionsScript GetInstructionScript;
    // Una referencia a mi propio codigo para poder accesar a sus funciones.
    private Metric MetricScript;

    // Functions
    void Start(){

        // Este script es a donde voy a enviar las metricas procesadas.
        MetricScript = GetComponent<Metric>();
        /* Busco el script de las instrucciones, para poder comparar el objeto que pasa 
           el raycast con el nombre del objeto que esta pidiendo las instrucciones. */
        GetInstructionScript = FindObjectOfType<InstructionsScript>();

        MetricName = GetInstructionScript.EnglishM == false ? "Color Incorrecto " : "Incorrect Color ";
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    public void ColorIsIncorrect(string RaycastColor, string RaycastColor2, string RaycastObject){

        if (RaycastObject.Length > 1){

            if (GetInstructionScript.Level < 4){

                if (RaycastColor != GetInstructionScript.ColorValue){

                    CountMetricPerInstruction();
                }
            }

            if (GetInstructionScript.Level >= 4){

                /* Aqui checo que las 4 posibles combinaciones de colores correctos: 
                 * Color 1 != inColor1 && Color 2 != inColor2
                 * Color 1 != inColor2 && Color 2 != inColor1
                 * Color 1 != in2Color1 && Color 2 != in2Color2
                 * Color 1 != in2Color2 && Color 2 != in2Color1 */

                if (RaycastColor != GetInstructionScript.ColorValue && RaycastColor2 != GetInstructionScript.ColorValue2){

                    if (RaycastColor != GetInstructionScript.ColorValueIn2 && RaycastColor2 != GetInstructionScript.ColorValue2In2){

                        if (RaycastColor != GetInstructionScript.ColorValue2 && RaycastColor2 != GetInstructionScript.ColorValue){

                            if (RaycastColor != GetInstructionScript.ColorValue2In2 && RaycastColor2 != GetInstructionScript.ColorValueIn2){

                                CountMetricPerInstruction();
                            }
                        }
                    }
                }
            }
        }    
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función cuenta cuantas veces el niño cumplió las condiciones de esta metrica. 
    void CountMetricPerInstruction(){

        MetricValuePerInstruction++;
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función procesa la información de cada ronda de instrucciones.
    public void CopyCountMetricPerInstruction(){

        // Aqui voy guardando la suma final de todas las veces que se selecciono un objeto correcto.
        FinalMetricValue += MetricValuePerInstruction;
        // Aqui Genero el string de la metrica que se va a mandar escribir en el documento de texto.
        MetricScript.CopyPerInstructionMetrics(MetricName + " = ", MetricValuePerInstruction, "\n\n");
        MetricValuePerInstruction = 0;
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función lo que hace es crear la ultima entrada de metricas para el archivo de texto con los resultados finales.
    public void EndGameMetricValues(){

        if (GetInstructionScript.EnglishM == false){
            MetricScript.FinalLogWrite("Suma final " + MetricName + " = ", FinalMetricValue, "\n\n");
        }
        else{
            MetricScript.FinalLogWrite("Final Result " + MetricName + " = ", FinalMetricValue, "\n\n");
        }
    }
}