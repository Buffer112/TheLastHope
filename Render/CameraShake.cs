using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	[SerializeField] Transform camTransform;
	// How long the object should shake for.
	[SerializeField] float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	[SerializeField] float shakeAmount = 0.7f;
	[SerializeField] float decreaseFactor = 1.0f;

    public Transform CamTransform { get { return camTransform; } set { camTransform = value; } }
    public float ShakeDuration { get { return shakeDuration; } set { shakeDuration = value; } }
    public float ShakeAmount { get { return shakeAmount; } set { shakeAmount = value; } }
    public float ВecreaseFactor { get { return decreaseFactor; } set { decreaseFactor = value; } }

	Vector3 originalPos;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}
}