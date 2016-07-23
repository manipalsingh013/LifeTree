using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour
{
    public GameObject MicController;
    MicControl MicControl;
   
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
            OnClickExitButton();
        }
    }

    public void OnClickExitButton()
    {
        Application.Quit();
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
