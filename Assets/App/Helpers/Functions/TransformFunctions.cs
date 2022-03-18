using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace App.Helpers
{
	public class TransformFunctions : Functions
	{
		#region Queue

		private readonly List<IEnumerator> _listIEnumerator = new List<IEnumerator>();

		public void AddQueue(IEnumerator function)
		{
			_listIEnumerator.Add(function);

			if (_listIEnumerator.Count == 1)
				StartCoroutine(ListProcess());
		}

		public void ListInsert(int index, IEnumerator function)
		{
			_listIEnumerator.Insert(index, function);
		}
		
		public void AddSubQueue(List<IEnumerator> functionList)
		{
			functionList.Reverse();

			int index = 0;
			if (_listIEnumerator.Count != 0)
			{
				index = 1;
			}

			foreach (var item in functionList)
			{
				_listIEnumerator.Insert(index, item);
			}

			if (index == 0)
			{
				StartCoroutine(ListProcess());
			}
		}

		private IEnumerator ListProcess()
		{
			while (_listIEnumerator.Count > 0)
			{
				yield return StartCoroutine(_listIEnumerator[0]);

				if (_listIEnumerator.Count > 0)
					_listIEnumerator.RemoveAt(0);
			}
		}

		#endregion

		#region Transform Component

		#region Position Functions

		public void Teleport(Transform teleportThis, Transform toThis, float? delay = null)
		{
			MyInvoke(delay ?? delayDefault, () => { teleportThis.position = toThis.position; });
		}

		public void Teleport(Transform teleportThis, Vector3 toThis, float? delay = null)
		{
			MyInvoke(delay ?? delayDefault, () => { teleportThis.position = toThis; });
		}

		public void Move(Transform moveThis, Transform toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_Move(moveThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		public void Move(Transform moveThis, Vector3 toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_Move(moveThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		private static IEnumerator _Move(Transform moveThis, Vector3 toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);

			var passed = 0f;
			var initPos = moveThis.position;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var normalize = passed / time;
				var rate = curve.Evaluate(normalize);

				moveThis.position = Vector3.LerpUnclamped(initPos, toThis, rate);
				yield return null;
			}
		}

		private static IEnumerator _Move(Transform moveThis, Transform toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var initPos = moveThis.position;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var normalize = passed / time;
				var rate = curve.Evaluate(normalize);

				moveThis.position = Vector3.LerpUnclamped(initPos, toThis.position, rate);
				yield return null;
			}
		}

		#endregion

		#region Rotation Functions

		public void ChangeRotation(Transform changeThis, Transform toThis, float? delay = null)
		{
			MyInvoke(delay ?? delayDefault, () => { changeThis.rotation = toThis.rotation; });
		}

		public void Rotate(Transform rotateThis, Transform toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_Rotate(rotateThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		private static IEnumerator _Rotate(Transform rotateThis, Transform toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var initRot = rotateThis.rotation;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var normalize = passed / time;
				var rate = curve.Evaluate(normalize);

				rotateThis.rotation = Quaternion.LerpUnclamped(initRot, toThis.rotation, rate);
				yield return null;
			}
		}

		public void ChangeRotation(Transform changeThis, Quaternion toThis, float? delay = null)
		{
			MyInvoke(delay ?? delayDefault, () => { changeThis.rotation = toThis; });
		}

		public void Rotate(Transform rotateThis, Quaternion toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_Rotate(rotateThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		private static IEnumerator _Rotate(Transform rotateThis, Quaternion toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var initRot = rotateThis.rotation;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var normalize = passed / time;
				var rate = curve.Evaluate(normalize);

				rotateThis.rotation = Quaternion.LerpUnclamped(initRot, toThis, rate);
				yield return null;
			}
		}

		#endregion

		#region Scale Functions

		public void ChangeScale(Transform changeThis, Transform toThis, float? delay = null)
		{
			MyInvoke(delay ?? delayDefault, () => { changeThis.localScale = toThis.localScale; });
		}

		public void ChangeScale(Transform changeThis, Vector3 toThis, float? delay = null)
		{
			MyInvoke(delay ?? delayDefault, () => { changeThis.localScale = toThis; });
		}

		public void Scale(Transform scaleThis, Transform toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_Scale(scaleThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		public void Scale(Transform scaleThis, Vector3 toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_Scale(scaleThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		private static IEnumerator _Scale(Transform scaleThis, Transform toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);

			var passed = 0f;
			var initScale = scaleThis.localScale;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var normalize = passed / time;
				var rate = curve.Evaluate(normalize);

				scaleThis.localScale = Vector3.LerpUnclamped(initScale, toThis.localScale, rate);
				yield return null;
			}
		}

		private static IEnumerator _Scale(Transform scaleThis, Vector3 toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);

			var passed = 0f;
			var initScale = scaleThis.localScale;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var normalize = passed / time;
				var rate = curve.Evaluate(normalize);

				scaleThis.localScale = Vector3.LerpUnclamped(initScale, toThis, rate);
				yield return null;
			}
		}

		#endregion

		#endregion

		#region Component Functions

		public void SetEnabledAfter<T>(GameObject go, bool isActive, float? delay = null) where T : Component
		{
			StartCoroutine(_SetEnabledAfter<T>(go, isActive, delay ?? delayDefault));
		}

		private IEnumerator _SetEnabledAfter<T>(GameObject go, bool isActive, float delay) where T : Component
		{
			var component = go.GetComponent<T>();
			yield return new WaitForSeconds(delay);

			if (!typeof(T).IsSubclassOf(typeof(MonoBehaviour))) yield break;
			if (component is MonoBehaviour { } current) current.enabled = isActive;
		}

		#endregion

		#region Position UI -Rect Transform- Functions

		public void MoveUI(RectTransform moveThis, RectTransform toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_MoveUI(moveThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		public void MoveUI(RectTransform moveThis, Vector3 toThis, float? delay = null, float? time = null, [CanBeNull] AnimationCurve curve = null)
		{
			StartCoroutine(_MoveUI(moveThis, toThis, delay ?? delayDefault, time ?? timeDefault, curve ?? curveDefault));
		}

		private IEnumerator _MoveUI(RectTransform moveThis, RectTransform toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			ChangeAnchor(moveThis, toThis);
			var passed = 0f;
			var initPos = moveThis.localPosition;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var normalize = passed / time;
				var rate = curve.Evaluate(normalize);

				moveThis.localPosition = Vector3.LerpUnclamped(initPos, toThis.localPosition, rate);
				yield return null;
			}
		}

		private IEnumerator _MoveUI(RectTransform moveThis, Vector3 toThis, float delay, float time, AnimationCurve curve)
		{
			yield return new WaitForSeconds(delay);
			var passed = 0f;
			var initPos = moveThis.localPosition;

			while (passed < time)
			{
				passed += Time.deltaTime;
				var normalize = passed / time;
				var rate = curve.Evaluate(normalize);

				moveThis.localPosition = Vector3.LerpUnclamped(initPos, toThis, rate);
				yield return null;
			}
		}

		private void ChangeAnchor(RectTransform current, RectTransform target)
		{
			ChangePivot(current, target);

			var currentTransform = current.transform;
			Vector3 initialPos = currentTransform.position;
			current.anchorMin = target.anchorMin;
			current.anchorMax = target.anchorMax;
			currentTransform.position = initialPos;
		}

		private void ChangePivot(RectTransform current, RectTransform target)
		{
			var currentRect = current.rect;
			float width = currentRect.width; //Get width Current object 
			float height = currentRect.height; //Get height Current object 

			Vector2 initialAnchoredPosition = current.anchoredPosition; //Get initial position
			var pivot = target.pivot;
			Vector2 pivotDistance = pivot - current.pivot; //Dönüşecegi target'in pivotları arası fark bulunur.


			//Pivot noktası değiştiği zaman achored pos sabit kaldığı için objenin konumu değişiyor gibi görünüyor.
			current.pivot = pivot;

			//Objenin width & height'ına göre ufak bir konum değişikligi yapmalıyız.
			Vector2 offsetPos = new Vector2(0, 0);

			offsetPos.x = width * pivotDistance.x; //pivot farklarına göre oranlar bulunur ve offset konum bulunur.
			offsetPos.y = height * pivotDistance.y;

			current.anchoredPosition =
				initialAnchoredPosition + offsetPos; //İlk konumuna offset eklenince obje hiç kıpırdamamış olur.

			/*Böylece pivot noktası değişse dahi obje aynı şekilde kalacak.*/
		}

		#endregion
	}
}