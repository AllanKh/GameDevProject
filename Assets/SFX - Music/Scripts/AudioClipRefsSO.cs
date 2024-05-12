using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    //Enemies
    public AudioClip[] skeletonLightAttacked;
    public AudioClip[] flyingEyeLightAttacked;
    
    //Player
    public AudioClip[] playerWalk;
    public AudioClip[] playerDamageTaken;

    //Misc
    public AudioClip[] potionPickUp;
    public AudioClip[] coinPickUp;
    public AudioClip[] barrelBreak;

}
