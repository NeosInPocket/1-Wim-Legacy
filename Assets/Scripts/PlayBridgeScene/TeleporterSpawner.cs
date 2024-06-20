using System.Collections.Generic;
using UnityEngine;

public class TeleporterSpawner : MonoBehaviour
{
	[SerializeField] public TeleporterPiece prefab;
	[SerializeField] private TeleporterPiece first;
	[SerializeField] private Vector2 spawns;
	[SerializeField] private float yVertical;
	[SerializeField] private float yVerticalBlocker;
	[SerializeField] private PortalJumper portalJumper;
	[SerializeField] private Blocker blocker;

	private TeleporterPiece current;
	private List<TeleporterPiece> pieces;
	public TeleporterPiece CurrentTarget => pieces[CurrentTargetIndex];
	public int CurrentTargetIndex { get; set; }

	private void Awake()
	{
		pieces = new();

		current = first;
		pieces.Add(current);
	}

	private void Update()
	{
		if (portalJumper.transform.position.y + yVertical > current.transform.position.y)
		{
			SpawnSingleTeleporter();
		}
	}

	public void SpawnSingleTeleporter()
	{
		TeleporterPiece lastPiece = current;

		current = Instantiate(prefab, new Vector2(0, current.transform.position.y + Random.Range(spawns.x, spawns.y)), Quaternion.identity, transform);
		Vector2 blockerPosition = new();

		Vector2 screenSize = BridgeMain.ScreenSize();
		float offset = 2 * screenSize.x * yVerticalBlocker;
		blockerPosition.x = Random.Range(-screenSize.x + offset, screenSize.x - offset);
		blockerPosition.y = Random.Range(lastPiece.transform.position.y + offset, current.transform.position.y - offset);
		var newBlocker = Instantiate(blocker, blockerPosition, Quaternion.identity, transform);

		pieces.Add(current);
	}
}
