using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinRandomFly : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private Vector2 randomXY, center;
    private Vector2 rand;
    private FireSystem fireSystem;
    private Animator anim;
    void Start()
    {
        transform.position = new Vector2(center.x + Random.Range(-randomXY.x, randomXY.x), transform.position.y);
        anim = GetComponent<Animator>();
        anim.SetBool("ground", false);
        fireSystem = GetComponentInChildren<FireSystem>();
        rand = new Vector2(center.x + Random.Range(-randomXY.x, randomXY.x), center.y + Random.Range(-randomXY.y, randomXY.y));
        InvokeRepeating(nameof(Fly), 0 , 0.1f);
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, rand, fireSystem.speedFly / 7);
        if(Vector2.Distance(transform.position, rand) < 5) rand = new Vector2(center.x + Random.Range(-randomXY.x, randomXY.x), center.y + Random.Range(-randomXY.y, randomXY.y));
    }
    void Fly()
    {
        Instantiate(effect, transform.position - new Vector3(0,2,0), transform.rotation);
    }
}
