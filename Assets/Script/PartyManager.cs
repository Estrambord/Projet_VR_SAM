using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    private int roundNb = 0;

    public TMP_Text instructionText;
    public TMP_Text roundText;
    public TMP_Text timerText;
    public AudioSource audioSource;
    public Round introduction;
    private bool introductionInit = false;
    public Round firstStep;
    public Round Game1;

    public Toggle player1CheckBox;
    public Toggle player2CheckBox;
    public Toggle player3CheckBox;

    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    public GameObject SlotPlayer1;
    public GameObject SlotPlayer2;
    public GameObject SlotPlayer3;
    public bool roleLocked = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {

        Debug.Log("Choose role");
    }

    void Update()
    {
        if(!introduction.completed) ChooseRole();

        if (introduction.completed && !firstStep.completed)
        {
            FirstStep();
        }
        else if(firstStep.completed)
        {
            PlayRound(Game1);
        }
    }

    void FirstStep()
    {
        PlayRound(firstStep);
        //Display Illustration
        //Check if Controller buttons are pressed
        if (Input.GetKeyDown(KeyCode.PageUp) && Input.GetKeyDown(KeyCode.PageDown) && Input.GetKeyDown(KeyCode.B)) firstStep.completed = true;
    }

    void ChooseRole()
    {
        if (!introductionInit)
        {
            PlayRound(introduction);
            panel1.SetActive(true); panel2.SetActive(true); panel3.SetActive(true);
        }

        if(SlotPlayer1 && SlotPlayer2 && SlotPlayer3 && !roleLocked)
        {
            roleLocked = true;
        }

        if (Input.GetKeyDown(KeyCode.B) && roleLocked)
        {
            panel1.SetActive(false); panel2.SetActive(false); panel3.SetActive(false);
            introduction.completed = true;
        }
    }

    void PlayRound(Round myRound)
    {
        if (myRound.isGame && !myRound.started)
        {
            roundNb++;
            roundText.text = roundNb.ToString();
            myRound.started = true;
        }
        else if (!myRound.started) 
        {
            roundText.text = "";
            myRound.started = true;
        }
        if (myRound.instructionText != null) instructionText.text = myRound.instructionText;
        if (myRound.instructionAudio) { audioSource.clip = myRound.instructionAudio; audioSource.Play(); }
        if (myRound.timer == 0f) timerText.text = "";
        else timerText.text = myRound.timer.ToString();
    }

    public void AddPlayer(GameObject myPlayer)
    {
        if (SlotPlayer1 == null) {
            SlotPlayer1 = myPlayer;
            Debug.Log("1");
            player1CheckBox.isOn = true;
        }
        else if (SlotPlayer2 == null)
        {
            SlotPlayer2 = myPlayer;
            Debug.Log("2");
            player2CheckBox.isOn = true;
        }
        else if (SlotPlayer3 == null)
        {
            SlotPlayer3 = myPlayer;
            Debug.Log("3");
            player3CheckBox.isOn = true;
        }
    }
}
