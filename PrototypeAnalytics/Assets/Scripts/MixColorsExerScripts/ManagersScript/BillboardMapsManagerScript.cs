using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BillboardMapsManagerScript : MonoBehaviour{

    public bool EscribirRonda;

    /* En este diccionario voy a meter todas las opciones que pueden salir en las instrucciones para combinación de objetos.
       Es decir, por ejemplo: Cubos; Grandes; Pequeños; Izquierdo; Derecho. */
    private Dictionary<string, List<string>> InstructionsMap = new Dictionary<string, List<string>>();

    /* Este es el “Hashtable” es similar a un diccionario, pero es más flexible a la hora de 
       decidir que tipo de elementos puede haber adentro de él. */
    private Hashtable Map = new Hashtable();

    // aqui voy acomodar las llaves del "Map" para poderlas meter al diccionario de una manera mas "streamline"
    private List<string> Mapkeys = new List<string>();

    /* Este int va a ser un contador para que si después de cierto limite, la instrucción 2 no encuentra una combinación aceptable, 
       voy a utilizar el mapa de instrucciones para checar todas las instrucciones al mismo tiempo (es una especie de ayuda). Si eso
       tampoco funciona, voy a apagar la segunda instrucción, porque significa que solo queda un tipo de figura disponible con solo
       un tipo de tamaño, así que tener 2 veces la misma instrucción seria redundante. */
    private int SecondInstructionsTries;

    /*Este bool es para asegurarme que los “for” que buscan combinaciones validas para las instrucciones 
      se detengan en cuanto encuentre la primer combinación valida, que no continuen con el resto de “for”*/
    private bool ManualSearchFirstInstructionFound;
    // Lo mismo que el bool de arriba pero para la segunda instrucción.
    private bool ManualSearchSecondInstructionFound;

    // Script References
    private RaycastPalleteScript RaycastPallete;
    private InstructionsScript GetInstructions;
    private ObjToPaint[] GetObjectsToPaints;

    // Use this for initialization
    void Start(){

        RaycastPallete = FindObjectOfType<RaycastPalleteScript>();
        GetInstructions = FindObjectOfType<InstructionsScript>();
        StartCoroutine(LateStart());
        EscribirRonda = true;

    }

    IEnumerator LateStart(){

        yield return new WaitForSeconds(0.2f);

        // Aqui busco todos los objetos que se pueden pintar.
        GetObjectsToPaints = FindObjectsOfType<ObjToPaint>();

        if (GetInstructions.EnglishM == false){

            // Aquí creo las entradas del Hashtables que voy a utilizar para comparar las instrucciones con los objetos todavía disponibles. 
            if (GetInstructions.Level < 3){

                // En los primeros 2 niveles, como solo checamos por figuras, los objetos del nivel pueden accederse directamente. 
                Map.Add("Pinguinos", 0);
                Map.Add("Pelotas", 0);
                Map.Add("Arboles", 0);
                Map.Add("Peces", 0);
            }

            // Aquí voy a empezar a checar la parte de los tamaños de las figuras.
            if (GetInstructions.Level == 3 || GetInstructions.Level == 4){

                // Aquí creo las entradas del primer Hashtable, donde diferencio entre todas las figuras.
                Map.Add("Pinguinos", new Hashtable());
                Map.Add("Pelotas", new Hashtable());
                Map.Add("Arboles", new Hashtable());
                Map.Add("Peces", new Hashtable());

                if (GetInstructions.Level == 4){

                    Map.Add("Estrellas", new Hashtable());
                }

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para los Cubos. 
                (Map["Pinguinos"] as Hashtable).Add("Grandes", 0);
                (Map["Pinguinos"] as Hashtable).Add("Pequeños", 0);

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para las Pelotas.
                (Map["Pelotas"] as Hashtable).Add("Grandes", 0);
                (Map["Pelotas"] as Hashtable).Add("Pequeños", 0);

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para las Cápsulas.
                (Map["Arboles"] as Hashtable).Add("Grandes", 0);
                (Map["Arboles"] as Hashtable).Add("Pequeños", 0);

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para las Cilindros.
                (Map["Peces"] as Hashtable).Add("Grandes", 0);
                (Map["Peces"] as Hashtable).Add("Pequeños", 0);

                if (GetInstructions.Level == 4){

                    (Map["Estrellas"] as Hashtable).Add("Grandes", 0);
                    (Map["Estrellas"] as Hashtable).Add("Pequeños", 0);
                }
            }

            // Aquí voy a separar los objetos no solo entre tamaños, sino tambien entre sus posiciones (Izquierda; Derecha).
            if (GetInstructions.Level == 5){

                // Aquí creo las entradas del primer Hashtable, donde diferencio entre todas las figuras.
                Map.Add("Pinguinos", new Hashtable());
                Map.Add("Pelotas", new Hashtable());
                Map.Add("Arboles", new Hashtable());
                Map.Add("Peces", new Hashtable());

                Map.Add("Estrellas", new Hashtable());
                Map.Add("Cohetes", new Hashtable());

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para los Cubos. 
                (Map["Pinguinos"] as Hashtable).Add("Grandes", new Hashtable());
                (Map["Pinguinos"] as Hashtable).Add("Pequeños", new Hashtable());

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para las Pelotas.
                (Map["Pelotas"] as Hashtable).Add("Grandes", new Hashtable());
                (Map["Pelotas"] as Hashtable).Add("Pequeños", new Hashtable());

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para las Cápsulas.
                (Map["Arboles"] as Hashtable).Add("Grandes", new Hashtable());
                (Map["Arboles"] as Hashtable).Add("Pequeños", new Hashtable());

                // Aquí creo los nuevos “Hashtables” para distinguir el "camino" para saber si quedan objetos grandes o pequeños.
                (Map["Peces"] as Hashtable).Add("Grandes", new Hashtable());
                (Map["Peces"] as Hashtable).Add("Pequeños", new Hashtable());

                (Map["Estrellas"] as Hashtable).Add("Grandes", new Hashtable());
                (Map["Estrellas"] as Hashtable).Add("Pequeños", new Hashtable());

                (Map["Cohetes"] as Hashtable).Add("Grandes", new Hashtable());
                (Map["Cohetes"] as Hashtable).Add("Pequeños", new Hashtable());

                // Aquí voy metiendo los cubos a la izquierda o derecha conforme sea el caso. 
                ((Map["Pinguinos"] as Hashtable)["Grandes"] as Hashtable).Add("Derecho", 0);
                ((Map["Pinguinos"] as Hashtable)["Grandes"] as Hashtable).Add("Izquierdo", 0);

                ((Map["Pinguinos"] as Hashtable)["Pequeños"] as Hashtable).Add("Derecho", 0);
                ((Map["Pinguinos"] as Hashtable)["Pequeños"] as Hashtable).Add("Izquierdo", 0);

                // Aquí voy metiendo las Pelotas a la izquierda o derecha conforme sea el caso.
                ((Map["Pelotas"] as Hashtable)["Grandes"] as Hashtable).Add("Derecho", 0);
                ((Map["Pelotas"] as Hashtable)["Grandes"] as Hashtable).Add("Izquierdo", 0);

                ((Map["Pelotas"] as Hashtable)["Pequeños"] as Hashtable).Add("Derecho", 0);
                ((Map["Pelotas"] as Hashtable)["Pequeños"] as Hashtable).Add("Izquierdo", 0);

                // Aquí voy metiendo las Capsulas a la izquierda o derecha conforme sea el caso.
                ((Map["Arboles"] as Hashtable)["Grandes"] as Hashtable).Add("Derecho", 0);
                ((Map["Arboles"] as Hashtable)["Grandes"] as Hashtable).Add("Izquierdo", 0);

                ((Map["Arboles"] as Hashtable)["Pequeños"] as Hashtable).Add("Derecho", 0);
                ((Map["Arboles"] as Hashtable)["Pequeños"] as Hashtable).Add("Izquierdo", 0);

                // Aquí voy metiendo los Cilindros a la izquierda o derecha conforme sea el caso.
                ((Map["Peces"] as Hashtable)["Grandes"] as Hashtable).Add("Derecho", 0);
                ((Map["Peces"] as Hashtable)["Grandes"] as Hashtable).Add("Izquierdo", 0);


                ((Map["Peces"] as Hashtable)["Pequeños"] as Hashtable).Add("Derecho", 0);
                ((Map["Peces"] as Hashtable)["Pequeños"] as Hashtable).Add("Izquierdo", 0);

                ((Map["Estrellas"] as Hashtable)["Grandes"] as Hashtable).Add("Derecho", 0);
                ((Map["Estrellas"] as Hashtable)["Grandes"] as Hashtable).Add("Izquierdo", 0);

                ((Map["Estrellas"] as Hashtable)["Pequeños"] as Hashtable).Add("Derecho", 0);
                ((Map["Estrellas"] as Hashtable)["Pequeños"] as Hashtable).Add("Izquierdo", 0);

                ((Map["Cohetes"] as Hashtable)["Grandes"] as Hashtable).Add("Derecho", 0);
                ((Map["Cohetes"] as Hashtable)["Grandes"] as Hashtable).Add("Izquierdo", 0);

                ((Map["Cohetes"] as Hashtable)["Pequeños"] as Hashtable).Add("Derecho", 0);
                ((Map["Cohetes"] as Hashtable)["Pequeños"] as Hashtable).Add("Izquierdo", 0);
            }
        }

        if (GetInstructions.EnglishM == true){

            // Aquí creo las entradas del Hashtables que voy a utilizar para comparar las instrucciones con los objetos todavía disponibles. 
            if (GetInstructions.Level < 3){

                // En los primeros 2 niveles, como solo checamos por figuras, los objetos del nivel pueden accederse directamente. 
                Map.Add("Pinguins", 0);
                Map.Add("Balls", 0);
                Map.Add("Trees", 0);
                Map.Add("Fishes", 0);
            }

            // Aquí voy a empezar a checar la parte de los tamaños de las figuras.
            if (GetInstructions.Level == 3 || GetInstructions.Level == 4){

                // Aquí creo las entradas del primer Hashtable, donde diferencio entre todas las figuras.
                Map.Add("Pinguins", new Hashtable());
                Map.Add("Balls", new Hashtable());
                Map.Add("Trees", new Hashtable());
                Map.Add("Fishes", new Hashtable());

                if (GetInstructions.Level == 4){

                    Map.Add("Stars", new Hashtable());
                }

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para los Cubos. 
                (Map["Pinguins"] as Hashtable).Add("(Big)", 0);
                (Map["Pinguins"] as Hashtable).Add("(Small)", 0);

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para las Pelotas.
                (Map["Balls"] as Hashtable).Add("(Big)", 0);
                (Map["Balls"] as Hashtable).Add("(Small)", 0);

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para las Cápsulas.
                (Map["Trees"] as Hashtable).Add("(Big)", 0);
                (Map["Trees"] as Hashtable).Add("(Small)", 0);

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para las Cilindros.
                (Map["Fishes"] as Hashtable).Add("(Big)", 0);
                (Map["Fishes"] as Hashtable).Add("(Small)", 0);

                if (GetInstructions.Level == 4){

                    (Map["Stars"] as Hashtable).Add("(Big)", 0);
                    (Map["Stars"] as Hashtable).Add("(Small)", 0);
                }
            }

            // Aquí voy a separar los objetos no solo entre tamaños, sino tambien entre sus posiciones (Izquierda; Derecha).
            if (GetInstructions.Level == 5){

                // Aquí creo las entradas del primer Hashtable, donde diferencio entre todas las figuras.
                Map.Add("Pinguins", new Hashtable());
                Map.Add("Balls", new Hashtable());
                Map.Add("Trees", new Hashtable());
                Map.Add("Fishes", new Hashtable());

                Map.Add("Stars", new Hashtable());
                Map.Add("Rockets", new Hashtable());

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para los Cubos. 
                (Map["Pinguins"] as Hashtable).Add("(Big)", new Hashtable());
                (Map["Pinguins"] as Hashtable).Add("(Small)", new Hashtable());

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para las Pelotas.
                (Map["Balls"] as Hashtable).Add("(Big)", new Hashtable());
                (Map["Balls"] as Hashtable).Add("(Small)", new Hashtable());

                // Aquí creo los nuevos “Hashtables” para distinguir entre las figuras grandes y pequeñas para las Cápsulas.
                (Map["Trees"] as Hashtable).Add("(Big)", new Hashtable());
                (Map["Trees"] as Hashtable).Add("(Small)", new Hashtable());

                // Aquí creo los nuevos “Hashtables” para distinguir el "camino" para saber si quedan objetos grandes o pequeños.
                (Map["Fishes"] as Hashtable).Add("(Big)", new Hashtable());
                (Map["Fishes"] as Hashtable).Add("(Small)", new Hashtable());

                (Map["Stars"] as Hashtable).Add("(Big)", new Hashtable());
                (Map["Stars"] as Hashtable).Add("(Small)", new Hashtable());

                (Map["Rockets"] as Hashtable).Add("(Big)", new Hashtable());
                (Map["Rockets"] as Hashtable).Add("(Small)", new Hashtable());

                // Aquí voy metiendo los cubos a la izquierda o derecha conforme sea el caso. 
                ((Map["Pinguins"] as Hashtable)["(Big)"] as Hashtable).Add("Right", 0);
                ((Map["Pinguins"] as Hashtable)["(Big)"] as Hashtable).Add("Left", 0);

                ((Map["Pinguins"] as Hashtable)["(Small)"] as Hashtable).Add("Right", 0);
                ((Map["Pinguins"] as Hashtable)["(Small)"] as Hashtable).Add("Left", 0);

                // Aquí voy metiendo las Pelotas a la izquierda o derecha conforme sea el caso.
                ((Map["Balls"] as Hashtable)["(Big)"] as Hashtable).Add("Right", 0);
                ((Map["Balls"] as Hashtable)["(Big)"] as Hashtable).Add("Left", 0);

                ((Map["Balls"] as Hashtable)["(Small)"] as Hashtable).Add("Right", 0);
                ((Map["Balls"] as Hashtable)["(Small)"] as Hashtable).Add("Left", 0);

                // Aquí voy metiendo las Capsulas a la izquierda o derecha conforme sea el caso.
                ((Map["Trees"] as Hashtable)["(Big)"] as Hashtable).Add("Right", 0);
                ((Map["Trees"] as Hashtable)["(Big)"] as Hashtable).Add("Left", 0);

                ((Map["Trees"] as Hashtable)["(Small)"] as Hashtable).Add("Right", 0);
                ((Map["Trees"] as Hashtable)["(Small)"] as Hashtable).Add("Left", 0);

                // Aquí voy metiendo los Cilindros a la izquierda o derecha conforme sea el caso.
                ((Map["Fishes"] as Hashtable)["(Big)"] as Hashtable).Add("Right", 0);
                ((Map["Fishes"] as Hashtable)["(Big)"] as Hashtable).Add("Left", 0);

                ((Map["Fishes"] as Hashtable)["(Small)"] as Hashtable).Add("Right", 0);
                ((Map["Fishes"] as Hashtable)["(Small)"] as Hashtable).Add("Left", 0);

                ((Map["Stars"] as Hashtable)["(Big)"] as Hashtable).Add("Right", 0);
                ((Map["Stars"] as Hashtable)["(Big)"] as Hashtable).Add("Left", 0);

                ((Map["Stars"] as Hashtable)["(Small)"] as Hashtable).Add("Right", 0);
                ((Map["Stars"] as Hashtable)["(Small)"] as Hashtable).Add("Left", 0);

                ((Map["Rockets"] as Hashtable)["(Big)"] as Hashtable).Add("Right", 0);
                ((Map["Rockets"] as Hashtable)["(Big)"] as Hashtable).Add("Left", 0);

                ((Map["Rockets"] as Hashtable)["(Small)"] as Hashtable).Add("Right", 0);
                ((Map["Rockets"] as Hashtable)["(Small)"] as Hashtable).Add("Left", 0);
            }
        }

        /* Aquí es donde copio las llaves del “Map” a una lista para poder llenar más facilmente  */
        Mapkeys = Map.Keys.OfType<string>().ToList();

        yield return new WaitForSeconds(0.1f);
        // Esta función es la que rellena el “Map” cuando se acabo de pintar el billboard y al principio del juego.
        SizeManager();
        // Esta función rellena el diccionario para buscar manualmente una opción valida para la segunda instruccion. 
        FillInstructionsMap();
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    public void SizeManager(){

        for (int i = 0; i < GetObjectsToPaints.Length; i++){

            // Aquí lleno el mapa de únicamente los tipos de objetos.
            if (GetInstructions.Level < 3){

                Map[GetObjectsToPaints[i].ObjectName] = (int)(Map[GetObjectsToPaints[i].ObjectName]) + 1;
            }

            // Aquí ya lleno el mapa con objetos de diferentes tamaños.
            if (GetInstructions.Level == 3 || GetInstructions.Level == 4){

                ((Map[GetObjectsToPaints[i].ObjectName] as Hashtable)[GetObjectsToPaints[i].ObjectSize]) = (int)(((Map[GetObjectsToPaints[i].ObjectName] as Hashtable)[GetObjectsToPaints[i].ObjectSize])) + 1;
            }
            // Aquí los objetos ya se separan por lados, al igual que por tamaños.
            if (GetInstructions.Level == 5){

                ((Map[GetObjectsToPaints[i].ObjectName] as Hashtable)[GetObjectsToPaints[i].ObjectSize] as Hashtable)[GetObjectsToPaints[i].ObjectPositions]
                = (int)(((Map[GetObjectsToPaints[i].ObjectName] as Hashtable)[GetObjectsToPaints[i].ObjectSize] as Hashtable)[GetObjectsToPaints[i].ObjectPositions]) + 1;
            }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Aqui lleno el diccionario que voy a utilizar para manualmente buscar una combinación valida para la segunda instrucción.
    void FillInstructionsMap(){

        if (GetInstructions.Level > 3){

            for (int i = 0; i < Mapkeys.Count; i++){
                InstructionsMap.Add(Mapkeys[i] as string, new List<string>());
            }

            if (GetInstructions.EnglishM == false){

                for (int i = 0; i < InstructionsMap.Count; i++){

                    InstructionsMap[Mapkeys[i]].Add("Grandes,Izquierdo");
                    InstructionsMap[Mapkeys[i]].Add("Pequeños,Izquierdo");
                    InstructionsMap[Mapkeys[i]].Add("Grandes,Derecho");
                    InstructionsMap[Mapkeys[i]].Add("Pequeños,Derecho");
                }

            }

            if (GetInstructions.EnglishM == true){
                for (int i = 0; i < InstructionsMap.Count; i++){

                    InstructionsMap[Mapkeys[i]].Add("(Big),Left");
                    InstructionsMap[Mapkeys[i]].Add("(Small),Left");
                    InstructionsMap[Mapkeys[i]].Add("(Big),Right");
                    InstructionsMap[Mapkeys[i]].Add("(Small),Right");
                }
            }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // La intención de esta función es checar que elementos de cada tipo ya han sido pintados.  
    public void CheckHashtableList(){

        ManualSearchFirstInstructionFound = false;
        ManualSearchSecondInstructionFound = false;

        // En este “if” solo checamos figura.
        if (GetInstructions.Level < 3){

            if ((int)(Map[GetInstructions.ObjectValue]) <= 0){

                EscribirRonda = false;
            }
        }

        // En este “if” estamos checando tamaño y figura. Cuando estemos en el nivel 4, esta parte solo checa por la instrucción 1.  
        if (GetInstructions.Level == 3 || GetInstructions.Level == 4){

            if ((int)(Map[GetInstructions.ObjectValue] as Hashtable)[GetInstructions.ObjectSize] <= 0){

                EscribirRonda = false;
            }
        }

        // En este “if” Checamos objeto, tamaño y posición. Nuevamente solo estamos checando para la primera instrucción dentro de este “if”. 
        if (GetInstructions.Level == 5){

            if ((int)((Map[GetInstructions.ObjectValue] as Hashtable)[GetInstructions.ObjectSize] as Hashtable)[GetInstructions.ObjectSide] <= 0){

                EscribirRonda = false;
            }
        }

        //
        if (GetInstructions.Level == 4 && GetInstructions.SecondInstructionOff == false){

            if ((int)(Map[GetInstructions.ObjectValue2] as Hashtable)[GetInstructions.ObjectSize2] <= 0){

                // La intención de esta función es verificar que aun quedan opciones validas para la segunda instrucción.
                ManualSearchSecondInstructions();

                if (GetInstructions.SecondInstructionOff == false){

                    // Este “int” se suma porque cuando “SecondInstructionsTries” llegue a cierto valor, la función de “ManualSearchSecondInstructions()” va a entrar en efecto.
                    SecondInstructionsTries++;

                    EscribirRonda = false;
                }
            }
            if (EscribirRonda == true){
                SecondInstructionsTries = 0;
            }
        }

        // Este es el if de la instrucción 2. Aquí checo que la instrucción que se vaya a escribir todavía sea valida y que no sea la misma a la instrucción 1. 
        // Como en la instrucción 4 todavía no mandamos la instrucción referente a los lados, tengo que separar el checar las instrucciones del 4 y las del nivel 5
        if (GetInstructions.Level == 5 && GetInstructions.SecondInstructionOff == false){

            if ((int)((Map[GetInstructions.ObjectValue2] as Hashtable)[GetInstructions.ObjectSize2] as Hashtable)[GetInstructions.ObjectSide2] <= 0){

                // La intención de esta función es verificar que aun quedan opciones validas para la segunda instrucción.
                ManualSearchSecondInstructions();

                if (GetInstructions.SecondInstructionOff == false){

                    // Este “int” se suma porque cuando “SecondInstructionsTries” llegue a cierto valor, la función de “ManualSearchSecondInstructions()” va a entrar en efecto.
                    SecondInstructionsTries++;

                    EscribirRonda = false;
                }
            }

            if (EscribirRonda == true){
                SecondInstructionsTries = 0;
            }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función ayuda a encontrar alguna combinación valida para la segunda instrucción.
    void ManualSearchSecondInstructions(){

        if (SecondInstructionsTries >= 32){

            if (GetInstructions.Level == 4){

                // aqui voy a buscar manualmente una combinación valida para la instrucción 1.
                for (int i = 0; i < InstructionsMap.Count; i++){

                    for (int j = 0; j < InstructionsMap[Mapkeys[i]].Count; j++){

                        if ((int)((Map[Mapkeys[i]] as Hashtable)[InstructionsMap[Mapkeys[i]][j].Split(',')[0]]) >= 1){

                            GetInstructions.ObjectValue = Mapkeys[i];
                            GetInstructions.ObjectSize = InstructionsMap[Mapkeys[i]][j].Split(',')[0];
                            ManualSearchFirstInstructionFound = true;
                            EscribirRonda = true;
                            break;
                        }
                    }
                    // Este break es para terminar el “for” principal, ya que el break de arriba solo interrumpe el for interior.
                    if (ManualSearchFirstInstructionFound == true){
                        break;
                    }
                }

                // __________________________________________________________________________________________________________________________

                /* Aqui hago la misma busqueda de una combinación valida para la instrucción 2, pero empezando por el final del diccionario de instrucciones.
                   De esta manera, la unica ocación en la que las instrucciones van a ser identicas es cuando solo queda una opción disponible.*/
                for (int i = InstructionsMap.Count - 1; i >= 0; i--){

                    for (int j = InstructionsMap[Mapkeys[i]].Count - 1; j >= 0; j--){

                        if ((int)((Map[Mapkeys[i]] as Hashtable)[InstructionsMap[Mapkeys[i]][j].Split(',')[0]]) >= 1){

                            GetInstructions.ObjectValue2 = Mapkeys[i];
                            GetInstructions.ObjectSize2 = InstructionsMap[Mapkeys[i]][j].Split(',')[0];
                            ManualSearchSecondInstructionFound = true;
                            SecondInstructionsTries = 0;
                            break;
                        }

                    }
                    // Este break es para terminar el “for” principal, ya que el break de arriba solo interrumpe el for interior.
                    if (ManualSearchSecondInstructionFound == true){
                        break;
                    }
                }

                // Aqui voy a determinar si las 2 instrucciones son iguales o no, para apagar la segunda instrucción en caso de que sean identicas.
                if ((GetInstructions.ObjectValue2 == GetInstructions.ObjectValue) && (GetInstructions.ObjectSize2 == GetInstructions.ObjectSize)){
                    SecondInstructionsTries = 1;
                }
                //
                if (SecondInstructionsTries != 0){
                    GetInstructions.SecondInstructionOff = true;
                    SecondInstructionsTries = 0;
                    EscribirRonda = true;
                }
            }

            if (GetInstructions.Level == 5){

                // aqui voy a buscar manualmente una combinación valida para la instrucción 1.
                for (int i = InstructionsMap.Count - 1; i >= 0; i--){

                    for (int j = InstructionsMap[Mapkeys[i]].Count - 1; j >= 0; j--){

                        if ((int)((Map[Mapkeys[i]] as Hashtable)[InstructionsMap[Mapkeys[i]][j].Split(',')[0]] as Hashtable)[InstructionsMap[Mapkeys[i]][j].Split(',')[1]] >= 1){

                            GetInstructions.ObjectValue = Mapkeys[i];
                            GetInstructions.ObjectSize = InstructionsMap[Mapkeys[i]][j].Split(',')[0];
                            GetInstructions.ObjectSide = InstructionsMap[Mapkeys[i]][j].Split(',')[1];
                            ManualSearchFirstInstructionFound = true;
                            break;
                        }
                    }
                    if (ManualSearchFirstInstructionFound == true)
                        break;
                }

                /* Aqui hago la misma busqueda de una combinación valida para la instrucción 2, pero empezando por el final del diccionario de instrucciones.
                   De esta manera, la unica ocación en la que las instrucciones van a ser identicas es cuando solo queda una opción disponible.*/
                for (int i = 0; i < InstructionsMap.Count; i++){

                    for (int j = 0; j < InstructionsMap[Mapkeys[i]].Count; j++){

                        if ((int)((Map[Mapkeys[i]] as Hashtable)[InstructionsMap[Mapkeys[i]][j].Split(',')[0]] as Hashtable)[InstructionsMap[Mapkeys[i]][j].Split(',')[1]] >= 1){

                            GetInstructions.ObjectValue2 = Mapkeys[i];
                            GetInstructions.ObjectSize2 = InstructionsMap[Mapkeys[i]][j].Split(',')[0];
                            GetInstructions.ObjectSide2 = InstructionsMap[Mapkeys[i]][j].Split(',')[1];
                            ManualSearchSecondInstructionFound = true;
                            SecondInstructionsTries = 0;
                            break;
                        }
                        if (ManualSearchSecondInstructionFound == true){
                            break;
                        }
                    }
                    if (ManualSearchSecondInstructionFound == true){
                        break;
                    }
                }
                // Aqui voy a determinar si las 2 instrucciones son iguales o no, para apagar la segunda instrucción en caso de que sean identicas.
                if ((GetInstructions.ObjectValue2 == GetInstructions.ObjectValue) && (GetInstructions.ObjectSize2 == GetInstructions.ObjectSize) 
                   && (GetInstructions.ObjectSide2 == GetInstructions.ObjectSide)){
                    SecondInstructionsTries = 1;
                }
                // Aqui voy a determinar si las 2 instrucciones son iguales o no, para apagar la segunda instrucción en caso de que sean identicas.
                if (SecondInstructionsTries != 0){
                    GetInstructions.SecondInstructionOff = true;
                    SecondInstructionsTries = 0;
                }
            }
        }
    }

    // _________________________________________________________________________________________________________________________________________________________________________________________

    // Esta función la uso para ir restando los objetos que se van pintando. La llamo desde el script “PaintObjects”
    public void SubstractObjFromMap(string obj, string size, string side){

        // Aquí solo hay tipos de objetos, asi que la resta es directa
        if (GetInstructions.Level < 3 && RaycastPallete.hit.collider.gameObject.GetComponent<ObjToPaint>().isCounted == false){

            Map[obj] = ((int)(Map[obj])) - 1;

            if (((int)(Map[obj])) <= 0)
            {
                Map[obj] = 0;
            }
        }
        // Aquí el mapa se divide, hay objetos “Grandes” y “Pequeños”.
        if ((GetInstructions.Level == 3 || GetInstructions.Level == 4) && RaycastPallete.hit.collider.gameObject.GetComponent<ObjToPaint>().isCounted == false){

            (Map[obj] as Hashtable)[size] = ((int)(Map[obj] as Hashtable)[size]) - 1;

            if (((int)(Map[obj] as Hashtable)[size]) <= 0){

                ((Map[obj] as Hashtable)[size]) = 0;
            }
        }
        // Aquí el mapa se divide a 4 ramas por objeto: ejemplo; “Grandes Izquierda”, “Pequeños Derecha” etc.
        if (GetInstructions.Level == 5 && RaycastPallete.hit.collider.gameObject.GetComponent<ObjToPaint>().isCounted == false){

            ((Map[obj] as Hashtable)[size] as Hashtable)[side] = ((int)((Map[obj] as Hashtable)[size] as Hashtable)[side]) - 1;

            if (((int)((Map[obj] as Hashtable)[size] as Hashtable)[side]) <= 0){

                ((Map[obj] as Hashtable)[size] as Hashtable)[side] = 0;
            }
        }
    }
}