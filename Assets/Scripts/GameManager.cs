using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using System.Threading;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;

    private float respawnTimer = 0.5f;

    private bool playerDead = false; 

    //Används ej just nu, men ni kan använda de senare
    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 0;

    private void Awake()
    {


        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        invaders = FindObjectOfType<Invaders>();
        mysteryShip = FindObjectOfType<MysteryShip>();
        bunkers = FindObjectsOfType<Bunker>();

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
        else if (lives <= 0)
        {
            GameOver();

        }
            
        if (playerDead)
        {
            StartCoroutine("Respawn");
        }

    }

    private void NewGame()
    {

        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);
        mysteryShip.gameObject.SetActive(true);

        for (int i = 0; i < bunkers.Length; i++)
        {
            bunkers[i].ResetBunker();
        }

        Respawn();
    }


    private void GameOver()
    {
        invaders.gameObject.SetActive(false);
        mysteryShip.gameObject.SetActive(false);      
    }

    private void SetScore(int score)
    {
        
    }

    private void SetLives(int _lives)
    {
        lives = _lives;
    }

    public void OnPlayerKilled(Player player)
    {
        playerDead = true; 

        player.gameObject.SetActive(false);
        lives--;
        StartCoroutine("Respawn");
    }


    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimer);

        if (lives > 0)
        {            
            Vector3 position = player.transform.position;
            position.x = 0f;
            player.transform.position = position;
            player.gameObject.SetActive(true);
            playerDead = false; 
        }

        yield return null; 

    }

    public void OnInvaderKilled(Invader invader)
    {
        invader.gameObject.SetActive(false);

       

        if (invaders.GetInvaderCount() == 0)
        {
            NewRound();
        }
    }

    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        mysteryShip.gameObject.SetActive(false);
    }

    public void OnBoundaryReached()
    {
        if (invaders.gameObject.activeSelf)
        {
            invaders.gameObject.SetActive(false);
            OnPlayerKilled(player);
        }
    }

}
