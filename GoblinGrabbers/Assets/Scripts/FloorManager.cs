using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class FloorManager : MonoBehaviour {

	public static FloorManager MainInstance = null;
	void Awake() {
		MainInstance = this;
	}
	void OnDestroy() {
		MainInstance = null;
	}

	public static uint CurrentFloor = 0;
	static float DragonFloor = -1;

	Queue<GameObject> spawnedFloors = new();

	public PlayerContoller PlayerCharacter;
	public uint FloorCount = 10;
	[Tooltip("How far below to spawn next floor")]
	public float YOffset = 30;
	public float DragonSpeed = 1;
	public GameObject FloorPrefab;
	public Transform FloorParent;
	public WaveDefinition Waves;

	public static WaveDefinition.FloorEntry CurrentFloorWaves = null;

	public static float PrincessProgress = 1;
	public static float DragonProgress = 1;

	public UnityEvent OnWin;
	public UnityEvent OnLose;

	private void Start() {
		DragonFloor = -1;
		CurrentFloor = 0;
		MoveDownInternal(skipMove: true);
	}

	private void Update() {
		DragonFloor += DragonSpeed * .01f * Time.deltaTime;
		DragonProgress = DragonFloor / FloorCount;
		if (DragonFloor > CurrentFloor) {
			Lose();
		}
	}

	public static void MoveDown() {
		if (MainInstance)
			MainInstance.MoveDownInternal();
	}

	private void MoveDownInternal(bool skipMove = false) {
		if (!skipMove) {
			CurrentFloor++;
			// PlayerCharacter.transform.position += Vector3.down * YOffset;
			PlayerCharacter.Descend(YOffset);

			if (CurrentFloor > FloorCount) {
				Win();
			}
		}
		PrincessProgress = ((float)CurrentFloor) / FloorCount;
		Debug.Log($"princess progress: {PrincessProgress}");
		CurrentFloorWaves = Waves.GetNearestFloor(CurrentFloor);
		Vector3 newPos = new Vector3(PlayerCharacter.TileParent.position.x, -YOffset * CurrentFloor, 0);
		var floor = Instantiate(FloorPrefab, newPos, Quaternion.identity, FloorParent);
		spawnedFloors.Enqueue(floor);


		// Despawn floors above last
		while (spawnedFloors.Count > 2) {
			Destroy(spawnedFloors.Dequeue());
		}
	}

	public void Win() {
		Debug.Log("win");
		Time.timeScale = 0.001f;
		// TODO: win
		OnWin.Invoke();
	}

	public void Lose() {
		Debug.Log("lose");
		// TODO: lose
		Time.timeScale = 0.001f;
		OnLose.Invoke();
	}
}
