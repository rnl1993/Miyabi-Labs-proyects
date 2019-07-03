using UnityEngine;

public class DestroyFoodScript : MonoBehaviour {

    private int lifeSpawnTimer;

    // Functions
	
	void Start () {
		
        lifeSpawnTimer = 8;

        LifeSpawn();

	}
	
    void LifeSpawn()
    {

        Destroy(this.gameObject, lifeSpawnTimer);

    }
}
