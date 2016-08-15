using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
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
        Debug.Log("clicked");
        Application.LoadLevel("Game");
    }

    public void enterTimer()
    {
        Debug.Log("Entered");
        Entered = true;
        ExhaleStartTime = Time.time;
    }

    public void exitTimer()
    {
        Debug.Log("Exited");
        Entered = false;
    }
}
