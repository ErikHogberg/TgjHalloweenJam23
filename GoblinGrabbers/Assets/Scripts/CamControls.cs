using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{

    public float FollowSpeedMul = 1f;
    public bool RightMode = true;

    [Space]
    public Camera Cam;
    public Transform LeftNode;
    public Transform RightNode;

    void Update()
    {

        Vector3 localPosition = Cam.transform.localPosition;
        Vector3 nodePos = Cam.transform.InverseTransformPoint(RightMode ? RightNode.position : LeftNode.position);
        Vector3 otherNodePos = Cam.transform.InverseTransformPoint(!RightMode ? RightNode.position : LeftNode.position);

        Vector3 delta = localPosition - nodePos;
        Cam.transform.localPosition = Vector3.MoveTowards(localPosition, nodePos, delta.sqrMagnitude * Time.deltaTime);

        float lerp = delta.magnitude / (nodePos - otherNodePos).magnitude;
        Cam.transform.rotation = Quaternion.Lerp(LeftNode.rotation, RightNode.rotation, RightMode ? lerp : 1f - lerp);
    }
}
