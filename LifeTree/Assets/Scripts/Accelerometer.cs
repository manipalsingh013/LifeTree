using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Accelerometer : MonoBehaviour
{

    public Text Temp;
    bool PlayerSeated;

    void Start()
    {
        PlayerSeated = false;
    }

    void Update()
    {
        //Temp.text = Input.acceleration.x + " " + Input.acceleration.y + "  " + Input.acceleration.z ;
        if (Input.acceleration.x <= -1.5f && !PlayerSeated)
        {
            PlayerSeated = true;
            Temp.text = "greater than -1.5";
            Handheld.Vibrate();
        }
        if(Input.acceleration.x >-1.5 && Input.acceleration.x<-1.0f && !PlayerSeated)
        {
            Temp.text = "between -1.0 and -1.5";
        }

        //transform.Translate(Input.acceleration.x, 0, -Input.acceleration.z);
    }
}