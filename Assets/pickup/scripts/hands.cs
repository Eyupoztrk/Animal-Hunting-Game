using UnityEngine;
using System.Collections;

public class hands : MonoBehaviour {
	Animator anim;

	float ikWeightLeft1=1;

	float ikWeightRight1=1;

	public Transform leftIkTarget;
	public Transform leftIkTarget2;
	public Transform leftIkTarget3;
	public Transform leftIkTarget4;
	public Transform leftIkTarget5;

	public Transform rightIkTarget;
	public Transform rightIkTarget2;
	public Transform rightIkTarget3;
	public Transform rightIkTarget4;
	public Transform rightIkTarget5;

	public Transform hintLeft;
	public Transform hintRight;

	float steerangle;
	float rightHandAngle;
	float leftHandAngle;
	float test;

	Quaternion rightHandDefaulRot;
	Vector3 rightHandDefaultPos;

	Quaternion leftHandDefaulRot;
	Vector3 leftHandDefaultPos;

	Quaternion rightHandDefaulRot3;
	Vector3 rightHandDefaultPos3;
	
	Quaternion leftHandDefaulRot3;
	Vector3 leftHandDefaultPos3;

	Quaternion rightHandDefaulRot5;
	Vector3 rightHandDefaultPos5;
	
	Quaternion leftHandDefaulRot5;
	Vector3 leftHandDefaultPos5;

	void Start () {
		anim = GetComponent<Animator>();
		rightHandDefaulRot=rightIkTarget.localRotation;
		rightHandDefaultPos=rightIkTarget.localPosition;

		leftHandDefaulRot=leftIkTarget.localRotation;
		leftHandDefaultPos=leftIkTarget.localPosition;

		rightHandDefaulRot3=rightIkTarget3.localRotation;
		rightHandDefaultPos3=rightIkTarget3.localPosition;
		
		leftHandDefaulRot3=leftIkTarget3.localRotation;
		leftHandDefaultPos3=leftIkTarget3.localPosition;

		rightHandDefaulRot5=rightIkTarget5.localRotation;
		rightHandDefaultPos5=rightIkTarget5.localPosition;
		
		leftHandDefaulRot5=leftIkTarget5.localRotation;
		leftHandDefaultPos5=leftIkTarget5.localPosition;
	}

