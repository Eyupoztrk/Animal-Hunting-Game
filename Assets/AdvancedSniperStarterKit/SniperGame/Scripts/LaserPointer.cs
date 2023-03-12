using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {

	public GameObject PointerObject;
	public float Length = 100000;
	void Start () {

	}


	void Update () {

		RaycastHit hit;
		if (Physics.Raycast (this.transform.position, this.transform.forward, out hit, Length)) {
			if (PointerObject) {
				PointerObject.transform.position = hit.point - (this.transform.forward * 0.2f);
				PointerObject.transform.forward = -this.transform.forward;
			}
		}
	}
}
