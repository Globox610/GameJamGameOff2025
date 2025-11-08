using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class BasicHookable : MonoBehaviour, IHookable, IInteractable
{

	public Action OnHooked;
	Vector3 bezierLerp(float t, Vector3 p0, Vector3 p1, Vector2 p2)
	{
		Vector3 a = Vector3.Lerp(p0, p1, t);
		Vector3 b = Vector3.Lerp(p1, p2, t);
		return Vector3.Lerp(a, b, t);
	}

	IEnumerator playAnimation(GameObject player)
	{
		Vector3 startPos = transform.position;
		Vector3 endPos = player.transform.position;
		Vector3 middlePos = Vector3.Lerp(startPos, endPos, 0.7f) + Vector3.up * 2;

		const float MIN_DURATION = 0.3f;

		float progress = 0.0f;
		float duration = (startPos - endPos).magnitude * 0.1f;
		duration = Mathf.Max(duration, MIN_DURATION);

		while (progress < 1.0f)
		{

			endPos = player.transform.position;

			//transform.position = bezierLerp(progress, startPos, middlePos, endPos);

			transform.position = Vector3.Lerp(startPos, player.transform.position, progress);

			progress += Time.deltaTime / duration;

			yield return null;
		}

		OnHooked.Invoke();
		Destroy(gameObject);
		print("Item collected");
		yield return 0;
	}

	public void OnHook(GameObject player)
	{
		StartCoroutine(playAnimation(player));
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnInteract(GameObject interactor)
	{
		OnHook(interactor);
	}
}
