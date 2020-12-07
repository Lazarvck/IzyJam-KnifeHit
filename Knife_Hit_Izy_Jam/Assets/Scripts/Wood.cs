using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public float speed;
    
    private WheelJoint2D whellJoint;
    private JointMotor2D motorJoint;

    private void Start() {
        whellJoint = GetComponent<WheelJoint2D>();
        motorJoint = new JointMotor2D();
        motorJoint.motorSpeed = speed;
        motorJoint.maxMotorTorque = 9999;
        whellJoint.motor = motorJoint;
    }
}