using UnityEngine;
using UnityEngine.UI;

public class SoundWaveSettings : MonoBehaviour
{
	[SerializeField] private Image musicToggler;
	[SerializeField] private Image soundsToggler;
	[SerializeField] private Sprite musicActive;
	[SerializeField] private Sprite musicInactive;
	[SerializeField] private Sprite soundsActive;
	[SerializeField] private Sprite soundsInactive;

	private void Start()
	{
		ChangeBridgeControlsState();
	}

	public void MusicToggler()
	{
		SoundWaveController.Wave.Toggle();
		ChangeBridgeControlsState();
	}

	public void SoundsToggler()
	{
		SoundWaveController.Wave.ToggleSoundEffectsWave();
		ChangeBridgeControlsState();
	}

	public void ChangeBridgeControlsState()
	{
		musicToggler.sprite = Bridger.bridger.MusicToggled == 1 ? musicActive : musicInactive;
		soundsToggler.sprite = Bridger.bridger.SoundsToggled == 1 ? soundsActive : soundsInactive;
	}
}
