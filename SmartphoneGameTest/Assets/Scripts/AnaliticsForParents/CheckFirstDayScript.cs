using System;
using UnityEngine;

public class CheckFirstDayScript : MonoBehaviour {

    DateTime FirstDayOfPlay;

    TimeSpan EndedPeriod;

	// Use this for initialization
	void Start () {
		
	}
	
    public void CheckForfirstDay()
    {

        if (PlayerPrefs.HasKey(KeysForAnalytic.FirstDayOfplay))
        {

            FirstDayOfPlay = Convert.ToDateTime(PlayerPrefs.GetString(KeysForAnalytic.FirstDayOfplay));
            print("este es el primer dia de juego" + FirstDayOfPlay.ToString());

            EndedPeriod = DateTime.Now - FirstDayOfPlay;

            print("cuantos dias van = " + EndedPeriod.Days.ToString());

        }

        else
        {

            PlayerPrefs.SetString(KeysForAnalytic.FirstDayOfplay, DateTime.Now.ToString());
            print("estoy salvado la fecha porque no hay = " + PlayerPrefs.GetString(KeysForAnalytic.FirstDayOfplay));

        }

    }
}
