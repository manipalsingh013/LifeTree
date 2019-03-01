using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class TimeLeft : MonoBehaviour {

    float StartTime;
    Text Text;

    public int TotalTime = 300;//in seconds
    public bool StartBlurringEffect;

    public GameObject MainMenu;
    public GameObject Player;
    RhythmCheck RhythmCheck;

    public GameObject FeedbackMenu;
    public Text FeedbackText;

    public GameObject Tree_Cylinder0;
    public GameObject Tree_Cylinder1;
    public GameObject Tree_Cylinder2;
    Renderer Rend0;
    Renderer Rend1;
    Renderer Rend2;
    Color GrayScaleColor;

    public GameObject FallingLeaves01;
    public GameObject FallingLeaves02;
    public GameObject FallingLeaves03;
    public GameObject FallingLeaves04;
    bool LeavesParticleEffectStart;

    public GameObject LifeTreeAudioSource;
    bool BackgroundMusicEnabled = false;

    AudioSource PursedLipBreathing;
    bool PursedLipBreathingMusicEnabled;

    public GameObject EndMusic;

    public GameObject MainCameraRight;
    public GameObject MainCameraLeft;
    BlurOptimized BlurOptimizedRight;
    BlurOptimized BlurOptimizedLeft;
    void Start()
    {
        BlurOptimizedLeft = MainCameraLeft.GetComponent<BlurOptimized>();
        BlurOptimizedRight = MainCameraRight.GetComponent<BlurOptimized>();

        PursedLipBreathing = GetComponent<AudioSource>();
        PursedLipBreathing.enabled = false;
        PursedLipBreathingMusicEnabled = false;

        BackgroundMusicEnabled = false;
        this.GetComponent<AudioSource>().Play();
        LifeTreeAudioSource.SetActive(false);
        StartBlurringEffect = false;

        LeavesParticleEffectStart = false;
        GrayScaleColor = Color.black;

        Rend0 = Tree_Cylinder0.GetComponent<Renderer>();
        Rend1 = Tree_Cylinder1.GetComponent<Renderer>();
        Rend2 = Tree_Cylinder2.GetComponent<Renderer>();

        StartTime = Time.time;
        Text = GetComponent<Text>();
        RhythmCheck = Player.GetComponent<RhythmCheck>();
        Text.text = "";
    }
	
	void Update ()
    {
        int min = (int)((TotalTime - (Time.time - StartTime)) / 60);
        int sec = (int)((TotalTime - (Time.time - StartTime)) % 60);
        //Text.text = "Practice Time Left\n" + min.ToString("00") + " : " + sec.ToString("00");

        if (Time.time - StartTime > 20f && !StartBlurringEffect)
        {
            StartBlurringEffect = true;
        }

        if (Time.time - StartTime > 25f && !BackgroundMusicEnabled)
        {
            BackgroundMusicEnabled = true;
            LifeTreeAudioSource.SetActive(true);
        }

        if (Time.time -StartTime > 60.52f)
        {
            PursedLipBreathing.enabled = false;
        }

        if (Time.time - StartTime > 2f && !PursedLipBreathingMusicEnabled)
        {
            PursedLipBreathingMusicEnabled = true;
            PursedLipBreathing.enabled = true;
            PursedLipBreathing.Play();
        }


        if ((Time.time - StartTime) >= 130f && LeavesParticleEffectStart == false)
        {
            //75% of time is done, start leaves particles effect.
            FallingLeaves01.SetActive(true);
            FallingLeaves02.SetActive(true);
            FallingLeaves03.SetActive(true);
            FallingLeaves04.SetActive(true);

            LeavesParticleEffectStart = true;
        }


        if (TotalTime - (Time.time - StartTime) > 0)
        {
            float colorValue = 1 - ( (TotalTime - (Time.time - StartTime)) / TotalTime);

            GrayScaleColor = new Color(colorValue, colorValue, colorValue);
            for (int i = 0; i < 4; i++)
            {
                Rend0.materials[i].SetColor("_Color", GrayScaleColor);
                Rend1.materials[i].SetColor("_Color", GrayScaleColor);
                Rend2.materials[i].SetColor("_Color", GrayScaleColor);
            }

            //Debug.Log(GrayScaleColor);
        }
        else
        {
            RhythmCheck.enabled = false;

            BlurOptimizedLeft.enabled = false;
            BlurOptimizedRight.enabled = false;

            EndMusic.SetActive(true);

            FeedbackText.text = "Rhythmic Breath :" + RhythmCheck.RhythmicBreathCount + "\n" +
                                    "Non Rhythmic Breath :" + RhythmCheck.NonRhythmicBreathCount;

            FeedbackMenu.SetActive(true);

            this.gameObject.GetComponent<TimeLeft>().enabled = false;
            //move to some other scene
            //Application.LoadLevel("Game");
            //MainMenu.SetActive(true);
            //gameObject.SetActive(false);


        }
	}
}
