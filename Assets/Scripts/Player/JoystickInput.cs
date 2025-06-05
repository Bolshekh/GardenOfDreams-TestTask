using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMovement))]
public class JoystickInput : MonoBehaviour
{
	[SerializeField] Joystick joystick;
	PlayerMovement playerMovement;
	PlayerShoots playerShoots;
	// Start is called before the first frame update
	void Start()
	{
		playerMovement = GetComponent<PlayerMovement>();
		playerShoots = GetComponent<PlayerShoots>();
	}

	// Update is called once per frame
	void Update()
	{
		playerMovement.VerticalAxisInput = joystick.Vertical;
		playerMovement.HorizontalAxisInput = joystick.Horizontal;
	}
	public void ClickFireButton()
	{
		playerShoots.FireInput = true;
	}
}
