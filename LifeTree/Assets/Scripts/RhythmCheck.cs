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

    public float GrayscaleChangeFactor;

    //public Text temp;

    public int RhythmicBreathCount = 0;
    public int NonRhythmicBreathCount = 0;

    // Use this for initialization
    void Awake()
    {

        //color0[0] = rend.materials[0].color;
        //color0[1] = rend.materials[1].color;
        //color0[2] = rend.materials[2].color;
        //color0[3] = rend.materials[3].color;

        //// rend.material.shader = Shader.Find("Standard");
        //rend.materials[0].color += new Color(0.1f, 0.1f, 0.1f) + color0[0];
        //rend.materials[1].color += new Color(0.1f, 0.1f, 0.1f) + color0[1];
        //rend.materials[2].color += new Color(0.1f, 0.1f, 0.1f) + color0[2];
        //rend.materials[3].color += new Color(0.1f, 0.1f, 0.1f) + color0[3];

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
            //temp.text = "Breathing is in rhythem";
            RhythmicBreathCount++;

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
                ////temp.text = "Breathing is not in rhythem";

                //GrayScaleColor -= new Color(GrayscaleChangeFactor, GrayscaleChangeFactor, GrayscaleChangeFactor);
                //if (GrayScaleColor.r < 0)
                //    GrayScaleColor.r = 0;
                //if (GrayScaleColor.g < 0)
                //    GrayScaleColor.g = 0;
                //if (GrayScaleColor.b < 0)
                //    GrayScaleColor.b = 0;

                //for (int i = 0; i < 4; i++)
                //{
                //    Rend0.materials[i].SetColor("_Color", GrayScaleColor);
                //    Rend1.materials[i].SetColor("_Color", GrayScaleColor);
                //    Rend2.materials[i].SetColor("_Color", GrayScaleColor);
                //}

                NonRhythmicBreathCount++;
            }
        }
    }
}