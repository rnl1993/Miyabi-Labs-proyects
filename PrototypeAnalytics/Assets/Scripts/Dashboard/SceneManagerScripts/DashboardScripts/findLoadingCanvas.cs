using UnityEngine;

public class findLoadingCanvas : MonoBehaviour {

    private static findLoadingCanvas instance;

    void Awake()
    {
        
        if (instance != null)
        {

            Destroy(gameObject);

        }
        else
        {

            instance = this;
            DontDestroyOnLoad(gameObject);

        }

    }

}
