using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Cinemachine;
using UnityEngine.Serialization;

public enum PlayerState
{
    ThirdPerson, Isometric
}

[RequireComponent(typeof(Rigidbody))]
public class WheelController : MonoBehaviour
{
    [SerializeField] private PlayerState state;
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private CinemachineVirtualCamera isometric;
    [SerializeField] private WheelCollider FR, FL, BR, BL;
    [SerializeField] private Transform FRTrans, FLTrans, BRTrans, BLTrans, centerOfMass, steeringWheel;
    [SerializeField] private float acceleration = 500f, brakingForce = 300f, maxTurnAngle = 25f;

    private Rigidbody rb;
    private float currentAcceleration, currentBrakeforce, currentTurnAngle, horizontalInput, verticalInput;

    private bool movementDisabled;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        
        if(state == PlayerState.ThirdPerson) isometric.gameObject.SetActive(false);
        if(state == PlayerState.Isometric) freeLook.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.centerOfMass = centerOfMass.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        InputDetection();
        
        Braking();
        
        CameraRecentering();
        
        MoveWheelMesh(FR, FRTrans);
        MoveWheelMesh(FL, FLTrans);
        MoveWheelMesh(BR, BRTrans);
        MoveWheelMesh(BL, BLTrans);
    }

    private void FixedUpdate()
    {
        Turn();
        Move();
    }

    void InputDetection()
    {
        if (!movementDisabled)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            horizontalInput = 0;
            verticalInput = 0;
        }
    }
    
    void Move()
    {
        currentAcceleration = acceleration * verticalInput;
        
        FR.motorTorque = currentAcceleration/2;
        FL.motorTorque = currentAcceleration/2;

        BR.motorTorque = currentAcceleration;
        BL.motorTorque = currentAcceleration;

        FR.brakeTorque = currentBrakeforce;
        FL.brakeTorque = currentBrakeforce;
        BR.brakeTorque = currentBrakeforce;
        BL.brakeTorque = currentBrakeforce;
    }

    void Turn()
    {
        currentTurnAngle = maxTurnAngle * horizontalInput;
        
        steeringWheel.eulerAngles = new Vector3(steeringWheel.eulerAngles.x,steeringWheel.eulerAngles.y, -currentTurnAngle);
        
        FR.steerAngle = currentTurnAngle;
        FL.steerAngle = currentTurnAngle;
    }

    void Braking()
    {
        if (verticalInput == 0)
        {
            currentBrakeforce = brakingForce;
        }
        else
        {
            currentBrakeforce = 0;
        }
    }

    void CameraRecentering()
    {
        if (freeLook.gameObject.activeSelf)
        {
            if (verticalInput != 0)
            {
                if (verticalInput == 1) freeLook.m_Heading.m_Bias = 0;
                else freeLook.m_Heading.m_Bias = 180;
                freeLook.m_YAxisRecentering.m_enabled = true;
                freeLook.m_RecenterToTargetHeading.m_enabled = true;
            }
            else
            {
                freeLook.m_Heading.m_Bias = 0;
                freeLook.m_YAxisRecentering.m_enabled = false;
                freeLook.m_RecenterToTargetHeading.m_enabled = false;
            }
        }
    }
    
    void MoveWheelMesh(WheelCollider col, Transform mesh)
    {
        Vector3 position;
        Quaternion rotation;
        
        col.GetWorldPose(out position, out rotation);
        
        mesh.rotation = rotation;
    }

    public void DisableMovement()
    {
        movementDisabled = true;
    }
    
    public void EnableMovement()
    {
        movementDisabled = false;
    }
}
