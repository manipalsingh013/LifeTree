using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

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

    public GameObject TimeLeftObject;
    TimeLeft TimeLeft;
    public GameObject MainCameraRight;
    public GameObject MainCameraLeft;
    BlurOptimized BlurOptimizedRight;
    BlurOptimized BlurOptimizedLeft;
    float BlurAmount;

    public GameObject FallingLeaves1;
    public GameObject FallingLeaves2;
    ParticleSystem LeavesParticleSystem1;
    ParticleSystem LeavesParticleSystem2;

    // Use this for initialization
    void Awake()
    {
        LeavesParticleSystem1 = FallingLeaves1.GetComponent<ParticleSystem>();
        LeavesParticleSystem2 = FallingLeaves2.GetComponent<ParticleSystem>();
        LeavesParticleSystem1.maxParticles = 0;
        LeavesParticleSystem2.maxParticles = 0;
        LeavesParticleSystem1.startColor = new Color(255, 255, 255);
        LeavesParticleSystem2.startColor = new Color(255, 255, 255);

        TimeLeft = TimeLeftObject.GetComponent<TimeLeft>();

        BlurAmount = 0;
        BlurOptimizedLeft = MainCameraLeft.GetComponent<BlurOptimized>();
        BlurOptimizedRight = MainCameraRight.GetComponent<BlurOptimized>();

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

        if (getLoudness > 1 && exhale == true)
        {
            exhaleCount += 1;
            exhaleStartTime = Time.time;
            exhale = false;
        }
        if (getLoudness < 1)
        {
            if (exhale == false)
            {
                exhaleTime = Time.time - exhaleStartTime;

                previousExhaleTime = currentExhaleTime;
                currentExhaleTime = exhaleTime;
            }
            exhale = true;
        }

        if (getLoudness >= 1)
        {
            if (LeavesParticleSystem1.maxParticles < 200)
            {
                LeavesParticleSystem1.maxParticles += 10;
                LeavesParticleSystem2.maxParticles += 10;
            }
        }
        else
        {
            if (LeavesParticleSystem1.maxParticles > 0)
            {
                LeavesParticleSystem1.maxParticles -= 10;
                LeavesParticleSystem2.maxParticles -= 10;
            }
        }


        if (currentExhaleTime - previousExhaleTime >= -1.0f && currentExhaleTime - previousExhaleTime <= 1.0f && currentExhaleTime > 1.5f)
        {
            // play positive messages at a min of 9 second interval
            //temp.text = "Breathing is in rhythem";
            LeavesParticleSystem1.startColor = new Color(0,160,255);
            LeavesParticleSystem2.startColor = new Color(0, 160, 255);

            if (TimeLeft.StartBlurringEffect)
            {
                DecreaseBurring();
            }
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
            if (getLoudness <= 1)
            {
                BreathingIsInRyhthem = false;
                ////temp.text = "Breathing is not in rhythem";
                LeavesParticleSystem1.startColor = new Color(255, 255, 255);
                LeavesParticleSystem2.startColor = new Color(255, 255, 255);

                if (TimeLeft.StartBlurringEffect)
                {
                    IncreaseBlurring();
                }
                NonRhythmicBreathCount++;
            }
        }
    }

    void IncreaseBlurring()
    {
        BlurAmount += 0.03f;
        if (BlurAmount > 3)
            BlurAmount = 3f;

        BlurOptimizedLeft.blurSize = BlurAmount;
        BlurOptimizedRight.blurSize = BlurAmount;

        BlurOptimizedLeft.enabled = true;
        BlurOptimizedRight.enabled = true;
    }

    void DecreaseBurring()
    {
        BlurAmount -= 0.03f;
        if (BlurAmount < 0)
            BlurAmount = 0;

        BlurOptimizedLeft.blurSize = BlurAmount;
        BlurOptimizedRight.blurSize = BlurAmount;

        if (BlurAmount == 0)
        {
            BlurOptimizedLeft.enabled = false;
            BlurOptimizedRight.enabled = false;
        }
    }
}