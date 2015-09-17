using UnityEngine;
using System.Collections;

public class PendulumBehavior : MonoBehaviour {

    public HingeJoint2D hinge;
    JointMotor2D motor;
    bool swingingLeft;
	// Use this for initialization
	void Start () {
        hinge = gameObject.GetComponent<HingeJoint2D>();
        motor = hinge.motor;
        motor.maxMotorTorque = 100000;
        motor.motorSpeed = 100;
        swingingLeft = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (hinge.jointAngle >= 179.0f && swingingLeft)
        {
            swingingLeft = false;
            motor.motorSpeed = -motor.motorSpeed;
            hinge.motor = motor;
        }

        if (hinge.jointAngle <= 1.0f && !swingingLeft)
        {
            swingingLeft = true;
            motor.motorSpeed = -motor.motorSpeed;
            hinge.motor = motor;
        }
	}
}
