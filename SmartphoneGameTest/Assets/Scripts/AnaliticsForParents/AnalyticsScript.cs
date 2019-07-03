using System;
using UnityEngine;
using UnityEngine.UI;

public class AnalyticsScript : MonoBehaviour
{
    // canvas variables

    public Text SessionFirstDay;

    public Text InattentionText;
    public Text ImpulsivenessText;
    public Text HyperactivityText;

    public Text infotext1;
    public Text infotext2;
    public Text infotext3;

    public Text LastSessionText;

    public Text PreviousSessionInattention;
    public Text PreviousSessionImpulsiveness;
    public Text PreviousSessionHyperactivity;

    public Slider InattentionSlider;
    public Slider ImpulsivenessSlider;
    public Slider HyperactivitySlider;

    public Button Infobutton1;
    public Button Infobutton2;
    public Button Infobutton3;

    public Button exitPanelButton;
    public Button returnButton;

    public Image InfoPanel;

    //* 

    // private Variables

    private float tempInattentionVar;
    private float tempImpulsivenessVar;
    private float tempHyperactivityVar;

    private float shownSliderValueInattention;
    private float shownSliderValueImpulsiveness;
    private float shownSliderValueHyperactivity;

    private float PreviousAnalyticValues_Inattention;
    private float PreviousAnalyticValues_Impulsiveness;
    private float PreviousAnalyticValues_Hyperactivity;

    // estas variables por el momento van a ser fijas, despues ya trabajaremos mas con ellas

    private int days; 
    private int weeks;
    private int months;

    //*

    private bool Played;
    private string checkDay;

    // Variables para redondear los valores de los analiticos

    private Double roundDecimals1;
    private Double roundDecimals2;
    private Double roundDecimals3;

    //*

    // date time Variables

    DateTime FirstDayOfPlay;

    TimeSpan EndedPeriod;

    //*

    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Functions

