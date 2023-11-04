using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour {

	public float NodeLerpSpeed = 1f;
	public AnimationCurve NodeLerpCurve = AnimationCurve.Linear(0, 0, 1, 1);
	[Tooltip("Speed multiplier applied at the end"), Min(0.001f)]
	public float FollowSpeedMul = 1f;
	[Tooltip("Min and max speed at each delta threshold")]
	public Vector2 FollowSpeedMinMax = Vector2.up;
	[Tooltip("Delta thresholds, at which distances between camera and target position the max and min speeds are reached")]
	public Vector2 DeltaMinMax = Vector2.up;
	public AnimationCurve FollowSpeedCurve = AnimationCurve.Linear(0, 0, 1, 1);

	public bool StartRightMode = true;
	public static bool RightMode = true;

	public static void FlipMode() { RightMode = !RightMode; }

	[Space]
	public Camera Cam;
	public Transform LeftNode;
	public Transform LeftNodeLookAt;
	public Transform RightNode;
	public Transform RightNodeLookAt;


	float lerp = 0;

	private void Start() {
		RightMode = StartRightMode;
	}

	void Update() {

		lerp = Mathf.MoveTowards(lerp, RightMode ? 1 : 0, NodeLerpSpeed * Time.deltaTime);

		Vector3 camPos = Cam.transform.position;

		float lerpEval = NodeLerpCurve.Evaluate(lerp);
		Vector3 target = Vector3.Lerp(LeftNode.position, RightNode.position, lerpEval);

		float deltaDistance = (camPos - target).magnitude;

		float speedLerp = (deltaDistance - DeltaMinMax.x) / (DeltaMinMax.y - DeltaMinMax.x);
		float speedEval = Mathf.Lerp(FollowSpeedMinMax.x, FollowSpeedMinMax.y, FollowSpeedCurve.Evaluate(speedLerp));

		Cam.transform.position = Vector3.MoveTowards(camPos, target, speedEval * Time.deltaTime);
		Cam.transform.LookAt(
			Vector3.Lerp(LeftNodeLookAt.position, RightNodeLookAt.position, lerpEval),
			Vector3.Lerp(LeftNodeLookAt.up, RightNodeLookAt.up, lerpEval)
		);
	}
}
