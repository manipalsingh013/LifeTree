using UnityEngine;
using System.Collections;

public class stateChangeIndicator : MonoBehaviour {

	public float timeToLive = 7.0f;

	// Use this for initialization
	void Start () {
		Invoke ("DestroySelf", timeToLive);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void DestroySelf(){
		Destroy (this.gameObject);
	}
}
