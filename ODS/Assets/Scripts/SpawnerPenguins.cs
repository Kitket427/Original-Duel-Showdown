using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct Penguins
{
    public GameObject[] penguins;
}
public class SpawnerPenguins : MonoBehaviour
{
    [SerializeField] private Penguins[] penguins;
    private int number, count;
    [SerializeField] private GameObject wall, effectSpawn;
    void Start()
    {
        wall.SetActive(true);
        Spawn();
    }
    void Spawn()
    {
        if(number == penguins.Length) wall.SetActive(false);
        else
        {
            effectSpawn.SetActive(false);
            effectSpawn.SetActive(true);
            for (int i = 0; i < penguins[number].penguins.Length; i++)
            {
                penguins[number].penguins[i].SetActive(true);
            }
        }
    }
    public void Counting()
    {
        count++;
        if (count == penguins[number].penguins.Length)
        {
            number++;
            Invoke(nameof(Spawn), 2f);
            count = 0;
        }
    }
}
