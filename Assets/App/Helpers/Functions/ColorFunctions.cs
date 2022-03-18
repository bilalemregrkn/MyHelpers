using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Helpers
{
	public class ColorFunctions : Functions
	{
		#region Sprite

		public void ChangeColor(SpriteRenderer changeThis, Color toThis, float? delay = null)
		{
			MyInvoke(delay ?? delayDefault, () => { changeThis.color = toThis; });
		}

		public void ColorTransition(SpriteRenderer changeThis, Color toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_ColorTransition(changeThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		private static IEnumerator _ColorTransition(SpriteRenderer changeThis, Color toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var initColor = changeThis.color;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var rate = curve.Evaluate(passed / time);

				changeThis.color = Color.LerpUnclamped(initColor, toThis, rate);
				yield return null;
			}
		}

		#endregion

		#region Image

		public void ChangeColor(Image changeThis, Color toThis, float? delay = null)
		{
			MyInvoke(delay ?? delayDefault, () => { changeThis.color = toThis; });
		}

		public void ColorTransition(Image changeThis, Color toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_ColorTransition(changeThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		private static IEnumerator _ColorTransition(Image changeThis, Color toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var initColor = changeThis.color;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var rate = curve.Evaluate(passed / time);

				changeThis.color = Color.LerpUnclamped(initColor, toThis, rate);
				yield return null;
			}
		}

		#endregion

		#region Text

		public void ChangeColor(TMP_Text changeThis, Color toThis, float? delay = null)
		{
			MyInvoke(delay ?? delayDefault, () => { changeThis.color = toThis; });
		}

		public void ColorTransition(TMP_Text changeThis, Color toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_ColorTransition(changeThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		private static IEnumerator _ColorTransition(Graphic changeThis, Color toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var initColor = changeThis.color;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var rate = curve.Evaluate(passed / time);

				changeThis.color = Color.LerpUnclamped(initColor, toThis, rate);
				yield return null;
			}
		}

		#endregion

		#region CanvasAlpha

		public void CanvasGroupAlpha(CanvasGroup canvasObject, float targetAlpha, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_CanvasGroupAlpha(canvasObject, targetAlpha, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		private static IEnumerator _CanvasGroupAlpha(CanvasGroup canvasObject, float targetAlpha, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var initAlpha = canvasObject.alpha;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var rate = curve.Evaluate(passed / time);

				var alpha = Mathf.Lerp(initAlpha, targetAlpha, rate);
				canvasObject.alpha = alpha;
				yield return null;
			}
		}

		#endregion

		#region Material

		#region Color

		public void ChangeColor(Renderer changeThis, Color toThis, float? delay = null)
		{
			MyInvoke(delay ?? delayDefault, () => { changeThis.material.color = toThis; });
		}

		public void ColorTransition(Renderer changeThis, Color toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_ColorTransition(changeThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		private static IEnumerator _ColorTransition(Renderer changeThis, Color toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var initColor = changeThis.material.color;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var rate = curve.Evaluate(passed / time);

				changeThis.material.color = Color.LerpUnclamped(initColor, toThis, rate);
				yield return null;
			}
		}

		#endregion

		public void ChangeMaterial(Renderer changeThis, Material toThis, float? delay = null)
		{
			MyInvoke(delay ?? delayDefault, () => { changeThis.material = toThis; });
		}

		public void MaterialTransition(Renderer changeThis, Material toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_MaterialTransition(changeThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		private IEnumerator _MaterialTransition(Renderer changeThis, Material toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var initMat = changeThis.material;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var rate = curve.Evaluate(passed / time);

				changeThis.material.Lerp(initMat, toThis, rate);
				yield return null;
			}
		}

		#endregion
	}
}