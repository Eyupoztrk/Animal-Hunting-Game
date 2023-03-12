using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class collisionSound : MonoBehaviour {

	public AudioMixer masterMixer;

	public AudioClip[] middleSound; 
	public AudioClip[] greatSound; 
	AudioSource colAudio = null;
	public float middleDamage=20f;
	public float greatDamage=100f;

	float lastTime;
	float interval = 0.1f;
	float intervalRand = 0.4f;	
	float impactSpeed;


	Vector3 sumImpactVelocity;
	Vector3 localImpactVelocity;

	void Start () {
		colAudio = gameObject.AddComponent<AudioSource>();
		colAudio.loop = false;
		colAudio.clip = middleSound[Random.Range(0,middleSound.Length)];
		colAudio.volume = 1f;
		colAudio.spatialBlend=0.8f;
	}
	

	void Update () {
		colAudio.outputAudioMixerGroup=masterMixer.FindMatchingGroups("collisions")[0];
		
		bool canProcessCollisions = Time.time-lastTime >= interval;

		if(canProcessCollisions){
			localImpactVelocity=sumImpactVelocity;

			sumImpactVelocity=Vector3.zero;

			lastTime = Time.time + interval * Random.Range(-intervalRand, intervalRand);
		}
		else{
			localImpactVelocity = Vector3.zero;
		}

		impactSpeed=localImpactVelocity.magnitude;

		if (impactSpeed > middleDamage&& impactSpeed<greatDamage){
			colAudio.volume=0.4f+((impactSpeed-middleDamage)/(greatDamage-middleDamage));
			colAudio.clip = middleSound[Random.Range(0,middleSound.Length)];
			colAudio.Play();
		}
		else if(impactSpeed>greatDamage){
			colAudio.volume=1;
			colAudio.clip = greatSound[Random.Range(0,greatSound.Length)];
			colAudio.Play();
		}

	}

	 void OnCollisionEnter(Collision collision){
		ProcessContact(collision);
	}

	void ProcessContact(Collision col){
		Vector3 colImpactVelocity=Vector3.zero;

		foreach(ContactPoint contact in col.contacts){
			Collider thisCol = contact.otherCollider;
			Collider otherCol = contact.thisCollider;

			if (thisCol == null || thisCol.attachedRigidbody != GetComponent<Rigidbody>()){
				if (thisCol.GetType() != typeof(WheelCollider) && otherCol.GetType () != typeof(WheelCollider)){
					colImpactVelocity+=col.relativeVelocity;
					sumImpactVelocity+= transform.InverseTransformDirection(colImpactVelocity);
				}
			}
		}
	}
}
