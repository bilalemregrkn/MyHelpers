using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace App.Helpers
{
	public abstract class Functions : MonoBehaviour
	{
		[SerializeField] protected AnimationCurve curveDefault = new AnimationCurve();
		[SerializeField] [Range(0, 5)] protected float delayDefault;
		[SerializeField] [Range(0, 5)] protected float timeDefault = 1f;

		private void OnValidate()
		{
			if (curveDefault.length == 0)
				curveDefault = AnimationCurve.EaseInOut(0, 0, 1, 1);
		}

		public void MyInvoke(float delay, UnityAction action)
		{
			StartCoroutine(_MyInvoke(delay, action));
		}

		private static IEnumerator _MyInvoke(float delay, UnityAction action)
		{
			yield return new WaitForSeconds(delay);
			action?.Invoke();
		}
	}
}