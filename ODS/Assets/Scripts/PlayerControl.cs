using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Control control;
    [SerializeField]private Vector2 direction, aim;

    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float speed, jumpForce;

    [SerializeField] private LayerMask jumpGround;
    private Collider2D col;
    private bool isGrounded, jumpBuffer, isJump, isDead;

    [SerializeField] private Transform aimRotate;
    private void Start()
    {
        control = InputManager.inputManager.control;
        control.Game.Enable();
        control.Game.Move.performed += move => direction = move.ReadValue<Vector2>();
        control.Game.Move.canceled += move => direction = move.ReadValue<Vector2>();
        control.Game.Jump.started += Jump;
        control.Game.Jump.canceled += JumpEnd;
        control.Game.Aim.performed += aim => this.aim = aim.ReadValue<Vector2>();
        control.Game.Aim.canceled += aim => this.aim = aim.ReadValue<Vector2>();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim.speed = speed / 17;
    }
    private void FixedUpdate()
    {
        if (direction.x > 0.2f) rb.AddForce(Vector2.right * speed * 777, ForceMode2D.Force);
        if (direction.x < -0.2f) rb.AddForce(Vector2.left * speed * 777, ForceMode2D.Force);
        if (direction.x > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
        if (direction.x < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
        if (direction.x != 0 && (rb.velocity.x > 1 || rb.velocity.x < -1))
        {
            anim.SetBool("move", true);
        }
        else
        {
            anim.SetBool("move", false);
        }
    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapBox(col.bounds.center - new Vector3(0, col.bounds.extents.y, 0), new Vector2(3.9f, 0.1f), 0, jumpGround);

        if (rb.velocity.y > 0) rb.gravityScale = 12;
        else
        {
            rb.gravityScale = 60;
            isJump = true;
        }
        anim.SetBool("ground", isGrounded);
        if (isGrounded && jumpBuffer && control.Game.Jump.IsPressed() && Time.timeScale != 0)
        {
            rb.AddForce(Vector2.up * jumpForce * 700, ForceMode2D.Impulse);
            anim.Play("Jump" + Random.Range(1, 3));
            CancelInvoke(nameof(BufferEnd));
            jumpBuffer = false;
        }
        if (Time.timeScale != 0)
        {
            if (aim.y > 0) aimRotate.rotation = Quaternion.Euler(0, 0, 90);
            else if (aim.y < 0) aimRotate.rotation = Quaternion.Euler(0, 0, -90);
            else
            {
                if (transform.rotation.y == 0) aimRotate.rotation = Quaternion.Euler(0, 0, 0);
                else aimRotate.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
    }

    private void Jump(InputAction.CallbackContext callback)
    {
        if (Time.timeScale != 0)
        {
            jumpBuffer = true;
            Invoke(nameof(BufferEnd), 0.12f);
        }
    }
    void BufferEnd()
    {
        jumpBuffer = false;
    }
    private void JumpEnd(InputAction.CallbackContext callback)
    {
        if (rb.velocity.y > 0) rb.velocity = new Vector2(rb.velocity.x, 0);
        CancelInvoke(nameof(BufferEnd));
        jumpBuffer = false;
        isJump = true;
    }
    private void OnDestroy()
    {
        control.Game.Disable();
    }
}
