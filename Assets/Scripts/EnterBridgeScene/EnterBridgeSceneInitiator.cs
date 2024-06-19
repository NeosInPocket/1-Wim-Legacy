using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBridgeSceneInitiator : MonoBehaviour
{
	[SerializeField] private TMP_Text portalLevelText;


	private void Start()
	{
		var bridger = Bridger.bridger;
		portalLevelText.text = $"level {bridger.BridgerLevel}";
	}

	public void TransitToPlayPortal()
	{
		SceneManager.LoadScene("PlayBridgeScene");
	}
}
