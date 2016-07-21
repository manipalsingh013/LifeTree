using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPositionChecker : MonoBehaviour
{
    //public Text Temp;

    public GameObject MainMenu;
    public GameObject SitDownMenu;

    bool PlayerSeated;

    public GameObject MicController;
    MicControl MicControl;

    float ExhaleStartTime;
    float ExitTime;
    bool Entered;

    bool Exhale;


    void Start()
    {
        PlayerSeated = false;
        
        MicControl = MicController.GetComponent<MicControl>();
        Entered = false;
    }

    void Update()
    {
        //Temp.text = Input.acceleration.y + " " + Input.acceleration.y + "  " + Input.acceleration.z ;
        if (!PlayerSeated)
        {
            if (Input.acceleration.y <= -1.4f)
            {
                PlayerSeated = true;
                GoToMainMenu();

                //Handheld.Vibrate();
            }

            if (MicControl.loudness > 0.01f && !Exhale)
            {
                Exhale = true;
                ExhaleStartTime = Time.time;
            }
            if (MicControl.loudness <= 0.01f)
            {
                Exhale = false;
            }

            if (Entered && Time.time - ExhaleStartTime > 2f && Exhale)
            {
                PlayerSeated = true;
                GoToMainMenu();
            }
        }
    }
    

    void GoToMainMenu()
    {
        SitDownMenu.SetActive(false);
        MainMenu.SetActive(true);
        //After player is seated enable main menu
    }


    public void enterTimer()
    {
        Entered = true;
        ExhaleStartTime = Time.time;
    }

    public void exitTimer()
    {
        Entered = false;
    }
}