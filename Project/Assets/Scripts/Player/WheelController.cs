using UnityEngine;
using Cinemachine;
using Utility.GameFlow;
using Unity.VisualScripting.Antlr3.Runtime;

/// <summary>
/// Player cam state: ThirdPerson or Isometric
/// </summary>
public enum PlayerState
{
    ThirdPerson, Isometric
}

/// <summary>
/// This class manages car movement and it's camera config.
/// It requires a rigidbody, and certain references defined on "Assignable parameters".
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class WheelController : MonoBehaviour
{
    //Assignable parameters
    [Header("Parameters")]
    [SerializeField] private PlayerState state;
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private CinemachineVirtualCamera isometric;
    [SerializeField] private WheelCollider FR, FL, BR, BL;
    [SerializeField] private Transform FRTrans, FLTrans, BRTrans, BLTrans, centerOfMass, steeringWheel;
    [SerializeField] private float acceleration = 500f, brakingForce = 300f, maxTurnAngle = 25f;

    [Header("Audio")] 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip brake;
    [SerializeField][Range(0f, 10f)] private float velocityForBreakSound = 5f;

    
    //Utility parameters
    private Rigidbody rb;
    private float currentAcceleration, currentBrakeforce, currentTurnAngle, horizontalInput, verticalInput;
    private bool movementDisabled, canBrakeSound, breakAlreadyCalled;

    private void Awake()
    {
        //Rigidbody reference (before class context the Rigidbody component was required)
        rb = GetComponent<Rigidbody>();
        
        //Setting up cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Determine which cam to use depending on state variable
        if(state == PlayerState.ThirdPerson) isometric.gameObject.SetActive(false);
        if(state == PlayerState.Isometric) freeLook.gameObject.SetActive(false);

        GameManager.Pause += GamePuseControl;
    }

    void Start()
    {
        //Setting up center of mass to desired point
        rb.centerOfMass = centerOfMass.localPosition;
    }
    
    void Update()
    {
        //Player input detection
        InputDetection();
        
        //Apply brake force
        Braking();
        
        //Recenter cam 
        CameraRecentering();
        
        //Sync wheels meshes with wheels colliders
        MoveWheelMesh(FR, FRTrans);
        MoveWheelMesh(FL, FLTrans);
        MoveWheelMesh(BR, BRTrans);
        MoveWheelMesh(BL, BLTrans);
    }

    private void FixedUpdate()
    {
        //Turns wheels colliders and meshes
        Turn();
        
        //Apply force to player 
        Move();
    }

    private void OnDestroy()
    {
        GameManager.Pause -= GamePuseControl;
    }

    #region MovementRelated

    /// <summary>
    /// Determined by a bool type variable, it checks for player inputs.
    /// Assign axis raw values to local variables ("horizontalInput" and "verticalInput")
    /// if bool "movementDisabled" is set to false or set them to zero if it is set to true.
    /// </summary>
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
    
    /// <summary>
    /// Calculates currentAcceleration with the assignable variable "acceleration" multiplied
    /// by "verticalInput" (variable determined by "InputDetection" method).
    /// Using currentAcceleration result it applies motorTorque to the four wheels colliders.
    /// NOTE: currentAcceleration applied to front wheels is divided by 2.
    /// Finally, it applies currentBrakeForce (variable determined by "Braking" method) to four wheels. 
    /// </summary>
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
    
    /// <summary>
    /// Determines currentTurnAngle with maxTurnAngle (variable assigned by: user, defined at: "Assingnable Parameters" section)
    /// multiplied by horizontalInput (variable determined by "InputDetection" method).
    /// Then applies currentTurnAngle to steeringWheel (reference needed! defined at: "Assingnable Parameters" section)
    /// and front wheels colliders.
    /// </summary>
    void Turn()
    {
        currentTurnAngle = maxTurnAngle * horizontalInput;
        
        steeringWheel.eulerAngles = new Vector3(steeringWheel.eulerAngles.x,steeringWheel.eulerAngles.y, -currentTurnAngle);
        
        FR.steerAngle = currentTurnAngle;
        FL.steerAngle = currentTurnAngle;
    }

    /// <summary>
    /// Determines currentBrakeForce depending on verticalInput's value (variable determined by "InputDetection" method).
    /// If verticalInput is == 0 currentBrakeForce will be brakingForce (variable assigned by: user, defined at: "Assingnable Parameters" section).
    /// If VerticalInput is != 0 currentBrakeForce will be 0.
    /// </summary>
    void Braking()
    {
        if (verticalInput == 0)
        {
            currentBrakeforce = brakingForce;

            if ((rb.linearVelocity.x > velocityForBreakSound || rb.linearVelocity.x < -velocityForBreakSound) && !breakAlreadyCalled)
            {
                breakAlreadyCalled = true;
                canBrakeSound = true;
            }

            if (canBrakeSound)
            {
                canBrakeSound = false;
                _audioSource.PlayOneShot(brake);
            }
        }
        else
        {
            breakAlreadyCalled = false;
            canBrakeSound = false;
            currentBrakeforce = 0;
        }
    }

    #endregion

    #region CameraRelated
    
    /// <summary>
    /// Controls CineMachineFreeLook recentering behavior depending on freeLook GameObject activeSelf (true)
    /// to execute and verticalInput value to determine whether if it is != 0 or 0.
    /// Also, internally it checks if verticalInput's value is == 1 (forward) or == -1 (backward) to determine Heading Bias value.
    /// </summary>
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

    #endregion

    #region Utility

    /// <summary>
    /// Applies WheelCollider's rotation to a Transform.
    /// </summary>
    /// <param name="col"> Defined as the WheelCollider used as the reference rotation. </param>
    /// <param name="mesh"> Defined as the Transform that will receive a new rotation </param>
    void MoveWheelMesh(WheelCollider col, Transform mesh)
    {
        Vector3 position;
        Quaternion rotation;
        
        col.GetWorldPose(out position, out rotation);
        
        mesh.rotation = rotation;
    }

    /// <summary>
    /// Sets moventDisabled variable to true.
    /// </summary>
    public void DisableMovement()
    {
        rb.isKinematic = true;
        movementDisabled = true;
    }
    
    /// <summary>
    /// Sets moventDisabled variable to false.
    /// </summary>
    public void EnableMovement()
    {
        if (Cursor.visible) return;

        rb.isKinematic = false;
        movementDisabled = false;
    }

    private void GamePuseControl(bool paused)
    {
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;

        if (paused)
        {
            if (freeLook.gameObject.activeSelf && state == PlayerState.ThirdPerson) freeLook.enabled = false;

            DisableMovement();
        }
        else
        {
            if (freeLook.gameObject.activeSelf && state == PlayerState.ThirdPerson) freeLook.enabled = true;

            EnableMovement();
        }
    }

    #endregion
}
