using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOne : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private PlayerInput playerInput;
    private Vector2 input;
    private bool alive = true;

    [Header("Movement")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpHeight;
    [Range(0, 0.3f)] public float smothing;
    [SerializeField] LayerMask groundMask;
    private Vector3 _velocidadZero = Vector3.zero;
    private bool canMove;
    public int direction = 1;

    [Header("Jump")]
    [SerializeField] private float timeJumpSaved;
    [SerializeField] private float coyoteTime;
    private bool falling;
    public bool saveJump;
    public bool inCoyoteTime;
    public bool canJump;

    public static PlayerOne Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // No destruir el LM durante el cambio de escenas
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        SetInitialValues();
    }

    void SetInitialValues()
    {
        alive = true;
        DisableMotion(true);
    }

    private void Update()
    {
        if (alive)
        {
            input = playerInput.actions["Move"].ReadValue<Vector2>();

            if (InGround())
            {
                canJump = true;
                inCoyoteTime = false;

                if (saveJump)
                {
                    saveJump = false;
                    Jump();
                }

                if (falling)
                {
                    falling = false;
                    //anim.SetTrigger("Arrive");
                }
            }
            else
            {
                if (!inCoyoteTime)
                {
                    Debug.Log("Dar coyote time");
                    inCoyoteTime = true;
                    StartCoroutine(CoyoteTime());
                }

                if (rb.velocity.y < 0 && falling == false)
                {
                    falling = true;
                    //anim.SetTrigger("Fall");
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
            Move();
    }

    // Detecta si el jugador est� tocando el suelo
    private bool InGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.x), 0f, Vector2.down, 0.2f, groundMask);
        return raycastHit.collider != null;
    }

    public void Move()
    {
        //rb.AddForce(new Vector2(input.x * runSpeed, 0));
        Vector3 velocidadFinal = new Vector2(input.x * runSpeed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, velocidadFinal, ref _velocidadZero, smothing);

        if (input.x < 0 & direction == 1)
            Turn(true);
        else if (input.x > 0 & direction == -1)
            Turn(false);

        //if (input.x != 0 && canJump)
        //    anim.SetBool("Walk", true);
        //else
        //    anim.SetBool("Walk", false);
    }

    public void Turn(bool lookAtRight)
    {
        Debug.Log("Girar");
        //spriteRenderer.flipX = lookAtRight;
        direction *= -1;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    public void DisableMotion(bool e)
    {
        canMove = e;
    }

    // Este salto se llama desde el Input System
    public void Jump(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Saltar 2");

        // Si est� en el suelo salta, si no, llama a guardar salto, lo cual har� que salte si toca el suelo en un corto tiempo
        if (callbackContext.performed && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0, jumpHeight));
            //anim.SetTrigger("Jump");
        }
        else if (callbackContext.performed && !canJump)
        {
            StartCoroutine(SaveJump());
        }
    }

    // Este salto se llama cuando tiene un salto guardado
    public void Jump()
    {
        if(canJump)
        {
            Debug.Log("Saltar 1");
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(new Vector2(0, jumpHeight));
            //anim.SetTrigger("Jump");
        }
    }

    //// Para comprobar si esta en una plataforma movible
    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("MovingPlatform"))
    //    {
    //        transform.parent = collision.transform;
    //    }
    //}

    //// Para comprobar si ya no esta en plataforma movible
    //public void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("MovingPlatform"))
    //    {
    //        transform.parent = null;
    //    }
    //}

    // Guarda el salto por si el jugador presiona saltar justo antes de tocar el suelo
    IEnumerator SaveJump()
    {
        saveJump = true;
        yield return new WaitForSeconds(timeJumpSaved);
        saveJump = false;
    }

    // Da un peque�o tiempo en el que el jugador puede saltar despues de dejar el suelo
    IEnumerator CoyoteTime()
    {
        yield return new WaitForSeconds(coyoteTime);
        Debug.Log("Hola " + coyoteTime);
        canJump = false;
    }
}
