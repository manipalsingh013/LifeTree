using UnityEngine;
using System.Collections;

public class FFT2 : MonoBehaviour {

	public const float freq = 24000f;
	const int WINDOW_SIZE = 1<<13;
	public float[] spectrum = new float[WINDOW_SIZE];




	public AudioSource audioSauce;
	public string CurrentAudioInput = "none";
	int deviceNum = 0;

	void Start()
	{

		string[] inputDevices = new string[Microphone.devices.Length];
		deviceNum = 0;

		for (int i = 0; i < Microphone.devices.Length; i++) {
			inputDevices [i] = Microphone.devices [i].ToString ();
			Debug.Log("Device " + i + ": " + inputDevices [i]);
		}
		CurrentAudioInput = Microphone.devices[deviceNum].ToString();
		StartMic ();
	}


	public void StartMic(){
		audioSauce.clip = Microphone.Start(CurrentAudioInput, true, 5, (int) freq); 
	}

	public void OnGUI(){
		GUI.Label (new Rect (10, 10, 400, 400), CurrentAudioInput);
	}

	void Update() {
		
		if (Input.GetKeyDown(KeyCode.S))
		{
			Debug.Log ("S pressed");
			Microphone.End (CurrentAudioInput);
			deviceNum+= 1;
			if (deviceNum > Microphone.devices.Length - 1)
				deviceNum = 0;
			CurrentAudioInput = Microphone.devices[deviceNum].ToString();

			StartMic ();
		}

		if (Input.GetKeyDown (KeyCode.A)) {
			Debug.Log ("A pressed");
			audioSauce.Play ();
		}

		float delay = 0.030f;
		int microphoneSamples = Microphone.GetPosition (CurrentAudioInput);

		if (microphoneSamples / freq > delay) {
			if (!audioSauce.isPlaying) {
				Debug.Log ("Starting thing");
				audioSauce.timeSamples = (int) (microphoneSamples - (delay * freq));
				audioSauce.Play ();
			}
		}
		audioSauce.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);

		int i = 1;
		while (i < WINDOW_SIZE) {
			Debug.DrawLine( new Vector3(i - 1, 50000f * spectrum[i - 1] + 10, 0), 
				new Vector3(i, 50000f * spectrum[i] + 10, 0), 
				Color.red, 10, false);
			Debug.DrawLine( new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2),
				new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2),
				Color.cyan, 10, false);
			Debug.DrawLine( new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), 
				new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), 
				Color.green, 10, false);
			Debug.DrawLine( new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), 
				new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), 
				Color.yellow, 10, false);
			i++;
		}
}
}
