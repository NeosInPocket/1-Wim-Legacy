using System.Collections;
using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Finger = UnityEngine.InputSystem.EnhancedTouch.Finger;
using UnityEngine.InputSystem.EnhancedTouch;
using System;

public class PortalJumper : MonoBehaviour
{
	[Header("Wind force upgrades")]
	[SerializeField] private float[] windForces;
	[Header("Vertical speed upgrades")]
	[SerializeField] private float[] verticalSpeeds;
	[Header("Misc")]
	[SerializeField] private Rigidbody2D portalRigidBody;
	[SerializeField] private SpriteRenderer portalRenderer;
	[SerializeField] private float slowDownSpeed;
	[Header("Wind gameobjects")]
	[SerializeField] private ParticleSystem leftRight;
	[SerializeField] private ParticleSystem rightLeft;
	[SerializeField] private TeleporterSpawner teleporterSpawner;
	[SerializeField] private GameObject overloadedEffect;
	[SerializeField] private Collider2D colliderTwoDim;
	[SerializeField] private float shipDimension;
	[SerializeField] private FuelShifter fuelShifter;

	private float windForce;
	private float verticalSpeed;
	[HideInInspector] public bool ApplyingForce { get; set; }
	[HideInInspector] public bool overloaded;
	[HideInInspector] public Vector2 screenSize;
	[SerializeField] private BridgeMain bridgeMain;


	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		verticalSpeed = verticalSpeeds[Bridger.bridger.TopSkill];
		windForce = windForces[Bridger.bridger.BottomSkill];
		screenSize = BridgeMain.ScreenSize();
	}

	public void EnableVerticalSpeed()
	{
		portalRigidBody.velocity = Vector2.up * verticalSpeed;
	}

	public void EnableWindApplying()
	{
		Touch.onFingerDown += OnWindApply;
		Touch.onFingerUp += OnWindDeny;
		EnableVerticalSpeed();
		StopAllCoroutines();
	}

	public void DisableWindApplying()
	{
		Touch.onFingerDown -= OnWindApply;
		Touch.onFingerUp -= OnWindDeny;
		StopWind();
		portalRigidBody.velocity = Vector2.zero;
	}

	private void Update()
	{
		if (transform.position.y > teleporterSpawner.CurrentTarget.transform.position.y && !overloaded)
		{
			if (!bridgeMain.levelCompleted)
			{
				bridgeMain.OnOverloaded("net missed!");
				overloaded = true;
				OverloadedEffect();
			}
		}

		if (transform.position.x - shipDimension < -screenSize.x || transform.position.x + shipDimension > screenSize.x)
		{
			if (!bridgeMain.levelCompleted)
			{
				bridgeMain.OnOverloaded("crashed!");
				overloaded = true;
				OverloadedEffect();
			}
		}

		if (ApplyingForce) return;

		if (Mathf.Abs(portalRigidBody.velocity.x) > 0)
		{
			int velocityDirection = portalRigidBody.velocity.x > 0 ? 1 : -1;
			Vector2 velocity = portalRigidBody.velocity;
			velocity.x -= velocityDirection * windForce * Time.deltaTime;
			portalRigidBody.velocity = velocity;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<TeleporterPiece>(out TeleporterPiece piece))
		{
			if (piece.Passed) return;

			if (!bridgeMain.levelCompleted)
			{
				teleporterSpawner.CurrentTargetIndex++;
				piece.PassThrough();
				bridgeMain.OnPassThrough();
				return;
			}
		}

		if (collider.TryGetComponent<Blocker>(out Blocker blocker))
		{
			if (!bridgeMain.levelCompleted)
			{
				bridgeMain.OnOverloaded("crashed!");
				overloaded = true;
				OverloadedEffect();
			}
		}
	}

	public void OverloadedEffect()
	{
		overloadedEffect.SetActive(true);
		DisableWindApplying();
		portalRigidBody.velocity = Vector2.zero;
		colliderTwoDim.enabled = false;
		portalRenderer.enabled = false;
		ApplyingForce = false;
		StopAllCoroutines();
	}

	public void OnWindApply(Finger finger)
	{
		AddWindForce(finger.screenPosition.x / Screen.width > 0.5f);
	}

	public void OnWindDeny(Finger finger)
	{
		StopWind();
	}

	public void AddWindForce(bool fromRightSide)
	{
		ApplyingForce = true;
		StartCoroutine(WindForceCoroutine(fromRightSide));
		fuelShifter.Active = true;
	}

	public void StopWind()
	{
		rightLeft.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		leftRight.Stop(true, ParticleSystemStopBehavior.StopEmitting);
		ApplyingForce = false;
		fuelShifter.Active = false;
	}

	private IEnumerator WindForceCoroutine(bool fromRightSide)
	{
		int windForceDirection = fromRightSide ? -1 : 1;

		if (fromRightSide)
		{
			if (!rightLeft.isEmitting)
			{
				rightLeft.Play();
				leftRight.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}
		}
		else
		{
			if (!leftRight.isEmitting)
			{
				leftRight.Play();
				rightLeft.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}
		}

		while (ApplyingForce)
		{
			Vector2 velocity = portalRigidBody.velocity;
			velocity.x += windForceDirection * windForce * Time.deltaTime;
			portalRigidBody.velocity = velocity;
			yield return null;
		}
	}

	private void OnDestroy()
	{
		DisableWindApplying();
	}
}

