using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FeedbackText : MonoBehaviour {

    public static string Feedback;
    Text Text;

	void Start ()
    {
	    Text = GetComponent<Text>();
        Text.text = Feedback;
	}
	
	void Update ()
    {
	
	}
}
