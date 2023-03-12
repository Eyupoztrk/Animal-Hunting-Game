using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour {

	public car car1;
	public GearBox GB;
	public skidMarks[] SM;

	public AudioMixer masterMixer;

	public float maxPitch=2.4f;

	float maxEngineLevel, maxSkidLevel, maxRollLevel;
	float EngineLevel, SkidLevel, RollLevel;

//	float rollPitch=1;
	float enginePitch;
	float procentPitch;

	AudioSource[] aSources;

	bool skidPlay;
	bool oldConrolled;

	void Start () {
		
		aSources=GetComponents<AudioSource>();

		for(int i=0; i< aSources.Length; i++){
			if(car1.controlled)
				aSources[i].Play();
			else
				aSources[i].Stop();
		}
	}

	void Update () {
		skidPlay=false;
		if(car1.controlled){
			procentPitch = Procents(maxPitch-1,1);
			aSources[2].pitch=1+(procentPitch*ProcentOfValue(GB.speed,100));

			if((GB.speed/20)<0.7)
				aSources[2].volume =GB.speed/20;
			aSources[0].pitch = GB.currentPitch/2;
			if(!GB.shiftinGear&&aSources[0].volume <GB.currentPitch)
				aSources[0].volume +=0.05f;

			if(GB.shiftinGear && aSources[0].volume>0.2)
				aSources[0].volume -=0.05f;
				
			for(int i=0; i<SM.Length;i++){
				if(SM[i].skid==true){
					skidPlay=true;
					break;
				}
			}

			if(skidPlay){
				if(!aSources[1].isPlaying)
					aSources[1].Play();
				}
				else
					aSources[1].Stop();

			for(int i=0; i<SM.Length;i++){
				if(SM[i].bump==true){

					aSources[3].volume=SM[i].bumpVolume;
					aSources[3].pitch=SM[i].bumpPitch;
					if(!aSources[3].isPlaying)
						aSources[3].Play();
				}
			}
		}

		if(!car1.controlled && oldConrolled){
			for(int i=0; i<aSources.Length; i++)
				aSources[i].Stop();
		}

		if(car1.controlled && !oldConrolled){
			for(int i=0; i<aSources.Length; i++)
				aSources[i].Play();
		}


		oldConrolled=car1.controlled;
	}




	float Procents(float value, float procents){
		float newValue = (value/100)*procents;
		return newValue;
	}

	float ProcentOfValue(float firstValue, float secondValue){
		float newValue = (int)(firstValue / (secondValue/100));
		return newValue;
	}
}
