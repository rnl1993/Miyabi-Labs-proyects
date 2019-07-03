using UnityEngine;
using UnityEngine.UI;

public class KeySample : MonoBehaviour{ 
    // Este es un Scriptable Object con el valor de las letras, numeros y simbolos     public Key Key;
    public GameObject keyboarManager;
    public KeyboardManager kbm;     public Text NameText;     public Image Artwork;
    public Color OGcolor = new Color(1, 1, 1, 1);

    private ChangeColorScript changeColor;      // Use this for initialization     void Awake(){          NameText.text = Key.Name;         Artwork.sprite = Key.Artwork;
        kbm = keyboarManager.GetComponent<KeyboardManager>();
        kbm.SKR[5].gameObject.SetActive(false);
        kbm.SKR[6].gameObject.SetActive(false);
        changeColor = FindObjectOfType<ChangeColorScript>();     }      void Update(){
                 S();
        ChangeKeyValues();
        resetPositions();

        if(changeColor.colorswitch == false){

            ChangeColor();
        }     }      void S(){

        if (kbm.SKR[0].switchKey == true){ 
            Key.ShiftKey = true;         }          else{              Key.ShiftKey = false;         }     }

    void resetPositions(){

        if (kbm.SKR[6].minus == true){

            kbm.SKR[0].switchKey = false;
            kbm.SKR[4].switchSimbols = false;
            kbm.SKR[5].switchSimbols2 = false;
            kbm.SKR[6].minus = false;

            kbm.SKR[0].gameObject.SetActive(true);
            kbm.SKR[4].gameObject.SetActive(true);
            kbm.SKR[5].gameObject.SetActive(false);
            kbm.SKR[6].gameObject.SetActive(false);
        }
    }

    void ChangeKeyValues(){

        if (Key.ShiftKey == true){

            NameText.text = Key.Name.ToUpper();
        }

        if (kbm.SKR[4].switchSimbols == true){

            NameText.text = Key.simbol;
            kbm.SKR[0].gameObject.SetActive(false);
            kbm.SKR[4].gameObject.SetActive(false);
            kbm.SKR[5].gameObject.SetActive(true);
            kbm.SKR[6].gameObject.SetActive(true);


            if (kbm.SKR[4].switchSimbols == true && kbm.SKR[5].switchSimbols2 == true){

                NameText.text = Key.secondSimbol;
            }
        }

        if(kbm.SKR[0].switchKey == false && kbm.SKR[4].switchSimbols == false && kbm.SKR[5].switchSimbols2 ==  false){

            NameText.text = Key.Name;
        }
    } 

    public void ChangeColor(){

        Artwork.color = OGcolor;
    } }