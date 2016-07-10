using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TestGraphMovement : MonoBehaviour {

	public enum GraphType{Loudness, Variance};

	public GraphType gType;

	public float maxY;
	public float minY;

	public float minX;
	public float maxX;
	public float xSpeed;

	private float prevLoudness = 0f;
	private float varianceBuffer = 0f;

	public MicControl micControl;

	public Text loudnessText;


	//Spremenljivke za vizualizacijo
	public GameObject stateChangeIndicator;

	void OnEnable(){
		BreathingEvents.onExhale += exhaleStarted;
		BreathingEvents.onInhale += inhaleStarted;
	}

	void OnDisable(){
		BreathingEvents.onExhale -= exhaleStarted;
		BreathingEvents.onInhale -= inhaleStarted;
	}


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		loudnessText.text = "Loudness: " + Mathf.Round (micControl.loudness*100.0f)/100.0f;

		switch (gType) {
			case (GraphType.Loudness):
				this.transform.position = new Vector3 (this.transform.position.x + xSpeed * Time.deltaTime, minY + (maxY - minY) * micControl.loudness, this.transform.position.z);

				if (this.transform.position.x >= maxX) {
					this.transform.position = new Vector3(minX, this.transform.position.y, this.transform.position.z);
				}
				break;

			case (GraphType.Variance):
			this.transform.position = new Vector3 (this.transform.position.x + xSpeed * Time.deltaTime, minY + (maxY - minY) * getVariance(), this.transform.position.z);

				if (this.transform.position.x >= maxX) {
					this.transform.position = new Vector3(minX, this.transform.position.y, this.transform.position.z);
				}
				break;


			default:
				Debug.Log ("This shouldn't happen");
				break;
		}


	}

	float getVariance(){
		varianceBuffer = micControl.loudness - prevLoudness;
		prevLoudness = micControl.loudness;
		return varianceBuffer;
	}

	void exhaleStarted(){
		Instantiate (stateChangeIndicator, this.transform.position, Quaternion.identity);
	}

	void inhaleStarted(){
		Instantiate (stateChangeIndicator, this.transform.position, Quaternion.identity);

	}
}
