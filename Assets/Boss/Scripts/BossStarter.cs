using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStarter : MonoBehaviour
{
    public static BossStarter Instance {  get; private set; }

    [SerializeField] private GameObject bossPrefab;

    public void Awake()
    {
        Instance = this;
    }
    public void SpawnBoss()
    {
        Instantiate(bossPrefab, transform.position, Quaternion.identity);
    }
}
