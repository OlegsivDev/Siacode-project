using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Search;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource walkingSound;

    [SerializeField] private AudioSource shootBowSound;
    [SerializeField] private AudioSource ArrowImpactSound;
    [SerializeField] private AudioSource SoundtrackSound;

    // Start is called before the first frame update
    void OnEnable()
    {
        CharacterController.OnPlayerStartWalking += playWalkingSound;
        CharacterController.OnPlayerStopWalking += stopWalkingSound;
        CharacterController.OnPlayerShootBow += playShootSound;
        Projectile.OnArrowImpact += playArrowImpactSound;
    }

    private void OnDestroy()
    {
        CharacterController.OnPlayerStartWalking -= playWalkingSound;
        CharacterController.OnPlayerStopWalking -= stopWalkingSound;
        CharacterController.OnPlayerShootBow -= playShootSound;
        Projectile.OnArrowImpact -= playArrowImpactSound;
    }

    void playWalkingSound()
    {
        if (!walkingSound.isPlaying)
        {
            walkingSound.Play();
        }
    }

    void stopWalkingSound()
    {
        if (walkingSound.isPlaying)
        {
            walkingSound.Stop();
        }
    }

    void playShootSound()
    {
        shootBowSound.Play();
    }

    void playArrowImpactSound()
    {
        ArrowImpactSound.Play();
    }
    

    // Update is called once per frame
    void Update()
    {
    }
}