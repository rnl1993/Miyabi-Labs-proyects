using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScenesForDevices : MonoBehaviour
{
    public void ManageScenes()
    {
        SceneManager.LoadScene("Android_TestScene");

        /*
        if (Application.platform == RuntimePlatform.Android)
        {
            SceneManager.LoadScene("Android_TestScene");
        }

        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            SceneManager.LoadScene("IOS_TestScene");
        }
*/
    }

    public void ReturnToDashboard(){

        SceneManager.LoadScene("TestDashboard");
    }

    public void ExitApp(){

        Application.Quit();
    }
}
