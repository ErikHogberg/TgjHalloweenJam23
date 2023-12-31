using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

	protected abstract void HitPlayer(PlayerContoller player);


	private void OnTriggerEnter(Collider other) {
		Debug.Log($"enemy collision: {other.gameObject.name}");
		if (other.CompareTag("Player") && other.transform.parent.TryGetComponent<PlayerContoller>(out var component)){
			HitPlayer(component);
		}
	}
}
