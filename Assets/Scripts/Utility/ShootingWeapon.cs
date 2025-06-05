using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(WeaponSway))]
public class ShootingWeapon : MonoBehaviour, IWeapon
{
	//stats
	[SerializeField] protected float weaponDamage;
	public float Damage => weaponDamage;

	[SerializeField] protected float weaponKnockback;
	public float Knockback => weaponKnockback;

	//tip
	[SerializeField] GameObject gunPoint;
	[SerializeField] LayerMask targetLayer;
	WeaponSway weaponSway;

	//cooldown
	[SerializeField] float cooldown = 0.1f;
	float shotTime;

	//events
	public event EventHandler<WeaponHitEventArgs> WeaponHit;

	void Start()
	{
		weaponSway = GetComponent<WeaponSway>();

		WeaponHit += (s, e) =>
		{
			e.Hit.Hit(new HitInfo() { Damage = this.Damage, Hitter = gameObject });
		};
	}
	public void Use()
	{
		if (Time.time < shotTime) return;
		shotTime = Time.time + cooldown;

		Debug.Log("shot");

		var _hit = Physics2D.Raycast(gunPoint.transform.position, transform.up, Mathf.Infinity, targetLayer.value);

		var _h = _hit.collider.gameObject.GetComponent<IHitable>();
		if (_h != null)
		{
			WeaponHit?.Invoke(this, new WeaponHitEventArgs() { Hit = _h, Collision = _hit.collider });
		}
	}
}