using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinGRABBER : Enemy {

	public float Speed = 2;

	bool move = true;

	private void Update() {
		if (move)
			transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.Self);
	}

	protected override void HitPlayer(PlayerContoller player) {
		move = false;
		// TODO: grab player		
	}
}
