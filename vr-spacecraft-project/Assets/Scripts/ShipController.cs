using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	public float forwardSpeed, strafeSpeed, hoverSpeed;
	public float pitchSpeed, rollSpeed, jawSpeed;

	bool gravityStabilizer;

	Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	public void Thrust (Vector2 input) {
		var fwd = transform.forward * input.y;
		var right = transform.right * input.x;

		rb.AddForce((fwd * forwardSpeed + right * strafeSpeed) * Time.deltaTime);
	}

	public void Torque (Vector2 input) {
		transform.rotation *= Quaternion.Euler(input.y * pitchSpeed, 0, -input.x * rollSpeed);
	}

	public void Jaw (float input) {
		transform.rotation *= Quaternion.Euler(0, input * jawSpeed, 0);
	}

	public void Hover (float input) {
		rb.AddForce(transform.up * input * hoverSpeed * Time.deltaTime);
	}

	public void ToggleGravityStabilizer () {
		gravityStabilizer = !gravityStabilizer;
	}

	void FixedUpdate () {
		if (gravityStabilizer) {
			rb.AddForce(-Physics.gravity);
		}
	}
}
