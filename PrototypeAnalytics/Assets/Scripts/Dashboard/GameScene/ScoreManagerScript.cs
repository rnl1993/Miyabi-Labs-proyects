using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerScript : MonoBehaviour {

    public Text ScoreText;

    public int Score;

    // Use this for initialization
    void Start () {

        Score = 0;
        ScoreText.text = " Puntos: " + Score.ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

   public void ScoreFuntion() {

        Score += 1;
        ScoreText.text = " Puntos: " + Score.ToString();
    
    }

    public void ScoreFuntion2()
    {

        Score -= 1;
        ScoreText.text = " Puntos: " + Score.ToString();

    }
}
