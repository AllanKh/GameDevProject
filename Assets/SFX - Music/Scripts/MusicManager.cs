using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{

    [SerializeField] private AudioClip musicClip;

    public static MusicManager Instance {  get; private set; }


    private AudioSource audioSource;
    private float volume = 0.3f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        BossAttack.secondPhase += BossAttack_secondPhase;
    }

    private void BossAttack_secondPhase(object sender, System.EventArgs e)
    {
        ChangeMusic(musicClip);
    }

    public void ChangeVolume() //Change the game effects volume
    {
        volume += 0.1f;
        if (volume > 1.1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }


    public void ChangeMusic(AudioClip music)  //This changes the music to whatever you want it to be, kinda.
    {
        Debug.Log("Test me :)");
        audioSource.Stop();
        audioSource.clip = music;
        audioSource.Play();
    }



}
