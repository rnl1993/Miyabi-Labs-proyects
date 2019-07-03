using UnityEngine;

public class SpawnCubesScript : MonoBehaviour {

    public float speed;
    public int minTime;
    public int maxTime;
    public GameObject player;
    public GameObject [] CubeInstance;

    private int UnlockCubes;
    private int randomNum;

    private GoalPointsManager GetGoalPoints;

	// Use this for initialization
	void Start () {

        GetGoalPoints = FindObjectOfType<GoalPointsManager>();

        Invoke("InstantianteCubes",Random.Range(minTime, maxTime));
		
	}
	
	// Update is called once per frame
	void Update () {

       

        UnlockCubes = GetGoalPoints.Unlock;
       
        transform.RotateAround(player.transform.position, Vector3.up, speed * Time.deltaTime);

    }

    void InstantianteCubes(){

        float delay = Random.Range(minTime, maxTime);

        Instantiate(CubeInstance[Random.Range(0,(CubeInstance.Length + UnlockCubes))],this.transform.position,Quaternion.identity);

        Invoke("InstantianteCubes", delay);

    }


}
