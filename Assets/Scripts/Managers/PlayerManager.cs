using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	[SerializeField] GameObject player;
	public static GameObject Player { get; private set; }
	public static PlayerManager Manager { get; private set; }
	public HealthSystem PlayerHealth { get; private set; }
	public PlayerMovement PlayerMovement { get; private set; }

	//TODO: Weapon switch (maybe?)
	public IWeapon PlayerWeapon { get; private set; }
	// Start is called before the first frame update
	void Start()
	{
		Player = player;
		Manager = this;
		PlayerHealth = Player.GetComponent<HealthSystem>();
		PlayerMovement = Player.GetComponent<PlayerMovement>();
		PlayerWeapon = Player.GetComponentInChildren<IWeapon>();
	}

}
