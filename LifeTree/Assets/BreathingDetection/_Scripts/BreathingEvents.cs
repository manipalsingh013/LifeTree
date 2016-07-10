using UnityEngine;
using System.Collections;

public class BreathingEvents : MonoBehaviour {

	public static bool debugMessages = true;
	
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}


	/************************/
	/* BREATHING EVENTS */
	/************************/

	public delegate void BreathingAction();
	public static event BreathingAction onExhale;
	public static event BreathingAction onInhale;

	public static void TriggerOnExhale(){
		if (debugMessages) {
			Debug.Log ("Event triggered: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
		}
		
		if (onExhale != null) {
			onExhale();
		}
	}

	public static void TriggerOnInhale(){
		if (debugMessages) {
			Debug.Log ("Event triggered: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
		}
		
		if (onInhale != null) {
			onInhale();
		}
	}

}
