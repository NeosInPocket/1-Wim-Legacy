using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundWaveController : MonoBehaviour
{
	public AudioSource waveSource;
	public static SoundWaveController Wave;

	public void Awake()
	{
		if (Wave == null)
		{
			Wave = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		OnSoundWave(Bridger.bridger.MusicToggled == 1);
	}

	public void Toggle()
	{
		OnSoundWave(Bridger.bridger.MusicToggled != 1);
	}

	public void OnSoundWave(bool value)
	{
		int finalVolume = value ? 1 : 0;
		waveSource.volume = finalVolume;
		Bridger.bridger.MusicToggled = finalVolume;
	}

	public void ToggleSoundEffectsWave()
	{
		int finalVolume = Bridger.bridger.SoundsToggled == 1 ? 0 : 1;
		Bridger.bridger.SoundsToggled = finalVolume;
	}
}
