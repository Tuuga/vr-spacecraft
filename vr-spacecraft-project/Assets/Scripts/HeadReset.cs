using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadReset : MonoBehaviour {

	public Transform headResetPoint, head, cameraRig;

	void ResetHeadPosition () {
		cameraRig.position = headResetPoint.position + (cameraRig.position - head.position);
	}

	void ResetHeadRotation () {
		var headY = head.localEulerAngles.y;
		var resetY = headResetPoint.localEulerAngles.y;
		var diff = headY - resetY;
		cameraRig.rotation = Quaternion.Euler(0, -diff + transform.root.localEulerAngles.y, 0);
	}

	public void ResetHead () {
		ResetHeadRotation();
		ResetHeadPosition();
	}
}
