using UnityEngine;
using System.Collections;

[RequireComponent (typeof(DamageManager))]
[RequireComponent (typeof(CharacterController))]

public class Enemy : MonoBehaviour
{
	
	public float GravityMult = 1;
	public float Slip = 10;
	private float fallvelocity = 0;
	private Vector3 moveDirection;
		
	public Animation AnimationObject;
	public float Speed = 3;
	public float PatolDistance = 200;
	public float AiDelayMax = 10;
	public float AiDelayMin = 1;
	public AudioClip[] footstepSound;
	public Vector3 targetPosition;

	public string RunPose = "Run";
	public string IdlePose = "Idle";

	private int state = 0;
	private float timethink = 0;
	private CharacterController characterController;

	void Start ()
	{
		if (AnimationObject == null)
			AnimationObject = this.GetComponent<Animation> ();
		
		characterController = this.GetComponent<CharacterController> ();
		if (AnimationObject)
			AnimationObject.PlayQueued (IdlePose);
	}

	void Update ()
	{
		if (timethink <= 0) {
			targetPosition = new Vector3 (Random.Range (-PatolDistance, PatolDistance), 0, Random.Range (-PatolDistance, PatolDistance));
			timethink = Random.Range (AiDelayMin, AiDelayMax);
			state = Random.Range (0, 2);
		} else {
			timethink -= 1 * Time.deltaTime;
		}
		
		isGrounded = GroundChecking ();
		
		targetPosition.y = transform.position.y;
		Quaternion rotationTarget = Quaternion.LookRotation ((targetPosition - this.transform.position).normalized);
		transform.rotation = Quaternion.Lerp (this.transform.rotation, rotationTarget, Time.deltaTime * 5);
		
		switch (state) {
		case 0:
			if (AnimationObject)
				AnimationObject.CrossFade (RunPose, 0.3f);
			Vector3 direction = (targetPosition - transform.position).normalized;
			moveDirection = Vector3.Lerp (moveDirection, direction, Time.deltaTime * Slip);
			break;
		case 1:
			if (AnimationObject)
				AnimationObject.CrossFade (IdlePose, 0.3f);
			moveDirection = Vector3.zero;
			break;
		}
		

		moveDirection.y = fallvelocity;
		characterController.Move (moveDirection * Speed * Time.deltaTime);
	
		if (!isGrounded) {
			fallvelocity -= 90 * GravityMult * Time.deltaTime;
		}
	}

	public float DistanceToGround = 0.1f;
	private bool isGrounded = false;

	public bool GroundChecking ()
	{
		if (GetComponent<Collider> ()) {
			RaycastHit hit;
			if (characterController.isGrounded)
				return true;
			if (Physics.Raycast (GetComponent<Collider> ().bounds.center, -Vector3.up, out hit, DistanceToGround + 0.1f)) {
				return true;
			}
		}
		return false;
		
	}
}
