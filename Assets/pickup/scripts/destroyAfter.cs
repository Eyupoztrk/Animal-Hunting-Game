using UnityEngine;
using System.Collections;

public class destroyAfter : MonoBehaviour {

	public float destroyTime = 5;
	float timer=0;

	void Update () {
		timer+=Time.deltaTime;
		if(destroyTime<=timer)
			Destroy(gameObject);
	}
}
