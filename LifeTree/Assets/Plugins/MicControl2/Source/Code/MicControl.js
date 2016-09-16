#pragma strict


import UnityEngine.Audio;

@script RequireComponent(AudioSource)

#if UNITY_EDITOR
import System.IO;
@ExecuteInEditMode
#endif

//if false the below will override and set the mic selected in the editor
 //Select the microphone you want to use (supported up to 6 to choose from). If the device has number 1 in the console, you should select default as it is the first defice to be found.
enum Devices {Slot1, Slot2, Slot3, Slot4, Slot5, Slot6}

var InputDevice : Devices;
private var selectedDevice:String;
private var SourceContainer:AudioSource;

var advanced:boolean=false;

var audioSource:AudioSource;
//The maximum amount of sample data that gets loaded in, best is to leave it on 256, unless you know what you are doing. A higher number gives more accuracy but 
//lowers performance allot, it is best to leave it at 256.
var amountSamples:int=1024;

var loudness:float;
var rawInput:float;

var fftSpectrum:float[];

var sensitivity:float=1;


enum freqList {_44100HzCD,_48000HzDVD}
var freq: freqList;
var maxFreq: int=44100;
var minFreq: int=0;
 

var Mute:boolean=true;
var debug:boolean=false;
var ShowDeviceName:boolean=true;
private var micSelected:boolean=false; 

private var recording:boolean=true; 

/*
var :Transform;
var VolumeFallOff:float=1;
var PanThreshold:float=1;
private var ListenerDistance:float;
private var ListenerPosition:Vector3;
*/
var focused:boolean=false;
private var Initialised:boolean=false;


var doNotDestroyOnLoad:boolean=false;



function Start () {

//make this controller persistent
if(doNotDestroyOnLoad){
DontDestroyOnLoad (transform.gameObject);
	}


//return and throw error if no device is connected
 if(Microphone.devices.Length==0){
 	Debug.LogError("No connected device detected! Connect at least one device.");
 		Debug.LogError("No usable device detected! Try setting your device as the system's default.");
 			return;
		 		}

   	// Request permission to use the microphone on a mobile device or WebGLPlayer.
 	if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.WP8Player){
		yield Application.RequestUserAuthorization (UserAuthorization.Microphone);
		if (Application.HasUserAuthorization(UserAuthorization.Microphone)){
			InitMic();
				Initialised=true;
					}

		//if no permission is granted do not execute script
		else{
			return;
		}
  }
  else{
  InitMic();
  Initialised=true;
  }
  


}

 
//apply the mic input data stream to a float;
function Update () {
//pause everything when not focused on the app and then re-initialize.
if (!focused){
			StopMicrophone();
			Initialised=false;
			if(debug){
				Debug.Log("mic stopped");
					}
						return;
							}


		if (!Application.isPlaying) {
		//stop the microphone if you are clicking inside the editor and the player is stopped
			StopMicrophone();
				Initialised=false;
						if(debug){
							Debug.Log("mic stopped");
								}
									return;
										}

		if(focused){
			if(!Initialised){
					InitMic();
					if(debug){
				 Debug.Log("mic started");
				 }
					Initialised=true;
		
						}
							}


if(focused){

if(Microphone.IsRecording(selectedDevice) && recording){
  loudness = GetDataStream()*sensitivity;
  rawInput = GetDataStream();

  GetSpectrumDataStream();


  }
/*   if(debug){
   Debug.Log(loudness);
  }*/

  //Make sure the AudioSource volume is always 1
  audioSource.volume = 1;


}

}
 
 function GetSpectrumDataStream(){

 	if(Microphone.IsRecording(selectedDevice)){
 	//var spectrum: float[] = new float[1024];
 	var test: float = 0.0f;
 	audioSource.GetSpectrumData(fftSpectrum,  0, FFTWindow.Hamming);
 	//Debug.Log("SpectrumData received");
	 	var i: int = 1;
			while ( i < fftSpectrum.Length - 1 ) {
				Debug.DrawLine(new Vector3(i - 1, fftSpectrum[i] + 10, 0), new Vector3(i, fftSpectrum[i + 1] + 10, 0), Color.red );
				Debug.DrawLine(new Vector3(i - 1, 10*Mathf.Log(fftSpectrum[i - 1]) + 10, 2), new Vector3(i, 10*Mathf.Log(fftSpectrum[i]) + 10, 2), Color.cyan);
				Debug.DrawLine(new Vector3(Mathf.Log(i - 1), fftSpectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), fftSpectrum[i] - 10, 1), Color.green);
				Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(fftSpectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(fftSpectrum[i]), 3), Color.yellow);
				i++;
		}
		SendMessage("updateSpectrumData", fftSpectrum);
 	}
 }

 public function returnSpectrumData(){
 	return fftSpectrum;
 }


