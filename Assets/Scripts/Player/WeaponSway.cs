using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
	[SerializeField] PlayerMovement playerMovement;

	//smoothing
	[SerializeField] float smoothing = 20f;
	float posXVel;
	float posYVel;
	[SerializeField] float angleSmoothing = 5f;

	//gun positioning
	public GameObject Target { get; set; }

	[SerializeField] GameObject gunPoint;
	[SerializeField] LayerMask targetLayer;
	[SerializeField] float gunDistanceMultipleyer = 1f;

	LineRenderer lineRenderer;
	Vector2 targeting;

	//gun scale
	float initialScale;
	private void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		initialScale = transform.localScale.x;
	}
	void Update()
	{
		Targeting();
		RenderLine();
		PositionGun();
		RotateGun();
		MirrorGun();

		//Debug.DrawRay(transform.position, targeting, Color.red, 1f);
		//	Debug.DrawRay(playerMovement.transform.position, targeting, Color.yellow, 1f);
	}
	void Targeting()
	{
		if (Target == null)
			targeting = (gunDistanceMultipleyer * playerMovement.MoveDirection + (Vector2)playerMovement.transform.position);
		else
			targeting = gunDistanceMultipleyer * (Target.transform.position - playerMovement.transform.position).normalized 
				+ playerMovement.transform.position;

		if (Target.IsDestroyed())
			Target = null;
	}
	void PositionGun()
	{

		transform.position = new Vector2(
			Mathf.SmoothDamp(transform.position.x, targeting.x, ref posXVel, smoothing * Time.deltaTime),
			Mathf.SmoothDamp(transform.position.y, targeting.y, ref posYVel, smoothing * Time.deltaTime));
	}
	void RotateGun()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation,
			Quaternion.FromToRotation(Vector2.up, targeting - (Vector2)playerMovement.transform.position),
			angleSmoothing * Time.deltaTime);
	}
	void RenderLine()
	{
		var _hit = Physics2D.Raycast(gunPoint.transform.position, transform.up, Mathf.Infinity, targetLayer.value);
		lineRenderer.SetPositions(new Vector3[] { gunPoint.transform.position, _hit.point });
	}
	void MirrorGun()
	{//TODO: find better method on texture mirroring

		if (transform.localPosition.x >= 0)
			transform.localScale = new Vector3(initialScale, initialScale, initialScale);
		else
			transform.localScale = new Vector3(-initialScale, initialScale, initialScale);
	}
}
