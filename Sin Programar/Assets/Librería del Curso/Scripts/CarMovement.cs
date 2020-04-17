using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    float m_horizontalInput;
    float m_veticalInput;
    float m_steeringAngle;
    [SerializeField] float maxSteerAngle = 30;
    [SerializeField] float motorForce = 50;

    [SerializeField] WheelCollider frontDriverW, frontPassangerW;
    [SerializeField] WheelCollider rearDriverW, rearPassangerW;
    [SerializeField] Transform frontDriverT, frontPassangerT;
    [SerializeField] Transform rearDriverT, rearPassangerT;

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
        //rearDriverW.motorTorque = m_veticalInput * motorForce;
        //rearPassangerW.motorTorque = m_veticalInput * motorForce;
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
