using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelShifter : MonoBehaviour
{
	[SerializeField] private Image fuelFill;
	[SerializeField] private TMP_Text fuelStatus;
	[SerializeField] private TMP_Text levelStatus;
	[SerializeField] private BridgeMain bridgeMain;
	[HideInInspector] public float FuelValue;


	private float currentFuelLeft;
	private bool Active;

	private void Start()
	{
		levelStatus.text = "level " + Bridger.bridger.BridgerLevel;
	}

	public void StartFuelCount(float fuelValue)
	{
		FuelValue = fuelValue;
		currentFuelLeft = fuelValue;
		Active = true;
	}

	private void Update()
	{
		if (!Active) return;

		currentFuelLeft -= Time.deltaTime;

		if (currentFuelLeft < 0)
		{
			Active = false;
			bridgeMain.OnRunOutOfFuel();
			currentFuelLeft = 0;
		}

		fuelFill.fillAmount = (float)currentFuelLeft / (float)FuelValue;
	}
}
