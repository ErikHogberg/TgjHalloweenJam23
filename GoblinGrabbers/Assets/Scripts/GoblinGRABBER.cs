using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinGRABBER : Enemy {

	public float Speed = 2;

	bool move = true;
	bool grabbed = false;
	float followZ = 0;

	public int HP = 5;

	private void Update() {

		if (move) {
			transform.Translate(Vector3.forward * -Speed * Time.deltaTime, Space.Self);
		} else {
			var pos = transform.position;
			pos.z = followZ;
			transform.position = pos;
		}

	}

	public bool Wack(){
		HP -= 1;
		
		bool ded = HP < 1;
		if (ded) move = true;

		return ded;
	}

	protected override void HitPlayer(PlayerContoller player) {
		if (grabbed) return;
		grabbed = true;
		move = false;
		followZ = transform.position.z;
		Debug.Log("grabbed player");
		player.AttachGoblin(this);
	}
}
