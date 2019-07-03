using UnityEngine;

public class GoalPointsManager : MonoBehaviour {

    // public variables

    public int Unlock;
    public GameObject GoalPoint3;
    public GameObject GoalPoint4;

    // private variables

    private float timer;

    // Functions

	void Start () {

        GoalPoint3.gameObject.SetActive(false);
        GoalPoint4.gameObject.SetActive(false);
        Unlock = -2;
        timer = 60;

	}

	void Update () {

        ManageTime();
        ManageGoalPoint();

	}

    void ManageTime(){

        if (timer >= 0)
        {

            timer -= Time.deltaTime;

            if (timer <= 0)
            {

                Unlock++;

                if (Unlock >= 0)
                {
                    Unlock = 0;
                }

                timer = 60;

            }

        }

    }

    void ManageGoalPoint(){

        if(Unlock == -1){
           
            GoalPoint3.gameObject.SetActive(true);

        }

        if(Unlock == 0 && GoalPoint4 != null){
            
            GoalPoint4.gameObject.SetActive(true);

        }



    }

}
