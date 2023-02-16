using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerByOlteanu : MonoBehaviour
{
    public bool isLevel1;
    public bool isLevel2;
    public GameObject fadeScreen, maskScreen, thePlayer;
    public List<PlayerControllByOlteanu> playerControls = new List<PlayerControllByOlteanu>();
    //public GameObject[] players;
    public Transform spawnPoint, newSpawnPoint;
    public float gameStartsTime;
    public float gameStartsCounter;
    public Animator animSeringe;
    public bool isGrounded;

    private void Start()
    {
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

        if (Input.GetKeyDown(KeyCode.L))
        {
            var exixtingPlayer = FindObjectOfType<PlayerControllByOlteanu>();
            if (exixtingPlayer == null)
            {
                //Destroy(exixtingPlayer.gameObject);
                Instantiate(thePlayer, spawnPoint.position, spawnPoint.rotation);
                FindObjectOfType<CameraController>().thePlayer = FindObjectOfType<PlayerControllByOlteanu>().transform;
            }
            else if(exixtingPlayer!= null)
            {
                Destroy(exixtingPlayer.gameObject);
                Instantiate(thePlayer, spawnPoint.position, spawnPoint.rotation);
                FindObjectOfType<CameraController>().thePlayer = FindObjectOfType<PlayerControllByOlteanu>().transform;
            }

            //playerControls.Add(thePlayer.GetComponent<PlayerControllByOlteanu>());
            
        }

        var currentPlayer = FindObjectOfType<PlayerControllByOlteanu>();
        if (currentPlayer != null)
        {
            isGrounded = currentPlayer.isGrounded;
        }

        if (Input.GetKeyDown(KeyCode.K) && isGrounded)
        {
            newSpawnPoint = FindObjectOfType<PlayerControllByOlteanu>().transform;
            animSeringe.SetBool("isIn", false);
            StartCoroutine(SetSeringeNewSapwnPointCo());
        }
    }

    IEnumerator StartSeringeCo()
    {
        yield return new WaitForSeconds(1);
        animSeringe.SetBool("isIn", true);
    }

    IEnumerator SetSeringeNewSapwnPointCo()
    {
        Vector2 adjustSeringePos = new Vector2(newSpawnPoint.position.x - 1f, newSpawnPoint.position.y + 1.2f);
        yield return new WaitForSeconds(2f);
        animSeringe.gameObject.transform.position = adjustSeringePos;
        yield return new WaitForSeconds (1f);
        animSeringe.SetBool("isIn", true);

    }
}
