using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    //Enemies
    public AudioClip[] skeletonLightAttacked;
    public AudioClip[] flyingEyeLightAttacked;

    //Boss
    public AudioClip[] bossAttackNormal;
    public AudioClip[] bossAttackFast;
    public AudioClip[] bossDamageTaken;
    
    //Player
    public AudioClip[] playerWalk;
    public AudioClip[] playerDamageTaken;
    public AudioClip[] playerBlock;

    //Misc
    public AudioClip[] potionPickUp;
    public AudioClip[] coinPickUp;
    public AudioClip[] barrelBreak;


    //UI
    public AudioClip[] UI_HighLight;
    public AudioClip[] UI_PlayGame;
    public AudioClip[] UI_Select;
    public AudioClip[] UI_StepBack;
    
}
