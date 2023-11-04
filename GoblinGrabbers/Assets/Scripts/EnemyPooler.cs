using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPooler : MonoBehaviour {
	public static EnemyPooler MainInstance = null;

	void Awake() {
		MainInstance = this;
	}
	void OnDestroy() {
		MainInstance = null;
	}

	public class PoolEntry {
		public string Key;
		public Enemy Value;
	}

	public PoolEntry[] PoolEntries;

	Dictionary<string, Enemy> PoolDict;

	private void Start() {

		PoolDict = new(PoolEntries.Select(entry => new KeyValuePair<string, Enemy>(entry.Key, entry.Value)));
	}

	public static bool TryGet(string key, out Enemy value) {
		if (!MainInstance) {
			value = null;
			return false;
		}
		return MainInstance.PoolDict.TryGetValue(key, out value);
	}

}
