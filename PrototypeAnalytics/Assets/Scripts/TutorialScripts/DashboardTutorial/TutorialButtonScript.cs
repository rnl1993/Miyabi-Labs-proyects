using UnityEngine;

public class TutorialButtonScript : MonoBehaviour {

    // El tutorial que se va volver a mostrar.
    public GameObject TutorialImage;

    // esta función vuelve a mostrar el tutorial.
	public void ReplayTutorial(){

        TutorialImage.gameObject.SetActive(true);
        FindObjectOfType<TutorialScript>().TutorialButton();
	}
}