function GetDataStream():float{



if(Microphone.IsRecording(selectedDevice)){
  
   var dataStream: float[]  = new float[amountSamples];
       var audioValue: float = 0;
          audioSource.GetOutputData(dataStream,0);
  
        for(var i:float in dataStream){
            audioValue += Mathf.Abs(i);
        }
        return audioValue/amountSamples;
        }

 
 

  
}
 
 
 
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Initialize microphone
  function InitMic(){

//select audio source
if(!audioSource){
  audioSource = transform.GetComponent.<AudioSource>();
	} 

 //only Initialize microphone if a device is detected
if(Microphone.devices.Length>=0){

var i=0;
//count amount of devices connected
for(device in Microphone.devices){
i++;
}



//select the device if possible else give error
if(InputDevice==Devices.Slot1){
if(i>=1){
selectedDevice= Microphone.devices[0];
}
else{
Debug.LogError ("No device detected on this slot. Check Detected devices for slot numbers");
return;
}
 
}
 
 
if(InputDevice==Devices.Slot2){
if(i>=2){
selectedDevice= Microphone.devices[1];
}
else{
Debug.LogError ("No device detected on this slot. Check Detected devices for slot numbers");
return;}
 
}
 
 
 
if(InputDevice==Devices.Slot3){
if(i>=3){
selectedDevice= Microphone.devices[2];
}
else{
Debug.LogError ("No device detected on this slot. Check Detected devices for slot numbers");
return;
}
}
 
 
if(InputDevice==Devices.Slot4){
if(i>=4){
selectedDevice= Microphone.devices[2];
}
else{
Debug.LogError ("No device detected on this slot. Check Detected devices for slot numbers");
return;
}
}
if(InputDevice==Devices.Slot5){
if(i>=5){
selectedDevice= Microphone.devices[2];
}
else{
Debug.LogError ("No device detected on this slot. Check Detected devices for slot numbers");
return;
}
}
 
if(InputDevice==Devices.Slot6){
if(i>=6){
selectedDevice= Microphone.devices[2];
}
else{
Debug.LogError ("No device detected on this slot. Check Detected devices for slot numbers");
return;
}
}
 

//Now that we know which device to listen to, lets set the frequency we want to record at

if(freq== freqList._44100HzCD){
maxFreq=44100;

}

if(freq == freqList._48000HzDVD){
maxFreq=48000;

}



//detect the selected microphone one time to geth the first buffer.
 audioSource.clip = new Microphone.Start(selectedDevice, true, 1, maxFreq);

	
//loop the playing of the recording so it will be realtime
audioSource.loop = true;
//if you only need the data stream values  check Mute, if you want to hear yourself ingame don't check Mute. 
//audioSource.mute = Mute;	



  	var mixer:AudioMixer = Resources.Load("MicControl2Mixer") as AudioMixer;
		if(Mute){
			mixer.SetFloat("MicControl2Volume",-80);
				}
				else{
					mixer.SetFloat("MicControl2Volume",0);
						}

   



//don't do anything until the microphone started up
	while (!(Microphone.GetPosition(selectedDevice) > 0)){
		if(debug){
			Debug.Log("Awaiting connection");
				}
					}
	if(debug){
		Debug.Log("Connected");
			}
 
//Now that the basic initialisation is done, we are ready to start the microphone and gather data.
		 StartMicrophone ();
			recording=true; 

 }



 }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
 
 
 
    //for the above control the mic start or stop
 

 public function StartMicrophone () {

 		GetMicCaps (); 	//first detect if maxFreq is set or not. If not use a default value

 		   audioSource.clip = Microphone.Start(selectedDevice, true, 1, maxFreq);//Starts recording

          while (!(Microphone.GetPosition(selectedDevice) > 0)){} // Wait until the recording has started
        audioSource.Play(); // Play the audio source! 
    }
 
 
 public function StopMicrophone () {
        audioSource.Stop();//Stops the audio
         Microphone.End(selectedDevice);//Stops the recording of the device  

    }


    //if no frequency is set, the system will default to 44100
      function GetMicCaps () {
         Microphone.GetDeviceCaps(selectedDevice,  minFreq,  maxFreq);//Gets the frequency of the device
         if ((0 + maxFreq) == 0)//These 2 lines of code are mainly for windows computers
             maxFreq = 44100;
 
    			}
    

    
        
	 //start or stop the script from running when the state is paused or not.
    function OnApplicationFocus(focus: boolean) {
		focused = focus;
	}
	
	function OnApplicationPause(focus: boolean) {
		focused = focus;
	}
	
	function OnApplicationExit(focus: boolean) {
		focused = focus;
	}
	
	

	#if UNITY_EDITOR
			
	   //draw the gizmo
 function OnDrawGizmos () {
 if(Application.isEditor)
		Gizmos.DrawIcon (transform.position, "MicControlGizmo.tif", true);
		
					
			
			//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//if gizmo folder does not exist create it
	if (!Directory.Exists(Application.dataPath+"/Plugins/Gizmos")){
		Directory.CreateDirectory(Application.dataPath+"/Gizmos");
			}
	//then copy the gizmo into the folder
	var info = new DirectoryInfo(Application.dataPath+"/Plugins/Gizmos");
	var fileInfo = info.GetFiles();
	
		if(!File.Exists(Application.dataPath+"/Plugins/Gizmos/MicControlGizmo.tif")){
			File.Copy(Application.dataPath+"/Plugins/MicControl2/Source/MicControlGizmo.tif",Application.dataPath+"/Plugins/Gizmos/MicControlGizmo.tif");
				}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
		
	}
	
	#endif
    
    