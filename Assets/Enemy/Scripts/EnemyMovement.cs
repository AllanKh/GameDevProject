using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float movementSpeed = 1.5f;
    private Rigidbody2D rb;
    private Animator enemyAnimator;
    private bool startMoving = false;
    private GameObject attackColliderObject;
    private GameObject detectionColliderObject;

    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public GameObject AttackColliderObject { get { return attackColliderObject; } set { attackColliderObject = value; } }
    public GameObject DetectionColliderObject { get { return detectionColliderObject; } set { detectionColliderObject = value; } }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetInteger("Anim_State", 0);
        StartCoroutine(WaitBeforeMoving());
        attackColliderObject = transform.Find("AttackCollider").gameObject;
        detectionColliderObject = transform.Find("DetectionCollider").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAttacking enemyAttacking = GetComponent<EnemyAttacking>();

        if (startMoving && !enemyAttacking.AttackAnimationActive)
        {
            Movement();
        }
    }

    //Hold enemy idle the first 5 seconds
    IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(5);
        startMoving = true;
    }

    //Handle horizontal movement
    private void Movement()
    {
        if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon && startMoving)
        {
            //Start walking animation
            enemyAnimator.SetInteger("Anim_State", 1);
        }
        else
        {
            //Stops walking animation
            enemyAnimator.SetInteger("Anim_State", 0);
        }

        //Moves enemy
        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);

        //Moves enemy back and forth
        if (rb.position.x >= 70 && !EnemyManager.Instance.PlayerDetected)
        {
            movementSpeed = -1.5f;
        }
        else if (rb.position.x <= 65 && !EnemyManager.Instance.PlayerDetected)
        {
            movementSpeed = 1.5f;
        }

        //Flip asset according to direction
        if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            FlipAttackCollider(true);
            FlipDetectionCollider(true);
        }
        else if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            FlipAttackCollider(false);
            FlipDetectionCollider(false);
        }
    }

    public void PlayerDetected()
    {
        movementSpeed = (movementSpeed > 0) ? 2f : -2f;
    }

    //Check if attack collider flips
    private void FlipAttackCollider(bool flip)
    {
        attackColliderObject.transform.localPosition = new Vector2(flip ? -0.6f : 0, 0);
    }

    private void FlipDetectionCollider(bool flip)
    {
        detectionColliderObject.transform.localPosition = new Vector2(flip ? -1.55f : 1.55f, 0);
    }
}
