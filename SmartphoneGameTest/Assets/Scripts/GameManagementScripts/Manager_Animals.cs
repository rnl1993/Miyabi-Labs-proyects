using System;
using System.Linq;
using UnityEngine;

public class Manager_Animals : MonoBehaviour {

    // Public Variables

    public GameObject[] SpawnPositios;
    public float repetitions;
    public int repeatRate;

    // private Variables

    private int randomAnimalPosition;
    private float endRepetitions;
    private int tempRandomAnimal;
    private int RandomHunger;
    private int tempRandom;
  
    private GameObject[] animals;

    // Script References

    private CheckFirstDayScript checkFirstDay;
    private HighScoresScript GetScoresScript;
    private instanceCopyStars[] copyStars;
    private PercentagesScript Percentages;
    private HungerScript[] hunger;


	// Functions

	void Start () { 
       
        endRepetitions = 0;
        copyStars = FindObjectsOfType<instanceCopyStars>();
        Percentages = FindObjectOfType<PercentagesScript>();
        GetScoresScript = FindObjectOfType<HighScoresScript>();
        checkFirstDay = FindObjectOfType<CheckFirstDayScript>();
        animals = Resources.LoadAll("Animal").Cast<GameObject>().ToArray();
    
        for (int i = 0; i < (transform.childCount + GetScoresScript.dificultyLevel); i++){
           
            animals[i] = Instantiate(animals[i] as GameObject, SpawnPositios[i].transform.position, SpawnPositios[i].transform.rotation);
            animals[i].transform.SetParent(SpawnPositios[i].transform);
           
        } 

        // ____________________________________________________________________________________________________________________________________________________________________________________________________________

        // fin del primer "for", donde selecciono animales al azar y los pongo en los lugares assignados en orden. Tambien hago que todos los animales que instancie sean hijos de sus respectivos lugares
        // para que pueda encontrar sus scripts más facilmente, solo recorro todo el arreglo de las posiciones donde instancio a los animales y busco en sus hijos (que sus hijos ahora son los animales).

        //* ____________________________________________________________________________________________________________________________________________________________________________________________________________

        hunger = FindObjectsOfType<HungerScript>(); // aqui encuentro todos los script de "Hunger" de los animales que acabo de instanciar desde los prefabs

        for(int i = 0; i < transform.childCount + GetScoresScript.dificultyLevel; i++){

            if (animals[i].transform.gameObject.GetComponentInChildren<HungerScript>() != null)
            {
                
                hunger[i] = animals[i].transform.gameObject.GetComponentInChildren<HungerScript>();
                hunger[i].enabled = false;

            }

        }

        repetitions = Percentages.Totalcount;
        InvokeRepeating("RandomizeHunger", 2, repeatRate); // aqui mando a llamar la funcion que dicta que un animal al azar tenga hambre

	}

    //* ____________________________________________________________________________________________________________________________________________________________________________________________________________


    // ____________________________________________________________________________________________________________________________________________________________________________________________________________

    // Este for desactiva todos los scripts de "Hunger" para que immediatamante active solo uno al azar

    void RandomizeHunger(){ 
        
        for (int i = 0; i < (animals.Length+ GetScoresScript.dificultyLevel); i++)
            {
            hunger[i].enabled = false;
            }

        //* ____________________________________________________________________________________________________________________________________________________________________________________________________________

            tempRandom = RandomHunger;

        while ((RandomHunger = UnityEngine.Random.Range(0, (animals.Length + GetScoresScript.dificultyLevel))) == tempRandom) {}// Esto evita que un mismo animal tenga hambre 2 veces seguidas.

        hunger[RandomHunger].enabled = true; // el script de "Hunger" que fue elegido al azar para que el animal tenga hambre. 

        // ____________________________________________________________________________________________________________________________________________________________________________________________________________

        // Dentro de este "if" destruyo este GameObject para dejar de llamar la funcion de "Hunger". Pero antes de la destruccion de GO mando a llamar la funcion encargada de llevar los puntos, al igual que desemparento los animales, para que no desaparezcan de la nada.

        if (Mathf.Approximately(repetitions, endRepetitions))
        {
            PlayerPrefs.SetString(KeysForAnalytic.FirstDayOfplay,Convert.ToDateTime(DateTime.Now).ToString());
            Percentages.Percentanges();
            GetScoresScript.ShowScores();
            copyStars[0].InstanceStars();
            copyStars[1].InstanceStars();
            copyStars[2].InstanceStars();

            for (int i = 0; i < (hunger.Length); i++)
            {
                hunger[i].gameObject.transform.parent = null;
                hunger[i].enabled = false;

            }
            Destroy(this.gameObject);
        }
   
    }

   

}
