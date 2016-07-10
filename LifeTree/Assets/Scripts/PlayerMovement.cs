using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public GameObject cameraHead;
    Transform cameraHeadTransform;

    public GameObject MicController;

    MicControl MicControl;

    float PlayerSpeed;

    void Start()
    {
        cameraHeadTransform = cameraHead.GetComponent<Transform>();

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
        if (PlayerSpeed > 3f)
            PlayerSpeed = 3f;
        if (PlayerSpeed <= 0f)
            PlayerSpeed = 0f;

        //constrain motion of player inside sphere of radius 15.
        if ((transform.position + cameraHeadTransform.forward * PlayerSpeed).magnitude <= 15f)
        {
            transform.position += cameraHeadTransform.forward * PlayerSpeed;
        }
        
    }

    void OnTriggerEnter()
    {
        Debug.Log("enter");
    }



}
