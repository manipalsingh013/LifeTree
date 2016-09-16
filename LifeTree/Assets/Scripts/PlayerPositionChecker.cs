using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPositionChecker : MonoBehaviour
{
    //public Text Temp;
    public GameObject MainMenu;

    public GameObject Player;
    public GameObject SitDownMenu;

    //public GameObject TimeText;
    //public GameObject Water;

    GameObject MicController;
    MicControl MicControl;

    float ExhaleStartTime;
    float ExitTime;
    bool Entered;

    bool Exhale;


    void Start()
    {
        MicController = GameObject.FindWithTag("mic");
        GameData.PlayerSeated = false;
        
        MicControl = MicController.GetComponent<MicControl>();
        Entered = false;

        //if (GameData.PlayerSeated == true)
        //{
        //    Debug.Log(GameData.PlayerSeated);
        //    PlayerSeated = true;
        //    StartGame();

        //    GetComponent<PlayerPositionChecker>().enabled = false;
        //}
    }

    void Update()
    {
        //Temp.text = Input.acceleration.y + " " + Input.acceleration.y + "  " + Input.acceleration.z ;
        if (!GameData.PlayerSeated)
        {
            if (Input.acceleration.y <= -1.4f)
            {
                GameData.PlayerSeated = true;
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

                GameData.PlayerSeated = true;
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
        //After player is seated start the game
        Player.GetComponent<PlayerMovement>().enabled = true;
        Player.GetComponent<RhythmCheck>().enabled = true;

        //TimeText.SetActive(true);

        //Water.GetComponent<IncreaseWaterLevel>().enabled = true;
        
        this.GetComponent<StartButton>().enabled = true;
        MainMenu.SetActive(true);
        SitDownMenu.SetActive(false);

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