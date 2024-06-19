using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImprovementCase : MonoBehaviour
{
	[SerializeField] public List<Image> lockedImages;
	[SerializeField] public List<Image> activeImages;
	[SerializeField] public List<TMP_Text> texts;
	[SerializeField] public List<Button> buttons;
	[SerializeField] public Image upgradeLine;
	[SerializeField] public bool topUpgrade;
	[SerializeField] public int startCost;

	public Action ImprovementPurchased;

	public void CheckImprovements()
	{
		var bridger = Bridger.bridger;
		int currentImprovement = topUpgrade ? bridger.TopSkill : bridger.BottomSkill;
		lockedImages.ForEach(x => x.gameObject.SetActive(false));
		activeImages.ForEach(x => x.gameObject.SetActive(false));
		buttons.ForEach(x => x.interactable = false);

		switch (currentImprovement)
		{
			case 0:
				{
					lockedImages[1].gameObject.SetActive(true);
					lockedImages[2].gameObject.SetActive(true);
					buttons[0].interactable = true;

					upgradeLine.fillAmount = 0f;
				}
				break;

			case 1:
				{
					activeImages[0].gameObject.SetActive(true);
					lockedImages[2].gameObject.SetActive(true);
					buttons[1].interactable = true;

					upgradeLine.fillAmount = 0f;
				}
				break;

			case 2:
				{
					activeImages[0].gameObject.SetActive(true);
					activeImages[1].gameObject.SetActive(true);
					buttons[2].interactable = true;
					upgradeLine.fillAmount = 0.5f;
				}
				break;

			case 3:
				{
					activeImages[0].gameObject.SetActive(true);
					activeImages[1].gameObject.SetActive(true);
					activeImages[2].gameObject.SetActive(true);
					upgradeLine.fillAmount = 1f;
				}
				break;
		}

		int currentEms = bridger.Ems;
		if (currentImprovement >= 3) return;

		if (currentEms < startCost * (currentImprovement + 1))
		{
			buttons[currentImprovement].interactable = false;
			texts[currentImprovement].color = Color.red;
		}
		else
		{
			buttons[currentImprovement].interactable = true;
			texts[currentImprovement].color = Color.white;
		}
	}

	public void PurchaseImprovement(int index)
	{
		var bridger = Bridger.bridger;
		bridger.Ems -= startCost * (index + 1);
		if (topUpgrade)
		{
			bridger.TopSkill++;
			if (index == 0)
			{
				bridger.TopSkill = 1;
			}
		}
		else
		{
			bridger.BottomSkill++;
			if (index == 0)
			{
				bridger.BottomSkill = 1;
			}
		}



		ImprovementPurchased?.Invoke();
	}
}
