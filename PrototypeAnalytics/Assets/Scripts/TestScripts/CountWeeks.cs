using UnityEngine;

public class CountWeeks : MonoBehaviour {

    public int days;

    private float Count;

	// Use this for initialization
	void Start () {

        Count = days % 13;
        Weeks();
	}

	void Weeks(){

        print(Count);

        if(Count >= 1){
            
            print("Ha pasado una semana");
        }
	}
}
