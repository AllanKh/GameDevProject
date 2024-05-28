using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStarter : MonoBehaviour
{
    public static BossStarter Instance {  get; private set; }

    //References
    [SerializeField] private GameObject bossPrefab;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); //Destroys GameObject script it is attatched to if there is a duplicate
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Prevents this script from being destroyed when changing scenes
        }
    }
    // call this to spawn boss
    public void SpawnBoss()
    {
        Instantiate(bossPrefab, transform.position, Quaternion.identity);
    }
}
