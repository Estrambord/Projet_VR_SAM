using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moule : MonoBehaviour
{
    public AudioClip instructionAudioClip;
    public AudioSource myAudioSource;

    private void Awake()
    {
        if(instructionAudioClip)
        {
            myAudioSource.clip = instructionAudioClip;
        }
    }

    private void Start()
    {
        if(myAudioSource) myAudioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject myParent = other.transform.parent.gameObject;
            myParent.GetComponentInChildren<Player>().SetRole(4);
            //other.GetComponentInChildren<Player>().SetRole(4);
            Destroy(this.gameObject);
        }
    }
}
