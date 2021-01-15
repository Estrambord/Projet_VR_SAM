using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisible : MonoBehaviour
{
    public GameObject panel;
    public AudioSource _audioSource;
    public AudioClip _audioClipGauche;
    public AudioClip _audioClipDroite;
    public AudioClip _audioClipCentre;

    private bool audioLoaded = false;

    private float id;

    public float ID
    {
        get { return id; }
        set { id = value; SetAudioClip(); }
    }


    public void Awake()
    {
        GameObject myPanel = Instantiate(panel, transform.position, Quaternion.identity, transform);
        myPanel.transform.position = new Vector3(0, 10, transform.position.z);
    }

    void SetAudioClip()
    {
        if (id == 0) _audioSource.clip = _audioClipGauche;
        else if (id == 1) _audioSource.clip = _audioClipCentre;
        else if (id == 2) _audioSource.clip = _audioClipDroite;

        _audioSource.Play();
        audioLoaded = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !_audioSource.isPlaying) _audioSource.Play(); 
    }
    /*
     * case (0):
                                xOffsetInvisible = -16f;
                                break;
                            case (1):
                                xOffsetInvisible = 0f;
                                break;
                            case (2):
                                xOffsetInvisible = 16f;
                                break;

     */
}
