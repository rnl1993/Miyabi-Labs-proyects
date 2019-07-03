using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GamesEndScript : MonoBehaviour {

    public Text TimerText;
    public Text endGameText;
    public float TimeRemaining;

    private Double roundedTime;
    private float endGameTimer;

    // script References

    private SpawnCubesScript spawnCubes;
    private FollowPlanet[] followPlanet;
    private GoalPoint_1_Script GoalPoint_1;
    private GoalPoint_2_Script GoalPoint_2;
    private GoalPoint_3_Script GoalPoint_3;
    private GoalPoint_4_Script GoalPoint_4;
    //private GamesScenesManager gamesScenesManager;

    // Functions

    void Start(){

        endGameText.gameObject.SetActive(false);
        //gamesScenesManager = FindObjectOfType<GamesScenesManager>();

        endGameTimer = 5;
    }

    void Update(){
        
        TimeRemaining -= Time.deltaTime;
        if (TimerText != null)
        {
            TimerText.text = "Tiempo restante: " + (roundedTime = Mathf.Round(TimeRemaining));
        }

        if (TimeRemaining <= 0)
        {
            TimeRemaining = 0;
            roundedTime = 0;
            endGameTimer -= Time.deltaTime;
            StartCoroutine(endGameFunctions());
        }
    }

    IEnumerator endGameFunctions (){

        followPlanet = FindObjectsOfType<FollowPlanet>();
        spawnCubes = FindObjectOfType<SpawnCubesScript>();
        GoalPoint_1 = FindObjectOfType<GoalPoint_1_Script>();
        GoalPoint_2 = FindObjectOfType<GoalPoint_2_Script>();
        GoalPoint_3 = FindObjectOfType<GoalPoint_3_Script>();
        GoalPoint_4 = FindObjectOfType<GoalPoint_4_Script>();

        if (GoalPoint_1 != null && GoalPoint_2 != null && GoalPoint_3 != null && GoalPoint_4 != null){

            GoalPoint_4.gameObject.SetActive(false);
            GoalPoint_1.gameObject.SetActive(false);
            GoalPoint_2.gameObject.SetActive(false);
            GoalPoint_3.gameObject.SetActive(false);
        }

        if (spawnCubes != null){

            Destroy(spawnCubes.gameObject);
        }

        for (int i = 0; i < followPlanet.Length; i++){

            followPlanet[i].gameObject.SetActive(false);
        }
        if (endGameText != null){

            endGameText.gameObject.SetActive(true);
            endGameText.text = "Regresaras al menu en : " + (roundedTime = Mathf.Round(endGameTimer));
        }


        yield return new WaitForSeconds(5);

        //GamesScenesManager.remainingTime -= 3;

        SceneManager.LoadScene("NewDashboard");

    }
}