using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireSystem : MonoBehaviour
{
    private Control control;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject effect;
    [SerializeField] private float speed, radius, rasbros, reloadAI, random, fireAI;
    private bool reload, fire, bossUp;
    public float speedFly;
    [SerializeField] private bool nekod, melody;
    private int count = 1;
    [SerializeField] private GameObject[] warning;
    [Header("ForBosses")]
    [SerializeField] private HitpointSystem bossType;
    [SerializeField] private FireSystem[] bossesUpgrate;
    void Start()
    {
        if (reloadAI == 0)
        {
            control = InputManager.inputManager.control;
            control.Game.Enable();
            control.Game.Fire.started += Fire;
            control.Game.Fire.canceled += NoFire;
        }
        speedFly = speed;
    }
    private void OnEnable()
    {
        if (reloadAI != 0) NoFireAI();
    }

    void Update()
    {
        if(reload == false && fire && Time.timeScale != 0)
        {
            Instantiate(bullet, new Vector2(transform.position.x + Random.Range(-radius, radius), transform.position.y + Random.Range(-radius, radius)), Quaternion.Euler(0, 0, transform.eulerAngles.z + Random.Range(-rasbros, rasbros)));
            Instantiate(effect, transform.position, transform.rotation);
            Invoke(nameof(Reload), 1 / speed);
            reload = true;
        }
        if(nekod)
        {
            speed += Time.deltaTime * speed * count;
            rasbros = speed;
            if (speed > 33) count = -1;
            if (speed < 3) count = 1;
        }
        if(bossType && bossType.half && bossUp == false)
        {
            foreach (var item in bossesUpgrate)
            {
                item.Upgrate(false);
            }
            bossUp = true;
        }
    }
    void Reload()
    {
        if (melody) speed = Random.Range(3f, 7f);
        reload = false;
    }
    void Fire(InputAction.CallbackContext callback)
    {
        fire = true;
    }
    void NoFire(InputAction.CallbackContext callback)
    {
        fire = false;
    }
    void FireAI()
    {
        fire = true;
        Invoke(nameof(NoFireAI), fireAI);
    }
    void ReadyFireAI()
    {
        Invoke(nameof(FireAI), Random.Range(0.3f, 0.9f));
        warning[1].SetActive(true);
        warning[0].SetActive(false);
    }
    void NoFireAI()
    {
        fire = false;
        Invoke(nameof(ReadyFireAI), reloadAI + Random.Range(-random, random));
        warning[1].SetActive(false);
        warning[0].SetActive(true);
    }
    private void OnDisable()
    {
        CancelInvoke();
        warning[1].SetActive(false);
        warning[0].SetActive(true);
        reload = false;
    }
    public void Upgrate(bool dead)
    {
        if(dead)
        {
            reloadAI /= 2;
            random /= 2;
            speedFly *= 1.1f;
        }
        rasbros += 3;
        fireAI *= 1.2f;
        speed *= 1.2f;
        speedFly *= 1.1f;
    }
}