    void Start()
    {
        days = 4;
        weeks = 4;
        months = 3;

        checkDay = PlayerPrefs.GetString(KeysForAnalytic.FirstDayOfplay);

        Played |= checkDay.Length > 0;

        if (Played == true)
        {
            FirstDayOfPlay = Convert.ToDateTime(PlayerPrefs.GetString(KeysForAnalytic.FirstDayOfplay));
            SessionFirstDay.text = ("Inicio de sesión trimestral: " + FirstDayOfPlay.ToString());
            EndedPeriod = DateTime.Now - FirstDayOfPlay;
        }

        if(Played == false){
            
            SessionFirstDay.text = DateTime.Now.ToString() + " Sin información.";
        }
  
        HyperactivitySlider.maxValue = 7 * days * weeks * months;
        InattentionSlider.maxValue = 7 * days * weeks * months;
        ImpulsivenessSlider.maxValue = 7 * days * weeks * months;
        tempInattentionVar = PlayerPrefs.GetFloat(KeysForAnalytic.InattentionKey);
        tempImpulsivenessVar = PlayerPrefs.GetFloat(KeysForAnalytic.ImpulsivenessKey);
        tempHyperactivityVar = PlayerPrefs.GetFloat(KeysForAnalytic.HyperactivityKey);

        PreviousSessionInattention.text = "Inatención sesión anterior: " + PlayerPrefs.GetFloat(KeysForAnalytic.previousInattentionKey,0);
        PreviousSessionImpulsiveness.text = "Impulsividad sesión anterior: " + PlayerPrefs.GetFloat(KeysForAnalytic.previousImpulsivenessKey,0);
        PreviousSessionHyperactivity.text = "Hiperactividad sesión anterior: " + PlayerPrefs.GetFloat(KeysForAnalytic.previousHyperactivityKey,0);

        if (FirstDayOfPlay.ToString().Length > 0)
        {
            infoTextsOff();
            Calculations();
            OverwritePreviousData();
        }
    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    void Calculations()
    {
        
        shownSliderValueInattention = tempInattentionVar;
        shownSliderValueImpulsiveness = tempImpulsivenessVar;
        shownSliderValueHyperactivity = tempHyperactivityVar;

        InattentionSlider.value = shownSliderValueInattention;
        ImpulsivenessSlider.value = shownSliderValueImpulsiveness;
        HyperactivitySlider.value = shownSliderValueHyperactivity;

        InattentionText.text = "Inatención " + (roundDecimals1 = Math.Round(shownSliderValueInattention, 2));
        ImpulsivenessText.text = "Impulsividad " + (roundDecimals2 = Math.Round(shownSliderValueImpulsiveness, 2));
        HyperactivityText.text = "Hiperactividad " + (roundDecimals3 = Math.Round(shownSliderValueHyperactivity, 2));

    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    void TurnOffUI()
    {

        SessionFirstDay.gameObject.SetActive(false);

        InattentionSlider.gameObject.SetActive(false);
        ImpulsivenessSlider.gameObject.SetActive(false);
        HyperactivitySlider.gameObject.SetActive(false);

        InattentionText.gameObject.SetActive(false);
        ImpulsivenessText.gameObject.SetActive(false);
        HyperactivityText.gameObject.SetActive(false);

        Infobutton1.gameObject.SetActive(false);
        Infobutton2.gameObject.SetActive(false);
        Infobutton3.gameObject.SetActive(false);

        returnButton.gameObject.SetActive(false);

        LastSessionText.gameObject.SetActive(false);

        PreviousSessionInattention.gameObject.SetActive(false);
        PreviousSessionImpulsiveness.gameObject.SetActive(false);
        PreviousSessionHyperactivity.gameObject.SetActive(false);

    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    void turnOnUI(){

        SessionFirstDay.gameObject.SetActive(true);

        InattentionSlider.gameObject.SetActive(true);
        ImpulsivenessSlider.gameObject.SetActive(true);
        HyperactivitySlider.gameObject.SetActive(true);

        InattentionText.gameObject.SetActive(true);
        ImpulsivenessText.gameObject.SetActive(true);
        HyperactivityText.gameObject.SetActive(true);

        Infobutton1.gameObject.SetActive(true);
        Infobutton2.gameObject.SetActive(true);
        Infobutton3.gameObject.SetActive(true);

        LastSessionText.gameObject.SetActive(true);

        PreviousSessionInattention.gameObject.SetActive(true);
        PreviousSessionImpulsiveness.gameObject.SetActive(true);
        PreviousSessionHyperactivity.gameObject.SetActive(true);

        Calculations();

        returnButton.gameObject.SetActive(true);

    }

    void turnONPanel(){

        InfoPanel.gameObject.SetActive(true);
        exitPanelButton.gameObject.SetActive(true);

    }

    void infoTextsOff(){

        InfoPanel.gameObject.SetActive(false);
        exitPanelButton.gameObject.SetActive(false);

        infotext1.gameObject.SetActive(false);
        infotext2.gameObject.SetActive(false);
        infotext3.gameObject.SetActive(false);

    }

    public void Button1(){

        TurnOffUI();
        turnONPanel();
        infotext1.gameObject.SetActive(true);


        if (InattentionSlider.value >= 268 && InattentionSlider.value <= 336)
        {

            infotext1.text = "Rango 5. El jugador es capaz de concentrarse y enfocarse en las tareas/ejercicios realizados, los estímulos externos no impactan en su rendimiento sin embargo ocasionalmente presta atención brevemente a estos, volviendo enseguida a la actividad. Los errores en ejecución son mínimos y la finalización de las actividades se da sin ningún problema.";

        }

        else if (InattentionSlider.value >= 201 && InattentionSlider.value < 268)
        {

            infotext1.text = "Rango 4. El periodo de atención/concentración del jugador se encuentra dentro del promedio, sin embargo los estímulos externos aún impactan la atención del menor con poca frecuencia. Concluye los ejercicios y actividades aun presentando errores pero con mejores resultados.";

        }

        else if (InattentionSlider.value >= 134 && InattentionSlider.value < 201)
        {

            infotext1.text = "Rango 3. El periodo de atención/concentración del jugador ha aumentado, como consecuencia,  la frecuencia con la que comete errores por fallo en el seguimiento de instrucciones y atención a los detalles ha disminuido ,se logra la conclusión de tareas y actividades sin necesidad de hacer seguimiento del menor.Es de vital importancia en este punto mantener el juego y fomentar conductas positivas en todos los sistemas del menor (escuela, casa, social).";

        }

        else if (InattentionSlider.value >= 67 && InattentionSlider.value < 134) { 
        
            infotext1.text = "Rango 2.  El periodo de atención/concentración del jugador se ha prolongado pero aun no es significativo,  el grado de  distracción ante cualquier estímulo externo ha disminuido, sin embargo aún se distrae con facilidad, la frecuencia con la que comete errores por fallo en el seguimiento de instrucciones disminuyó, pero persiste, así como el fallo en la conclusión de tareas y actividades de la manera esperada.";
        
        }

        else if (InattentionSlider.value < 67)
        {

            infotext1.text = "Rango 1. El periodo de atención/concentración del jugador es corto y hay distracción ante cualquier estímulo externo, la frecuencia con la que comete errores por fallo en el seguimiento de instrucciones persiste, así como el fallo en la conclusión de tareas y actividades de la manera esperada.";
        }
    }

    public void Button2() { 
    
        TurnOffUI();
        turnONPanel();
        infotext2.gameObject.SetActive(true);


        if (ImpulsivenessSlider.value >= 268 && ImpulsivenessSlider.value <= 336)
        {
            
            infotext2.text = "Rango 5. El jugador autocontrola sus impulsos, la frecuencia con la que comete errores por conductas sin meditar es escasa, utiliza su conocimiento sobre el juego para obtener mejores resultados y medita antes de actuar.";

        }

        else if (ImpulsivenessSlider.value >= 201 && ImpulsivenessSlider.value < 268)
        {

            infotext2.text = "Rango 4. El jugador ha mejorado,   aún se presentan con cierta frecuencia algunas conductas impulsivas, el número de errores ha disminuido de manera considerable, su estrategia y planeación en el juego es evidente y funcional.";

        }



        else if (ImpulsivenessSlider.value >= 134 && ImpulsivenessSlider.value < 201)
        {

            infotext2.text = "Rango 3. Se presenta disminución en las conductas impulsivas pero aún tienen cierta frecuencia, la cantidad de errores cometidos también ha disminuido. Se puede apreciar que existe cierta planeación y estrategia en su interacción con el juego.";

        }

        else if (ImpulsivenessSlider.value >= 67 && ImpulsivenessSlider.value < 134)
        {

            infotext2.text = "Rango 2. Las conductas impulsivas se mantienen, sin embargo comienza a presentarse un grado de concientización acerca de las conductas y decisiones en el juego, lo cual más adelante favorecerá a el control de impulsos.";

        }

        else if (ImpulsivenessSlider.value < 67)
        {

            infotext2.text = "Rango 1. Al jugador se le dificulta el controlar sus conductas, teniendo como consecuencia la persistencia en los errores durante la realización de actividades, no suele pensar antes de actuar y carece de planeación su estrategia de juego. ";
        }


    }


    public void Button3() { 
    
        TurnOffUI();
        turnONPanel();
        infotext3.gameObject.SetActive(true);


        if (HyperactivitySlider.value >= 268 && HyperactivitySlider.value <= 336)
        {

            infotext3.text = "Rango 5: El jugador presenta un mínimo de conductas hiperactivas, es capaz de permanecer sentado o quieto mientras juega sin necesidad de que se le esté solicitando que esté quieto. Los errores relacionados con la hiperactividad se presentan un mínimo";

        }

        else if (HyperactivitySlider.value >= 201 && HyperactivitySlider.value < 268)
        {

            infotext3.text = "Rango 4: La capacidad de inhibición de conductas hiperactivas  ha disminuido, es capaz de realizar el juego sin necesidad de desplazamientos o movimientos innecesarios, aún persiste cierto grado de movimiento continuo de ciertas áreas motoras las cuales pueden provocar errores en la ejecución del juego aunque en menor medida.";

        }



        else if (HyperactivitySlider.value >= 134 && HyperactivitySlider.value < 201)
        {

            infotext3.text = "Rango 3: Las conductas hiperactivas del jugador han disminuido progresivamente, por lo que la frecuencia con la que comete errores a consecuencia de su hiperactividad han disminuido, aunque se siguen presentando.";

        }

        else if (HyperactivitySlider.value >= 67 && HyperactivitySlider.value < 134)
        {

            infotext3.text = "Rango 2: Las conductas hiperactivas se continúan manifestando en gran medida. Al jugador falla en ocasiones en permanecer quieto en un mismo lugar, por lo que se le dificulta realizar actividades que requiera un movimiento repetitivo o posición estable.  El  constante movimiento por parte del menor ha disminuido y en algunas ocasiones logra estar tranquilo ante alguna indicación, esto durante cortos periodos de tiempo.";

        }

        else if (HyperactivitySlider.value < 67)
        {

            infotext3.text = "Rango 1: El jugador aún muestra varias conductas hiperactivas. Se le dificulta permanecer quieto en un mismo lugar y suele estar en constante movimiento, comete errores debido a su constante movimiento motor.";
        }

    }

    public void exitPanel(){

        infoTextsOff();
        turnOnUI();

    }


    void OverwritePreviousData()
    {

        if (EndedPeriod.Days >= 90)
        {
            PlayerPrefs.SetString(KeysForAnalytic.FirstDayOfplay, DateTime.Now.ToString());

            PreviousAnalyticValues_Inattention = PlayerPrefs.GetFloat(KeysForAnalytic.InattentionKey);
            PlayerPrefs.SetFloat(KeysForAnalytic.previousInattentionKey, PreviousAnalyticValues_Inattention);
            PlayerPrefs.DeleteKey(KeysForAnalytic.InattentionKey);

            PreviousAnalyticValues_Impulsiveness = PlayerPrefs.GetFloat(KeysForAnalytic.ImpulsivenessKey);
            PlayerPrefs.SetFloat(KeysForAnalytic.previousImpulsivenessKey, PreviousAnalyticValues_Impulsiveness);
            PlayerPrefs.DeleteKey(KeysForAnalytic.ImpulsivenessKey);

            PreviousAnalyticValues_Hyperactivity = PlayerPrefs.GetFloat(KeysForAnalytic.HyperactivityKey);
            PlayerPrefs.SetFloat(KeysForAnalytic.previousHyperactivityKey, PreviousAnalyticValues_Hyperactivity);
            PlayerPrefs.DeleteKey(KeysForAnalytic.HyperactivityKey);
        }

    }

}
