using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   
    public static SoundManager Instance { get; private set; }

    private float volume = 1f;

    private void Awake()
    {
        Instance = this;
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier =1f)
    {
       AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
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

}
