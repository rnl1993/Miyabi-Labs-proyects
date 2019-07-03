using UnityEngine;

public class instanceCopyStars : MonoBehaviour {

    // Variables

    public GameObject[] spawnStarsPoints;
    public GameObject[] StarsObjs;

    private int stars;

    FinalScoreScript finalScore;

    // Functions
    void Start()
    {

        finalScore = FindObjectOfType<FinalScoreScript>();

    }

    public void InstanceStars()
    {
        finalScore.FinalScore();

        finalScore.FinalScore();

        if (finalScore.finalScore > 90)
        {

            stars = 3;              for (int i = 0; i < stars; i++)
            {                  GameObject go = Instantiate(StarsObjs[i], spawnStarsPoints[i].transform.position, spawnStarsPoints[i].transform.rotation);                 go.transform.parent = spawnStarsPoints[i].transform;             }          }

        else if (finalScore.finalScore > 60)
        {

            stars = 2;

            for (int i = 0; i < stars; i++)
            {

                GameObject go = Instantiate(StarsObjs[i], spawnStarsPoints[i].transform.position, spawnStarsPoints[i].transform.rotation);
                go.transform.parent = spawnStarsPoints[i].transform;
            }

        }

        else if (finalScore.finalScore > 30)
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
