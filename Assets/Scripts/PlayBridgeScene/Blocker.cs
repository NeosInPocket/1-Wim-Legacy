using UnityEngine;

public class Blocker : MonoBehaviour
{
	[SerializeField] private Vector2 randomSize;

	private void Start()
	{
		var random = Random.Range(randomSize.x, randomSize.y);
		transform.localScale = new Vector3(random, random, random);
	}
}
