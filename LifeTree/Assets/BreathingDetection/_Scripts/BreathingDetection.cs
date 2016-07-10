using UnityEngine;
using System.Collections;

[RequireComponent(typeof (MicControl))]
public class BreathingDetection : MonoBehaviour {

	enum Breathing{Inhale, Exhale}; //Two possible states

	private MicControl micControl; //microphone controller for loudness and variance calculation

	private float prevLoudness = 0f; //loudness in previous frame
	private float variance = 0f; //variance of current frame


	private Breathing currentState = Breathing.Inhale; 

	//Spremenljivke za histerezo
	public float exhaleLoudnessThreshold;
	public float inhaleLoudnessThreshold;

	public float exhaleVarianceThreshold;
	public float inhaleVarianceThreshold;

	private int varianceUnderThresholdCounter = 0; //counts how many frames the variance spent under threshold
	private bool fastExhalePossible = false;

	// Use this for initialization
	void Start () {
		micControl = this.GetComponent<MicControl> ();
		if (micControl == null) {
			Debug.LogError("Cannot find MicControl attached to this object.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateVariance ();
		//Debug.Log ("Variance: " + variance);	
		//Debug.Log ("CurrentState: " + currentState.ToString());
		switch (currentState) {

			case (Breathing.Inhale):
				checkIfExhaling();
				break;
				
			case (Breathing.Exhale):
				checkIfInhaling();
				break;
			default:
				Debug.Log ("This should never ever happen.");
				break;
		}
	}

	void checkIfExhaling(){
		


		if (currentState == Breathing.Inhale) {
			//Exhaling if loudness higher than threshold && variance over threshold && next 2 variances under threshold
			if (micControl.loudness > exhaleLoudnessThreshold && variance > exhaleVarianceThreshold) {

				fastExhalePossible = true;

			}

			if ((fastExhalePossible && varianceUnderThresholdCounter >= 2) || 
				(micControl.loudness > exhaleLoudnessThreshold && varianceUnderThresholdCounter > 8)) { //ALI moč precej velika && zadnjihnekaj varianc pod thresholdom
				varianceUnderThresholdCounter = 0;
				fastExhalePossible = false;

				currentState = Breathing.Exhale; //Change state to exhaling
				BreathingEvents.TriggerOnExhale (); //Trigger onExhale event
			} 

		}
	}

	void checkIfInhaling(){
		//Moč pod  exhale thresholdom && varianca < inhale threshold
		//ALI loudness občutno pod thresholdom
		if (currentState == Breathing.Exhale && ((micControl.loudness < exhaleLoudnessThreshold && variance < inhaleVarianceThreshold) ||
			micControl.loudness < inhaleLoudnessThreshold)) {

			currentState = Breathing.Inhale; //Change state to inhaling
			
			BreathingEvents.TriggerOnInhale(); //Trigger onInhale event
			
		}
	}

	void updateVariance(){
		variance = micControl.loudness - prevLoudness;
		prevLoudness = micControl.loudness;


		//update variance counter
		if (variance < exhaleVarianceThreshold) {
				varianceUnderThresholdCounter++;

		} else {
			varianceUnderThresholdCounter = 0;
		}
	}
}
