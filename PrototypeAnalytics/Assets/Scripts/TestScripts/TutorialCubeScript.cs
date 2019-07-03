using UnityEngine;
using System.Collections;

public class TutorialCubeScript : MonoBehaviour {

    private Renderer Rend;

	// Use this for initialization
	void Start () {

        Rend = GetComponent<Renderer>();
	}

    public IEnumerator FadeIn()
    {
        for (float i = 0; i < 1; i += 0.1f)
        {

            Rend.material.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, i));

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void RecolorCube(){
        
        Rend.material.color = Color.red;
    }
}
