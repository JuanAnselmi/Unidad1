using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_veticalInput;
    private float m_steeringAngle;

    public WheelCollider frontDriverW, frontPassangerW;
    public WheelCollider rearDriverW, rearPassangerW;
    public Transform frontDriverT, frontPassangerT;
    public Transform rearDriverT, rearPassangerT;
    public float maxSteerAngle = 30;
    public float motorForce = 50;

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_veticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassangerW.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = m_veticalInput * motorForce;
        frontPassangerW.motorTorque = m_veticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassangerW, frontPassangerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassangerW, rearPassangerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
}
