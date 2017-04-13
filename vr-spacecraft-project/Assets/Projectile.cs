using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	ParticleSystem ps;

	void Start () {
		if (name.Contains("Missile")) {
			ps = GameObject.Find("Missile Hit").GetComponent<ParticleSystem>();
		} else if (name.Contains("Bullet")) {
			ps = GameObject.Find("Bullet Hit").GetComponent<ParticleSystem>();
		}
	}

	void OnTriggerEnter (Collider c) {
		ps.transform.position = transform.position;
		ps.Play();
	}
}
