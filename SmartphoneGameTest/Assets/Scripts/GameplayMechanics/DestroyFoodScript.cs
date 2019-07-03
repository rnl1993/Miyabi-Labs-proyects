using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFoodScript : MonoBehaviour {

    private int lifeSpawnTimer;

	// Use this for initialization
	void Start () {

        lifeSpawnTimer = 6;

        LifeSpawn();

	}
	
    void LifeSpawn()
    {

        Destroy(this.gameObject, lifeSpawnTimer);

    }
}
