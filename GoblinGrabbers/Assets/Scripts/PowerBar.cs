using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBar : MonoBehaviour {
	public PlayerContoller Player;
	public Transform Bar;
	public float MinOffset = 1;

	private void Update() {
		Bar.localScale = new Vector3(1, Mathf.Max(0.01f, (Player.speedCache - MinOffset) / (Player.SpeedCap - MinOffset)), 1);
	}
}
