using UnityEngine;

public class DistractionManagerScript : MonoBehaviour {

    // Variables

    public GameObject[] Sps;

    private int RandomAppearance;
    private float activeTime;
    private float timer;
    private bool isActive;
    private DistractionsScript distraction;
    private HighScoresScript GetHighScores;
    private int RandomPosition;

	// Funtions
	void Start () {

        GetHighScores = FindObjectOfType<HighScoresScript>();
        distraction = FindObjectOfType<DistractionsScript>();
        isActive = true;
        RandomPosition = Random.Range(0, (Sps.Length + GetHighScores.dificultyLevel));
        activeTime = Random.Range(5.0f,8.0f);
        timer = 0;
        if (distraction.gameObject != null)
        {
            distraction.gameObject.transform.position = Sps[RandomPosition].transform.position;
        }
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if(timer >= activeTime){ // Aqui mando a llamar el booleano que se encarga de activar y desactivar el GO de la distraccion

            isActive = !isActive;
            RandomPosition = Random.Range(0, (Sps.Length ));
            timer = 0;

        }

        if(isActive == false){

            distraction.gameObject.SetActive(false);

        }

        else{

            distraction.gameObject.SetActive(true);
            distraction.gameObject.transform.position = Sps[RandomPosition].transform.position;

        }

	}
}
