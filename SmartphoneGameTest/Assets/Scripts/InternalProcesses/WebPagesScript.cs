using UnityEngine;

public class WebPagesScript : MonoBehaviour {

    public void Url(){

        Application.OpenURL("https://www.facebook.com/search/top/?q=miyabi%20labs");

    }

    public void ImagenesLinks(){

        Application.OpenURL("https://www.publicdomainpictures.net/en/index.php");
    }

    public void StarLink(){
        // Link de la estrella 3d
        Application.OpenURL("https://free3d.com/3d-model/star-mobile-ready-60-tris-49986.html");

    }

    public void HouseLink(){
        // Link de la casa 3d
        Application.OpenURL("https://free3d.com/3d-model/old-farm-house-91130.html");
    }

    public void SoundsLink(){
        // Link audios 
        Application.OpenURL("https://freesound.org");
    }


}
