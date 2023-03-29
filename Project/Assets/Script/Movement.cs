using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    /*[SerializeField] private float moveSpeed, runSpeed, speedMultiplier = 10f, playerHeight, groundDrag, iceDrag, jumpForce, airMultiplier, dashForce, poundDelay, poundForce, escalableDistance;
    [SerializeField] private Transform orientation, body;
    [SerializeField] private LayerMask groundMask, iceMask;


    public bool pounding { get; private set; }

    private float horizontalInput = 0, verticalInput = 0, rayFix = 0.5f, originalMoveSpeed;
    private Vector3 moveDir;
    private Rigidbody rb;
    private bool grounded, secondJump, canDash, iced;

    public UnityEvent Jumped, Interact;

    public float climbSpeed = 5f;
    public float climbHorizontalSpeed = 2.5f;
    public float climbVerticalSpeed = 2.5f;
    public float climbObjectHeight = 1.5f;

    private bool climbing;
    private Collider currentClimbable;
    private Vector3 climbDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        originalMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Revisar si el jugador est� en el suelo
        grounded = GroundCheck();

        iced = IceCheck();

        // Aplicar fricci�n en caso de estar en el suelo
        ApplyDrag();

        // Revisar que no sobrepase el l�mite de velocidad
        SpeedControl();

        if (grounded && pounding) pounding = false;

        if (climbing && !ClimbableCheck())
        {
            // El personaje ha llegado al final del objeto escalable
            climbing = false;
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
        }

        // Si el jugador est� escalando, moverlo hacia arriba
        if (climbing)
        {
            rb.velocity = climbDir * climbSpeed;
            rb.useGravity = false;

            // Mover el jugador hacia los lados si hay entrada de movimiento horizontal
            if (horizontalInput != 0)
            {
                Vector3 horizontalDir = orientation.right * horizontalInput;
                horizontalDir.y = 0;
                horizontalDir.Normalize();
                rb.MovePosition(rb.position + horizontalDir * climbHorizontalSpeed * Time.deltaTime);
            }

            // Mover el jugador hacia arriba o abajo si hay entrada de movimiento vertical
            if (verticalInput != 0)
            {
                Vector3 verticalDir = orientation.up * verticalInput;
                verticalDir.Normalize();
                rb.MovePosition(rb.position + verticalDir * climbVerticalSpeed * Time.deltaTime);
            }

            // Si el jugador ha llegado a la parte superior del objeto escalable, dejar de escalar
            if (transform.position.y >= currentClimbable.bounds.max.y - climbObjectHeight)
            {
                climbing = false;
                rb.useGravity = true;
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        //Agregar fuerza al jugador para moverlo
        Move();

        // Agregar fuerza al jugador para moverlo
        if (!climbing)
        {
            Move();
        }
    }

    #region Utility

    public void JumpDetection(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            Jumped.Invoke();
            if (grounded)
            {
                secondJump = true;
                canDash = true;
                Jump();
            }
            else if (secondJump && !grounded)
            {
                secondJump = false;
                Jump();
            }
        }
    }

    public void InputDetection(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;
        verticalInput = context.ReadValue<Vector2>().y;

        if (horizontalInput == 0 && verticalInput == 0) moveSpeed = originalMoveSpeed;
    }

    public void SprintDetection(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if (horizontalInput != 0 || verticalInput != 0 && grounded) moveSpeed = runSpeed;
            else if (!grounded && !pounding)
            {
                pounding = true;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                Invoke("Pound", poundDelay);
            }
        }
    }

    public void DashDetection(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if (canDash)
            {
                canDash = false;
                Dash();
            }
        }
    }

    bool GroundCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * rayFix, groundMask);
    }

    bool IceCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * rayFix, iceMask);

    }

    #endregion

    #region FlatMovement

    void Move()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded) rb.AddForce(moveDir * (moveSpeed * speedMultiplier), ForceMode.Force);
        else if (!grounded) rb.AddForce(moveDir * (moveSpeed * speedMultiplier * airMultiplier), ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void ApplyDrag()
    {
        if (iced) rb.drag = iceDrag;
        else if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;
    }

    #endregion

    #region AirRelated

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void Dash()
    {
        rb.AddForce(body.forward * dashForce, ForceMode.Impulse);
    }

    void Pound()
    {
        rb.useGravity = true;
        rb.AddForce(-body.up * poundForce, ForceMode.Impulse);
    }

    #endregion

    #region InteractionRelated

    public void InteractDetection(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton()) Interact.Invoke();
    }

    #endregion

    #region Escalar

    void Climb()
    {
        //Desactivar la gravedad del personaje
        rb.useGravity = false;

        //Obtener informaci�n del objeto escalable
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, escalableDistance, LayerMask.GetMask("Otrepable"));
        if (hitColliders.Length > 0)
        {
            Collider climbable = hitColliders[0];

            //Mover al personaje hacia arriba
            transform.Translate(Vector3.up * climbSpeed * Time.deltaTime, Space.World);

            //Apuntar al objeto escalable
            Vector3 lookDirection = climbable.transform.position - transform.position;
            lookDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(lookDirection);

            if (Physics.OverlapSphere(transform.position + Vector3.up * 0.1f, 0.5f, LayerMask.GetMask("climbable")).Length == 0)
            {

            }

        }

    }

    bool ClimbableCheck() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, escalableDistance, LayerMask.GetMask("Otrepable"));
        if (hitColliders.Length > 0)
        {
            Collider closestCollider = hitColliders[0];
            float closestDistance = Vector3.Distance(transform.position, closestCollider.bounds.center);
            for (int i = 1; i < hitColliders.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, hitColliders[i].bounds.center);
                if (distance < closestDistance)
                {
                    closestCollider = hitColliders[i];
                    closestDistance = distance;
                }
            }

            return true;

        }

        return false;

    }
    public void EscalateDetection(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && ClimbableCheck())
        {
            Climb();
        }
    }
    #endregion*/
}
