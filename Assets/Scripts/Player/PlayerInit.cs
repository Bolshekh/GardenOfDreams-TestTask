using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInit : MonoBehaviour
{
	[SerializeField] Slider healthSlider;
	[SerializeField] float slowMotionOnHit;
	float beatTarget;
	float velocity;
	[SerializeField] float smoothing = 20f;
	[SerializeField] Animator sliderAnimator;
	float healthVel;
	float health;
	[SerializeField] GameObject deathScreen;
	// Start is called before the first frame update
	void Start()
	{
		var _health = GetComponent<HealthSystem>();
		var _rigid = GetComponent<Rigidbody2D>();
		healthSlider.maxValue = _health.MaxHealthPoints;
		healthSlider.value = _health.MaxHealthPoints;
		health = _health.MaxHealthPoints;
		//_health.BeforeEntityHit += (s, e) =>
		//{
		//	if (e.HitInfo.Hitter.CompareTag("PlayerBullet"))
		//	{
		//		e.IsCancelled = true;
		//		e.OverrideResponse = true;
		//		e.OverridenResponse = HitResponse.Ignore | HitResponse.PassThrough;
		//	}
		//};
		_health.EntityHit += (s, e) =>
		{
			_rigid.AddForce(e.HitInfo.Knockback, ForceMode2D.Impulse);

			healthSlider.value = e.HealthAfter;
			health = e.HealthAfter;

			if (e.HealthAfter < _health.MaxHealthPoints)
				healthSlider.gameObject.SetActive(true);
			else
				healthSlider.gameObject.SetActive(false);

		};
		_health.EntityHealed += (s, e) =>
		{
			health = e.HealthAfter;
			healthSlider.maxValue = _health.MaxHealthPoints;

			if (e.HealthAfter < _health.MaxHealthPoints)
				healthSlider.gameObject.SetActive(true);
			else
				healthSlider.gameObject.SetActive(false);
		};
		_health.EntityDied += (s, e) =>
		{
			deathScreen.SetActive(true);
		};
	}
	private void Update()
	{
		if (healthSlider.value != health)
			healthSlider.value = Mathf.SmoothDamp(healthSlider.value, health, ref healthVel, smoothing * Time.deltaTime);
	}
}
