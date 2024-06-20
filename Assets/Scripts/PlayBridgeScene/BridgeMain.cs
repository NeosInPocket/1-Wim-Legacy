using UnityEngine;

public class BridgeMain : MonoBehaviour
{
	[SerializeField] private PortalJumper portalJumper;
	[SerializeField] private GuidanceStart guidanceStart;
	[SerializeField] private StartActionFrame startActionFrame;
	[SerializeField] private TopLevelInformationHolder topLevelInformationHolder;
	[SerializeField] private LevelCompleteStatus levelCompleteStatus;
	[SerializeField] private FuelShifter fuelShifter;

	private void Start()
	{
		portalJumper.EnableWindApplying();
	}

	public static Vector2 ScreenSize()
	{
		Vector2 result = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
		return result;
	}

	public void OnBridgeMainAllPortalsCompleted(int ems)
	{

	}

	public void OnRunOutOfFuel()
	{

	}
}
