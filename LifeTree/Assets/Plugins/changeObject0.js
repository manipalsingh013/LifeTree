#pragma strict

var micController:GameObject;

function Start () {
	GetComponent(SpriteRenderer).enabled = true;
}

function Update () {

//Calls the loudness value of selected controller (in this case the controller in micController variable).
var getLoudness=micController.GetComponent(MicControl).loudness;

	if (getLoudness > 0.01) {
		GetComponent(SpriteRenderer).enabled = false;
	}
	
	if (getLoudness < 0.01) {
		GetComponent(SpriteRenderer).enabled = true;
	}
}