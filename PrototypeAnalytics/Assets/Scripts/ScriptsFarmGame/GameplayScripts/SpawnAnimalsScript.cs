using System;
using System.Linq;
using UnityEngine;

public class SpawnAnimalsScript : MonoBehaviour {

    // Public Variables

    public GameObject[] SpawnPositios;
    public float repetitions;
    public int repeatRate;

    // private Variables

    private int randomAnimalPosition;
    private GameObject[] animals;
    private float endRepetitions;
    private int tempRandomAnimal;
    private int RandomHunger;
    private int tempRandom;

    // Script references

    private HungerScript[] hunger;
    private FinalScoreScript FinalScore;
    private instanceCopyStars[] copyStars;

	// Functions

	void Start () {

        endRepetitions = 0;
        FinalScore = FindObjectOfType<FinalScoreScript>();
        copyStars = FindObjectsOfType<instanceCopyStars>();
        animals = Resources.LoadAll("Animals").Cast<GameObject>().ToArray();

        for (int i = 0; i < (transform.childCount); i++)
        {
            animals[i] = Instantiate(animals[i] as GameObject, SpawnPositios[i].transform.position, SpawnPositios[i].transform.rotation);
            animals[i].transform.SetParent(SpawnPositios[i].transform);
        }

        hunger = FindObjectsOfType<HungerScript>();

        for (int i = 0; i < transform.childCount; i++)
        {

            if (animals[i].transform.gameObject.GetComponentInChildren<HungerScript>() != null)
            {

                hunger[i] = animals[i].transform.gameObject.GetComponentInChildren<HungerScript>();
                hunger[i].enabled = false;

            }
        }

        repetitions = FinalScore.Totalcount;
        InvokeRepeating("RandomHungryAnimal", 2, repeatRate); 

	}

    void RandomHungryAnimal(){

        for (int i = 0; i < animals.Length; i++)
        {
            
            hunger[i].enabled = false;

        }

        tempRandom = RandomHunger;

        while ((RandomHunger = UnityEngine.Random.Range(0, animals.Length)) == tempRandom) { }// Esto evita que un mismo animal tenga hambre 2 veces seguidas.

        hunger[RandomHunger].enabled = true; // el script de "Hunger" que fue elegido al azar para que el animal tenga hambre. 

        if (Mathf.Approximately(repetitions, endRepetitions))
        {

            FinalScore.FinalScoreCalculation();

            copyStars[0].InstanceStars();
            copyStars[1].InstanceStars();
            copyStars[2].InstanceStars();

            for (int i = 0; i < (animals.Length); i++)
            {


                hunger[i].enabled = false;
            }

            Destroy(this.gameObject);

        }
    }
}
