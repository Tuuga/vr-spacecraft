using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {

	public LayerMask mask;
	public Transform head, weapon, lazer;
	public GameObject missilePrefab, bulletPrefab;
	public float missileSpeed, bulletSpeed;
	public float missileRPM, bulletRPM;
	float lastMissileShot, lastBulletShot;

	Vector3 aimPoint;

	void Update () {
		var dir = head.forward;
		RaycastHit hit;
		if (Physics.Raycast(head.position, dir, out hit, Mathf.Infinity, mask)) {
			aimPoint = hit.point;
			weapon.LookAt(aimPoint);

			var startToEnd = aimPoint - weapon.position;
			var lenght = startToEnd.magnitude;
			lazer.position = weapon.position + (startToEnd / 2);
			lazer.localScale = new Vector3(lazer.localScale.x, lazer.localScale.y, lenght);
			lazer.LookAt(aimPoint);
		}
	}

	public void FireMissile () {
		if (lastMissileShot < Time.time - (60f / missileRPM)) {
			GameObject missile = (GameObject)Instantiate(missilePrefab, weapon.position + weapon.forward, Quaternion.identity);
			missile.transform.LookAt(aimPoint);
			var rb = missile.GetComponent<Rigidbody>();
			var dir = (aimPoint - weapon.position).normalized;
			rb.AddForce(dir * missileSpeed, ForceMode.Impulse);
			lastMissileShot = Time.time;
		}		
	}

	public void FireMachinegun () {
		if (lastBulletShot < Time.time - (60f / bulletRPM)) {
			GameObject bullet = (GameObject)Instantiate(bulletPrefab, weapon.position + weapon.forward, Quaternion.identity);
			bullet.transform.LookAt(aimPoint);
			var rb = bullet.GetComponent<Rigidbody>();
			var dir = (aimPoint - weapon.position).normalized;
			rb.AddForce(dir * bulletSpeed, ForceMode.Impulse);
			lastBulletShot = Time.time;
		}		
	}
}
