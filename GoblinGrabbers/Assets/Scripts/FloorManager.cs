using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FloorManager : MonoBehaviour {

	public static FloorManager MainInstance = null;
	void Awake() {
		MainInstance = this;
	}
	void OnDestroy() {
		MainInstance = null;
	}

	public static uint CurrentFloor = 0;

	Queue<GameObject> spawnedFloors = new();

	public PlayerContoller PlayerCharacter;
	[Tooltip("How far below to spawn next floor")]
	public float YOffset = 30;
	public GameObject FloorPrefab;
	public Transform FloorParent;
	public WaveDefinition Waves;

	public static WaveDefinition.FloorEntry CurrentFloorWaves = null;


	private void Start() {
		CurrentFloor = 0;
		MoveDownInternal(skipMove: true);
	}

	public static void MoveDown() {
		if (MainInstance)
			MainInstance.MoveDownInternal();
	}

	private void MoveDownInternal(bool skipMove = false) {
		if (!skipMove) {
			CurrentFloor++;
			PlayerCharacter.transform.position += Vector3.down * YOffset;
		}
		CurrentFloorWaves = Waves.GetNearestFloor(CurrentFloor);
		var floor = Instantiate(FloorPrefab, PlayerCharacter.transform.position, Quaternion.identity, FloorParent);
		spawnedFloors.Enqueue(floor);


		// Despawn floors above last
		while (spawnedFloors.Count > 2) {
			Destroy(spawnedFloors.Dequeue());
		}
	}

}
