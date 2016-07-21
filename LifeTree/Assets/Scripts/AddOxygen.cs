using UnityEngine;
using System.Collections;

public class AddOxygen : MonoBehaviour {

    GameObject Player;
    RhythmCheck RhythmCheck;

    float OxygenAddTime = 2f;
    float LastOxygenAddedTime;

    bool StartAddingOxygen;

    int NumberOfOxygenInstantiated;
    public int MaxNumberOfOxygen;

	// Use this for initialization
	void Start ()
    {
        NumberOfOxygenInstantiated = 0;
        StartAddingOxygen = false;
        Player = GameObject.FindWithTag("Player");
        RhythmCheck = Player.GetComponent<RhythmCheck>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (RhythmCheck.BreathingIsInRyhthem && StartAddingOxygen == false)
        {
            StartAddingOxygen = true;
            LastOxygenAddedTime = Time.time;
        }

        if (RhythmCheck.BreathingIsInRyhthem == false)
        {
            StartAddingOxygen = false;
        }

        if (StartAddingOxygen)
        {
            if (Time.time > LastOxygenAddedTime + OxygenAddTime && NumberOfOxygenInstantiated < MaxNumberOfOxygen)
            {
                // Adding oxygen when breathing is in rhythem
                float z = Random.RandomRange(-400, 400) / 10f;
                float y = Random.RandomRange(10, 400) / 10f;
                float x = Random.RandomRange(-400, 400) / 10f;

                GameObject obj = Instantiate(Resources.Load("Oxygen")) as GameObject;
                obj.transform.position = new Vector3(x, y, z);

                LastOxygenAddedTime = Time.time;
                NumberOfOxygenInstantiated++;
            }
        }
	}
}
