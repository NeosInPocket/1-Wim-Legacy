using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Finger = UnityEngine.InputSystem.EnhancedTouch.Finger;

public class StartActionFrame : MonoBehaviour
{
	[SerializeField] private BridgeMain bridgeMain;

	public void ActionFrame()
	{
		gameObject.SetActive(true);
		Touch.onFingerDown += Close;
	}

	public void Close(Finger finger)
	{
		Touch.onFingerDown -= Close;
		bridgeMain.OnStartActionFrameClosed();
		gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= Close;
	}
}
