using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : MonoBehaviour {

	// public CamControls Cam;
	public Transform TileParent;
	public Animator Anim;
	[Min(0.001f)]
	public float AnimSpeedMul = 1f;

	[Space]
	[Min(0.01f)]
	public float MoveSpeed = 1;
	public float SpeedCap = 1;

	[Min(0.01f)]
	public float RunAcceleration = 1;
	public float JumpVelocity = 1;
	public float Gravity = 1;
	public float GoblinSlowdown = 1;

	public float ReferenceY { get; private set; } = 0;
	float FallVelocity = 0;

	public float speedCache { get; private set; } = 0;
	bool grounded = false;

	Queue<GoblinGRABBER> goblins = new();

	private void Start() {
		ReferenceY = TileParent.position.y - transform.position.y;
		speedCache = MoveSpeed;
	}

	void Update() {
		var kbd = Keyboard.current;

		if (goblins.TryPeek(out var result)) {
			if (kbd.spaceKey.wasPressedThisFrame && result.Wack()) {
				goblins.Dequeue();
			}
			speedCache -= goblins.Count * GoblinSlowdown * Time.deltaTime;
		} else if (kbd.spaceKey.wasPressedThisFrame && grounded) {
			FallVelocity = -JumpVelocity;
			Anim.transform.localPosition = new Vector3(0, -1.2f, 0);
			Anim.SetTrigger("JumpTrigger");
		}

		float y = TileParent.position.y - transform.position.y;
		bool wasGrounded = grounded;
		grounded = !(y < ReferenceY || FallVelocity < 0);
		if (!grounded) {
			FallVelocity += Gravity;
			transform.localPosition += FallVelocity * Time.deltaTime * Vector3.down;
			// } else {
			// 	var lpos = transform.localPosition;
			// 	lpos.y = referenceY;
			// 	transform.localPosition = lpos; 
		} else {
			FallVelocity = 0;
		}
		// else if (!wasGrounded) Anim.SetBool("", true);

		// if (kbd.fKey.wasPressedThisFrame) {
		// 	CamControls.FlipMode();
		// }

		speedCache = Mathf.Max(6f * Time.deltaTime, Mathf.MoveTowards(speedCache, SpeedCap, RunAcceleration * Time.deltaTime));

		// Anim.SetFloat("Speed", speedCache * AnimSpeedMul);
		Anim.speed = speedCache * AnimSpeedMul;

		if (CamControls.RightMode) {
			TileParent.localPosition -= speedCache * Time.deltaTime * Vector3.forward;
		} else {
			TileParent.localPosition -= speedCache * Time.deltaTime * Vector3.back;
		}

	}

	public static float LastDescendZ = 0;
	public void Descend(float y) {
		goblins.Clear();
		ReferenceY += y;
		CamControls.FlipMode();
		Anim.transform.localRotation = CamControls.RightMode ? Quaternion.identity : Quaternion.AngleAxis(180, Vector3.up);
		LastDescendZ = transform.position.z;
	}

	public void AttachGoblin(GoblinGRABBER goblin) {
		goblins.Enqueue(goblin);
	}
}
