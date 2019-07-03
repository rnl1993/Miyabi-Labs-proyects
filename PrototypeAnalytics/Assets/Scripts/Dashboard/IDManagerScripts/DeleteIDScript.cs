using UnityEngine;

public class DeleteIDScript : MonoBehaviour {

    public void DeleteID(){

        PlayerPrefs.DeleteKey(IDScript.SaveID);
    }
}
