using UnityEngine;

public class DistractionManagerScript : MonoBehaviour {

    public GameObject [] SpawnPoints;

    // private variables

    private float timer;
    private bool isActive;
    private float activeTime;
    private int RandomPosition;
    private int RandomAppearance;

    // script References

    private DistractionScript distraction;
   

	// Use this for initialization
	void Start () {
        
        distraction = FindObjectOfType<DistractionScript>();
        isActive = true;
        RandomPosition = Random.Range(0, (SpawnPoints.Length));
        activeTime = Random.Range(5.0f, 8.0f);
        timer = 0;
        if (distraction.gameObject != null)
        {
            distraction.gameObject.transform.position = SpawnPoints[RandomPosition].transform.position;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        
        timer += Time.deltaTime;

        if (timer >= activeTime)
        { // Aqui mando a llamar el booleano que se encarga de activar y desactivar el GO de la distraccion

            isActive = !isActive;
            RandomPosition = Random.Range(0, (SpawnPoints.Length));
            timer = 0;

        }

        if (isActive == false)
        {

            distraction.gameObject.SetActive(false);

        }

        else
        {

            distraction.gameObject.SetActive(true);
            distraction.gameObject.transform.position = SpawnPoints[RandomPosition].transform.position;

        }
		
	}
}
