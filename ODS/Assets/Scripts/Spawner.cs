using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private float speed, time;
    [SerializeField] private bool random, heightDamage;
    [SerializeField] private float height;
    private bool active;
    void Start()
    {
        InvokeRepeating(nameof(Spawn), time, 1f / speed);
    }

    void Spawn()
    {
        if (random) Instantiate(bullet[Random.Range(0, bullet.Length)], transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        else Instantiate(bullet[Random.Range(0, bullet.Length)], transform.position, transform.rotation);
    }
    private void Update()
    {
        if (heightDamage)
        {
            if (height > transform.position.y && active == false)
            {
                active = true;
                CancelInvoke();
            }
            if(height < transform.position.y && active)
            {
                InvokeRepeating(nameof(Spawn), time, 1f / speed);
                active = false;
            }
        }
    }
}
