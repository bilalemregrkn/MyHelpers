using App.Helpers;
using UnityEngine;
using UnityEngine.Audio;

namespace App.MyAudio
{
	[RequireComponent(typeof(AudioSource))]
	public class AudioManager : Singleton<AudioManager>
	{
		public bool IsOn { get; set; } = true;

		[SerializeField] private AudioMixerSnapshot onSnapshot;
		[SerializeField] private AudioMixerSnapshot muteSnapshot;

		[SerializeField] [Range(0.01f, 1f)] private float transitionTime = 0.2f;

		private AudioSource _mySource;

		private void Awake()
		{
			_mySource = GetComponent<AudioSource>();
		}

		public void Play(AudioClip clip, float volume = 1)
		{
			if (clip == null) return;
			_mySource.PlayOneShot(clip, volume);
		}

		[ContextMenu(nameof(Toggle))]
		public void Toggle()
		{
			SetActive(!IsOn);
		}

		public void SetActive(bool active)
		{
			IsOn = active;
			var current = IsOn ? onSnapshot : muteSnapshot;
			current.TransitionTo(transitionTime);
		}
	}
}