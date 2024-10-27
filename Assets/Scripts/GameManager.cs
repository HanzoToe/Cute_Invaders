using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public GameObject DeathScreen;
    public GameObject PauseScreen;
    private SugarRush sugarRushScript;
    private SugarRushBar sugarRushBar; 
    public static GameManager Instance { get; private set; }

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;

    private float respawnTimer = 0.5f;

    private bool playerDead = false;
    private bool GameIsPaused = false;

    int mysteryShipHit = 0; 

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
        sugarRushScript = FindObjectOfType<SugarRush>();
        sugarRushBar = FindObjectOfType<SugarRushBar>();

        NewGame();
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

        ActivateBossFight();

        if (Input.GetKeyDown(KeyCode.Escape) && !GameIsPaused)
        {
            PauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameIsPaused)
        {
            UnPauseMenu();
        }
    }

    public void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
        DeathScreen.SetActive(false);
        UnPauseMenu();
        sugarRushScript.startCharge = 0;
        sugarRushBar.RemoveBar();
        mysteryShipHit = 0; 
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
        DeathScreen.SetActive(true);
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
       
        lives--;
        
        player.gameObject.SetActive(false);
    }


    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimer);

        if (lives > 0 && player != null)
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
        mysteryShipHit += 1; 
    }

    public void OnBoundaryReached()
    {
        if (invaders.gameObject.activeSelf)
        {
            invaders.gameObject.SetActive(false);
            OnPlayerKilled(player);
        }
    }


    public void ActivateBossFight()
    {
        if (mysteryShipHit == 3)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void PauseMenu()
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
}
