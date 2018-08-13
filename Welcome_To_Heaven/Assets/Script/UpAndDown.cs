using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour {

	[SerializeField]
	float moveSpeed = 5f;

	[SerializeField]
	float frequency = 20f;

	[SerializeField]
	float magnitude = .5f;

	bool facingRight = true;

	Vector3 pos, localScale;

	// Use this for initialization
	void Start () {
		pos = transform.position;
		localScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
		CheckWhereToFace ();

		if (facingRight)
			MoveRight ();
		else
			MoveLeft ();
	}

	public void CheckWhereToFace()
	{
		if (pos.x < -7f)
		facingRight = true;
		else if (pos.x > 7f)
		facingRight = false;
		if ((facingRight && localScale.x <0) || (!facingRight && localScale.x > 0))
		localScale.x *= -1;

		transform.localScale = localScale;
	}

	public void MoveRight()
	{
		pos += transform.right * Time.deltaTime * moveSpeed;
		transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
	}

	public void MoveLeft()
	{
		pos -= transform.right * Time.deltaTime * moveSpeed;
		transform.position = pos + transform.up * Mathf.Sin (Time.time * frequency) * magnitude;
	}
}