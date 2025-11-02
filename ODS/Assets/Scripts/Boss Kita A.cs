using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossKitaA : MonoBehaviour
{
    [SerializeField] private float speed, x;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Vector2 randomXY, center;
    private Vector2 rand;
    private Animator anim;
    private int phase, toPhase;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("ground", false);
        rand = new Vector2(960,20);
        Invoke(nameof(Phase), 5.5f);
        phase = 0;
    }
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, rand) < 5) rand = new Vector2(center.x + Random.Range(-randomXY.x, randomXY.x), center.y + Random.Range(-randomXY.y, randomXY.y));
        transform.position = Vector2.MoveTowards(transform.position, rand, speed);
    }
    void Phase()
    {
        toPhase = 0;
        int curPhase = phase;
        while(curPhase == phase) phase = Random.Range(1, 8);
        switch (phase)
        {
            case 1:
                speed = 0.2f;
                Invoke(nameof(Attack1n2), 1f);
                break;

            case 2:
                speed = 0.2f;
                Invoke(nameof(Attack1n2), 1f);
                break;
            
            case 3:
                speed = 2f;
                Invoke(nameof(Attack3), 1f);
                break;
            
            case 4:
                speed = 7f;
                Invoke(nameof(Attack4), Random.Range(1f, 2f));
                break;
            case 5:
                speed = 0.2f;
                Invoke(nameof(Attack5), 2f);
                break;
            case 6:
                x = Random.Range(940f, 980f);
                speed = 7f;
                Invoke(nameof(Attack6), Random.Range(1f, 2f));
                break;
            case 7:
                x = Random.Range(940f, 980f);
                speed = 7f;
                Invoke(nameof(Attack7), Random.Range(1f, 2f));
                break;
        }
    }
    void Attack1n2()
    {
        Instantiate(bullets[0], transform.position, Quaternion.Euler(0, 0, toPhase));
        if (phase == 1) toPhase+=4;
        else toPhase-=4;
        if (toPhase > 90 || toPhase < -90) Phase();
        else Invoke(nameof(Attack1n2), 0.07f);
    }
    void Attack3()
    {
        Instantiate(bullets[1], transform.position, Quaternion.Euler(0, 0, Random.Range(0,360)));
        toPhase++;
        if (toPhase > 22) Phase();
        else Invoke(nameof(Attack3), 0.07f);
    }
    void Attack4()
    {
        speed = 0.2f;
        if(toPhase > 0)Instantiate(bullets[2], transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        toPhase++;
        if (toPhase > Random.Range(3,6)) Phase();
        else Invoke(nameof(Attack4), Random.Range(0.5f, 2f));
    }
    void Attack5()
    {
        Instantiate(bullets[3], transform.position, Quaternion.Euler(0, 0, 0));
        toPhase += 1;
        if (toPhase > 30) Phase();
        else Invoke(nameof(Attack5), 0.09f);
    }
    void Attack6()
    {
        Instantiate(bullets[4], new Vector2(x, 90), Quaternion.Euler(0, 0, 0));
        toPhase += 1;
        if (toPhase > 11) Phase();
        else Invoke(nameof(Attack6), 0.12f);
    }
    void Attack7()
    {
        Instantiate(bullets[4], new Vector2(x, 90), Quaternion.Euler(0, 0, 0));
        toPhase += 1;
        if (toPhase > 11) Phase();
        else Invoke(nameof(Attack7), 0.12f);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
