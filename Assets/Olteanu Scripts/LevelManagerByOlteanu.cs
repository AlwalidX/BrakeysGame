using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerByOlteanu : MonoBehaviour
{
    public bool isLevel1;
    public bool isLevel2;
    public GameObject fadeScreen, maskScreen, deadPlayer;
    public List<PlayerControllByOlteanu> playerControls = new List<PlayerControllByOlteanu>();
    public GameObject[] players;
    public Transform spawnPoint, newSpawnPoint;
    public float gameStartsTime;
    public float gameStartsCounter;
    public Animator animSeringe;
    public bool isGrounded;
    public bool spawnPlayer, gameIsOver, youWin, nearSpawnPoint;
    public int thePlayerNumber;
    public SpriteRenderer theSpriteColor;
    private GameObject theDeadPlayer;
    public int deathLimit = 20;
    public TextMeshProUGUI limitText;
    public GameObject gameOver, youWinPanel;



    private void Start()
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlayBgm(0);
        ResetPlayer();
        animSeringe = FindObjectOfType<SeringeSpawner>().GetComponent<Animator>();
        spawnPoint = FindObjectOfType<SeringeSpawner>().seringeSpawnPoint;
        maskScreen.SetActive(true);
        fadeScreen.SetActive(true);
        fadeScreen.GetComponent<Animator>().SetBool("isBlack", true);
        gameStartsCounter = gameStartsTime;
        var theCells = FindObjectsOfType<EnvironmentColourControl>();
        foreach (EnvironmentColourControl cell in theCells)
        {
            if(isLevel1)
            {
                cell.theSprite.color = new Color(1, 0.8f, 0.6f, 1);
            }

            if(isLevel2)
            {
                cell.theSprite.color = new Color(0.5f, 0.01f, 0.01f, 1);
            }
        }
    }

    private void Update()
    {
        if (deathLimit == 0)
        {
          Debug.Log("IT IS 0");
        }
        limitText.SetText(deathLimit.ToString()); 

        if(gameStartsCounter > 0)
        {
            gameStartsCounter -= Time.deltaTime;
            if(gameStartsCounter <= 0)
            {
                maskScreen.SetActive(false);
                fadeScreen.GetComponent<Animator>().SetBool("isBlack", false);
                StartCoroutine(StartSeringeCo());
            }
        }

        var currentPlayer = FindObjectOfType<PlayerControllByOlteanu>();
        if (currentPlayer != null)
        {
            isGrounded = currentPlayer.isGrounded;
        }

        if (Input.GetKeyDown(KeyCode.L) && isGrounded)
        {
            newSpawnPoint = FindObjectOfType<PlayerControllByOlteanu>().transform;
            animSeringe.SetBool("isIn", false);
            StartCoroutine(SetSeringeNewSapwnPointCo());
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            InstantKill();
        }

        if (gameIsOver)
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene("Olteanu Scene");
                
            }

            if(Input.GetKeyDown(KeyCode.N))
            {
                Application.Quit();
            }
        }

        if (youWin)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene("Olteanu Scene");

            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                Application.Quit();
            }
        }

        if (deathLimit <= 0)
        {
            GameOver();
        }

        
    }

    IEnumerator StartSeringeCo()
    {
        yield return new WaitForSeconds(1);
        animSeringe.SetBool("isIn", true);
        yield return new WaitForSeconds(2);
        AddPlayer();
    }

    IEnumerator SetSeringeNewSapwnPointCo()
    {
        Vector2 adjustSeringePos = new Vector2(newSpawnPoint.position.x - 1f, newSpawnPoint.position.y + 1.8f);
        yield return new WaitForSeconds(2f);
        animSeringe.gameObject.transform.position = adjustSeringePos;
        yield return new WaitForSeconds (1f);
        animSeringe.SetBool("isIn", true);

    }

    public void KillPlayer()
    {
        if (spawnPlayer)
        {
            var existingPlayer = FindObjectOfType<PlayerControllByOlteanu>();
            if (existingPlayer == null)
            {
                
                Instantiate(players[thePlayerNumber], spawnPoint.position, spawnPoint.rotation);
                StartCoroutine(KillPlayerCameraDellayCo());
            }
            else if (existingPlayer != null)
            {
                Destroy(existingPlayer.gameObject);
                Instantiate(players[thePlayerNumber], spawnPoint.position, spawnPoint.rotation);
                StartCoroutine(KillPlayerCameraDellayCo());
            }
            spawnPlayer = false;
        }
    }


    IEnumerator KillPlayerCameraDellayCo()
    {
        var existingPlayer = FindObjectOfType<PlayerControllByOlteanu>();
        existingPlayer.canMove = false;
        yield return new WaitForSeconds(1);
        existingPlayer.canMove = true;
        FindObjectOfType<CameraController>().thePlayer = FindObjectOfType<PlayerControllByOlteanu>().transform;

    }
    public void ResetPlayer()
    {
        thePlayerNumber = Random.Range(0, players.Length);
    }
   
    public void AddPlayer()
    {
          
        Instantiate(players[thePlayerNumber], spawnPoint.position, spawnPoint.rotation);
        FindObjectOfType<CameraController>().thePlayer = FindObjectOfType<PlayerControllByOlteanu>().transform;
    }


    public void AddDeadPlayer()
    {
        AudioManager.instance.PlaySfx(1);
        var existingPlayer = FindObjectOfType<PlayerControllByOlteanu>();
        float minValue = Random.Range(.6f, 1f);
        float maxValue = minValue + Random.Range(0, .2f);
        theDeadPlayer = Instantiate(deadPlayer, existingPlayer.transform.position, existingPlayer.transform.rotation);
        theDeadPlayer.gameObject.transform.localScale = new Vector2(minValue, maxValue);
        deathLimit = deathLimit - 1;
        
    }

    public void AddDeadPlayerColour()
    {
        theDeadPlayer.GetComponentInChildren<SpriteRenderer>().color = theSpriteColor.color;        
    }

    public void SetNewSpawnPoint()
    {
        if (isGrounded)
        {
            newSpawnPoint = FindObjectOfType<PlayerControllByOlteanu>().transform;
            animSeringe.SetBool("isIn", false);
            StartCoroutine(SetSeringeNewSapwnPointCo());
        }
    }

    public void GameOver()
    {
        gameIsOver = true;
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void YouWonTheGame()
    {
        youWin = true;
        youWinPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void InstantKill()
    {
       
        if (!nearSpawnPoint)
        {            
            ResetPlayer();
            spawnPlayer = true;
            AddDeadPlayer();
            AddDeadPlayerColour();
            KillPlayer();
            nearSpawnPoint = true;
        }
    }

    
}
