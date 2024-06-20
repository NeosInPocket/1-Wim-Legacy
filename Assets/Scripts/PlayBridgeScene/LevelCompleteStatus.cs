using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteStatus : MonoBehaviour
{
	[SerializeField] private TMP_Text levelLoseWinRaiseCaption;
	[SerializeField] private TMP_Text emsCaption;
	[SerializeField] private TMP_Text playButton;

	public void CompleteStatus(int ems, int bridgeLevel, int winState)
	{
		gameObject.SetActive(true);

		if (winState == 1)
		{
			levelLoseWinRaiseCaption.text = $"level {bridgeLevel} passed!";
			emsCaption.text = ems.ToString();
			playButton.text = $"level {bridgeLevel + 1}";
		}
		else
		{
			levelLoseWinRaiseCaption.text = $"level {bridgeLevel} failed";
			emsCaption.text = 0.ToString();
			playButton.text = $"try again";
		}
	}

	public void GenerateNextScene(bool value)
	{
		string nextSceneValue = String.Empty;

		if (value)
		{
			nextSceneValue = "PlayBridgeScene";
		}
		else
		{
			nextSceneValue = "EnterBridgeScene";
		}

		SceneManager.LoadScene(nextSceneValue);
	}
}
