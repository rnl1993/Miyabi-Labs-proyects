using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class InstantiateObjectsScript : MonoBehaviour {

    public GameObject[] SpawnPoints;

    //Private variables.

    // Esta variable va a cambiar dependiendo de en que nivel estes jugando, para ir desbloqueando más piezas.
    private int ObjectsRange;

    // En este diccionario voy a meter todos los objetos que se van a instanciar. Mi plan es instanciar objectos al azar de manera tal que haya por lo menos un objeto de cada tipo 
    private Dictionary<int, GameObject> Figures = new Dictionary<int, GameObject>();

    // Aqui voy a checar con bool que haya por lo menos un objeto de cada uno. 
    private List<bool> ObjsMaps = new List<bool>();

    // Esta lista se va a usar para crear llaves para el diccionario que va a instancear los objetos. (Una funcion completamente diferente a “MapKeys”)
    private List<int> Keys = new List<int>();

    // En este arreglo voy a meter todos los objetos que van a tener que pintar los niños.
    private GameObject[] Objs;

    // Esta variable escoje que objeto va a instanciar este spawn point. 
    private int RandomIndex;

    // Este int checa si ya se instanciaron todas las figuras disponibles.  
    private int checkObjects;

    //script Reference
    private InstructionsScript GetInstructions;

    // Functions

    void Start (){
        
        GetInstructions = FindObjectOfType<InstructionsScript>();

        if (GetInstructions != null){
            // En el nivel 1, solo se utilizan 4 piezas.
            if (GetInstructions.Level < 4) { ObjectsRange = 4; }
            // En el nivel 4, agregamos una figura completamente nueva.
            if (GetInstructions.Level == 4) { ObjectsRange = 5; }
            // En el nivel 5, Agregamos una ultima figura.
            if (GetInstructions.Level == 5) { ObjectsRange = 6; }
        }

        // Aqui lleno un arreglo de objetos con los objetos que van a tener que pintar los niños
        Objs = Resources.LoadAll("ColorFiguresExercise").Cast<GameObject>().ToArray();

        // 
        for (int i = 0; i < Objs.Length; i++){
            
            Keys.Add(i); 
            Figures.Add(Keys[i], Objs[i]);
        }

        for (int i = 0; i < ObjectsRange; i++){

            ObjsMaps.Add(false);
        }

        for (int i = 0; i < SpawnPoints.Length; i++){

            RandomNumber();
            // Aqui instancio las figuras que se van a pintar, al igual que las emparento a la posicion donde salieron, para poder cambiarlas de lugar
            // ya que lo que muevo con el script “RearrangeObjectScript” son los puntos de instanciadores.
            Instantiate(Figures[RandomIndex], SpawnPoints[i].transform.position, Quaternion.identity).transform.SetParent(SpawnPoints[i].transform);

        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    void RandomNumber(){
        
        // Aqui genero numeros al azar.
        RandomIndex = Random.Range(0, ObjectsRange);

        // Aqui checo que no haya usado el resultado del numero al azar antes.
        if (ObjsMaps[RandomIndex] == false){
            
            // Si el resultado al azar que salió no lo habia usado antes, es un resultado valido y dejo que se instancie ese objeto.
            ObjsMaps[RandomIndex] = true;

            // Aqui llevo la cuenta de cuantos objetos diferentes ya se instanciarón.
            checkObjects++;

            // Cuando el numero de objetos diferentes que ya se instanciaron es igual al numero de posibles objetos para instanciar.
            if(checkObjects == ObjectsRange){
                // Reseteo la cuenta de objetos que ya se instanciarón, para que puedan volver a ser instanciados.
                checkObjects = 0;

                // Aqui reseteo la lista de booleanos para liberar las opciones de los objetos que se pueden instanciar.
                for (int i = 0; i < ObjsMaps.Count; i++){

                    ObjsMaps[i] = false;
                }
            }
        }
        // Si el numero al azar que salió es repetido, vuelvo a llamar la función para escojer otro numero.
        else { RandomNumber(); }
    }
}