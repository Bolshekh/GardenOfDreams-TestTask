using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(WeaponSway))]
public class ShootingWeapon : MonoBehaviour, IWeapon
{
	//stats
	[Header("Stats")]
	[SerializeField] protected float weaponDamage;
	public float Damage => weaponDamage;

	[SerializeField] protected float weaponKnockback;
	public float Knockback => weaponKnockback;

	//tip
	[Header("Targeting")]
	[SerializeField] GameObject gunPoint;
	[SerializeField] LayerMask targetLayer;
	WeaponSway weaponSway;

	//cooldown
	[Header("Cooldown")]
	[SerializeField] float cooldown = 0.1f;
	float shotTime;

	//recoil
	[Header("Recoil")]
	[SerializeField] float recoil;
	[SerializeField] float recoilDuration;
	[SerializeField] float recoilAngle; 

	//events
	public event EventHandler<WeaponHitEventArgs> WeaponHit;

	void Start()
	{
		weaponSway = GetComponent<WeaponSway>();

		WeaponHit += (s, e) =>
		{
			e.Hit.Hit(new HitInfo() {
				Damage = this.Damage,
				Hitter = gameObject,
				Knockback = Knockback * (e.Collision.transform.position - (Vector3)e.Collision.ClosestPoint((Vector2)transform.position)).normalized });
		};
	}
	public void Use()
	{
		if (Time.time < shotTime) return;
		shotTime = Time.time + cooldown;

		Debug.Log("shot");

		transform.DOLocalMove(recoil * (transform.localPosition.x >= 0 ? - transform.right : transform.right), recoilDuration);
		transform.DORotate(new Vector3(0, 0, transform.localPosition.x >= 0? recoilAngle : -recoilAngle), recoilDuration, RotateMode.LocalAxisAdd);

		var _hit = Physics2D.Raycast(gunPoint.transform.position, transform.up, Mathf.Infinity, targetLayer.value);
		
		var _h = _hit.collider.gameObject.GetComponent<IHitable>();
		if (_h != null)
		{
			WeaponHit?.Invoke(this, new WeaponHitEventArgs() { Hit = _h, Collision = _hit.collider });
		}
	}
}