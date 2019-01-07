using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource source;

    public AudioClip shootSound;
    public AudioClip jumpSound;

    private int vol = 75;

    public void playShootSound()
    {
        source.PlayOneShot(shootSound, vol);
    }

    public void playJumpSound()
    {
        source.PlayOneShot(jumpSound, vol);
    }
}
