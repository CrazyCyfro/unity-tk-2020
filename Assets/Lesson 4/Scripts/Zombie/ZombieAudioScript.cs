using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAudioScript : MonoBehaviour
{
    public ZombieData zombieData;
    public AudioSource zombieAudio;

    public void PlayIdleClip()
    {
        zombieAudio.PlayOneShot(zombieData.idleClip);
    }

    public void PlayHurtClip()
    {
        zombieAudio.Stop();
        zombieAudio.PlayOneShot(zombieData.hurtClip);
    }

    public void PlayAttackClip()
    {
        zombieAudio.PlayOneShot(zombieData.attackClip);
    }
}
