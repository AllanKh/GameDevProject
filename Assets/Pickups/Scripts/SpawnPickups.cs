using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{

    public GameObject myPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(myPrefab, new Vector2(81, 5), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
