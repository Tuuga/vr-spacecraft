using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour {

	public Collider controller;
	public bool controllerInside { get; private set; }

	void OnTriggerEnter (Collider c) {
		if (c == controller) {
			controllerInside = true;
		}
	}

	void OnTriggerExit (Collider c) {
		if (c == controller) {
			controllerInside = false;
		}
	}
}
