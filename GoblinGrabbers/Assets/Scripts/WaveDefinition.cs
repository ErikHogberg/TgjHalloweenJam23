using UnityEngine;

[CreateAssetMenu(fileName = "WaveDefinition", menuName = "GoblinGrabbers/WaveDefinition", order = 0)]
public class WaveDefinition : ScriptableObject {
	[System.Serializable]
	public class WaveEntry {
		public float Z = 0;
		public string[] Enemies;
	}

	[System.Serializable]
	public class FloorEntry {
		public int FloorsDown = 0;
		public WaveEntry[] Waves;
	}

	public FloorEntry[] Waves;

}
