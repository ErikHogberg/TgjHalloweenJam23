using UnityEngine;

[CreateAssetMenu(fileName = "WaveDefinition", menuName = "GoblinGrabbers/WaveDefinition", order = 0)]
public class WaveDefinition : ScriptableObject {

	[System.Serializable]
	public class WaveEntry {
		public float Z = 0;
		public string[] Enemies;

		public int RolledEnemyIndex => Random.Range(0, Enemies.Length - 1);
		public string RandomEnemy => Enemies[RolledEnemyIndex];

	}

	[System.Serializable]
	public class FloorEntry {
		public int FloorsDown = 0;
		public WaveEntry[] Waves;

		public WaveEntry GetNearestWave(float zReached) {
			WaveEntry best = null;

			foreach (var item in Waves) {
				if (item.Z > zReached) continue;

				if (best == null || item.Z > best.Z) {
					best = item;
					continue;
				}
			}

			return best;
		}

		public bool TryGetNearestWave(float zReached, out WaveEntry wave) {
			wave = GetNearestWave(zReached);
			return wave != null;
		}
	}

	public FloorEntry[] Floors;

	public FloorEntry GetNearestFloor(uint floorReached) {
		FloorEntry best = null;

		foreach (var item in Floors) {
			if (item.FloorsDown > floorReached) continue;

			if (best == null || item.FloorsDown > best.FloorsDown) {
				best = item;
				continue;
			}
		}

		return best;
	}
	public WaveEntry GetNearestWave(FloorEntry floor, int zReached) {
		return floor.GetNearestWave(zReached);
	}
	public WaveEntry GetNearestWave(uint floorReached, int zReached) {
		return GetNearestWave(GetNearestFloor(floorReached), zReached);
	}
}
