using UnityEngine;
using System.Collections.Generic;

public class RearrangeObjectScript : MonoBehaviour { 
    // En esta lista Guardo todos los objetos para poderlos mover despues.     List<Transform> SpawPositions = new List<Transform>(); 
    // Aqui guardo las pociciones originales, para saber a donde tengo aque mover las piezas.     List<Vector3> SavePositions = new List<Vector3>(); 
    // En esta lista guardo todos los resultados de unos numeros que creo al azar para reposicionar los objetos.     List<int> AlreadyUse = new List<int>(); 
    // Este int selecciona la nueva posición de los objtos.     private int RanPos;     //Functions
  void Start () { 
        // Aqui guardo las pocisiones de los objetos para poder cambiar sus posiciones.         for (int i = 0; i < this.transform.childCount; i++){              SpawPositions.Add(this.transform.GetChild(i));              SavePositions.Add(SpawPositions[i].transform.position);          }      }

    // _________________________________________________________________________________________________________________________________________________________________________________________     public void ChangePositions(){                 for (int i = 0; i < SpawPositions.Count; i++){
             RandomNumber();
            SpawPositions[RanPos].transform.position = SavePositions[i];         }          CleanLists();     }

    //* _________________________________________________________________________________________________________________________________________________________________________________________


    // _________________________________________________________________________________________________________________________________________________________________________________________
     // Aqui genero numeros al azar, tambien checo que los numeros generados al azar no se repitan.
    // Estos numeros representan las nuevas posiciones de los objetos.     void RandomNumber(){          RanPos = Random.Range(0, SpawPositions.Count);          if (AlreadyUse.Contains(RanPos) == false)         {             AlreadyUse.Add(RanPos);         }         else { RandomNumber(); }           }

    //* _________________________________________________________________________________________________________________________________________________________________________________________ 
    // Aqui solo limpio la lista de numeros al azar para poder volverla a usar.     void CleanLists(){          AlreadyUse.Clear();     } }