using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : MonoBehaviour {

	// public CamControls Cam;
	public Transform TileParent;

	[Space]
	[Min(0.01f)]
	public float MoveSpeed = 1;
	public float SpeedCap = 1;

	[Min(0.01f)]
	public float RunAcceleration = 1;
	public float JumpVelocity = 1;
	public float Gravity = 1;

	float referenceY = 0;
	float FallVelocity = 0;

	float speedCache = 0;

	private void Start() {
		referenceY = TileParent.position.y - transform.position.y;
		speedCache = MoveSpeed;
	}

	void Update() {
		var kbd = Keyboard.current;

		if (kbd.spaceKey.wasPressedThisFrame) {
			FallVelocity = -JumpVelocity;
		}

		float y = TileParent.position.y - transform.position.y;
		if (y < referenceY || FallVelocity < 0){
			FallVelocity += Gravity;
			transform.localPosition += Vector3.down * FallVelocity * Time.deltaTime;
		}

		// if (kbd.dKey.isPressed) {
		// 	// transform.localPosition += Vector3.forward * MoveSpeed * Time.deltaTime;
		// 	TileParent.localPosition -= Vector3.forward * MoveSpeed * Time.deltaTime;
		// 	CamControls.RightMode = true;
		// }
		// if (kbd.aKey.isPressed) {
		// 	// transform.localPosition += Vector3.back * MoveSpeed * Time.deltaTime;
		// 	TileParent.localPosition -= Vector3.back * MoveSpeed * Time.deltaTime;
		// 	CamControls.RightMode = false;
		// }

		if (kbd.fKey.wasPressedThisFrame) {
			CamControls.FlipMode();
		}

		speedCache = Mathf.MoveTowards(speedCache, SpeedCap, RunAcceleration * Time.deltaTime);

		if (CamControls.RightMode) {
			TileParent.localPosition -= Vector3.forward * speedCache * Time.deltaTime;
		} else {
			TileParent.localPosition -= Vector3.back * speedCache * Time.deltaTime;
		}

	}

	public static float LastDescendZ = 0;
	public void Descend(float y){
		referenceY += y;
		CamControls.FlipMode();
		LastDescendZ = transform.position.z;
	}
}
