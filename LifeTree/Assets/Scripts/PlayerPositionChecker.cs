using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPositionChecker : MonoBehaviour
{
    //public Text Temp;
    public GameObject MainMenu;
    public GameObject SitDownMenu;

    //public GameObject TimeText;
    //public GameObject Water;

    GameObject MicController;
    MicControl MicControl;

    float ExhaleStartTime;
    float ExitTime;
    bool Entered;

    bool Exhale;

    public GameObject Tree;
    Animation TreeAnimation;
    public bool SitOrStandAnimationPlaying;
    public float FrameRateOfAnimation;
    float AnimationStartTime;
    bool SitDownAnimationPlayed;

    void Start()
    {
        SitDownAnimationPlayed = false;
        AnimationStartTime = 0f;
        SitOrStandAnimationPlaying = false;
        TreeAnimation = Tree.GetComponent<Animation>();

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

        if (GameData.PlayerSeated && TreeAnimation["Take 001"].time >= 325 / 25)
        {
            SitOrStandAnimationPlaying = false;
            TreeAnimation["Take 001"].speed = 0f;
        }

        if (!GameData.PlayerSeated && TreeAnimation["Take 001"].time >= 275 / 25)
        {
            SitOrStandAnimationPlaying = false;
            TreeAnimation["Take 001"].speed = 0f;
        }

        //Temp.text = Input.acceleration.y + " " + Input.acceleration.y + "  " + Input.acceleration.z ;
        if (!GameData.PlayerSeated)
        {
            if (Input.acceleration.y <= -1.1f)
            {
                GameData.PlayerSeated = true;
                PlayTreeSitDownAnimation();
                AnimationStartTime = Time.time;

                SitDownMenu.SetActive(false);
                //Handheld.Vibrate();
            }

            if (MicControl.loudness > 1f && !Exhale)
            {
                Exhale = true;
                ExhaleStartTime = Time.time;
            }
            if (MicControl.loudness <= 1f)
            {
                Exhale = false;
            }

            if (Entered && Time.time - ExhaleStartTime > 2f && Exhale)
            {
                // Player is seated;
                //provide it proper time to play before turning on any other animations;

                GameData.PlayerSeated = true;
                PlayTreeSitDownAnimation();
                AnimationStartTime = Time.time;

                SitDownMenu.SetActive(false);
            }
        }

        if (GameData.PlayerSeated == true && !SitDownAnimationPlayed && Time.time>AnimationStartTime+2f)
        {
            StartGame();
            SitOrStandAnimationPlaying = true;
            SitDownAnimationPlayed = true;
        }
    }
    void StartGame()
    {
        //After player is seated start the game

        //TimeText.SetActive(true);

        //Water.GetComponent<IncreaseWaterLevel>().enabled = true;
        
        this.GetComponent<StartButton>().enabled = true;
        MainMenu.SetActive(true);
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


    void PlayTreeSitDownAnimation()
    {
        TreeAnimation.Play("Take 001");
        TreeAnimation["Take 001"].speed = (FrameRateOfAnimation)/25f;
        TreeAnimation["Take 001"].time = 300 / 25;
    }

    void PlayTreeStandUpAnimation()
    {
        TreeAnimation.Play("Take 001");
        TreeAnimation["Take 001"].speed = (FrameRateOfAnimation)/25f;
        TreeAnimation["Take 001"].time = 250 / 25;
    }
}