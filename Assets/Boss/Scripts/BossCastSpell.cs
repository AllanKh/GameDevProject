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
    private GameObject currentSpell;

    //Bools
    private bool isEnabled = false;
    private bool isCasting = false;
    [SerializeField] public float min, max;

    

    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
        bossWalk = GetComponent<BossWalk>();
        player = GameObject.FindWithTag("Player").transform;
    }

    //create a spell
    private void CastSpell()
    {
        currentSpell = Instantiate(spellPrefab, new Vector2(player.position.x, player.position.y + 2.6f), Quaternion.identity);
    }

    //method for the boss the be still att random times
    private void BossRandomStationary()
    {
        float randTime = Random.Range(min, max);
        Invoke("ExecuteSpellCast", randTime);

    }

    //Do spell
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

    //Wait before destroying spell
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
    
    //Spell casting is active
    public void EnableSpellCasting()
    {
        if (!isEnabled)
        {
            isEnabled = true;
            BossRandomStationary();
        }
    }

    //Spell casting is inactive
    public void DisableSpellCasting() 
    {
        isEnabled = false;
        CancelInvoke("ExecuteSpellCast");
        isCasting= false;
    }
}
