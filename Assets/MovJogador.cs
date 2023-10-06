using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovJogador : MonoBehaviour
{
    // VARIÁVEIS
    [SerializeField] private float moveSpeed; // mover
    [SerializeField] private float walkSpeed; // andar
    //[SerializeField] private float jumpSpeed;
    [SerializeField] private float runSpeed; // correr

    [SerializeField] private bool isGrounded; // verifica se o personagem está no chão
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight; // altura do pulo

    // variável de 3 floats
    private Vector3 moveDirection; // direção para onde nos movimentamos
    private Vector3 velocity;

    // REFERÊNCIAS
    private CharacterController controller; // referência para o controlador do personagem
    private Animator anim;

    //private bool isJumping;

    private void Start()
    {
        // 
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        Move();
    }

    // função para movimento
    private void Move()
    {
        // acessamos o eixo z
        float moveZ = Input.GetAxis("Vertical");


        // VERIFICA SE O PERSONAGEM ESTÁ NO CHÃO
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        // transform.position: posição do jogador
        // groundCheckDistance: raio da esfera gerada no pé do jogador.
        // groundMask: camada que verificamos. O chão está em uma camada separada.

        // se estiver no chão,paramos de aplicar a gravidade
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // direção para onde queremos ir
        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        // somente se estiver no chão
        if (isGrounded)
        {
            // (0,0,0)
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                // andar
                Walk();
            }

            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                // correr
                Run();
            }

            else if (moveDirection == Vector3.zero)
            {
                // idle
                Idle();
            }
            moveDirection *= moveSpeed;

        }

        // Time.deltaTime: faz com que o movimento continue o mesmo
        // a velocidade de movimento vai ser igual à velocidade selecionada
        controller.Move(moveDirection * Time.deltaTime);


        // calculamos a gravidade do personagem
        velocity.y += gravity * Time.deltaTime;

        // aplicamos a gravidade ao personagem
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSecondsRealtime(2);
    }
}