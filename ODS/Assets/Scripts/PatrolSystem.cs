using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
struct Patrol
{
    public Vector2 pos;
    public float time;
}
public class PatrolSystem : MonoBehaviour
{
    [SerializeField] private Patrol[] patrols;
    [SerializeField] private Vector2 pos;
    [SerializeField] private int p;
    [SerializeField] private float speed;
    private bool active;
    private float time, timeToReady;
    private void Start()
    {
        pos = transform.position;
        for (int i = 0; i < patrols.Length; i++)
        {
            patrols[i].pos += pos;
        }
    }
    void FixedUpdate()
    {
        if(active) transform.position = Vector2.MoveTowards(transform.position, patrols[p].pos, speed * Time.fixedDeltaTime);
        else
        {
            time += Time.fixedDeltaTime;
            if (time >= timeToReady)
            {
                active = true;
                time = 0;
            }
        }
        if(Vector2.Distance(transform.position, patrols[p].pos) < 0.7f)
        {
            transform.position = patrols[p].pos;
            active = false;
            timeToReady = patrols[p].time;
            if (p < patrols.Length - 1) p++;
            else p = 0;
        }
    }
}
