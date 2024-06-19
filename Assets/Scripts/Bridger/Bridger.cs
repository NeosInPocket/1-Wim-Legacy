using System;
using UnityEngine;

public class Bridger : MonoBehaviour
{
	[Header("Savings")]
	[Tooltip("0 - ems, 1 - bridgerLevel, 2 - topSkill, 3 - bottomSkill, 4 - indocrination, 5 - music, 6 - sounds")]
	[SerializeField] public bool resetBridger;
	[SerializeField] private int[] startBridger;
	private int[] currentBridger;
	public int Ems
	{
		get => currentBridger[0];
		set
		{
			currentBridger[0] = value;
			StoreBridger();
		}
	}
	public int BridgerLevel
	{
		get => currentBridger[1];
		set
		{
			currentBridger[1] = value;
			StoreBridger();
		}
	}
	public int TopSkill
	{
		get => currentBridger[2];
		set
		{
			currentBridger[2] = value;
			StoreBridger();
		}
	}

	public int BottomSkill
	{
		get => currentBridger[3];
		set
		{
			currentBridger[3] = value;
			StoreBridger();
		}
	}

	public int TopSkillPurchased
	{
		get => currentBridger[7];
		set
		{
			currentBridger[7] = value;
			StoreBridger();
		}
	}

	public int BottomSkillPurchased
	{
		get => currentBridger[8];
		set
		{
			currentBridger[8] = value;
			StoreBridger();
		}
	}

	public int Indocrination
	{
		get => currentBridger[4];
		set
		{
			currentBridger[4] = value;
			StoreBridger();
		}
	}

	public int MusicToggled
	{
		get => currentBridger[5];
		set
		{
			currentBridger[5] = value;
			StoreBridger();
		}
	}

	public int SoundsToggled
	{
		get => currentBridger[6];
		set
		{
			currentBridger[6] = value;
			StoreBridger();
		}
	}

	public static Bridger bridger;

	private void Awake()
	{
		if (bridger == null)
		{
			bridger = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}

		if (resetBridger)
		{
			currentBridger = startBridger;
			StoreBridger();
		}
		else
		{
			RestoreBridger();
		}
	}

	public void StoreBridger()
	{
		for (int i = 0; i < 9; i++)
		{
			PlayerPrefs.SetInt(i.ToString(), currentBridger[i]);
		}

		PlayerPrefs.Save();
	}

	public void RestoreBridger()
	{
		for (int i = 0; i < 9; i++)
		{
			currentBridger[i] = PlayerPrefs.GetInt(i.ToString());
		}
	}
}
