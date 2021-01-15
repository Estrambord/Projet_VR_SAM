using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject deaf;
    public GameObject mute;
    public GameObject blind;

    public GameObject fail;
    public GameObject succeed;

    private bool isBlind;

    public bool Blind
    {
        get { return isBlind; }
        set { isBlind = value; }
    }

    private bool isDeaf;

    public bool Deaf
    {
        get { return isDeaf; }
        set { isDeaf = value; }
    }

    private bool isMute;

    public bool Mute
    {
        get { return isMute; }
        set { isMute = value; }
    }


    public AudioSource playerAudioSource;
    public AudioClip failSound;
    public AudioClip successSound;

    private int currentRole = 0;

    private void Awake()
    {
        ResetUI();
        isBlind = false;
        isDeaf = false;
    }

    public void SetRole(int id)
    {
        currentRole = id;
        switch (id)
        {
            case 0:
                break;
            case 1:
                blind.SetActive(true);
                break;
            case 2:
                mute.SetActive(true);
                break;
            case 3:
                deaf.SetActive(true);
                break;
            case 4:
                fail.SetActive(true);
                if (isBlind) 
                {
                     
                    playerAudioSource.clip = failSound;
                    if (!playerAudioSource.isPlaying)
                    {
                        playerAudioSource.Play();
                    }
                }
                break;
            case 5:
                succeed.SetActive(true);
                if (isBlind)
                {
                    playerAudioSource.clip = successSound;
                    if (!playerAudioSource.isPlaying)
                    {
                        playerAudioSource.Play();
                    }
                }
                break;
        }
    }

    public int GetRole()
    {
        return currentRole;
    }

    public void PlayerResult(bool side)
    {
        if (side)
        {
            ResetUI();
        }
    }

    public void ResetUI()
    {
        currentRole = 0;
        deaf.SetActive(false);
        mute.SetActive(false);
        blind.SetActive(false);
        fail.SetActive(false);
        succeed.SetActive(false);
    }
}
