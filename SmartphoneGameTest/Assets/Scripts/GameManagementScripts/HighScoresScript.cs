using System;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresScript : MonoBehaviour
{

    // Public Variables

    public int dificultyLevel = -4;

    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Valores Canvas y demas UI elements 

    public Text HS1_text;
    public Text HS2_text;
    public Text HS3_text;
    public Text HS4_text;
    public Text HS5_text;

    //public Text StageNumber;

    public Slider HS1_Slider;
    public Slider HS2_Slider;
    public Slider HS3_Slider;
    public Slider HS4_Slider;
    public Slider HS5_Slider;

    //*  ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________



    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Valores HighScore

    float HighScore1;
    float HighScore2;
    float HighScore3;
    float HighScore4;
    float HighScore5;

    Double roundDecimals1;
    Double roundDecimals2;
    Double roundDecimals3;
    Double roundDecimals4;
    Double roundDecimals5;

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Valores Llaves 

    string HighScoreKey1;
    string HighScoreKey2;
    string HighScoreKey3;
    string HighScoreKey4;
    string HighScoreKey5;
    string difficultyKey;
    string dateTimeSaved;

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // private Variables

    float currentGameScore;
    DateTime currentTime;
    TimeSpan difference;

    float ScorePenalty;
    int pairWeeks;
    float days;

    // Variables de las metricas

    private float TopInattention;
    private float TopImpulsiveness;
    private float TopHyperactivity;

    private float StoredInattention;
    private float StoredImpulsiveness;
    private float StoredHyperactivity;

    private float CurrentGameInattention;
    private float CurrentGameImpulsiveness;
    private float CurrentGameHyperactivity;

    private float InattentionPercentage = 0.7f;
    private float ImpulsivenessPercentage = 0.2f;
    private float HyperactivityPercentage = 0.1f;


    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Script References

    private PercentagesScript percentages;
    private CopyHighScoresUI[] copyHighScores;


    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Functions 
    // En esta función de Start le doy su valor a todas las "keys" que voy a usar para guardar los valores en los playerPrefs. 

    void Start()
    {
        ScorePenalty = 2; // cuantos puntos va a perder el niño por cada 3er dia que no juegue (este numero se multiplica por cada 3er que pase sin jugar).

        // las llaves ya con su valor
        dateTimeSaved = "LastDay";
        difficultyKey = "key";
        HighScoreKey1 = "key1";
        HighScoreKey2 = "key2";
        HighScoreKey3 = "key3";
        HighScoreKey4 = "key4";
        HighScoreKey5 = "key5";

        // al principio de cada juego tengo que saber cuales son los scores guardados para luego poder compararlos conforme sea necesario

        PlayerPrefs.GetFloat(HighScoreKey1, 0);
        PlayerPrefs.GetFloat(HighScoreKey2, 0);
        PlayerPrefs.GetFloat(HighScoreKey3, 0);
        PlayerPrefs.GetFloat(HighScoreKey4, 0);
        PlayerPrefs.GetFloat(HighScoreKey5, 0);

        // aqui apago todos los elementos del canvas para que solo salgan al final del juego

        HS1_Slider.gameObject.SetActive(false);
        HS2_Slider.gameObject.SetActive(false);
        HS3_Slider.gameObject.SetActive(false);
        HS4_Slider.gameObject.SetActive(false);
        HS5_Slider.gameObject.SetActive(false);

        HS1_text.gameObject.SetActive(false);
        HS2_text.gameObject.SetActive(false);
        HS3_text.gameObject.SetActive(false);
        HS4_text.gameObject.SetActive(false);
        HS5_text.gameObject.SetActive(false);

        dificultyLevel = PlayerPrefs.GetInt(difficultyKey, dificultyLevel);

        copyHighScores = FindObjectsOfType<CopyHighScoresUI>();
        percentages = FindObjectOfType<PercentagesScript>();

    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // funcion creada para las pruebas, si no la necesito despues; borrar

    public void CreateScore()
    {
        percentages.FinalScore();
        currentGameScore = percentages.finalScore;
    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Aqui checo si el score que sacarón al final del juego es mayor a alguno de los ultimos 5 "HighScores" que hayan tenido.

    public void UpdateScore()
    {
        CreateScore();
       
        // cada "if"  checa si el score del juego actual es mayor al valor que ya tienen guardado. Checo primero el que seria el score más alto, para poder ir "recorriendo" a modo de cascada
        // todos los scores, ya que si hiciste un score más alto que el primer lugar, este ahora debe de ser el 2do lugar, el 2do lugar el 3ro etc... y asi sucesivamente.
        HighScore5 = PlayerPrefs.GetFloat(HighScoreKey5, HighScore5);
        HighScore4 = PlayerPrefs.GetFloat(HighScoreKey4, HighScore4);
        HighScore3 = PlayerPrefs.GetFloat(HighScoreKey3, HighScore3);
        HighScore2 = PlayerPrefs.GetFloat(HighScoreKey2, HighScore2);
        HighScore1 = PlayerPrefs.GetFloat(HighScoreKey1, HighScore1);


        if (currentGameScore > HighScore1)
        {
            HighScore5 = HighScore4;
            HighScore4 = HighScore3;
            HighScore3 = HighScore2;
            HighScore2 = HighScore1;
            HighScore1 = currentGameScore;
            saveScoreChanges();
        }

        if (currentGameScore >= HighScore2 && currentGameScore < PlayerPrefs.GetFloat(HighScoreKey1))
        {
            HighScore5 = HighScore4;
            HighScore4 = HighScore3;
            HighScore3 = HighScore2;
            HighScore2 = currentGameScore;
            saveScoreChanges();
        }

        if (currentGameScore > HighScore3 && currentGameScore < PlayerPrefs.GetFloat(HighScoreKey2))
        {
            HighScore5 = HighScore4;
            HighScore4 = HighScore3;
            HighScore3 = currentGameScore;
            saveScoreChanges();
        }

        if (currentGameScore > HighScore4 && currentGameScore < PlayerPrefs.GetFloat(HighScoreKey3))
        {
            HighScore5 = HighScore4;
            HighScore4 = currentGameScore;

            PlayerPrefs.SetFloat(HighScoreKey4, HighScore4);
            PlayerPrefs.SetFloat(HighScoreKey5, HighScore5);
        }

        if (currentGameScore > HighScore5 && currentGameScore < PlayerPrefs.GetFloat(HighScoreKey4))
        {
            HighScore5 = currentGameScore;
            PlayerPrefs.SetFloat(HighScoreKey5, HighScore5);
        }

        //*

        AnalyticsPercentageResults();

    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Aqui vuelvo a activar los elementos del canvas, pero si ya tienen un valor guardado que sea > 0, porque sino siento que vamos a atiborrar al niño de mucha información innecesaria.

    public void ShowScores()
    {
        UpdateScore();

        // 
        if (PlayerPrefs.GetFloat(HighScoreKey1, HighScore1) > 0)
        {
            HS1_text.gameObject.SetActive(true);
            HS1_Slider.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetFloat(HighScoreKey2, HighScore2) > 0)
        {
            HS2_text.gameObject.SetActive(true);
            HS2_Slider.gameObject.SetActive(true);

        }

        if (PlayerPrefs.GetFloat(HighScoreKey3, HighScore3) > 0)
        {
            HS3_text.gameObject.SetActive(true);
            HS3_Slider.gameObject.SetActive(true);

        }

        if (PlayerPrefs.GetFloat(HighScoreKey4, HighScore4) > 0)
        {
            HS4_text.gameObject.SetActive(true);
            HS4_Slider.gameObject.SetActive(true);

        }

        if (PlayerPrefs.GetFloat(HighScoreKey5, HighScore5) > 0)
        {
            HS5_text.gameObject.SetActive(true);
            HS5_Slider.gameObject.SetActive(true);

        }

        //*

        // ________________________________________________________________________________

        HS1_Slider.value = PlayerPrefs.GetFloat(HighScoreKey1);

        HS1_text.text = "Puntuación más alta 1 = " + (roundDecimals1 = (Math.Round(PlayerPrefs.GetFloat(HighScoreKey1), 2)));

        // ________________________________________________________________________________

        HS2_Slider.value = PlayerPrefs.GetFloat(HighScoreKey2);

        HS2_text.text = "Puntuación más alta 2  = " + (roundDecimals2 = (Math.Round(PlayerPrefs.GetFloat(HighScoreKey2), 2)));

        // ________________________________________________________________________________

        HS3_Slider.value = PlayerPrefs.GetFloat(HighScoreKey3);

        HS3_text.text = "Puntuación más alta 3  = " + (roundDecimals3 = (Math.Round(PlayerPrefs.GetFloat(HighScoreKey3), 2)));

        // ________________________________________________________________________________

        HS4_Slider.value = PlayerPrefs.GetFloat(HighScoreKey4);

        HS4_text.text = "Puntuación más alta 4  = " + (roundDecimals4 = (Math.Round(PlayerPrefs.GetFloat(HighScoreKey4), 2)));

        // ________________________________________________________________________________

        HS5_Slider.value = PlayerPrefs.GetFloat(HighScoreKey5);

        HS5_text.text = "Puntuación más alta 5  = " + (roundDecimals5 = (Math.Round(PlayerPrefs.GetFloat(HighScoreKey5), 2)));

        copyHighScores[0].CopyCanvasValues();
        copyHighScores[1].CopyCanvasValues();
        copyHighScores[2].CopyCanvasValues();

    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // En esta función restamos los puntos por no jugar minimo cada 3er día.

    void NoPlayPenalty()
    {
        if (PlayerPrefs.HasKey(dateTimeSaved))
        {

            currentTime = Convert.ToDateTime(PlayerPrefs.GetString(dateTimeSaved));

            difference = DateTime.Now - currentTime;

            if (difference.Days >= 3)
            {
                // Aqui tengo que dividir al final entre 3 porque la intención es que el niño solo pierda 2 puntos en sus HighScores por cada 3 días sin jugar. si no divido entre 3, perderia 6 puntos por cada 3 dias sin jugar.
                // & puntos por cada 3 días sin jugar podría ser una buena opción, pero no fue lo que acordamos, por lo que la división se queda.

                HighScore1 = PlayerPrefs.GetFloat(HighScoreKey1) - ((ScorePenalty * float.Parse(difference.Days.ToString())) / 3);
                HighScore2 = PlayerPrefs.GetFloat(HighScoreKey2) - ((ScorePenalty * float.Parse(difference.Days.ToString())) / 3);
                HighScore3 = PlayerPrefs.GetFloat(HighScoreKey3) - ((ScorePenalty * float.Parse(difference.Days.ToString())) / 3);
                HighScore4 = PlayerPrefs.GetFloat(HighScoreKey4) - ((ScorePenalty * float.Parse(difference.Days.ToString())) / 3);
                HighScore5 = PlayerPrefs.GetFloat(HighScoreKey5) - ((ScorePenalty * float.Parse(difference.Days.ToString())) / 3);

                saveScoreChanges();

            }

            // Esta función tiene como proposito bajar por completo un nivel al niño por cada 2 semanas que pase sin jugar.

            if (difference.Days >= 14)
            {

                if (int.Parse(difference.Days.ToString()) / 14 >= 1)
                {
                    pairWeeks = int.Parse(difference.Days.ToString()) / 14;
                }

                //*

                dificultyLevel = dificultyLevel - pairWeeks;

                if (dificultyLevel <= -4)
                {

                    dificultyLevel = -4;
                }

                HighScore1 = 0;
                HighScore2 = 0;
                HighScore3 = 0;
                HighScore4 = 0;
                HighScore5 = 0;

                saveScoreChanges();
                SaveDifficultyChanges();

            }
        }
    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // save data functions

    void saveScoreChanges()
    {
        PlayerPrefs.SetFloat(HighScoreKey1, HighScore1);
        PlayerPrefs.SetFloat(HighScoreKey2, HighScore2);
        PlayerPrefs.SetFloat(HighScoreKey3, HighScore3);
        PlayerPrefs.SetFloat(HighScoreKey4, HighScore4);
        PlayerPrefs.SetFloat(HighScoreKey5, HighScore5);
    }

    public void SaveDifficultyChanges()
    {
        PlayerPrefs.SetInt(difficultyKey, dificultyLevel);
        PlayerPrefs.Save();
    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    //* save data functions

    public void ChangeDifficultyLevel()
    {
        if (PlayerPrefs.GetFloat(HighScoreKey5) > 90 && PlayerPrefs.GetFloat(HighScoreKey4) > 90 && PlayerPrefs.GetFloat(HighScoreKey3) > 90 && PlayerPrefs.GetFloat(HighScoreKey2) > 90 && PlayerPrefs.GetFloat(HighScoreKey1) > 90 && PlayerPrefs.GetInt(difficultyKey, -4) != 0)
        {

            dificultyLevel = dificultyLevel + 1;

            PlayerPrefs.DeleteKey(HighScoreKey5);
            PlayerPrefs.DeleteKey(HighScoreKey4);
            PlayerPrefs.DeleteKey(HighScoreKey3);
            PlayerPrefs.DeleteKey(HighScoreKey2);
            PlayerPrefs.DeleteKey(HighScoreKey1);

            HighScore5 = 0;
            HighScore4 = 0;
            HighScore3 = 0;
            HighScore2 = 0;
            HighScore1 = 0;

            saveScoreChanges();
            PlayerPrefs.SetInt(difficultyKey, dificultyLevel);
            SaveDifficultyChanges();

            if (PlayerPrefs.GetInt(difficultyKey) == 0)
            {

                dificultyLevel = 0;
            }
        }
    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Internal Functions forv the values for the parents graphs and other metrics

    void AnalyticsPercentageResults()
    {

        CurrentGameInattention = (currentGameScore * InattentionPercentage) / 10;
        CurrentGameImpulsiveness = (currentGameScore * ImpulsivenessPercentage) / 10;
        CurrentGameHyperactivity = (currentGameScore * HyperactivityPercentage) / 10;

        StoredInattention = PlayerPrefs.GetFloat(KeysForAnalytic.InattentionKey,0);
        StoredImpulsiveness = PlayerPrefs.GetFloat(KeysForAnalytic.ImpulsivenessKey,0);
        StoredHyperactivity = PlayerPrefs.GetFloat(KeysForAnalytic.HyperactivityKey,0);

        StoredInattention += CurrentGameInattention;
        StoredImpulsiveness += CurrentGameImpulsiveness;
        StoredHyperactivity += CurrentGameHyperactivity;

        EndGameFunction();
    
    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    void EndGameFunction()
    {
        PlayerPrefs.SetString(dateTimeSaved, DateTime.Now.ToString());
        PlayerPrefs.SetFloat(KeysForAnalytic.InattentionKey, StoredInattention);
        PlayerPrefs.SetFloat(KeysForAnalytic.ImpulsivenessKey, StoredImpulsiveness);
        PlayerPrefs.SetFloat(KeysForAnalytic.HyperactivityKey, StoredHyperactivity);

    }
}
