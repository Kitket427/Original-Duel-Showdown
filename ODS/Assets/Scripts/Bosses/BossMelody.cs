using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMelody : MonoBehaviour
{
    private int phase, posSelect, half;
    [SerializeField] private HitpointSystem hitpointSystem; 
    [SerializeField] private GameObject[] fire;
    [SerializeField] private Transform[] pos;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private LayerMask jumpGround;
    private Collider2D col;
    private bool isGrounded;    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb.AddForce(Vector2.up * 6000, ForceMode2D.Impulse);
        fire[0].SetActive(true);
        Invoke(nameof(Off), 3);
        posSelect = Random.Range(0, 2);
        Invoke(nameof(JumpPhase), 4);
    }
    void Off()
    {
        foreach (var f in fire)
        {
            f.SetActive(false);
        }
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapBox(col.bounds.center - new Vector3(0, col.bounds.extents.y, 0), new Vector2(3.9f, 0.1f), 0, jumpGround);
        anim.SetBool("ground", isGrounded);
    }
    void JumpPhase()
    {
        if(posSelect == 0)
        {
            transform.position = pos[0].position;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            posSelect = 1;
        }
        else
        {
            transform.position = pos[1].position;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            posSelect = 0;
        }
        rb.AddForce(Vector2.down * 6000, ForceMode2D.Impulse);
        if(Random.Range(0,2) == 0) JustFire();
        else JustFireLong();
    }
    void PhaseSelect()
    {
        if(half == 0) phase = Random.Range(0, 4);
        else phase = Random.Range(0, 5);
        if (hitpointSystem.half) half = 4;
        switch (phase)
        {
            case 0:
                Jump();
                break;
            case 1:
                JustFire();
                break;
            case 2:
                JustFireLong();
                break;
            case 3:
                JumpAttack();
                break;
            case 4:
                JumpUltra();
                break;
            default:
                break;
        }
    }
    void JustFire()
    {
        fire[1 + half].SetActive(true);
        Invoke(nameof(Off), 2.5f);
        Invoke(nameof(PhaseSelect), 2.5f);
    }
    void JustFireLong()
    {
        fire[3 + half].SetActive(true);
        Invoke(nameof(Off), 7f);
        Invoke(nameof(PhaseSelect), 7f);
    }
    void Jump()
    {
        rb.AddForce(Vector2.up * 6000, ForceMode2D.Impulse);
        fire[2 + half].SetActive(true);
        Invoke(nameof(Off), 3);
        Invoke(nameof(JumpPhase), 3f);
    }
    void JumpAttack()
    {
        rb.AddForce(Vector2.up * 2000, ForceMode2D.Impulse);
        fire[4 + half].SetActive(true);
        Invoke(nameof(Off), 4);
        Invoke(nameof(PhaseSelect), 4f);
        Invoke(nameof(JumpAttackEnd), 2f);
    }
    void JumpAttackEnd()
    {
        rb.AddForce(Vector2.down * 5000, ForceMode2D.Impulse);
    }
    void JumpUltra()
    {
        rb.AddForce(Vector2.up * 6000, ForceMode2D.Impulse);
        fire[9].SetActive(true);
        Invoke(nameof(Off), 6);
        Invoke(nameof(JumpPhase), 6f);
    }
}
