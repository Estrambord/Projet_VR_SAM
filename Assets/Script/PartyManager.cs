using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PartyManager : MonoBehaviour
{
    public TrackMoving mySecondGame;

    public AudioSource musicSource;
    public AudioClip gameOverClip;

    private int roundNb = 0;
    private int score = 0;
    public int health = 20;

    public int gateAmount;

    public TMP_Text instructionText;
    public TMP_Text roundText;
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public GameObject gameOver;
    private bool gameLost = false;

    private bool stepInit = false;

    public Toggle player1CheckBox;
    public Toggle player2CheckBox;
    public Toggle player3CheckBox;

    public GameObject panelChooseRole;
    public GameObject manette;
    public GameObject manette_left;
    public GameObject manette_right;
    public GameObject manette_down;
    public GameObject gameTitle;

    public Player SlotPlayer1;
    public Player SlotPlayer2;
    public Player SlotPlayer3;
    private List<Player> myPlayerList = new List<Player>();
    bool roleLocked = false;
    bool gameLaunched = false;
    [SerializeField]
    bool secondGameLaunched = false;

    public GameObject gate;
    public Transform gateSpawn;
    GameObject currentGate;

    private void Awake()
    {
        roundText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        healthText.gameObject.SetActive(false);
        scoreText.text = "Score : 0";
    }

    void Update()
    {
        if (!roleLocked) ChooseRole();

        if (roleLocked && !gameLaunched) FirstStep();

        if (gameLaunched && !gameLost)
        {
            PlayRound();
        }
        else if(gameLost)
        {
            if (stepInit) 
            { 
                musicSource.Stop();
                musicSource.clip = gameOverClip;
                musicSource.loop = false;
                musicSource.volume = 0.3f;
                musicSource.Play();
                stepInit = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void ChooseRole()
    {
        if (!stepInit)
        {
            instructionText.text = "Waiting for 3 player :";
            panelChooseRole.SetActive(true);
            stepInit = true;
        }

        if (SlotPlayer1 && SlotPlayer2 && SlotPlayer3)
        {
            instructionText.text = "Press down button to continue";
            manette.SetActive(true);
            manette_down.SetActive(true);
            //SetPlayerRole(); A FAIRE

            if (Input.GetKeyDown(KeyCode.B))
            {
                SetPlayerRole();
                panelChooseRole.SetActive(false);
                manette.SetActive(false);
                manette_down.SetActive(false);
                ResetPlayerUI();
                stepInit = false;
                roleLocked = true;
            }
        }
    }

    void FirstStep()
    {
        if (!stepInit)
        {
            instructionText.text = "Everybody press one of the 3 buttons on the controller";
            manette.SetActive(true);
            stepInit = true;
        }

        if (Input.GetKeyDown(KeyCode.PageUp) && manette_left.activeSelf == false) //Left
        {
            manette_left.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.PageDown) && manette_right.activeSelf == false) //Right
        {
            manette_right.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.B) && manette_down.activeSelf == false) //Down
        {
            manette_down.SetActive(true);
        }

        if (manette_left.activeSelf == true && manette_right.activeSelf == true && manette_down.activeSelf == true)
        {
            manette.SetActive(false);
            manette_left.SetActive(false);
            manette_right.SetActive(false);
            manette_down.SetActive(false);
            gameTitle.SetActive(false);
            gameLaunched = true;
            stepInit = false;
        }
    }

    void PlayRound()
    {
        if (!secondGameLaunched)
        {
            if (!stepInit)
            {
                ResetPlayerUI();
                roundNb++;
                roundText.text = roundNb.ToString();
                roundText.gameObject.SetActive(true);
                scoreText.gameObject.SetActive(true);
                currentGate = Instantiate(gate, gateSpawn.transform.position, Quaternion.identity, null);
                currentGate.transform.Rotate(-90f, 0f, 0f);
                instructionText.text = "Avoid touching walls, good Luck !";
                stepInit = true;
            }
            if (!currentGate)
            {
                stepInit = false;
                if (roundNb > (gateAmount - 1)) secondGameLaunched = true;
            }
        }
        else
        {
            if (!stepInit)
            {
                if (currentGate) Destroy(currentGate);
                ResetPlayerUI();
                roundNb++;
                instructionText.text = "Dogde obstacles";
                mySecondGame.gameOn = true;
                healthText.gameObject.SetActive(true);
                stepInit = true;
            }
        }
    }

    public void AddPlayer(Player myPlayer)
    {
        if (SlotPlayer1 == null)
        {
            SlotPlayer1 = myPlayer;
            myPlayer.SetRole(1);
            myPlayerList.Add(myPlayer);
            player1CheckBox.isOn = true;
        }
        else if (SlotPlayer2 == null)
        {
            SlotPlayer2 = myPlayer;
            myPlayerList.Add(myPlayer);
            myPlayer.SetRole(2);
            player2CheckBox.isOn = true;
        }
        else if (SlotPlayer3 == null)
        {
            SlotPlayer3 = myPlayer;
            myPlayer.SetRole(3);
            myPlayerList.Add(myPlayer);
            player3CheckBox.isOn = true;
        }
        else
        {
            Debug.Log("Party Already full !");
        }

    }

    public void ResetPlayerUI()
    {
        foreach (var player in myPlayerList)
        {
            player.ResetUI();
        }
    }

    private void SetPlayerRole()
    {
        bool firstTurn = true;
        Player playerOnTheLeft = SlotPlayer1;
        foreach (var player in myPlayerList)
        {
            if (!firstTurn && player.transform.position.x < playerOnTheLeft.transform.position.x) playerOnTheLeft = player;
            else firstTurn = false;
        }
        playerOnTheLeft.Blind = true;
        firstTurn = true;
        Player playerOnTheRight = SlotPlayer1;
        foreach (var player in myPlayerList)
        {
            if (!firstTurn && player.transform.position.x > playerOnTheLeft.transform.position.x) playerOnTheRight = player;
            else firstTurn = false;
        }
        playerOnTheRight.Deaf = true;
        foreach (var player in myPlayerList)
        {
            if (!player.Blind && !player.Deaf) player.Mute = true;
        }
    }

    public void AddPoint(int amount)
    {
        score += amount;
        scoreText.text = "Score : " + score;
    }

    public void DelLife()
    {
        health--;
        healthText.text = "Lives : " + health;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        gameLost = true;
    }
}

