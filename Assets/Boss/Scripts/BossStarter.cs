using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStarter : MonoBehaviour
{
    public static BossStarter Instance {  get; private set; }

    [SerializeField] private GameObject bossPrefab;

    private Vector2 spawnPoint = new Vector2(373.0f, -37.0f);

    public void Awake()
    {
        Instance = this;
    }
    public void SpawnBoss()
    {
        Instantiate(bossPrefab, spawnPoint, Quaternion.identity);
    }
}
