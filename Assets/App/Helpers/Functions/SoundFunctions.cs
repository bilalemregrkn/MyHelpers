using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace App.Helpers
{
	public class SoundFunctions : Functions
	{
		public void ChangeVolume(AudioSource volumeThis, float toVolume, float? delay = null)
		{
			MyInvoke(delay ?? delayDefault, () => { volumeThis.volume = toVolume; });
		}

		public void Volume(AudioSource volumeThis, float toVolume, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_Volume(volumeThis, toVolume, delay ?? delayDefault, time ?? timeDefault, curve));
		}

		private static IEnumerator _Volume(AudioSource volumeThis, float toVolume, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var initVolume = volumeThis.volume;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var rate = curve.Evaluate(passed / time);

				volumeThis.volume = Mathf.LerpUnclamped(initVolume, toVolume, rate);
				yield return null;
			}
		}
	}
}