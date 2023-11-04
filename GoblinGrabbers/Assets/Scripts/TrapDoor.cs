using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : Enemy {
	
	protected override void HitPlayer(PlayerContoller player) {
		enabled = false;
		FloorManager.MoveDown();
	}
}
