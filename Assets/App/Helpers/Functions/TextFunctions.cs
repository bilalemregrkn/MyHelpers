using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace App.Helpers
{
	public class TextFunctions : Functions
	{
		public void ChangeFontSize(TMP_Text scaleThis, float toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(ChangeFontSize(scaleThis, toThis, delay ?? delayDefault, time ?? delayDefault, curve ?? curveDefault));
		}

		private static IEnumerator ChangeFontSize(TMP_Text scaleThis, float toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var init = scaleThis.fontSize;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var normalize = passed / time;
				var rate = curve.Evaluate(normalize);

				scaleThis.fontSize = Mathf.LerpUnclamped(init, toThis, rate);
				yield return null;
			}
		}

		public void NumberChange(TMP_Text textNumber, float targetNumber, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_NumberChange(textNumber, targetNumber, delay ?? delayDefault, time ?? timeDefault, curve));
		}

		private static IEnumerator _NumberChange(TMP_Text textNumber, float targetNumber, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);

			float passed = 0;
			float initialNumber = System.Convert.ToInt32(textNumber.text);

			while (passed <= time)
			{
				passed += Time.deltaTime;
				var rate = curve.Evaluate(passed / time);

				textNumber.text = Mathf.Lerp(initialNumber, targetNumber, rate).ToString("0");
				yield return null;
			}
		}
	}
}