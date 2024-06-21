using UnityEngine;

public class StartTransition : MonoBehaviour
{
	[SerializeField] private BridgeMain bridgeMain;

	public void StartBridgeMain()
	{
		bridgeMain.Init();
		gameObject.SetActive(false);
	}
}
