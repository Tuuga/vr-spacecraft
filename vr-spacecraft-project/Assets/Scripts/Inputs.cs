using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Inputs : MonoBehaviour {

	EVRButtonId trigger = EVRButtonId.k_EButton_SteamVR_Trigger;
	EVRButtonId menu = EVRButtonId.k_EButton_ApplicationMenu;
	EVRButtonId grip = EVRButtonId.k_EButton_Grip;
	EVRButtonId pad = EVRButtonId.k_EButton_SteamVR_Touchpad;

	public SteamVR_TrackedObject trackedRight, trackedLeft;

	public float deadZoneSize;

	public ControlPoint leftPoint, rightPoint;

	bool controllersFound;
	SteamVR_Controller.Device rightInput, leftInput;
	public Transform rightHand { get; private set; }
	public Transform leftHand { get; private set; }

	HeadReset headReset;
	ShipController shipController;

	void Start () {
		rightHand = trackedRight.transform;
		leftHand = trackedLeft.transform;
		StartCoroutine(GetControllers());

		headReset = FindObjectOfType<HeadReset>();
		shipController = FindObjectOfType<ShipController>();
	}

	IEnumerator GetControllers () {
		while ((int)trackedRight.index == -1 || (int)trackedLeft.index == -1) {
			yield return new WaitForEndOfFrame();
		}
		rightInput = SteamVR_Controller.Input((int)trackedRight.index);
		leftInput = SteamVR_Controller.Input((int)trackedLeft.index);

		controllersFound = true;
	}

	void Update () {
		if(!controllersFound) { return; }

		if (rightInput.GetPressDown(menu) || leftInput.GetPressDown(menu)) {
			headReset.ResetHead();
		}

		if (rightInput.GetPressDown(grip) || leftInput.GetPressDown(grip)) {
			shipController.ToggleGravityStabilizer();
		}

		if (leftPoint.controllerInside) {
			var leftJs = GetJoystickInput(leftHand);
			shipController.Thrust(leftJs);
			shipController.Hover(leftInput.GetAxis(pad).y);
		}

		if (rightPoint.controllerInside) {
			var rightJs = GetJoystickInput(rightHand);
			shipController.Torque(rightJs);
			shipController.Jaw(rightInput.GetAxis(pad).x);
		}
	}

	Vector2 GetPitchAndRoll (Transform hand) {
		var stickVec = hand.localRotation * Vector3.forward;
		stickVec = Quaternion.Euler(90, 0, 0) * stickVec;

		return new Vector2(stickVec.x, -stickVec.y);
	}

	Vector2 DeadZone (Vector2 input) {
		if (input.magnitude < deadZoneSize) {
			return Vector2.zero;
		}
		return input;
	}

	Vector2 GetJoystickInput (Transform hand) {
		var js = GetPitchAndRoll(hand);
		js = DeadZone(js);
		return js;
	}
}