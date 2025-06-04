using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//input
	public float HorizontalAxisInput { get; set; }
	public float VerticalAxisInput { get; set; }
	public Vector3 MousePositionInput { get; set; }
	public bool CancelButtonInput { get; set; }

	//link variables
	Rigidbody2D playerRB;
	Animator animator;

	//gizmos
	Vector2 moveDirection;
	Vector2 lookDirection;

	//basic movement
	[SerializeField] float moveSpeed;
	[SerializeField] float speedMultiplier;
	[SerializeField] AnimationCurve forceMultiplier;
	float GetForceMultiplierValue(float value) => forceMultiplier.Evaluate(value);
	void Start()
	{
		playerRB = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		CheckMovement();
	}
	void FixedUpdate()
	{
		ApplyMovement();
		//ApplyRotation();
	}

	protected virtual void CheckMovement()
	{
		moveDirection = new Vector2(HorizontalAxisInput, VerticalAxisInput).normalized;

		var _mouse = (Vector2)Camera.main.ScreenToWorldPoint(MousePositionInput);

		lookDirection = _mouse - (Vector2)transform.position;

		if (CancelButtonInput)
			MenuManager.Manager.SwitchPause();
	}

	protected virtual void ApplyMovement()
	{
		if (moveDirection.magnitude != 0)
		{
			playerRB.AddForce(moveSpeed * speedMultiplier * GetForceMultiplierValue(playerRB.velocity.magnitude) * moveDirection, ForceMode2D.Force);
		}
	}
	protected virtual void ApplyRotation()
	{
		transform.rotation = Quaternion.FromToRotation(Vector2.up, lookDirection);
	}
	public void UpgradeSpeed(float Speed)
	{
		speedMultiplier += Speed;
	}
}
