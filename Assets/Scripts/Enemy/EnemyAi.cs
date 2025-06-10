using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
	//detection
	[Header("Detection")]
	[SerializeField] float playerDetectRange;
	public bool IsPlayerDetected { get; set; } = false;
	public bool IsDying { get; set; } = false;
	public bool IsIgnoringPlayer { get; set; } = false;

	//movement
	[Header("Movement")]
	Rigidbody2D enemyRigidbody;
	[SerializeField] float movementSpeed;

	//attacks
	[Header("Attacks")]
	[SerializeField] float attackRange = 1.5f;
	[SerializeField] float attackForce = 20f;
	[SerializeField] int attackDamage = 1;
	[SerializeField] float knockbackForce = 10f;

	[Header("AttackCooldown")]
	[SerializeField] float cooldown = 1f;
	float shotTime;
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
		AttackPlayer();
	}
	void DetectPlayer()
	{
		if (!IsPlayerDetected && Vector2.Distance(transform.position, PlayerManager.Player.transform.position) <= playerDetectRange)
		{
			IsPlayerDetected = true;
		}
	}
	void AttackPlayer()
	{
		if (IsIgnoringPlayer || IsDying || !IsPlayerDetected) return;

		if (Time.time < shotTime) return;

		shotTime = Time.time + cooldown;

		if (Vector2.Distance(transform.position, PlayerManager.Player.transform.position) <= attackRange)
		{
			enemyRigidbody.AddForce(attackForce * (PlayerManager.Player.transform.position - transform.position).normalized, ForceMode2D.Impulse);
		}
	}
	void MoveToPlayer()
	{
		if (IsPlayerDetected && !IsDying && !IsIgnoringPlayer)
		{
			enemyRigidbody.AddForce(movementSpeed * Time.deltaTime * (PlayerManager.Player.transform.position - transform.position).normalized, ForceMode2D.Force);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<HealthSystem>().Hit(new HitInfo()
			{
				Damage = attackDamage,
				Knockback = knockbackForce * (collision.transform.position - transform.position).normalized,
				Hitter = gameObject
			});
		}
	}
}
