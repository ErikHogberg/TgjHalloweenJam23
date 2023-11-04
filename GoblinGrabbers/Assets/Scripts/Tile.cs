using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tile : MonoBehaviour {
	public float Threshold = 50;
	public RectTransform SpawnArea;

	float initZ = 0;

	private void Start() {
		initZ = transform.localPosition.z;
	}

	void Update() {
		float z = transform.position.z;// + initZ;
		if (!CamControls.RightMode && z > Threshold*2) {
			transform.localPosition += Vector3.forward * (-Threshold * 4f);
			MaybeSpawn();
		} else if (CamControls.RightMode && z < -Threshold*2) {
			transform.localPosition += Vector3.forward * (Threshold * 4f );
			MaybeSpawn();
		}
	}

	void MaybeSpawn() {
		if (EnemyPooler.TryGet("goblin", out var goblinPrefab)) {
			var goblin = Instantiate(goblinPrefab, transform.position, quaternion.identity, transform);

		}
	}
}
