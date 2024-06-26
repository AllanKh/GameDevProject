using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDropThrough : MonoBehaviour
{
    //bool OnPlatform;
    //private Rigidbody2D myRigidbody;


    //private void Start()
    //{
    //    myRigidbody = GetComponent<Rigidbody2D>();
    //    myRigidbody.simulated = true;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    OnPlatform = true;
    //    Debug.Log(OnPlatform);
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    OnPlatform = false;
    //    Debug.Log(OnPlatform);
    //}

    //private void Update()
    //{
    //    if (OnPlatform && Input.GetKeyDown(KeyCode.S))
    //    {
    //        myRigidbody.simulated = false;
    //        Debug.Log("BORPASPIN EXTREME");

    //        if (myRigidbody.simulated == false && OnPlatform == false)
    //        {
    //            myRigidbody.simulated = true;
    //            Debug.Log("Corpa Spin Lowkey");
    //        }
    //    }
    //}



    private Collider2D _collider;
    private bool _playerOnPlatform;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (_playerOnPlatform && Input.GetKeyDown(KeyCode.S))
        {
            _collider.enabled = false;
            StartCoroutine(routine: EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, false);
    }
}
