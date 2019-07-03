using UnityEngine;

public class KeysGames : MonoBehaviour {

    // llaves para prender y/o apagar los juegos

    public static string KeyGamesEnable = "GamesEnabled";

    public static int GamesEnableValue;

    //*

    // llaves para controlar cada cuando vamos a cambiar el juego que puedes jugar

    public static string KeyGamesManager = "GamesManager";

    //*

    // cuanto tiempo te vamos a restar por juego

    public static int TimeToSubstract;

    //*

    // llaves para manejar cuanto tiempo te queda de juego 

    public static string KeyGameTimeRemaining = "GameTimeRemaining";

    public static int GameTimeRemainingValue;


    // llaves para controlar el tiempo que queda antes de cambiar los exercicios

    public static string KeyExercisesTimer = "ExercisesTimer";

    // llaves para los booleanos de los registros de los exercicios y los juegos (es decir se asegura que los booleanos si sean ciclicos)

    public static string CheckBscenesBool = "BscenesBool";

    public static int BscenesBoolValue;

    public static string CheckGamesBool = "GamesBool";

    public static int GamesBoolValue;

}
