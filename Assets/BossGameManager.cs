using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class BossGameManager : MonoBehaviour
{
    public GameObject DeathScreen;
    public GameObject PauseScreen;
    public static BossGameManager Instance { get; private set; }

    private BossPlayer bplayer;

    private float respawnTimer = 0.5f;

    private bool playerDead = false;

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
        bplayer = FindObjectOfType<BossPlayer>();
        SetLives(3);
    }

    private void Update()
    {

        if (lives <= 0)
        {
            GameOver();

        }

        if (playerDead)
        {
            StartCoroutine("Respawn");
        }

        Debug.Log(lives);
    }


    private void GameOver()
    {
        DeathScreen.SetActive(true);
    }


    private void SetLives(int _lives)
    {
        lives = _lives;
    }

    public void OnPlayerKilled(BossPlayer _bPlayer)
    {
        playerDead = true;

        lives--;

        _bPlayer.gameObject.SetActive(false);
        
            
    }


    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimer);

        if (lives > 0 && bplayer != null)
        {
            Vector3 position = bplayer.transform.position;
            position.x = 0f;
            bplayer.transform.position = position;
            bplayer.gameObject.SetActive(true);
            playerDead = false;
        }

        yield return null;

    }


    public void PauseMenu()
    {
        PauseScreen.SetActive(true);
    }
}
