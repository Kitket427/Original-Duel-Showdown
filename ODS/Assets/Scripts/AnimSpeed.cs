using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeed : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] private bool floatSpeed, randomSpeed;
    [SerializeField] private Transform target;
    private void Start()
    {
        anim = GetComponent<Animator>();
        if (randomSpeed) speed += Random.Range(-0.5f, 0.5f);
        if (floatSpeed == false) anim.speed = speed;
        else anim.SetFloat("speed", speed);
    }
    private void Update()
    {
        if (target) transform.position = target.position;
    }
}
