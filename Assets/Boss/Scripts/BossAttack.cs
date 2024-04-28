using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = 0;
    
    //References
    private Animator anim;
    private BossWalk bossWalk;

    //Phase 2
    private bool phase2Activated = false;
    private float phase2AttackCooldownMulti = 0.5f;
    private float phase2Threshold = 30f;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        bossWalk =  GetComponentInParent<BossWalk>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when boss sees player
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;

                anim.SetTrigger("attack");
            }
        }
        //When player is not in sight keep moving when player is in sight attack and stop moving
        if (bossWalk != null)
        {
            bossWalk.enabled = !PlayerInSight();
        }

        Phase2();
    }

    //Checks if the player is in sight of the boss
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    //Shows the the boss attacking box
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    //Damage player when boss attack
    private void DamagePlayer()
    {
        if (PlayerInSight()) 
        {
            PlayerManager.Instance.DamagePlayer(BossManager.Instance.AttackDamage);

            //See player health report with each boss attack
            Debug.Log(PlayerManager.Instance.Health);
        }
    }

    private void Phase2()
    {
        if (!phase2Activated && BossManager.Instance.Health <= phase2Threshold)
        {
            attackCooldown *= phase2AttackCooldownMulti;
            phase2Activated = true;
        }
    }
}
