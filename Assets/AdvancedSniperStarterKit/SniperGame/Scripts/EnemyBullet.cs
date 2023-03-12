using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

	public float Raysize = 3;
	private Rigidbody rigidBody;
	private Vector3 positionTmp;
	public GameObject GroundHitFX;
	public GameObject BodyHitFX;

	void Start ()
	{
		positionTmp = this.transform.position;
		rigidBody = this.GetComponent<Rigidbody> ();
		shootRay ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		shootRay ();
	}

	void shootRay ()
	{
		Vector3 direction = this.transform.forward;
		if (rigidBody) {
			direction = rigidBody.velocity.normalized;
		}
		RaycastHit hit;
		if (Physics.Raycast (this.transform.position, direction, out hit, 1000)) {
			if (hit.collider) {
				DamageManager damage = hit.collider.GetComponent<DamageManager> ();
				if (damage) {
					damage.ApplyDamage (10, rigidBody.velocity,null, Vector3.Distance (positionTmp, hit.point));
				}
				Destroy (this.gameObject);
				if (hit.collider.CompareTag ("Player")) {
					if (BodyHitFX) {
						GameObject fx = (GameObject)GameObject.Instantiate (BodyHitFX, hit.point, Quaternion.identity);
						fx.transform.forward = hit.normal;
						GameObject.Destroy (fx, 2);
					}
				} else {
					if (GroundHitFX) {
						GameObject fx = (GameObject)GameObject.Instantiate (GroundHitFX, hit.point, Quaternion.identity);
						fx.transform.forward = hit.normal;
						GameObject.Destroy (fx, 2);
					}
				}
			}
		}
	}
}
