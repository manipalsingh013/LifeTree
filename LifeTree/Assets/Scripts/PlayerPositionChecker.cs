using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPositionChecker : MonoBehaviour
{
    //public Text Temp;

    public GameObject Player;
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
                StartGame();

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
                // Player is seated;
                PlayTreeSittingAnimation();//provide it proper time to play before turning on any other animations;

                PlayerSeated = true;
                StartGame();
            }
        }
    }

    void PlayTreeSittingAnimation()
    {
        //Play animtion of tree in which tree is sitting
    }
    

    void StartGame()
    {
        Player.GetComponent<PlayerMovement>().enabled = true;
        Player.GetComponent<RhythmCheck>().enabled = true;

        this.GetComponent<AddOxygen>().enabled = true;

        this.GetComponent<StartButton>().enabled = false;

        SitDownMenu.SetActive(false);


        //After player is seated start the game
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