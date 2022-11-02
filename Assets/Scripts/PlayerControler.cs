using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
	[Header("General SetUp Settings")]
	[Tooltip("How fast Ship moves up & down player input")] [SerializeField] float controlSpeed = 10f;
	[Tooltip("How far player moves horizontally")] [SerializeField] float xRange = 10f;
	[Tooltip("How far player moves vertically")] [SerializeField] float yRange = 10f;

	[Header("Laser Gun Array")]
	[Tooltip("Add all player lasers here")]
	[SerializeField] GameObject[] lazers;

	[Header("Screen position based tuning")]
	[SerializeField] float positionPitchFactor = -2f;
	[SerializeField] float positionYawFactor = 2;

	[Header("Player input based tuning")]
	[SerializeField] float controlPitchFactor = -10f;
	[SerializeField] float controlRollFactor = -20f;
	float xThrow;
	float yThrow;

	// Update is called once per frame
	void Update()
	{
		ProcessTranslation();
		ProcessRotation();
		ProcessFiring();
	}

	void ProcessRotation()
	{
		float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
		float pitchDueToControl = yThrow * controlPitchFactor;

		float yawDueToPosition = transform.localPosition.x * positionYawFactor;

		float rollDueToControl = xThrow * controlRollFactor;

		float pitch = pitchDueToPosition + pitchDueToControl;
		float yaw = yawDueToPosition;
		float roll = rollDueToControl;
		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}

	private void ProcessTranslation()
	{
		xThrow = Input.GetAxis("Horizontal");
		yThrow = Input.GetAxis("Vertical");

		float xOffset = xThrow * Time.deltaTime * controlSpeed;
		float yOffset = yThrow * Time.deltaTime * controlSpeed;

		float newXPos = transform.localPosition.x + xOffset;
		float newYPos = transform.localPosition.y + yOffset;

		float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);
		float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

		transform.localPosition = new Vector3
			(clampedXPos, clampedYPos, transform.localPosition.z);
	}

	void ProcessFiring()
	{
		if (Input.GetButton("Fire1"))
		{
			SetLazersActive(true);
		}
		else
		{
			SetLazersActive(false);
		}
	}

	void SetLazersActive(bool isActive)
	{
		foreach (GameObject laser in lazers)
		{
			var emissionModule = laser.GetComponent<ParticleSystem>().emission;
			emissionModule.enabled = isActive;

		}
	}

}
