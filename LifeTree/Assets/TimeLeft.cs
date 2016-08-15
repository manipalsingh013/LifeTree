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


    void Start()
    {
        StartTime = Time.time;
        Text = GetComponent<Text>();
        RhythmCheck = Player.GetComponent<RhythmCheck>();
    }
	
	void Update ()
    {
        int min = (int)((TotalTime - (Time.time - StartTime)) / 60);
        int sec = (int)((TotalTime - (Time.time - StartTime)) % 60);
        Text.text = "Time Left\n" + min.ToString("00") + " : " + sec.ToString("00");

        if (TotalTime - (Time.time - StartTime) <= 0)
        {
            FeedbackText.Feedback = "Rhythmic Breath :" + RhythmCheck.RhythmicBreathCount + "\n" +
                                    "Non Rhythmic Breath :" + RhythmCheck.NonRhythmicBreathCount;
            //move to some other scene
            Application.LoadLevel("GameFeedback");
            //MainMenu.SetActive(true);
            //gameObject.SetActive(false);
        }
	}
}
