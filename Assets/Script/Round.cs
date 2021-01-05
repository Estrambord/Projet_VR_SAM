using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Round", menuName = "Round")]
public class Round : ScriptableObject
{
    public string instructionText;
    public AudioClip instructionAudio;
    public bool isGame;
    public bool started = false;
    public float timer;
    public GameObject firstMur;
    public GameObject secondMur;
    public GameObject thirdMur;
    public bool completed = false;

}
