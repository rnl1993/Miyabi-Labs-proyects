using UnityEngine;

public class DestroyCubes : MonoBehaviour {

    private float timeToDestroy;
 
    [SerializeField]
    private Collider col;

	// Use this for initialization
	void Start () {
        
        col = GetComponent<Collider>();
        timeToDestroy = 8;
        Destroy(this.gameObject, timeToDestroy);
       
	}

    void Update()
    {
        
        timeToDestroy -= Time.deltaTime;

        if(timeToDestroy <= 1.0f){
            
            this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y - (0.25f * Time.deltaTime), gameObject.transform.localScale.z);
            col.enabled = false;

        }

    }

}
