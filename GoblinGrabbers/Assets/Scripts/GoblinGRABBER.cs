using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinGRABBER : Enemy {

	public float Speed = 2;

	bool move = true;
	float followZ = 0;

	private void Update() {
		if (move) {
			transform.Translate(Vector3.forward * -Speed * Time.deltaTime, Space.Self);
		} else {
			var pos = transform.position;
			pos.z = followZ;
			transform.position = pos;
		}


	}

	protected override void HitPlayer(PlayerContoller player) {
		move = false;
		followZ = transform.position.z;
		Debug.Log("grabbed player");
		// TODO: grab player		
	}
}
