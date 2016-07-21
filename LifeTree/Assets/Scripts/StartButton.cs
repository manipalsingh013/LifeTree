using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour
{
    public GameObject MainMenu;

    public GameObject MicController;
    MicControl MicControl;

    public GameObject Player;

    float ExhaleStartTime;
    float ExitTime;
    bool Entered;

    bool Exhale;

    void Start()
    {
        MicControl = MicController.GetComponent<MicControl>();
        Entered = false;
    }

    void Update()
    {
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
            OnClick();
        }
    }

    public void OnClick()
    {
        MainMenu.SetActive(false);

        Player.GetComponent<PlayerMovement>().enabled = true;
        Player.GetComponent<RhythmCheck>().enabled = true;

        this.GetComponent<AddOxygen>().enabled = true;

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
