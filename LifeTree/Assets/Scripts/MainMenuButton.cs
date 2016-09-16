using UnityEngine;
using System.Collections;

public class MainMenuButton : MonoBehaviour
{
    GameObject MicController;
    MicControl MicControl;

    float ExhaleStartTime;
    float ExitTime;
    bool Entered;

    bool Exhale;

    void Start()
    {
        MicController = GameObject.FindWithTag("mic");

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
            OnClickStartButton();
        }
    }

    public void OnClickStartButton()
    {
        Application.LoadLevel("Game");
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
