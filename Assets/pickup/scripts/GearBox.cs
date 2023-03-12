using UnityEngine;
using System.Collections;

public class GearBox : MonoBehaviour {
	public car car1;
	public GameObject pointerSpeed;
	public GameObject pointerRPM;
	
	public float maxPitch=2.4f;
	public float[] totalSteps;

	public float currentPitch = 1;

	float procentPitch;
	float timeToShift=0;
	public float speed;
	
	public bool shiftinGear;
	int currentGear = 1;


	public CarControlMobile carControlMobile;
	Quaternion pointRot;

	void Awake(){

	}

	void Update(){
		speed = GetComponent<Rigidbody>().velocity.magnitude*3.6f;
	}

	void FixedUpdate () {
		if(car1.controlled){
			procentPitch = Procents(maxPitch-1,1);
			Transmission();
			pointerSpeed.transform.localRotation=Quaternion.Euler(Mathf.Abs(speed)*-0.9f,-90,0);
			pointerRPM.transform.localRotation=Quaternion.Euler(-70*(currentPitch-1),-90,0);
		}
	}


	float Procents(float value, float procents){
		float newValue = (value/100)*procents;
		return newValue;
	}

	float ProcentOfValue(float firstValue, float secondValue){
		float newValue = (int)(firstValue / (secondValue/100));
		return newValue;
	}

	void Transmission(){
		float difference=0;
		float tempSpeed=0;

		if(carControlMobile.Vertical>0){	
			if(!shiftinGear){
				for (int i = 0; i<totalSteps.Length; i++)
				{
					difference=totalSteps[i+1]-totalSteps[i];
					tempSpeed=speed-totalSteps[i];
					if(speed>=totalSteps[currentGear+1]&&currentGear==i)
						shiftinGear=true;
					if(speed<totalSteps[i+1]){
						break;
					}
				}
				currentPitch =Mathf.Lerp(currentPitch, (1+(procentPitch*ProcentOfValue(tempSpeed, difference))), 0.1f);
				if(currentPitch>maxPitch)
					currentPitch=maxPitch;
				timeToShift=0;
			}

		}
		else{
			currentPitch=Mathf.Lerp(currentPitch,1,0.05f);
			for (int i = 0; i<totalSteps.Length; i++){
				currentGear=i;
				if(speed<totalSteps[i+1]){
					break;
				}
			}
		}

		if(shiftinGear){
			currentPitch=Mathf.Lerp(currentPitch,1,0.01f);
			timeToShift+=Time.deltaTime;
			if(timeToShift>0.5){
				shiftinGear=false;
				currentGear+=1;
			}
		}
		if(currentPitch<1)
			currentPitch=1;
	}
}
