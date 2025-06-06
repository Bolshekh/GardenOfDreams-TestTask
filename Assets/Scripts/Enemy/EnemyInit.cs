using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(HealthSystem))]
public class EnemyInit : MonoBehaviour
{
	HealthSystem healthSystem;
	[SerializeField] Slider healthSlider;
	Rigidbody2D enemyRigidbody;
	// Start is called before the first frame update
	void Start()
	{
		healthSystem = GetComponent<HealthSystem>();
		enemyRigidbody = GetComponent<Rigidbody2D>();

		healthSlider.maxValue = healthSystem.MaxHealthPoints;
		healthSlider.value = healthSystem.CurrentHealthPoints;

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
			Destroy(transform.gameObject, 1f);
		};
	}
}
