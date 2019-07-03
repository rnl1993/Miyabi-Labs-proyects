using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour {

    public void AnalyticsSceneButton()
    {          SceneManager.LoadScene("AnalyticsScenes");      }      public void DashboardSceneButton()
    {          SceneManager.LoadScene("DashBoardScene");      }

    public void AboutSceneButton(){

        SceneManager.LoadScene("AboutScene");

    }      public void ExitApp()
    {          Application.Quit();      }

}
