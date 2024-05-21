using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCastSpell : MonoBehaviour
{
    public static BossCastSpell instance {  get; private set; } 

    //References
    private Animator anim;
    private BossWalk bossWalk;
    [SerializeField] private GameObject spellPrefab;
    private Transform player;

    //Bools
    private bool isEnabled = false;
    private bool isCasting = false;

    //
    [SerializeField] public float min, max;
    private GameObject currentSpell;

    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
        bossWalk = GetComponent<BossWalk>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void CastSpell()
    {
        currentSpell = Instantiate(spellPrefab, new Vector2(player.position.x, player.position.y + 2.6f), Quaternion.identity);
    }

    private void BossRandomStationary()
    {
        float randTime = Random.Range(min, max);
        Invoke("ExecuteSpellCast", randTime);

    }

    private void ExecuteSpellCast()
    {
        if (isEnabled)
        {
            isCasting = true;
            anim.SetTrigger("castSpell");
            bossWalk.startMoving = false;
            StartCoroutine(WaitAndResume());
        }
    }

    private IEnumerator WaitAndResume()
    {
        yield return new WaitForSeconds(3);
        if (currentSpell != null)
        {
            Destroy(currentSpell);
        }
        bossWalk.startMoving = true;
        isCasting = false;

        if (isEnabled)
        {
            BossRandomStationary();
        }
    }
      
    public void EnableSpellCasting()
    {
        if (!isEnabled)
        {
            isEnabled = true;
            BossRandomStationary();
        }
    }

    public void DisableSpellCasting() 
    {
        isEnabled = false;
        CancelInvoke("ExecuteSpellCast");
        isCasting= false;
    }
}
