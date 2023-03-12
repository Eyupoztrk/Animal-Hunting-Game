using UnityEngine;
using System.Collections;


public class cameraControl : MonoBehaviour {

	public car car1;

	public Transform target;

	public Camera CAM;

	public float xSpeed, ySpeed, zoomSpeed=0.1f;

	public float minValueY=-20;
	public float maxValueY=80;

	[SerializeField] Joystick joystick;

	Quaternion rotation;
	Vector3 position;
	float distance=5;

	float x,y;
	float tempX, tempY;
	public float smoothTime=0.1f;
	float xSmooth=0.0f;
	float ySmooth=0.0f;

	void Start () {
		x=transform.eulerAngles.y;
		y=transform.eulerAngles.x;
	}

	void Update () {
		if(car1.controlled){
				if(target!=null){

				/*x+=Input.GetAxis("Mouse X")*xSpeed;
				y-=Input.GetAxis("Mouse Y")*ySpeed;*/

				x += joystick.Horizontal * xSpeed;
				y -= joystick.Vertical * ySpeed;

				tempX =Mathf.SmoothDamp(tempX, x, ref xSmooth,smoothTime);
					tempY=Mathf.SmoothDamp(tempY, y, ref ySmooth,smoothTime);


					distance+=Input.GetAxis("Mouse ScrollWheel")*-zoomSpeed;

					y = ClampAngle(y,minValueY,maxValueY);

					rotation=Quaternion.Euler(tempY,tempX,0);
					position=rotation*new Vector3(0,0,-distance) + target.position;

					CAM.transform.rotation=rotation;
					CAM.transform.position=position;
			}
		}
	}
		
	float ClampAngle(float angle, float min, float max){
		if(angle < -360)
			angle+=360;
		if(angle>360)
			angle-=360;

		return Mathf.Clamp(angle, min, max);
	}


}
