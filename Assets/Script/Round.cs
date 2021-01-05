using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Round", menuName = "Round")]
public class Round : ScriptableObject
{
    public string instructions;
    public bool isGame;
    public float timer;
    public GameObject firstMur;
    public GameObject secondMur;
    public GameObject thirdMur;
}
