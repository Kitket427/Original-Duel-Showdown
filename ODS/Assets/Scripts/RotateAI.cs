using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotateAI : MonoBehaviour
{
    private float rotate;
    public float speed, offset;
    private float rotateZ, minDist;
    [SerializeField] private Transform target;
    //private Transform[] targets;
    [SerializeField] private bool player;
    private void Start()
    {
        OnEnable();
    }
    private void OnEnable()
    {
        rotate = transform.eulerAngles.z;
        if (player)
        {
            Find();
        }
        else target = FindObjectOfType<PlayerPos>().transform;
    }
    private void FixedUpdate()
    {
        rotate = Mathf.MoveTowardsAngle(transform.eulerAngles.z, rotateZ, speed * Time.fixedDeltaTime);
    }
    void Find()
    {
        minDist = 999;
        var targets = FindObjectsOfType<EnemyPos>();
        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (minDist > Vector2.Distance(transform.position, targets[i].transform.position))
                {
                    minDist = Vector2.Distance(transform.position, targets[i].transform.position);
                    target = targets[i].transform;
                }
            }
        }
        Invoke(nameof(Find), 0.1f);
    }
    void Update()
    {
        if (target)
        {
            Vector3 difference = target.position - transform.position;
            rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + offset;
            transform.rotation = Quaternion.Euler(0, 0, rotate);
        }
    }
}
