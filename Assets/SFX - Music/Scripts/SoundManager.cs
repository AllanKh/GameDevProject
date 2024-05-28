using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    public static SoundManager Instance { get; private set; }

    private float volume = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        PlayerColliders.OnAnySkeletonAttacked += PlayerColliders_OnAnySkeletonAttacked;
        PlayerColliders.OnAnyFlyingEyeAttacked += PlayerColliders_OnAnyFlyingEyeAttacked;
        PlayerManager.OnPlayerDamageTaken += PlayerManager_OnPlayerDamageTaken;
        SkeletonDamageHandler.OnPlayerBlockedAttack += SkeletonDamageHandler_OnPlayerBlockedAttack;
        PickupCollider.OnAnyPotionPickUp += PickupCollider_OnAnyPotionPickUp;
        PickupCollider.OnAnyCoinPickUp += PickupCollider_OnAnyCoinPickUp;
        BarrelLogic.OnAnyBarrelBreak += BarrelLogic_OnAnyBarrelBreak;
        Movement.OnWalking += Movement_onWalking;
        BossAttack.bossSwingAttack += BossAttack_bossSwingAttack;
        ButtonSoundCollider.ButtonHovered += ButtonSoundCollider_ButtonHovered;
        ButtonSoundCollider.ButtonPressed += ButtonSoundCollider_ButtonPressed;
        BossAttack.bossattackBlocked += BossAttack_bossattackBlocked;
        FlyingEyeDamageHandler.PlayerBlockedEyeAttack += FlyingEyeDamageHandler_PlayerBlockedEyeAttack;
        
        
    }


    private void OnDestroy()
    {
        PlayerColliders.OnAnySkeletonAttacked -= PlayerColliders_OnAnySkeletonAttacked;
        PlayerColliders.OnAnyFlyingEyeAttacked -= PlayerColliders_OnAnyFlyingEyeAttacked;
        PlayerManager.OnPlayerDamageTaken -= PlayerManager_OnPlayerDamageTaken;
        SkeletonDamageHandler.OnPlayerBlockedAttack -= SkeletonDamageHandler_OnPlayerBlockedAttack;
        PickupCollider.OnAnyPotionPickUp -= PickupCollider_OnAnyPotionPickUp;
        PickupCollider.OnAnyCoinPickUp -= PickupCollider_OnAnyCoinPickUp;
        BarrelLogic.OnAnyBarrelBreak -= BarrelLogic_OnAnyBarrelBreak;
        Movement.OnWalking -= Movement_onWalking;
        BossAttack.bossSwingAttack -= BossAttack_bossSwingAttack;
        ButtonSoundCollider.ButtonHovered -= ButtonSoundCollider_ButtonHovered;
        ButtonSoundCollider.ButtonPressed -= ButtonSoundCollider_ButtonPressed;

 
    }

    private void FlyingEyeDamageHandler_PlayerBlockedEyeAttack(object sender, System.EventArgs e)
    {
        FlyingEyeDamageHandler flyingEyeDamageHandler = sender as FlyingEyeDamageHandler;
        PlaySound(audioClipRefsSO.playerBlock, flyingEyeDamageHandler.transform.position);
    }

    private void BossAttack_bossattackBlocked(object sender, System.EventArgs e)
    {
       BossAttack bossAttack = sender as BossAttack;
        PlaySound(audioClipRefsSO.playerBlock, bossAttack.transform.position);
    }

    private void SkeletonDamageHandler_OnPlayerBlockedAttack(object sender, System.EventArgs e)
    {
       SkeletonDamageHandler skeletonDamageHandler = sender as SkeletonDamageHandler;
        PlaySound(audioClipRefsSO.playerBlock, skeletonDamageHandler.transform.position);
    }
    private void ButtonSoundCollider_ButtonPressed(object sender, System.EventArgs e)
    {
        ButtonSoundCollider buttonSoundCollider = sender as ButtonSoundCollider;
        PlaySound(audioClipRefsSO.UI_Select, Camera.main.transform.position);
        Debug.Log("ButtonPress Sound should play!");
    }

    private void ButtonSoundCollider_ButtonHovered(object sender, System.EventArgs e)
    {
        ButtonSoundCollider buttonSoundCollider = sender as ButtonSoundCollider;
        PlaySound(audioClipRefsSO.UI_HighLight, Camera.main.transform.position);
        Debug.Log("ButtonHover sound should play!");
    }

    private void PlayerManager_OnPlayerDamageTaken(object sender, System.EventArgs e)
    {       
        PlaySound(audioClipRefsSO.playerDamageTaken, Camera.main.transform.position);
        Debug.Log("Damage Taken should play!");

    }

    private void BossAttack_bossSwingAttack(object sender, System.EventArgs e)
    {
        BossAttack bossAttack = sender as BossAttack;
        PlaySound(audioClipRefsSO.bossAttackNormal, bossAttack.transform.position);
    }

    private void Movement_onWalking(object sender, System.EventArgs e)
    {
        //When the player is walking, play these sounds.
        Movement movement = sender as Movement;
        PlaySound(audioClipRefsSO.playerWalk, movement.transform.position);
    }


    private void BarrelLogic_OnAnyBarrelBreak(object sender, System.EventArgs e)
    {
        //When a barrel breaks, play these sounds.
        BarrelLogic barrelLogic = sender as BarrelLogic;
        PlaySound(audioClipRefsSO.barrelBreak, barrelLogic.transform.position);
    }

    private void PickupCollider_OnAnyCoinPickUp(object sender, System.EventArgs e)
    {
        //When player picks up a coin, play these sounds.
        PickupCollider pickupCollider = sender as PickupCollider;
        PlaySound(audioClipRefsSO.coinPickUp, pickupCollider.transform.position);
    }

    private void PickupCollider_OnAnyPotionPickUp(object sender, System.EventArgs e)
    {
        //When player picks up a potion, play these sounds.
        PickupCollider pickupCollider = sender as PickupCollider;
        PlaySound(audioClipRefsSO.potionPickUp, pickupCollider.transform.position);
    }

    private void PlayerColliders_OnAnyFlyingEyeAttacked(object sender, System.EventArgs e)
    {
        //When Flying Eye gets attacked, play these sounds.
        PlayerColliders playerColliders = sender as PlayerColliders;
        PlaySound(audioClipRefsSO.flyingEyeLightAttacked, playerColliders.transform.position);
    }

    private void PlayerColliders_OnAnySkeletonAttacked(object sender, System.EventArgs e)
    {
        //When Skeleton gets attacked, play these sounds.
        PlayerColliders playerColliders = sender as PlayerColliders;
        PlaySound(audioClipRefsSO.skeletonLightAttacked, playerColliders.transform.position);

    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier =1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {       
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }


    public void PlayButtonSouns()
    {

    }

    public void ChangeVolume() //Change the game effects volume
    {
        volume += 0.1f;
        if (volume > 1.1f)
        {
            volume = 0f;
        }
    }

    public float GetVolume()
    {
        return volume;
    }

    public void DestroySoundManager()
    {
        Destroy(gameObject);
    }

}
