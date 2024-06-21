using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GuidanceStart : MonoBehaviour
{
	[SerializeField] private TMP_Text guidanceCaption;
	[SerializeField] public Animator guidanceAnimator;
	[SerializeField] public BridgeMain bridgeMain;
	[SerializeField] public string animatorTriggerValue;
	[SerializeField] public float charDelay;
	[TextArea]
	[SerializeField] private string[] guidances;
	private int currentGuidance;

	public void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void ShowGuidanceStart()
	{
		gameObject.SetActive(true);
	}

	public void EnableTouch()
	{
		Touch.onFingerDown += ShowNextCaption;
	}

	public void ShowNextCaption(Finger finger)
	{
		StartCoroutine(ShowString());
		Touch.onFingerDown -= ShowNextCaption;
		Touch.onFingerDown += SkipCurrentAction;
		guidanceAnimator.SetTrigger(animatorTriggerValue);
	}

	public void SkipCurrentAction(Finger finger)
	{
		Touch.onFingerDown -= SkipCurrentAction;
		Touch.onFingerDown += ShowNextCaption;
		StopAllCoroutines();
		guidanceCaption.text = guidances[currentGuidance];
		currentGuidance++;

		if (currentGuidance == guidances.Length)
		{
			StopGuidance();
		}
	}

	public IEnumerator ShowString()
	{
		if (currentGuidance == guidances.Length)
		{
			StopGuidance();
			yield break;
		}

		StringBuilder stringBuilder = new StringBuilder();
		int length = guidances[currentGuidance].Length;
		string currentString = guidances[currentGuidance];
		int currentIndex = 0;

		while (currentIndex < length)
		{
			stringBuilder.Append(currentString[currentIndex]);
			guidanceCaption.text = stringBuilder.ToString();
			currentIndex++;
			yield return new WaitForSeconds(charDelay);
		}

		Touch.onFingerDown -= SkipCurrentAction;
		Touch.onFingerDown += ShowNextCaption;
		currentGuidance++;
	}

	public void StopGuidance()
	{
		bridgeMain.OnGuidanceStopped();
		Touch.onFingerDown -= ShowNextCaption;
		Touch.onFingerDown -= SkipCurrentAction;
		gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= ShowNextCaption;
		Touch.onFingerDown -= SkipCurrentAction;
	}
}
