using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "Keyboard/Key")]
public class Key : ScriptableObject
{
    //Variables________________________

    public string Name;
    public string simbol;
    public string secondSimbol;
    public Sprite Artwork;

    public bool ShiftKey;



    //Functions________________________

    public void ChangeShiftValue(){

        ShiftKey = !ShiftKey;

    }

}