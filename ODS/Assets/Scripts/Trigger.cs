using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct TriggerActive
{
    public GameObject obj;
    public bool active;
    public float time;
}
public class Trigger : MonoBehaviour
{
    [SerializeField] private TriggerActive[] triggerActives;
    private bool active;
    private int trigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerControl playerControl = collision.GetComponent<PlayerControl>();
        if (playerControl != null && active == false)
        {
            TimeToActive();
            active = true;
        }
    }
    void TimeToActive()
    {
        Invoke(nameof(Active), triggerActives[trigger].time);
    }
    private void Active()
    {
        triggerActives[trigger].obj.SetActive(triggerActives[trigger].active);
        if(trigger < triggerActives.Length - 1)
        {
            trigger++;
            TimeToActive();
        }
    }
}
