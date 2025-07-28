using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed, time, rotate;
    [SerializeField] private GameObject effect, trailEffect;
    [SerializeField] private HitpointSystem melodyHealing;
    private void Start()
    {
        Invoke(nameof(Dead), time);
        if(melodyHealing)
        {
            damage = Random.Range(11, 22);
        }
        if (trailEffect)
        {
            InvokeRepeating(nameof(Effect), 0, 0.03f);
        }
    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0,0, transform.eulerAngles.z + Time.deltaTime * rotate);
    }
    void Effect()
    {
        Instantiate(trailEffect, transform.position, transform.rotation);
    }
    void Dead()
    {
        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            if(melodyHealing && damage < 14)
            {
                melodyHealing.Healing((damage - 10)*3);
            }
        }
        Dead();
    }
}
