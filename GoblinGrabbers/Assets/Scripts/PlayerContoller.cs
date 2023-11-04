using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : MonoBehaviour {

	// public CamControls Cam;
	public Transform TileParent;
	public Animator Anim;
	public float AnimSpeedMul = 1f;

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
	bool grounded = false;

	private void Start() {
		referenceY = TileParent.position.y - transform.position.y;
		speedCache = MoveSpeed;
	}

	void Update() {
		var kbd = Keyboard.current;

		if (kbd.spaceKey.wasPressedThisFrame) {
			FallVelocity = -JumpVelocity;
			Anim.SetTrigger("JumpTrigger");
		}

		float y = TileParent.position.y - transform.position.y;
		bool wasGrounded = grounded;
		grounded = y < referenceY || FallVelocity < 0;
		if (grounded) {
			FallVelocity += Gravity;
			transform.localPosition += FallVelocity * Time.deltaTime * Vector3.down;
			// if (!wasGrounded) Anim.SetBool("", true);
		}

		// if (kbd.fKey.wasPressedThisFrame) {
		// 	CamControls.FlipMode();
		// }

		speedCache = Mathf.MoveTowards(speedCache, SpeedCap, RunAcceleration * Time.deltaTime);

		Anim.SetFloat("Speed", speedCache * AnimSpeedMul);

		if (CamControls.RightMode) {
			TileParent.localPosition -= speedCache * Time.deltaTime * Vector3.forward;
		} else {
			TileParent.localPosition -= speedCache * Time.deltaTime * Vector3.back;
		}

	}

	public static float LastDescendZ = 0;
	public void Descend(float y) {
		referenceY += y;
		CamControls.FlipMode();
		LastDescendZ = transform.position.z;
	}
}
