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
	public Vector2 MoveDirection { get; private set; }
	public Vector2 LookDirection { get; private set; }

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
		MoveDirection = new Vector2(HorizontalAxisInput, VerticalAxisInput).normalized;

		var _mouse = (Vector2)Camera.main.ScreenToWorldPoint(MousePositionInput);

		LookDirection = _mouse - (Vector2)transform.position;

		if (CancelButtonInput)
			MenuManager.Manager.SwitchPause();
	}

	protected virtual void ApplyMovement()
	{
		if (MoveDirection.magnitude != 0)
		{
			playerRB.AddForce(moveSpeed * speedMultiplier * GetForceMultiplierValue(playerRB.velocity.magnitude) * MoveDirection, ForceMode2D.Force);
		}
	}
	protected virtual void ApplyRotation()
	{
		transform.rotation = Quaternion.FromToRotation(Vector2.up, LookDirection);
	}
	public void UpgradeSpeed(float Speed)
	{
		speedMultiplier += Speed;
	}
}
