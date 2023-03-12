// Bullet marker. Using to adding into any objects that you want to have a Camera Action.
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AS_BulletHiter : MonoBehaviour {
	public bool ParticleSticky = true;
	public GameObject ParticleHit;
	public GameObject ParticleFlow;
	public float ParticleLifeTime = 5;
	public float ParticleParticleFlowLifeTime = 5;
	public GameObject RootObject;
	public GameObject DecayFX;
	public float DecayDuration = 10;
	public int Suffix = 0;
	public AudioClip[] Sounds;
	public bool HasAction = true;
	public float BaseActionDistance = 0;

	void Start(){
		
	}
	void Awake () {
		if(!RootObject){
			RootObject = this.transform.root.gameObject;	
		}
	}
	
	public virtual void OnHit(RaycastHit hit,AS_Bullet bullet){

		if (DecayFX) {
			GameObject decay = (GameObject)GameObject.Instantiate(DecayFX,hit.point,Quaternion.identity);
			decay.transform.forward = bullet.transform.forward - (Vector3.up*0.2f);
			GameObject.Destroy(decay,DecayDuration);
		}

		if (ParticleHit) {
			GameObject hitparticle = (GameObject)Instantiate (ParticleHit, hit.point, hit.transform.rotation);
			hitparticle.transform.forward = hit.normal;
			if (ParticleSticky) {
				hitparticle.transform.parent = this.transform;
				hitparticle.name = "FX";
			}
			GameObject.Destroy (hitparticle, ParticleLifeTime);
		}
		if (ParticleFlow) {
			GameObject flowparticle = (GameObject)Instantiate (ParticleFlow, this.transform.position, hit.transform.rotation);
			flowparticle.transform.SetParent(bullet.transform);
			GameObject.Destroy (flowparticle, ParticleParticleFlowLifeTime);
		}
	}
	
	public void AddAudio(Vector3 point){
		GameObject sound = new GameObject("SoundHit");
		sound.AddComponent<AS_SoundOnHit>();
		GameObject soundObj = (GameObject)GameObject.Instantiate(sound,point,Quaternion.identity);
		soundObj.GetComponent<AS_SoundOnHit>().Sounds = Sounds;
		GameObject.Destroy(soundObj,3);
	}
	
	void OnDrawGizmos ()
	{
		#if UNITY_EDITOR

		Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
		Gizmos.matrix = rotationMatrix;
		Gizmos.color = Color.green;
		Gizmos.DrawSphere (Vector3.zero, 0.1f);
		Handles.Label(transform.position, this.name);
		#endif
	}
}
