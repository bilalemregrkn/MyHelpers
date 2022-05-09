using UnityEngine;

namespace App.MyAudio
{
	public class AudioController : MonoBehaviour
	{
		[SerializeField] private AudioClip clip;

		[ContextMenu(nameof(Play))]
		public void Play()
		{
			AudioManager.Instance.Play(clip);
		}
	}
}