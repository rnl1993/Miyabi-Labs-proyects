using UnityEngine;

public class GoalPoint_4_Script : MonoBehaviour {

    public GameObject player;
    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(player.transform.position, Vector3.up, speed * Time.deltaTime);
		
	}
}
