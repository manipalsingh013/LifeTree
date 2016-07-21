using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RhythmCheck : MonoBehaviour
{
    public bool BreathingIsInRyhthem;


    GameObject micController;
    public int exhaleCount = 0;
    float getLoudness;

    public bool exhale;
    
    public float previousExhaleTime, currentExhaleTime;
    float exhaleTime, exhaleStartTime;

    float awakeTime;
    
    float RhythemTrueTime;
    bool rhythem;


    public Text temp;

    // Use this for initialization
    void Awake()
    {
        BreathingIsInRyhthem = true;

        RhythemTrueTime = Time.time;
        rhythem = true;

        previousExhaleTime = 0f;
        currentExhaleTime = 0f;
        
        awakeTime = Time.time;
    
        micController = GameObject.FindWithTag("mic");
        exhale = false;
        exhaleCount = 0;
        micController.GetComponent<MicControl>().StopMicrophone();
        micController.GetComponent<MicControl>().StartMicrophone();

    }
    void Update()
    {
        getLoudness = micController.GetComponent<MicControl>().loudness;

        if (getLoudness > 0.01 && exhale == true)
        {
            exhaleCount += 1;
            exhaleStartTime = Time.time;
            exhale = false;
        }
        if (getLoudness < 0.01)
        {
            if (exhale == false)
            {
                exhaleTime = Time.time - exhaleStartTime;

                previousExhaleTime = currentExhaleTime;
                currentExhaleTime = exhaleTime;
            }
            exhale = true;
        }


        if (currentExhaleTime - previousExhaleTime >= -1.0f && currentExhaleTime - previousExhaleTime <= 1.0f && currentExhaleTime > 1.5f)
        {
            // play positive messages at a min of 9 second interval
            temp.text = "Breathing is in rhythem";
            BreathingIsInRyhthem = true;
            if (rhythem == true)
            {
                rhythem = false;
                RhythemTrueTime = Time.time;
            }
        }
        else if (Time.time - RhythemTrueTime >= 7f || (((currentExhaleTime - previousExhaleTime <= -2.0f && currentExhaleTime - previousExhaleTime >= 2.0f) || currentExhaleTime < 0.5f)))
        {
            rhythem = false;//not actually rhythem but !rhythem
            if (getLoudness <= 0.01)
            {
                BreathingIsInRyhthem = false;
                temp.text = "Breathing is not in rhythem";
            }
        }
    }
}