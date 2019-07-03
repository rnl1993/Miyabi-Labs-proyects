using UnityEngine;

public class instanceCopyStars : MonoBehaviour {

    // Variables

    public GameObject[] spawnStarsPoints;
    public GameObject[] StarsObjs;

    private int stars;

    PercentagesScript percentages;

	// Functions
	void Start () {

        percentages = FindObjectOfType<PercentagesScript>();

	}
	
    public void InstanceStars(){

        percentages.FinalScore();

        if (percentages.finalScore > 90)
        {

            stars = 3;              for (int i = 0; i < stars; i++)
            {                  GameObject go = Instantiate(StarsObjs[i], spawnStarsPoints[i].transform.position, spawnStarsPoints[i].transform.rotation);                 go.transform.parent = spawnStarsPoints[i].transform;             }          }

        else if (percentages.finalScore > 60)
        {

            stars = 2;

            for (int i = 0; i < stars; i++)
            {

                GameObject go = Instantiate(StarsObjs[i], spawnStarsPoints[i].transform.position, spawnStarsPoints[i].transform.rotation);
                go.transform.parent = spawnStarsPoints[i].transform;
            }

        }

        else if (percentages.finalScore > 30)
        {

            stars = 1;

            for (int i = 0; i < stars; i++)
            {

                GameObject go = Instantiate(StarsObjs[i], spawnStarsPoints[i].transform.position, spawnStarsPoints[i].transform.rotation);
                go.transform.parent = spawnStarsPoints[i].transform;
            }

        }

    }
	
}
