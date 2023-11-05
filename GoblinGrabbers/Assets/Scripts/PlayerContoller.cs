using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

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
	public float DescendSpeedNeeded = 20;

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

	public GameObject ShowOnGrab;

	private void Start() {
		ReferenceY = TileParent.position.y - transform.position.y;
		speedCache = MoveSpeed;
	}

	void Update() {
		var kbd = Keyboard.current;

		if (goblins.TryPeek(out var result)) {
			if (kbd.spaceKey.wasPressedThisFrame && result.Wack()) {
				ScoreManager.AddScore(12);
				goblins.Dequeue();
			}
			speedCache -= goblins.Count * GoblinSlowdown * Time.deltaTime;
			if (goblins.Count < 1) {
				ShowOnGrab.SetActive(false);
			}
		} else if (kbd.spaceKey.wasPressedThisFrame && grounded) {
			FallVelocity = -JumpVelocity;
			Anim.transform.localPosition = new Vector3(0, -1f, 0);
			Anim.SetTrigger("JumpTrigger");
		}

		if (kbd.sKey.wasPressedThisFrame && speedCache > DescendSpeedNeeded) {
			speedCache -= DescendSpeedNeeded;
			FloorManager.MoveDown();
			ScoreManager.AddScore(999);
		}

		float y = TileParent.position.y - transform.position.y;
		bool wasGrounded = grounded;
		grounded = !(y < ReferenceY || FallVelocity < 0);
		if (!grounded) {
			if (wasGrounded)
				Anim.SetBool("IsGrounded", false);

			FallVelocity += Gravity;
			// transform.localPosition += FallVelocity * Time.deltaTime * Vector3.down;
			transform.localPosition += FallVelocity * 0.008333f * Vector3.down;
		} else {
			FallVelocity = 0;
			if (!wasGrounded) {
				Anim.transform.localPosition = new Vector3(0, -1f, 0);
				Anim.SetBool("IsGrounded", true);
			}
		}

		// }

		// private void Update() {

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
		ScoreManager.AddScore(99);
		goblins.Clear();
		ShowOnGrab.SetActive(false);
		ReferenceY += y;
		CamControls.FlipMode();
		Anim.transform.localRotation = CamControls.RightMode ? Quaternion.identity : Quaternion.AngleAxis(180, Vector3.up);
		LastDescendZ = transform.position.z;
	}

	public void AttachGoblin(GoblinGRABBER goblin) {
		ScoreManager.AddScore(-8);
		goblins.Enqueue(goblin);
		ShowOnGrab.SetActive(true);
	}
}
