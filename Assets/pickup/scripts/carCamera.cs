using UnityEngine;
using System.Collections;

public class carCamera : MonoBehaviour
{
	public car car1;
	public Camera CAM;
	public Transform target = null;
	public float height = 1f;
	public float positionDamping = 3f;
	public float velocityDamping = 3f;
	public float distance = 4f;
	public LayerMask ignoreLayers = -1;
	
	private RaycastHit hit = new RaycastHit();
	
	private Vector3 prevVelocity = Vector3.zero;
	private LayerMask raycastLayers = -1;
	
	private Vector3 currentVelocity = Vector3.zero;

	
	void Start()
	{
		raycastLayers = ~ignoreLayers;
	}
	
	void Update()
	{
		if(car1.controlled){
			Vector3 targetVelocity = target.root.GetComponent<Rigidbody>().velocity;

			if(target.root.GetComponent<Rigidbody>().velocity.magnitude<0.01)
				velocityDamping = 0;
				else
					velocityDamping = 3f;



			currentVelocity = Vector3.Lerp(prevVelocity, targetVelocity, velocityDamping * Time.deltaTime);

			currentVelocity.y = 0;

			prevVelocity = currentVelocity;
		}
	}
	
	void LateUpdate()
	{
		if(car1.controlled && CAM!=null){
			float speedFactor = Mathf.Clamp01(target.root.GetComponent<Rigidbody>().velocity.magnitude / 60.0f);
			if(speedFactor<0.01f)
				speedFactor=0.01f;
		
			CAM.fieldOfView = Mathf.Lerp(40, 65, speedFactor);
			float currentDistance = Mathf.Lerp(7.5f, 6.5f, speedFactor);
			
			currentVelocity = currentVelocity.normalized;
			
			Vector3 newTargetPosition = target.position + Vector3.up * height;
			Vector3 newPosition = newTargetPosition - ((currentVelocity * currentDistance));
			newPosition.y = newTargetPosition.y;
			
			Vector3 targetDirection = newPosition - newTargetPosition;
			if(Physics.Raycast(newTargetPosition, targetDirection, out hit, currentDistance, raycastLayers))
				newPosition = hit.point;
			
			CAM.transform.position = newPosition;

			CAM.transform.LookAt(newTargetPosition);
			}
		else if(CAM ==null){
			Debug.Log("please add a camera to the scene with tag 'MainCamera' ");
		}
	}
}
