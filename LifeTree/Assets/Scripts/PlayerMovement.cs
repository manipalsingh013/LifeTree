using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float RaduisOfMovement;

    public GameObject CameraHead;
    Transform CameraHeadTransform;

    public GameObject MicController;

    MicControl MicControl;

    float PlayerSpeed;
    public float MaxSpeedOfPlayer;

    bool BadBodyPositionToBreathe;
    bool Vibrated;

    void Start()
    {
        BadBodyPositionToBreathe = false;
        Vibrated = false;

        CameraHeadTransform = CameraHead.GetComponent<Transform>();

        PlayerSpeed = 0f;

        MicControl = MicController.GetComponent<MicControl>();

    }

    void Update()
    {
        //Setting player speed
        if (MicControl.loudness > 0.01)
            PlayerSpeed += 0.005f;
        else
            PlayerSpeed -= 0.005f;

        // setting constraints on player's speed
        if (PlayerSpeed > MaxSpeedOfPlayer)
            PlayerSpeed = MaxSpeedOfPlayer;
        if (PlayerSpeed <= 0f)
            PlayerSpeed = 0f;

        //constrain motion of player inside sphere of radius.
        if ((transform.position + CameraHeadTransform.forward * PlayerSpeed).magnitude <= RaduisOfMovement)
        {
            if ((transform.position + CameraHeadTransform.forward * PlayerSpeed).y >= 0)
            {
                transform.position += CameraHeadTransform.forward * PlayerSpeed;
            }
        }


        //Vibrate handlet if if layer sees down
        if (CameraHeadTransform.localEulerAngles.x >= 45 && CameraHeadTransform.localEulerAngles.x <= 90)
        {
            BadBodyPositionToBreathe = true;
        }
        else
        {
            BadBodyPositionToBreathe = false;
            Vibrated = false;
        }

        if (BadBodyPositionToBreathe && Vibrated == false)
        {
            Vibrated = true;
            Handheld.Vibrate();
            //Debug.Log("Vibrate");
        }

    }
}
