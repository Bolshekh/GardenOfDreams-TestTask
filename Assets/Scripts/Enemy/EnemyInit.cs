using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(HealthSystem))]
public class EnemyInit : MonoBehaviour
{
	HealthSystem healthSystem;
	Rigidbody2D enemyRigidbody;
	[SerializeField] Slider healthSlider;
	[SerializeField] GameObject ItemDropPrefab;
	// Start is called before the first frame update
	void Start()
	{
		healthSystem = GetComponent<HealthSystem>();
		enemyRigidbody = GetComponent<Rigidbody2D>();
		var _ai = GetComponent<EnemyAi>();

		healthSlider.maxValue = healthSystem.MaxHealthPoints;
		healthSlider.value = healthSystem.CurrentHealthPoints;

		PlayerManager.Manager.PlayerHealth.EntityDied += (s, e) => { _ai.IsIgnoringPlayer = true; };

		healthSystem.EntityHit += (s, e) =>
		{
			if (e.HealthAfter < healthSystem.MaxHealthPoints)
				healthSlider.gameObject.SetActive(true);
			else
				healthSlider.gameObject.SetActive(false);

			healthSlider.value = e.HealthAfter;

			enemyRigidbody.AddForce(e.HitInfo.Knockback, ForceMode2D.Impulse);
		};

		healthSystem.EntityHealed += (s, e) =>
		{
			if (e.HealthAfter < healthSystem.MaxHealthPoints)
				healthSlider.gameObject.SetActive(true);
			else
				healthSlider.gameObject.SetActive(false);

			healthSlider.value = e.HealthAfter;
		};

		healthSystem.EntityDied += (s, e) =>
		{
			GetComponent<EnemyAi>().IsDying = true;

			Instantiate(ItemDropPrefab, transform.position, Quaternion.identity);

			Destroy(transform.gameObject, 1f);
		};
	}
}
