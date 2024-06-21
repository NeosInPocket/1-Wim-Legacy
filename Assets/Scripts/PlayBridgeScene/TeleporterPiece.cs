using UnityEngine;

public class TeleporterPiece : MonoBehaviour
{
	[SerializeField] public float xScreenOffset;
	[SerializeField] public Vector2 frequencies;
	[SerializeField] public Vector3 startRotation;
	[SerializeField] public GameObject white;
	[SerializeField] public GameObject blue;
	[SerializeField] public GameObject troughEffect;
	private float offset;
	public Vector2 currentPosition;
	public float timeElapsed;
	public Vector2 screenSizeWithOffset;
	private float frequency;
	private int startDirection;
	public bool Passed;

	private void Start()
	{
		var screenSize = BridgeMain.ScreenSize();
		offset = 2 * xScreenOffset * screenSize.x;
		screenSizeWithOffset = screenSize;
		screenSizeWithOffset.x -= offset;

		timeElapsed = Random.Range(0, 2 * Mathf.PI);
		currentPosition = transform.position;
		transform.eulerAngles = startRotation;

		frequency = Random.Range(frequencies.x, frequencies.y);
		startDirection = Random.Range(0, 2) == 1 ? 1 : -1;
	}

	private void Update()
	{
		currentPosition.x = screenSizeWithOffset.x * Mathf.Sin(timeElapsed * frequency);
		transform.position = currentPosition;
		timeElapsed += startDirection * Time.deltaTime;
	}

	public void PassThrough()
	{
		if (Passed) return;
		Passed = true;
		white.SetActive(false);
		blue.SetActive(true);
		troughEffect.SetActive(true);
	}
}
