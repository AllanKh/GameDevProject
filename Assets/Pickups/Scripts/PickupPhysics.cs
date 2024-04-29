using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPhysics : MonoBehaviour
{
    private Rigidbody2D itemRB;
    [SerializeField] float dropForce = 4;
    [SerializeField] int xAxisForce = 2;
    //TODO: add angles so that items can fly in different directions.

    // Start is called before the first frame update
    private void Start()
    {
        int i = Random.Range(-xAxisForce, xAxisForce);
        itemRB = GetComponent<Rigidbody2D>();
        itemRB.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
        itemRB.AddForce(Vector2.right * i, ForceMode2D.Impulse);
    }
}