	void Update(){
		steerangle=gameObject.GetComponentInParent<car>().steerWheelAngle;
		if(steerangle<-340)
			steerangle=-340;
		if(steerangle>340)
			steerangle=340;


		// right hand
		rightHandAngle=-steerangle-40; //a graph of the function y=kx+b
		if(steerangle<-190)
			rightHandAngle=-steerangle-280; //a graph of the function y=kx+b

		// 1 phase
		if(rightHandAngle<0)
			rightHandAngle=0;
		if(rightHandAngle>60)
			rightHandAngle=60;



		rightIkTarget.localRotation=rightHandDefaulRot * Quaternion.Euler(new Vector3(-rightHandAngle, 0,rightHandAngle*0.5f));
		rightIkTarget.localPosition=rightHandDefaultPos - new Vector3(rightHandAngle*0.0013f,0,0);

		// 2 phase
		if(steerangle>60){
			rightIkTarget.position=Vector3.Lerp(rightIkTarget.position, rightIkTarget2.position, steerangle/30-2);//a graph of the 
			rightIkTarget.rotation=Quaternion.Lerp(rightIkTarget.rotation, rightIkTarget2.rotation, steerangle/30-2);//function y=kx+b
			anim.SetBool("rightOpen",true);
		}
		else
			anim.SetBool("rightOpen",false);

		// 3 phase
		if(steerangle>90){
			rightIkTarget.position=Vector3.Lerp(rightIkTarget2.position, rightIkTarget3.position, steerangle/30-3);//a graph of the 
			rightIkTarget.rotation=Quaternion.Lerp(rightIkTarget2.rotation, rightIkTarget3.rotation, steerangle/30-3);//function y=kx+b
		}

		// 4 phase
		if(steerangle>110){
			anim.SetBool("rightOpen",false);
			rightIkTarget.localRotation=rightIkTarget3.localRotation * Quaternion.Euler(new Vector3(-60, 0,60*0.5f));
			rightIkTarget.localPosition=rightIkTarget3.localPosition - new Vector3(0,0,60*-0.0013f);
		}
		// 5 phase
		if(steerangle<-110){
			anim.SetBool("rightOpen",true);
			rightIkTarget.position=Vector3.Lerp(rightIkTarget.position, rightIkTarget4.position, -steerangle/50-2);//a graph of the 
			rightIkTarget.rotation=Quaternion.Lerp(rightIkTarget.rotation, rightIkTarget4.rotation, -steerangle/50-2);//function y=kx+b
		}
		// 6 phase
		if(steerangle<-150){
			rightIkTarget.position=Vector3.Lerp(rightIkTarget.position, rightIkTarget5.position, -steerangle/50-3);//a graph of the 
			rightIkTarget.rotation=Quaternion.Lerp(rightIkTarget.rotation, rightIkTarget5.rotation, -steerangle/50-3);//function y=kx+b
			
		}
		if(steerangle<-190){
			anim.SetBool("rightOpen",false);
			rightIkTarget5.localRotation=rightHandDefaulRot5 * Quaternion.Euler(new Vector3(-rightHandAngle, 0,rightHandAngle*0.5f));
			rightIkTarget5.localPosition=rightHandDefaultPos5 - new Vector3(0,0,rightHandAngle*0.0016f);
		}
		// 7 phase
		if(steerangle>200){
			rightHandAngle=steerangle-260;
				if(rightHandAngle>0)
					rightHandAngle=0;
				if(rightHandAngle<-60)
					rightHandAngle=-60;

			rightIkTarget.localRotation=rightHandDefaulRot3 * Quaternion.Euler(new Vector3(rightHandAngle, 0,rightHandAngle*-0.5f));
			rightIkTarget.localPosition=rightHandDefaultPos3 - new Vector3(0,0,rightHandAngle*0.0013f);
		}

		// left hand

		leftHandAngle=steerangle-40; //a graph of the function y=kx+b
		if(steerangle>190)
			leftHandAngle=steerangle-280; //a graph of the function y=kx+b

		// 1 phase
		if(leftHandAngle<0)
			leftHandAngle=0;
		if(leftHandAngle>60)
			leftHandAngle=60;
		
		leftIkTarget.localRotation=leftHandDefaulRot * Quaternion.Euler(new Vector3(-leftHandAngle, 0,leftHandAngle*-0.5f));
		leftIkTarget.localPosition=leftHandDefaultPos - new Vector3(leftHandAngle*-0.0013f,0,0);

		// 2 phase
		if(steerangle<-60){
			leftIkTarget.position=Vector3.Lerp(leftIkTarget.position, leftIkTarget2.position, -steerangle/30-2);//a graph of the 
			leftIkTarget.rotation=Quaternion.Lerp(leftIkTarget.rotation, leftIkTarget2.rotation, -steerangle/30-2);//function y=kx+b
			anim.SetBool("leftOpen",true);
		}
		else
			anim.SetBool("leftOpen",false);

		// 3 phase
		if(steerangle<-90){
			leftIkTarget.position=Vector3.Lerp(leftIkTarget2.position, leftIkTarget3.position, -steerangle/30-3);//a graph of the 
			leftIkTarget.rotation=Quaternion.Lerp(leftIkTarget2.rotation, leftIkTarget3.rotation, -steerangle/30-3);//function y=kx+b
		}
		// 4 phase
		if(steerangle<-110){
			anim.SetBool("leftOpen",false);
			leftIkTarget.localRotation=leftIkTarget3.localRotation * Quaternion.Euler(new Vector3(-60, 0,60*-0.5f));
			leftIkTarget.localPosition=leftIkTarget3.localPosition - new Vector3(0,0,60*-0.0013f);
		}

		// 5 phase
		if(steerangle>110){
			anim.SetBool("leftOpen",true);
			leftIkTarget.position=Vector3.Lerp(leftIkTarget.position, leftIkTarget4.position, steerangle/50-2);//a graph of the 
			leftIkTarget.rotation=Quaternion.Lerp(leftIkTarget.rotation, leftIkTarget4.rotation, steerangle/50-2);//function y=kx+b
		}
		// 6 phase
		if(steerangle>150){
			leftIkTarget.position=Vector3.Lerp(leftIkTarget.position, leftIkTarget5.position, steerangle/50-3);//a graph of the 
			leftIkTarget.rotation=Quaternion.Lerp(leftIkTarget.rotation, leftIkTarget5.rotation, steerangle/50-3);//function y=kx+b

		}
		if(steerangle>190){
			anim.SetBool("leftOpen",false);
			leftIkTarget5.localRotation=leftHandDefaulRot5 * Quaternion.Euler(new Vector3(-leftHandAngle, 0,leftHandAngle*-0.5f));
			leftIkTarget5.localPosition=leftHandDefaultPos5 - new Vector3(0,0,leftHandAngle*0.0016f);
		}

		// 7 phase
		if(steerangle<-200){
			leftHandAngle=steerangle+260;

			if(leftHandAngle>60)
				leftHandAngle=60;

			if(leftHandAngle<0)
				leftHandAngle=0;
			
			leftIkTarget.localRotation=leftHandDefaulRot3 * Quaternion.Euler(new Vector3(-leftHandAngle, 0,leftHandAngle*-0.5f));
			leftIkTarget.localPosition=leftHandDefaultPos3 - new Vector3(0,0,leftHandAngle*-0.0013f);
		}
	}

	void FixedUpdate () {

	}

	void OnAnimatorIK(){
		anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, ikWeightLeft1);
		anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, ikWeightRight1);
		
		anim.SetIKHintPosition(AvatarIKHint.LeftElbow, hintLeft.position);
		anim.SetIKHintPosition(AvatarIKHint.RightElbow, hintRight.position);

		anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeightLeft1);
		anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeightRight1);

		anim.SetIKPosition(AvatarIKGoal.LeftHand, leftIkTarget.position);
		anim.SetIKPosition(AvatarIKGoal.RightHand, rightIkTarget.position);

		anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeightLeft1);
		anim.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeightRight1);
		
		anim.SetIKRotation(AvatarIKGoal.LeftHand, leftIkTarget.rotation);
		anim.SetIKRotation(AvatarIKGoal.RightHand, rightIkTarget.rotation);
	}
}
