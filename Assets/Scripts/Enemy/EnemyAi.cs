using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
	//detection
	[SerializeField] float playerDetectRange;
	public bool IsPlayerDetected { get; set; } = false;

	//movement
	Rigidbody2D enemyRigidbody;
	[SerializeField] float movementSpeed;

	// Start is called before the first frame update
	void Start()
	{
		enemyRigidbody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		DetectPlayer();
		MoveToPlayer();
	}
	void DetectPlayer()
	{

		if (Vector2.Distance(transform.position, PlayerManager.Player.transform.position) <= playerDetectRange)
		{
			IsPlayerDetected = true;
		}
	}
	void MoveToPlayer()
	{
		if (IsPlayerDetected)
		{
			enemyRigidbody.AddForce(movementSpeed * Time.deltaTime * (PlayerManager.Player.transform.position - transform.position).normalized, ForceMode2D.Force);
		}
	}
}
