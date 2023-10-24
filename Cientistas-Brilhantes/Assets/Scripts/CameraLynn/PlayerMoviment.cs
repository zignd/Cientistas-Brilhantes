using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public PuzzleManager PuzzleManager;
    public JoyCOMBridge joycomsocket;
    public float ControllerRotationSensitivity = 100f;

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform Orientation;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    private bool uiAtivada = false;
    //private CharacterController playerController;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        // Obtém a referência ao CharacterController do jogador
       // playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (!uiAtivada)
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            if (PuzzleManager.Mode == Mode.MouseAndKeyboard)
            {
                horizontalInput = Input.GetAxisRaw("Horizontal");
                verticalInput = Input.GetAxisRaw("Vertical");
            }
            else
            {
                Orientation.Rotate(Vector3.up * joycomsocket.ReceivedPayload.Joystick.X * Time.deltaTime * ControllerRotationSensitivity);
                verticalInput = joycomsocket.ReceivedPayload.Joystick.Y;
            }

            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }

            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;

            // Ativar ou desativar o controle do jogador com base na UI ativada
            // playerController.enabled = true;
        }
        else
        {
            // Quando a UI estiver ativada, bloqueia o movimento do jogador
            horizontalInput = 0;
            verticalInput = 0;

            // Ativar ou desativar o controle do jogador com base na UI ativada
           // playerController.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    public void AtivarUI()
    {
        uiAtivada = true;
    }

    public void DesativarUI()
    {
        uiAtivada = false;
    }
}
