using UnityEngine;

public class DestroySingletonsScript : MonoBehaviour {

    private GamesScenesManager gamesScenesManager;
    private ExerciseScenesManager exerciseScenesManager;
    private SwitchBackgroundsScript SwitchBackgroundsScript;

	// Use this for initialization
	void Start () {

        gamesScenesManager = FindObjectOfType<GamesScenesManager>();
        exerciseScenesManager = FindObjectOfType<ExerciseScenesManager>();
        SwitchBackgroundsScript = FindObjectOfType<SwitchBackgroundsScript>();



	}
	
	void Update () {

        if (gamesScenesManager != null)
        {

            Destroy(gamesScenesManager.gameObject);

        }

        if (exerciseScenesManager != null)
        {

            Destroy(exerciseScenesManager.gameObject);

        }

        if (SwitchBackgroundsScript != null)
        {

            Destroy(SwitchBackgroundsScript.gameObject);

        }
		
	}
}
