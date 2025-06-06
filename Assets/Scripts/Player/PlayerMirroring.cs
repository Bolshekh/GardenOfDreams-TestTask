using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMirroring : MonoBehaviour
{
	PlayerMovement playerMovement;
	// Start is called before the first frame update
	void Start()
	{
		playerMovement = PlayerManager.Manager.PlayerMovement;
	}

	// Update is called once per frame
	void Update()
	{
		if (playerMovement.HorizontalAxisInput >= 0.1f) ChangePlayerYRotation(0);
		else if (playerMovement.HorizontalAxisInput <= -0.1f) ChangePlayerYRotation(180);
	}

	void ChangePlayerYRotation(float Angle)
	{
		transform.rotation = Quaternion.Euler(0, Angle, 0);
	}
}
