using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviourPun
{
    [Header("변비약")]
    public AudioClip poopSound; 
    [Header("리모컨")]
    public AudioClip onTVSound; 
    [Header("")]
    public AudioClip attackSound;
    private AudioSource audioSource; 
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); 
    }

    public void PoopSound()
    {
        if (photonView.IsMine)
            audioSource.PlayOneShot(poopSound);
    }

    public void OnTVSound()
    {
        if (photonView.IsMine)
            audioSource.PlayOneShot(onTVSound);
    }

    public void PlayAttackSound()
    {
        audioSource.PlayOneShot(attackSound);
    }
}
