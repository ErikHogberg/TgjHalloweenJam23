using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBar : MonoBehaviour {

	public Transform BGBar;
	public Transform FGBar;

	float princessProgress = 1;
	float dragonProgress = 1;

	void Update() {
		princessProgress = Mathf.MoveTowards(princessProgress, 0, .15f * Time.deltaTime);
		dragonProgress = Mathf.MoveTowards(dragonProgress, 0, .1f * Time.deltaTime);

		FGBar.localScale = new Vector3(1, princessProgress, 1);
		BGBar.localScale = new Vector3(1, dragonProgress, 1);
	}
}
