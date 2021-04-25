using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	float moveTarget;
	float moveAxis;
	public float moveSpeed = 0.1f;
	float targetRotationY;
	float rotationY;
	Rigidbody2D rb;
	void Start()
	{
		moveAxis = 0f;
		rb = transform.parent.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		moveAxis = Input.GetAxis("Move");
		Vector3 diff = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
		moveTarget = Mathf.Atan2(diff.y, diff.x);
		if (Mathf.Abs(moveTarget) > Mathf.PI / 2)
		{
			targetRotationY = 180;
			rotationY = Mathf.Lerp(rotationY, targetRotationY, 0.1f);
			transform.eulerAngles = new Vector3(0, rotationY, 180 - moveTarget * Mathf.Rad2Deg);
		} else
		{
			targetRotationY = 0;
			rotationY = Mathf.Lerp(rotationY, targetRotationY, 0.1f);
			transform.eulerAngles = new Vector3(0, rotationY, moveTarget * Mathf.Rad2Deg);
		}
	}

	private void FixedUpdate()
	{
		if (moveAxis > 0)
		{
			rb.position = new Vector2(rb.position.x + Mathf.Cos(moveTarget) * moveAxis * moveSpeed, Mathf.Min(rb.position.y + Mathf.Sin(moveTarget) * moveAxis * moveSpeed, 78));
		}
	}
}
