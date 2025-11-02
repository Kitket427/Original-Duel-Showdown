using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKitaC : MonoBehaviour
{
    [SerializeField] private float speed, x;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Vector2 randomXY, center;
    private Vector2 rand;
    private Animator anim;
    private int phase, toPhase, toEnd, rotate, randoming;
    [SerializeField] private Transform cam;
    void Start()
    {
        cam.rotation = Quaternion.Euler(0,0,0);
        anim = GetComponent<Animator>();
        anim.SetBool("ground", false);
        rand = new Vector2(960, 20);
        Invoke(nameof(Phase), 2f);
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
        toEnd = 0;
        int curPhase = phase;
        while (curPhase == phase) phase = Random.Range(1, 8);
        switch (phase)
        {
            case 1:
                speed = 1.2f;
                Invoke(nameof(Attack1n2), Random.Range(1f, 2f));
                break;

            case 2:
                speed = 1.2f;
                Invoke(nameof(Attack1n2), Random.Range(1f, 2f));
                break;

            case 3:
                speed = 2f;
                Invoke(nameof(Attack6), Random.Range(1f, 2f));
                RandomPosX();
                break;
            case 4:
                x = Random.Range(940f, 980f);
                speed = 2f;
                Invoke(nameof(Attack3), Random.Range(1f, 2f));
                break;
            case 5:
                speed = 1.2f;
                Invoke(nameof(Attack7), Random.Range(3f, 4f));
                break;
            case 6:
                speed = 2f;
                Invoke(nameof(Attack8), Random.Range(1f, 2f));
                break;
            case 7:
                speed = 2f;
                Invoke(nameof(Attack8), Random.Range(1f, 2f));
                break;
        }
    }
    void Attack1n2()
    {
        speed = 0.4f;
        Instantiate(bullets[0], transform.position, Quaternion.Euler(0, 0, toPhase));
        if (phase == 1) toPhase += 4;
        else toPhase -= 4;
        if (toPhase > 180 || toPhase < -180) Phase();
        else Invoke(nameof(Attack1n2), 0.05f);
    }
    void Attack3()
    {
        speed = 0.4f;
        while (rotate == randoming) randoming = Random.Range(0, 4);
        rotate = randoming;
        switch (randoming)
        {
            case 0:
                cam.position = new Vector3(960, 0, -10);
                cam.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case 1:
                cam.position = new Vector3(960, 0, -10);
                cam.rotation = Quaternion.Euler(180, 180, 0);
                break;

            case 2:
                cam.position = new Vector3(960, 0, 10);
                cam.rotation = Quaternion.Euler(0, 180, 0);
                break;

            case 3:
                cam.position = new Vector3(960, 0, 10);
                cam.rotation = Quaternion.Euler(180, 0, 0);
                break;
        }
        toPhase += 1;
        if (toPhase > 0)
        {
            Phase();
        }
        else Invoke(nameof(Attack6), 0.07f);
    }
    void Attack6()
    {
        speed = 0.4f;
        Instantiate(bullets[1], new Vector2(x, 90), Quaternion.Euler(0, 0, 0));
        toPhase += 1;
        if (toPhase > 7)
        {
            if(toEnd > Random.Range(0,4)) Phase();
            else
            {
                toEnd++;
                RandomPosX();
                Invoke(nameof(Attack6), Random.Range(0.7f, 1.7f));
                toPhase = 0;
            }
        }
        else Invoke(nameof(Attack6), 0.07f);
    }
    void RandomPosX()
    {
        x = Random.Range(940f, 980f);
    }
    void Attack7()
    {
        speed = 0.4f;
        Instantiate(bullets[3], transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        toPhase++;
        if (toPhase > 33) Phase();
        else Invoke(nameof(Attack7), 0.09f);
    }
    void Attack8()
    {
        Instantiate(bullets[2], new Vector2(1100, 0), Quaternion.Euler(0, 0, -90));
        Instantiate(bullets[2], new Vector2(820, 0), Quaternion.Euler(0, 0, 90));
        toPhase += 1;
        if (toPhase > 2)
        {
            Invoke(nameof(Phase), Random.Range(1f, 2f));
        }
        else Invoke(nameof(Attack8), 0.1f);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}

