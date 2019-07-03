using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLevelsCollidersScript : MonoBehaviour {

    private ExerciseSceneScript [] GetExerciseScene;

	// Use this for initialization
	void Start () {

        GetExerciseScene = FindObjectsOfType<ExerciseSceneScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if(RaycastDashBoard.LevelsBoxCollidersOff == true){

            for (int i = 0; i < GetExerciseScene.Length; i++){

                GetExerciseScene[i].GetComponent<Collider>().enabled = false;
            }
        }

        else{

            for (int i = 0; i < GetExerciseScene.Length; i++){

                GetExerciseScene[i].GetComponent<Collider>().enabled = true;
            }
        }

	}
}
