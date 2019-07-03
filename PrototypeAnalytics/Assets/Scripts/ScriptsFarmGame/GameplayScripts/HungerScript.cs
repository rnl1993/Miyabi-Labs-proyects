using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HungerScript : MonoBehaviour {

    // public variables

    public GameObject WarningSimbol;     public AudioClip[] animalSounds;      public int failPoints;     public float timer;

    // private Variables

    private GameObject instantiatedObj;     private float destroyTime;     private int animaltype;     private bool hungry;     private bool haventEat;

    // Script References

    AudioSource aSource;
    WarningSignScript WSS;
    FinalScoreScript ScoreScript;
    AnimalsReferenceScript animals;
    SpawnAnimalsScript spawnAnimals;
    FinalScoreScript scoreManager;

	// Functions

	void Start () {


        spawnAnimals = FindObjectOfType<SpawnAnimalsScript>();
        animals = this.GetComponent<AnimalsReferenceScript>();
        scoreManager = FindObjectOfType<FinalScoreScript>();
        animaltype = animals.Animal_Script.animalType;
        destroyTime = spawnAnimals.repeatRate;
        aSource = GetComponent<AudioSource>();
        timer = spawnAnimals.repeatRate;
        haventEat = true;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (this.instantiatedObj == null && haventEat == true)
        {

            SpawnWarnings();

        }
		
	}

    void SpawnWarnings()
    {

        instantiatedObj = Instantiate(WarningSimbol, new Vector3(this.transform.position.x, this.transform.position.y + this.transform.localScale.y, this.transform.position.z), this.transform.rotation);
        aSource.clip = animalSounds[0];
        aSource.Play();
        spawnAnimals.repetitions--;
        hungry = true;
        haventEat = true;
        Destroy(instantiatedObj, destroyTime);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (hungry == true)
        {

            if (other.GetComponent<FoodReferenceScript>() != null && other.GetComponent<FoodReferenceScript>().Food_Script.foodType == animaltype)
            {

                scoreManager.IncreaseScore();
                aSource.clip = animalSounds[1];
                aSource.Play();
                WSS = FindObjectOfType<WarningSignScript>();
                WSS.destroythis();
                hungry = false;
                haventEat = false;
                Destroy(other.gameObject);

            }

            else
            {
                if (other.GetComponent<FoodReferenceScript>() != null && other.GetComponent<FoodReferenceScript>().Food_Script.foodType != animaltype)
                {

                    aSource.clip = animalSounds[2];
                    aSource.Play();
                    //failPoints += 3;
                    Destroy(other.gameObject);

                }
            }

        }

    }


    private void OnEnable()
    {

        haventEat = true;

    }

    private void OnDisable()
    {
        
        Destroy(instantiatedObj);

    }
}
