using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlider : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector2 size = new Vector2(240, 135);
    private void Start()
    {
        player = FindObjectOfType<PlayerPos>().GetComponent<Transform>();
    }
    void Update()
    {
        if (player)
        {
            if (player.position.x > transform.position.x + size.x / 2) transform.position = new Vector3(transform.position.x + size.x, transform.position.y, -10);
            if (player.position.x < transform.position.x - size.x / 2) transform.position = new Vector3(transform.position.x - size.x, transform.position.y, -10);
            if (player.position.y > transform.position.y + size.y / 2) transform.position = new Vector3(transform.position.x, transform.position.y + size.y, -10);
            if (player.position.y < transform.position.y - size.y / 2) transform.position = new Vector3(transform.position.x, transform.position.y - size.y, -10);
        }
    }
}
