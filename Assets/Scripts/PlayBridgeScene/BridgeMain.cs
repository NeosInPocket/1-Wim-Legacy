using UnityEngine;

public class BridgeMain : MonoBehaviour
{
	[SerializeField] private PortalJumper portalJumper;
	[SerializeField] private GuidanceStart guidanceStart;
	[SerializeField] private StartActionFrame startActionFrame;
	[SerializeField] private TopLevelInformationHolder topLevelInformationHolder;
	[SerializeField] private LevelCompleteStatus levelCompleteStatus;
	[SerializeField] private FuelShifter fuelShifter;
	public bool levelCompleted;

	public void Init()
	{
		topLevelInformationHolder.InitializeProgress();

		if (Bridger.bridger.Indocrination == 1)
		{
			Bridger.bridger.Indocrination = 0;
			guidanceStart.ShowGuidanceStart();
		}
		else
		{
			OnGuidanceStopped();
		}
	}

	public static Vector2 ScreenSize()
	{
		Vector2 result = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
		return result;
	}

	public void OnBridgeMainAllPortalsCompleted(int ems)
	{
		portalJumper.DisableWindApplying();
		int level = Bridger.bridger.BridgerLevel;
		levelCompleteStatus.CompleteStatus(ems, $"level {level} completed!", level, 1);

		Bridger.bridger.BridgerLevel++;
		Bridger.bridger.Ems += ems;
		levelCompleted = true;
	}

	public void OnRunOutOfFuel()
	{
		portalJumper.DisableWindApplying();
		int level = Bridger.bridger.BridgerLevel;
		levelCompleteStatus.CompleteStatus(0, $"ran out of fuel!", level, 0);
		levelCompleted = true;
	}

	public void OnOverloaded(string caption)
	{
		portalJumper.DisableWindApplying();
		int level = Bridger.bridger.BridgerLevel;
		levelCompleteStatus.CompleteStatus(0, caption, level, 0);
		levelCompleted = true;
	}

	public void OnPassThrough()
	{
		topLevelInformationHolder.CurrentPortals++;
	}

	public void OnStartActionFrameClosed()
	{
		portalJumper.EnableWindApplying();

		var x = Bridger.bridger.BridgerLevel;
		fuelShifter.StartFuelCount(5f / (x + 1f) + 5f);
	}

	public void OnGuidanceStopped()
	{
		startActionFrame.ActionFrame();
	}
}
