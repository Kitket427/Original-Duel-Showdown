using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPos : MonoBehaviour
{
    private Transform player;
    private void Start()
    {
        player = FindObjectOfType<PlayerControl>().GetComponent<Transform>();
    }
    void Update()
    {
        if (player.position.x < transform.position.x) transform.rotation = Quaternion.Euler(0, 180, 0);
        else transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
