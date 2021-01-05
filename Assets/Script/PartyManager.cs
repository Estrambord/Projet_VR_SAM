using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartyManager : MonoBehaviour
{
    private int roundNb = 0;

    public TMP_Text instructionText;
    public TMP_Text roundText;
    public TMP_Text timerText;
    public Round introduction;

    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    public bool SlotPlayer1 = false;
    public bool SlotPlayer2 = false;
    public bool SlotPlayer3 = false;

    void Start()
    {
        ChooseRole();
    }

    /*
    void Update()
    {
        
    }
    */

    void ChooseRole()
    {
        PlayRound(introduction);
        panel1.SetActive(true); panel2.SetActive(true); panel3.SetActive(true);
        //if skeleton enter zone --> slotRole = ok
        //if slot1 && slot2 && slot3 == ok --> Press DownArrowToContinue
        //Valid Slots & Pass to next Step
    }

    void PlayRound(Round myRound)
    {
        instructionText.text = myRound.instructions;
        if (myRound.isGame) { roundNb++; roundText.text = roundNb.ToString(); }
        else roundText.text = "";
        if (myRound.timer > 0f) timerText.text = "";
        else timerText.text = myRound.timer.ToString();
    }
}
