using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImprovementsController : MonoBehaviour
{
	[SerializeField] private List<ImprovementCase> cases;
	[SerializeField] private TMP_Text emsText;
	[SerializeField] private TMP_Text upgradedProgression;

	private void Start()
	{
		OnPurchasement();
		cases.ForEach(x => x.ImprovementPurchased += OnPurchasement);
	}

	public void OnPurchasement()
	{
		var bridger = Bridger.bridger;
		cases.ForEach(x => x.CheckImprovements());
		emsText.text = bridger.Ems.ToString();

		int totalProgression = bridger.BottomSkill + bridger.TopSkill;
		totalProgression += bridger.TopSkillPurchased == 1 ? 1 : 0;
		totalProgression += bridger.BottomSkillPurchased == 1 ? 1 : 0;

		totalProgression = (int)((float)(totalProgression) / 6f * 100f);
		upgradedProgression.text = $"upgraded: {totalProgression}%";
	}

	private void OnDestroy()
	{
		cases.ForEach(x => x.ImprovementPurchased -= OnPurchasement);
	}
}
