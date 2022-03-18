using System.Collections;
using UnityEngine;

namespace App.Helpers
{
	public class BoingAnimation : MonoBehaviour
	{
		[SerializeField, Range(0, 100f)] public float scalePercent = 15f;
		[SerializeField, Range(0.1f, 1)] public float timeAnimation = 0.2f;
		[SerializeField] private AnimationCurve animationCurve = new AnimationCurve();

		private bool _isFirst;
		private Vector3 _initialScale;
		private bool _isWork;

		private void OnValidate()
		{
			if (animationCurve.length == 0)
				animationCurve = new AnimationCurve(new Keyframe(0, 0, 0, 2),
					new Keyframe(1, 1, 0, 0));
		}

		public void Play()
		{
			if (!_isFirst)
			{
				_isFirst = true;
				_initialScale = transform.localScale;
			}

			StopAllCoroutines();

			_isWork = true;
			var myTransform = transform;
			myTransform.localScale = _initialScale;
			var target = _initialScale + (_initialScale * (scalePercent / 100f));

			IEnumerator Animation()
			{
				yield return _Scale(myTransform, target, 0, timeAnimation / 2, animationCurve);
				yield return _Scale(transform, _initialScale, 0, timeAnimation / 2, animationCurve);
				_isWork = false;
			}

			StartCoroutine(Animation());
		}

		private void OnDisable()
		{
			StopAllCoroutines();
		}

		private static IEnumerator _Scale(Transform scaleThis, Vector3 toThis, float delay, float time,
			AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var initScale = scaleThis.localScale;
			while (passed < time)
			{
				passed += Time.deltaTime;
				var normalized = passed / time;
				var rate = curve.Evaluate(normalized);
				scaleThis.localScale = Vector3.LerpUnclamped(initScale, toThis, rate);
				yield return null;
			}
		}
	}
}