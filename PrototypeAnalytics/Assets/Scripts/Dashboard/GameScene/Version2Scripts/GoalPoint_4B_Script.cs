using UnityEngine;

public class GoalPoint_4B_Script : MonoBehaviour {

    public float speed;
    public float distance;

    void Update()
    {

        transform.position = new Vector3(Mathf.Sin(speed * Time.time) * distance, transform.position.y, transform.position.z);

    }
}

