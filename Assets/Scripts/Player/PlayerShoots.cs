using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShoots : MonoBehaviour
{
	IWeapon playerWeapon;
	public event EventHandler OnPlayerShoot;
	// Start is called before the first frame update
	void Start()
	{
		playerWeapon = GetComponentInChildren<IWeapon>();

		if (playerWeapon == null) return;

		OnPlayerShoot += (s, e) =>
		{
			playerWeapon.Use();
		};
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButton("Fire1"))
			OnPlayerShoot?.Invoke(this, EventArgs.Empty);

	}
	
}
