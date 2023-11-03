using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{

	[Tooltip("Speed multiplier applied at the end"), Min(0.001f)]
    public float FollowSpeedMul = 1f;
	// [Tooltip("Min and max speed at each delta threshold")]
	// public Vector2 FollowSpeedMinMax = Vector2.up;
	// [Tooltip("Delta thresholds, at which distances between camera and target position the max and min speeds are reached")]
	// public Vector2 DeltaMinMax = Vector2.up;

    public bool RightMode = true;

    [Space]
    public Camera Cam;
    public Transform LeftNode;
    public Transform RightNode;


	// float lerp = 0;

    void Update()
    {

        Vector3 camPos = Cam.transform.position;
        Vector3 nodePos = RightMode ? RightNode.position : LeftNode.position;
        Vector3 otherNodePos = !RightMode ? RightNode.position : LeftNode.position;

        Vector3 delta =  nodePos - camPos;
        float lerp = delta.magnitude / (nodePos - otherNodePos).magnitude;


        Cam.transform.position = Vector3.MoveTowards(camPos, nodePos, delta.sqrMagnitude * FollowSpeedMul * Time.deltaTime);

        Cam.transform.rotation = Quaternion.Lerp(LeftNode.rotation, RightNode.rotation, !RightMode ? lerp : 1f - lerp);
    }
}
