using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeLeft : MonoBehaviour {

    float StartTime;
    Text Text;

    public int TotalTime = 300;//in seconds

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


    void Start()
    {
        LeavesParticleEffectStart = false;
        GrayScaleColor = Color.black;

        Rend0 = Tree_Cylinder0.GetComponent<Renderer>();
        Rend1 = Tree_Cylinder1.GetComponent<Renderer>();
        Rend2 = Tree_Cylinder2.GetComponent<Renderer>();

        StartTime = Time.time;
        Text = GetComponent<Text>();
        RhythmCheck = Player.GetComponent<RhythmCheck>();
    }
	
	void Update ()
    {
        int min = (int)((TotalTime - (Time.time - StartTime)) / 60);
        int sec = (int)((TotalTime - (Time.time - StartTime)) % 60);
        Text.text = "Time Left\n" + min.ToString("00") + " : " + sec.ToString("00");

        if ((Time.time - StartTime) >= 135f && LeavesParticleEffectStart == false)
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

            Debug.Log(GrayScaleColor);
        }
        else
        {
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
