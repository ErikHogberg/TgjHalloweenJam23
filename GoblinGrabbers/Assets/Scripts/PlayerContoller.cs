using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : MonoBehaviour {

	// public CamControls Cam;
	public Transform TileParent;

	[Space]
	public float MoveSpeed = 1;


	void Update() {
		var kbd = Keyboard.current;


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

		if (CamControls.RightMode) {
			TileParent.localPosition -= Vector3.forward * MoveSpeed * Time.deltaTime;
		} else {
			TileParent.localPosition -= Vector3.back * MoveSpeed * Time.deltaTime;
		}


	}
}
