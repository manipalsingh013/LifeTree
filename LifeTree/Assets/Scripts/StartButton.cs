using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject MainMenu;

    GameObject MicController;
    MicControl MicControl;

    public GameObject SitDownMenu;

    float ExhaleStartTime;
    float ExitTime;
    bool Entered;

    bool Exhale;

    public GameObject Player;
    public Text TimeText;
    public GameObject Water;

    void Start()
    {
        MicController = GameObject.FindWithTag("mic");

        MicControl = MicController.GetComponent<MicControl>();
        Entered = false;
        Debug.Log("GameData.PlayerSeated" + GameData.PlayerSeated);
    }

    void Update()
    {
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
            OnClickStartButton();
        }
    }

    public void OnClickStartButton()
    {
        if (GameData.PlayerSeated == true)
        {
            Player.GetComponent<PlayerMovement>().enabled = true;
            Player.GetComponent<RhythmCheck>().enabled = true;

            TimeText.gameObject.SetActive(true);

            Water.GetComponent<IncreaseWaterLevel>().enabled = true;

            //this.GetComponent<AddOxygen>().enabled = true;
            this.GetComponent<StartButton>().enabled = false;

            MainMenu.SetActive(false);
        }
        else
        {
            MainMenu.SetActive(false);

            SitDownMenu.SetActive(true);
            GameData.PlayerSeated = true;
            this.GetComponent<StartButton>().enabled = false;
        }
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
