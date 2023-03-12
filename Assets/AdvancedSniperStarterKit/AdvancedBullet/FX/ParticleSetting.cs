using UnityEngine;
using System.Collections;

public class ParticleSetting : MonoBehaviour
{
	public float LightIntensityMult = -0.5f;
	public float LifeTime = 1;
	public bool RandomRotation = false;
	public Vector3 PositionOffset;

	void  Start ()
	{
		this.gameObject.transform.position += PositionOffset;
		if (RandomRotation) {
			this.gameObject.transform.rotation = Random.rotation;
		}
		GameObject.Destroy (this.gameObject, LifeTime);

	}

	void  Update ()
	{
		if (this.gameObject.GetComponent<Light> ()) {
			this.GetComponent<Light> ().intensity += LightIntensityMult * Time.deltaTime;
		}
	}
}