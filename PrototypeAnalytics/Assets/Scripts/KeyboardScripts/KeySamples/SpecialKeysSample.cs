using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpecialKeysSample : MonoBehaviour{

    public Canvas loginCanvas;
    public Canvas PanelsCanvas;
    public Canvas loginCanvasObj;

    public SpecialKey Skey;
    public Image Artwork;
    public int FunctionValue;

    public bool switchKey;
    public bool switchSimbols;
    public bool switchSimbols2;
    public bool minus;

    public Color OGcolor = new Color(1, 1, 1, 1);

    // private variables

    private Raycast KeyboardRaycast;
    private string deleteString;
    private string Space;

    private ChangeColorScript changeColor;


    // Use this for initialization
    void Start(){

        Artwork.sprite = Skey.Artwork;
        Space = " ";
        FunctionValue = Skey.value;
        switchKey = false;
        switchSimbols = false;
        switchSimbols2 = false;
       

        KeyboardRaycast = FindObjectOfType<Raycast>();

        changeColor = FindObjectOfType<ChangeColorScript>();

    }

    private void Update(){

        if (changeColor.colorswitch == false){

            ChangeColor();
        }  
    }

    public void ShiftKey(int enter){          if (enter == 0){             
            switchKey = !switchKey;         }     }

    public void DelKey(int enter){

        if (enter == 1){

            deleteString = KeyboardRaycast.activeField.text.Remove(KeyboardRaycast.activeField.text.Length - 1);

            KeyboardRaycast.activeField.text = deleteString;
        }
    }

    public void SpaceBar(int enter){

        if (enter == 2){

            KeyboardRaycast.activeField.text += Space;
        }
    }

    public void EnterKey(int enter){

        if (enter == 3){

            if(KeyboardRaycast.MailField.text.Length >= 1 && KeyboardRaycast.PasswordField.text.Length >= 1){ 

                SceneManager.LoadScene("NewDashboard");
            }
        }
    }

    public void Simbols(int enter){
        if (enter == 4) { 

            switchSimbols = !switchSimbols;
        }
    }

    public void Simbols2(int enter){

        if (enter == 5){

            switchSimbols2 = !switchSimbols2;
        }
    }

    public void Minus(int enter){
        if (enter == 6){

            minus = !minus;
        }
    }

    public void ExitApp(int enter){

        if (enter == 7){

            Application.Quit();
        }
    }

    public void LoginButton(int enter){          if (enter == 8){                          loginCanvasObj.gameObject.SetActive(true);             PanelsCanvas.gameObject.SetActive(true);
             loginCanvas.gameObject.SetActive(false);         }     }

    public void loginButton2(int enter){

        if (enter == 9){
          
            KeyboardRaycast.activeField = KeyboardRaycast.MailField;
            KeyboardRaycast.activeField.text = "";
            KeyboardRaycast.activeField = KeyboardRaycast.PasswordField;
            KeyboardRaycast.activeField.text = "";
            KeyboardRaycast.activeField = KeyboardRaycast.MailField;

            loginCanvas.gameObject.SetActive(true);

            loginCanvasObj.gameObject.SetActive(false);
            PanelsCanvas.gameObject.SetActive(false);
        }
    }

    public void ALlFunctions(int enter){

        ShiftKey(enter);
        DelKey(enter);
        SpaceBar(enter);
        EnterKey(enter);
        Simbols(enter);
        Simbols2(enter);
        Minus(enter);
        ExitApp(enter);
        LoginButton(enter);
        loginButton2(enter);
    }

    public void ChangeColor(){

        Artwork.color = OGcolor;
    }
}