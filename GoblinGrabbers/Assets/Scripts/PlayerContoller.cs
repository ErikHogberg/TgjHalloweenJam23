using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : MonoBehaviour {

	public CamControls Cam;

	[Space]
	public float MoveSpeed = 1;


	void Update() {
		var kbd = Keyboard.current;


		if (kbd.dKey.isPressed) {
			transform.localPosition += Vector3.forward * MoveSpeed * Time.deltaTime;
			Cam.RightMode = true;
		}
		if (kbd.aKey.isPressed) {
			transform.localPosition += Vector3.back * MoveSpeed * Time.deltaTime;
			Cam.RightMode = false;
		}

	}
}
