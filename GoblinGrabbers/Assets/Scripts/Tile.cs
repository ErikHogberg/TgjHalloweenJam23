using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tile : MonoBehaviour {
	public float Threshold = 50;
	public RectTransform SpawnArea;

	void Update() {
		float z = transform.position.z;
		if (!CamControls.RightMode && z > Threshold * 2) {
			transform.localPosition += Vector3.forward * (-Threshold * 4f);
			MaybeSpawn();
		} else if (CamControls.RightMode && z < -Threshold * 2) {
			transform.localPosition += Vector3.forward * (Threshold * 4f);
			MaybeSpawn();
		}
	}

	void MaybeSpawn() {
		float z = Mathf.Abs(-transform.parent.position.z - PlayerContoller.LastDescendZ);
		bool hasWaves = FloorManager.CurrentFloorWaves != null;
		Debug.Log($"z: {z}, floors: {hasWaves}");

		if (
			hasWaves
			&& FloorManager.CurrentFloorWaves.TryGetNearestWave(z, out var bestWave)
		) {
			int rolledEnemyIndex = bestWave.RolledEnemyIndex+1;
			for (int i = 0; i < rolledEnemyIndex; i++) {
				string randomEnemyKey = bestWave.RandomEnemy;
				if (EnemyPooler.TryGet(randomEnemyKey, out var goblinPrefab)) {
					var newEnemy = Instantiate(goblinPrefab, transform.position + Vector3.forward * UnityEngine.Random.Range(1f,-1f), quaternion.identity, transform);
					Debug.Log($"spawned {newEnemy.name}");
				} else {
					Debug.LogWarning($"could not find {randomEnemyKey} in pool");
				}
			}
		}
	}
}
