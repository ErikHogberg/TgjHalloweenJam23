using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	private static ScoreManager mainInstance = null;
	void Awake() {
		mainInstance = this;
	}
	void OnDestroy() {
		mainInstance = null;
	}

	int scoreAmount = 0;
	public TMP_Text scoreText;
	public TMP_Text scoreDeltaText;

	void AddScoreInternal(int score){
		scoreAmount += score;
		if (scoreAmount < 0) scoreAmount = 0;
		scoreText.SetText(scoreAmount.ToString("000000000"));
		bool subtract = score < 0;
		scoreDeltaText.SetText((subtract ? "":"+") + score.ToString());
		scoreDeltaText.color = subtract ? Color.red : Color.green;
	}

	public static void AddScore(int score){
		if (mainInstance) mainInstance.AddScoreInternal(score);
	}


}
