using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : Enemy {

	static float Countdown = -1;

	static IEnumerator ReenableCountdown(){
		while (Countdown > 0)
		{
			Countdown -= Time.deltaTime;
			yield return null;
		}
	}

	protected override void HitPlayer(PlayerContoller player) {
		enabled = false;
		if (Countdown > 0) return;
		
		Countdown = 1;
		FloorManager.MoveDown();
		player.StartCoroutine(ReenableCountdown());
	}
}
