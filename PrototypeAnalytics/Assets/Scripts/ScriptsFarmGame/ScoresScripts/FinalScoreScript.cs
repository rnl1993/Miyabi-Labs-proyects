using UnityEngine;

public class FinalScoreScript : MonoBehaviour {

    // Public Variables

    public GameObject[] spawnStarsPoints;
    public GameObject[] StarsObjs;
    public float finalScore;
    public float Totalcount;
    public float score;

    // private Variables

    private int substractPoints;
    private int multiplier;
    private float Count;
    private int stars;

    // Script References

    private HungerScript hungerScriptReference;
    //private RaycastDistractions distractions;
    private FoodContainerScript FCS;
    private WarningSignScript WSS;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FinalScoreCalculation(){

        Count = (score / Totalcount) * 100;
        Count -= substractPoints;

        if (Count > 90)
        {

            stars = 3;              for (int i = 0; i < stars; i++)
            {                  GameObject go = Instantiate(StarsObjs[i], spawnStarsPoints[i].transform.position, spawnStarsPoints[i].transform.rotation);                 go.transform.parent = spawnStarsPoints[i].transform;              }         }          else if (Count > 60)
        {             stars = 2;

            for (int i = 0; i < stars; i++)             {                  GameObject go = Instantiate(StarsObjs[i], spawnStarsPoints[i].transform.position, spawnStarsPoints[i].transform.rotation);                 go.transform.parent = spawnStarsPoints[i].transform;             }         }          else if (Count > 30)
        {             stars = 1;              for (int i = 0; i < stars; i++)             {                 GameObject go = Instantiate(StarsObjs[i], spawnStarsPoints[i].transform.position, spawnStarsPoints[i].transform.rotation);                 go.transform.parent = spawnStarsPoints[i].transform;             }         }          else
        {              stars = 0;
        }
    }

    public void IncreaseScore()
    {         this.score++;     }

    public void FinalScore()
    {         finalScore = Count;     }
}
