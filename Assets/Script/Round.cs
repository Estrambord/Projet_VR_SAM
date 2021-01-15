using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Round", menuName = "Round")]
public class Round : ScriptableObject
{
    public string instructionText;
    public AudioClip instructionAudio;
    public bool isGame;
    public float timer;
    public bool init = false;
    public bool started = false;
    public bool completed = false;
}
