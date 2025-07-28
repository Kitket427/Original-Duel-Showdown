using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitpointSystem : MonoBehaviour, IDamageable
{
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private Material[] materials;
    [SerializeField] private float hp, maxhp;
    [SerializeField] private bool boss;
    [SerializeField] private TimeManager timeManager;
    private AudioSource sfx;
    private bool dead;
    [SerializeField] private GameObject effect;
    [SerializeField] private SpawnerPenguins spawnerCount;
    public bool half;
    [Header("ForBosses")]
    [SerializeField] private FireSystem[] bossesUpgrate;
    private void Start()
    {
        sfx = GetComponent<AudioSource>();
    }
    private void Update()
    {
        
    }
    void IDamageable.TakeDamage(int damage)
    {
        TakeDamage(damage);
    }
    void TakeDamage(int damage)
    {
        if(timeManager && boss == false)
        {
            timeManager.GetDamage(0);
        }
        if (timeManager && hp - damage <= 0)
        {
            timeManager.GetDamage(0.1f);
        }
        hp -= damage;
        if(hp < 0 && dead == false)
        {
            Dead();
            dead = true;
        }
        foreach(var item in sprites)
        {
            item.material = materials[1];
        }
        CancelInvoke();
        Invoke(nameof(ReturnMaterial), 0.05f);
    }
    public void Healing(int damage)
    {
        hp += damage;
        if(hp > maxhp) hp = maxhp;
        foreach (var item in sprites)
        {
            item.material = materials[3];
        }
        CancelInvoke();
        Invoke(nameof(ReturnMaterial), 0.1f);
    }
    void ReturnMaterial()
    {
        foreach (var item in sprites)
        {
            item.material = materials[0];
        }
        if(hp < maxhp/2)
        {
            Ind2();
            half = true;
        }
    }
    void Ind1()
    {
        sfx.Play();
        if (hp > maxhp / 10f) Invoke(nameof(Ind2), (hp * 1f) / (maxhp * 3f));
        else Invoke(nameof(Ind2), 1f / 30f);
        foreach (var item in sprites)
        {
            item.material = materials[2];
        }
    }
    void Ind2()
    {

        if (hp > maxhp / 10f) Invoke(nameof(Ind1), (hp * 1f) / (maxhp * 1f));
        else Invoke(nameof(Ind1), 1f / 10f);
        foreach (var item in sprites)
        {
            item.material = materials[0];
        }
    }
    void Dead()
    {
        if (bossesUpgrate.Length > 0)
        {
            foreach (var item in bossesUpgrate)
            {
                item.Upgrate(true);
            }
        }
        if (spawnerCount) spawnerCount.Counting();
        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
