using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class BossGameManager : MonoBehaviour
{
    public GameObject DeathScreen;
    public GameObject PauseScreen;
    public static BossGameManager Instance { get; private set; }
    AudioManagerScript AudioManagerscript;

    private BossPlayer bplayer;

    private float respawnTimer = 0.5f;
    public float playerInvincibleFrames = 0;

    private bool GameIsPaused = false;
    public bool playerDead = false;
    GameObject boss;    

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
        AudioManagerscript = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        AudioManagerscript.Instance.StopMusic("Theme1");
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

        BackToOriginalGame();

        if (playerInvincibleFrames > 0)
            playerInvincibleFrames -= Time.deltaTime;
        else
            playerInvincibleFrames = Mathf.Max(0, playerInvincibleFrames);

        if (Input.GetKeyDown(KeyCode.Escape) && !GameIsPaused)
        {
            PauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameIsPaused)
        {
            UnPauseMenu();
        }
    }

    private void SetLives(int _lives)
    {
        lives = _lives;
    }

    public void OnPlayerKilled(BossPlayer _bPlayer)
    {

        if(playerInvincibleFrames <= 0)
        {
            playerDead = true;

            lives--;

            _bPlayer.gameObject.SetActive(false);

            playerInvincibleFrames = 3;
        }
               
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


    void BackToOriginalGame()
    {
        boss = GameObject.Find("Boss");

        if (boss == null)
        {
            GameOver();
        }
        else if(lives == 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        DeathScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    void PauseMenu()
    {
        GameIsPaused = true;
        PauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    public void UnPauseMenu()
    {
        GameIsPaused = false;
        PauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}
