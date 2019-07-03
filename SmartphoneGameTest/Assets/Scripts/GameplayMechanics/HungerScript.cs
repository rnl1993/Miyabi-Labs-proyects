using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HungerScript : MonoBehaviour
{

    // Public Variables

    public GameObject WarningSimbol;
    public AudioClip[] animalSounds;

    public int failPoints;
    public float timer;

    // private Variables

    private GameObject instantiatedObj;
    private float destroyTime;
    private int animaltype;
    private bool hungry;
    private bool haventEat;
   
    // script References

    PercentagesScript scoreManager;
    Manager_Animals AnimalManager;
    AudioSource aSource;
    WarningSignScript WSS;
    Animals animals;

    // Functions

    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // aqui solo mando a llamar a mis variables y mis componentes

    void Start()
    {
        animals = this.GetComponent<Animals>();
        animaltype = animals.animals_SO.animalType;
        AnimalManager = FindObjectOfType<Manager_Animals>();
        scoreManager = FindObjectOfType<PercentagesScript>();
        destroyTime = AnimalManager.repeatRate;
        aSource = GetComponent<AudioSource>();
        timer = AnimalManager.repeatRate;
        haventEat = true;
    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    void Update()
    {
        
        if(this.instantiatedObj == null && haventEat == true){

            SpawnWarnings();

        }
    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Aqui mando llamar instanciar la señal de hambre de los animales, y dentro de la misma función mando a llamar por su destrucción dentro de cierto tiempo. El tiempo de destruccion esta sincronizado
    // con el tiempo que le toma al manager de los animales volver a llamar esta función para que otro animal tenga hambre.
    // Es decir, que apenas se destruya la señal de hambre mediante esta función, inmediatamente otro animal va a tener hambre.

    void SpawnWarnings()
    {

        instantiatedObj = Instantiate(WarningSimbol, new Vector3(this.transform.position.x, this.transform.position.y + this.transform.localScale.y, this.transform.position.z), this.transform.rotation);
        aSource.clip = animalSounds[0];
        aSource.Play();
        AnimalManager.repetitions--;
        hungry = true;
        haventEat = true;
        Destroy(instantiatedObj, destroyTime);
    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    // Esta funcion checa que la comida colisione con el animal correcto, y despues te suma un punto por hacerlo. 
    // Si la comida que manipulas no la conectas con el animal correcto, no es aceptada y simplemente no pasa nada.

    void OnTriggerEnter(Collider other)
    {
        if (hungry == true)
        {

            if (other.GetComponent<Food>() != null && other.GetComponent<Food>().food_OS.foodType == animaltype)
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
                if (other.GetComponent<Food>() != null && other.GetComponent<Food>().food_OS.foodType != animaltype)
                {
                    aSource.clip = animalSounds[2];
                    aSource.Play();
                    failPoints = 3;
                    KeysForAnalytic.foodErrors += failPoints;
                    Destroy(other.gameObject);
                }
            }

        }

    }

    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________


    // ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________

    private void OnEnable()
    {
        haventEat = true;
    }

    // _________________________________________________________________________

    private void OnDisable()
    {
        Destroy(instantiatedObj);
    }


    //* ________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________
}
