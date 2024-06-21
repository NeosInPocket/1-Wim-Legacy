using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopLevelInformationHolder : MonoBehaviour
{
	[SerializeField] private Image progressFilling;
	[SerializeField] private TMP_Text progressTexting;
	[SerializeField] BridgeMain bridgeMain;

	public int CurrentPortals
	{
		get => currentPortals;
		set
		{
			currentPortals = value;
			if (currentPortals >= maxPortals)
			{
				currentPortals = maxPortals;
				bridgeMain.OnBridgeMainAllPortalsCompleted(ems);
			}

			progressFilling.fillAmount = (float)currentPortals / (float)maxPortals;
			progressTexting.text = $"{(int)((float)currentPortals / (float)maxPortals * 100f)}%";
		}
	}
	private int currentPortals;
	private int maxPortals;
	public int ems { get; private set; }

	private void Start()
	{
		progressFilling.fillAmount = 0;
		progressTexting.text = "0%";
		InitializeProgress();
	}

	public void InitializeProgress()
	{
		float x = (float)Bridger.bridger.BridgerLevel;
		maxPortals = (int)(3f * Mathf.Sqrt(x));
		ems = (int)(5f * Mathf.Sqrt(x));
		currentPortals = 0;
	}
}
