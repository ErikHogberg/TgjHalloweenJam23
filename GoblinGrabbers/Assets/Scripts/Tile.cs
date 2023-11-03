using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	public float Threshold = 50;

	float initZ = 0;

	private void Start() {
		initZ = transform.localPosition.z;
	}

	void Update() {
		float z = transform.position.z - initZ;
		if (z > Threshold) {
			transform.localPosition += Vector3.back * (Threshold * 2f + initZ);
		} else if (z < -Threshold) {
			transform.localPosition += Vector3.forward * (Threshold * 2f - initZ);
		}
	}
}
