using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(WeaponSway))]
public class WeaponTargeting : MonoBehaviour
{
	[SerializeField] float targetRadius;
	[SerializeField] string targetTag;
	WeaponSway weaponSway;
	[SerializeField] LayerMask targetLayer;
	[SerializeField] GameObject castPoint;
	// Start is called before the first frame update
	void Start()
	{
		weaponSway = GetComponent<WeaponSway>();
	}
	private void Update()
	{
		CastAndRetarget();
	}
	void CastAndRetarget()
	{
		var _hits = Physics2D.CircleCastAll(castPoint.transform.position, targetRadius, Vector2.zero, 0, targetLayer.value);

		if (_hits.Count() == 0)
		{
			weaponSway.Target = null;
			return;
		}

		float _minDist = 999999f;
		RaycastHit2D _hit = new RaycastHit2D();
		foreach (RaycastHit2D hit in _hits)
		{
			if (_minDist > hit.distance)
			{
				_minDist = hit.distance;
				_hit = hit;
			}
		}
		weaponSway.Target = _hit.collider.gameObject;
	}
}
