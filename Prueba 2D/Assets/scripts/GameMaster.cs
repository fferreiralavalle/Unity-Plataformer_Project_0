using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    public static GameMaster Instance { get; set; }

    public float minYValue = -20f;
    public GameObject playerPrefab;
    public GameObject HUD;
    public Vector2 initialSpawn = Vector2.zero;
    public int currentPrioritySpawn = 0;

    private Vector3 currentRespawn;
    private GameObject player { get; set; }
    private int playerCoins = 0;
    private int playerCoinsAtLevelStart = 0;

    void Awake()
    {

        if (player == null && playerPrefab != null)
        {
            player = Instantiate(playerPrefab, transform);
        }
        Debug.Log("This = " + this + " / Instance = " + (Instance == null));
        if (Instance == null)
        {
            Instance = this;
            initialSpawn = transform.position;
            currentRespawn = initialSpawn;
            Debug.Log("CurrentRespawn = " + Instance.currentRespawn);
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            //Does this when entering a new level, updates spawns and self position
            Debug.Log("New GM found, updating...");
            Instance.updateInitialSpawn(transform.position);
            Instance.currentRespawn = Instance.initialSpawn;
            Debug.Log("CurrentRespawn = " + Instance.currentRespawn);
            Instance.transform.position = transform.position;
            if (Instance.player != null)
                Instance.player.transform.localPosition = Vector3.zero;
            //Updates initial level coins to current coins
            updateInitialCoins();
            Destroy(gameObject);
        }

    }

    void Update()
    {
        Vector2 playerPosition = player.transform.position;

        if (playerPosition.y < minYValue)
        {
            handlePlayerFallToDeath();
        }

    }

    public void handlePlayerDeath(float seconds)
    {
        Invoke("destroyPlayerObject", seconds);
    }

    public void handlePlayerFallToDeath()
    {
        player.GetComponent<Player_Controller>().fallToDeath();
        player.GetComponent<Player_Controller>().takeFallDamage(1);
        player.GetComponent<Player_Controller>().setHasFallenToFalseAfterXSeconds(2f);
        Invoke("goToCurrentSpawn", 2f);
    }

    public void destroyPlayerObject()
    {
        player.GetComponent<Player_Controller>().playDeadAnimation();
        showGameOver();
    }

    public void revivePlayer()
    {
        Debug.Log("Player revived!");
        player.GetComponent<Player_Controller>().revive();
    }

    public void resetLevel()
    {
        setCoinsTo(playerCoinsAtLevelStart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool updateSpawn(int prioritySpawn, Vector3 v)
    {
        if (prioritySpawn > currentPrioritySpawn)
        {
            currentPrioritySpawn = prioritySpawn;
            currentRespawn = v;
            Debug.Log("NEW CHECKPOINT");
            return true;
        }
        return false;
    }

    public void updateInitialSpawn(Vector3 v)
    {
        initialSpawn = v;
    }

    public void showGameOver()
    {
        Debug.Log("Showing game over");
        HUD_Manager.Instance.showGameOverPanel();
    }

    public void goToIntialSpawn()
    {
        player.transform.position = initialSpawn;
    }

    public void goToCurrentSpawn()
    {
        player.transform.position = currentRespawn;
    }

    public GameObject getPlayer()
    {
        return player;
    }

    public void setPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }

    public void gainCoins(int coins)
    {
        if (coins < 0)
            return;
        playerCoins += coins;
        if (HUD_Manager.Instance != null)
            HUD_Manager.Instance.updateCoins(playerCoins);
    }

    public void loseCoins(int coins)
    {
        if (coins > playerCoins)
            coins = playerCoins;
        playerCoins -= coins;
        HUD_Manager.Instance.updateCoins(playerCoins);
    }

    public void setCoinsTo(int coins)
    {
        playerCoins = coins;
        if (HUD_Manager.Instance != null)
            HUD_Manager.Instance.updateCoins(playerCoins);
    }

    public void updateInitialCoins()
    {
        playerCoinsAtLevelStart = playerCoins;
    }

    public void goToNextLevelScreen()
    {
        StartCoroutine(goToNextLevelCoroutine());
    }

    private IEnumerator goToNextLevelCoroutine()
    {
        StartCoroutine(GetComponent<MyLevelManager>().goToNextLevelScreen());
        HUD_Manager.Instance.fadeOut();
        SoundManager.instance.fadeOutMusic();
        player.GetComponent<Player_Controller>().animPlayWinPose();
        player.GetComponent<Player_Controller>().disableMovement();

        yield return new WaitUntil(() => GetComponent<MyLevelManager>().isLevelLoaded());

        SoundManager.instance.fadeInMusic();
        HUD_Manager.Instance.fadeIn();
        HUD_Manager.Instance.showNextLevelPanel(playerCoins);
        HUD_Manager.Instance.hidePlayerHealth();
        HUD_Manager.Instance.hideCoins();
        player.SetActive(false);
    }

    public void beginNextLevel()
    {
        StartCoroutine(beginNextLevelCoroutine());
    }

    private IEnumerator beginNextLevelCoroutine()
    {
        HUD_Manager.Instance.showPlayerHealth();
        HUD_Manager.Instance.showCoins();
        HUD_Manager.Instance.fadeOut();
        SoundManager.instance.fadeOutMusic();
        updateInitialCoins();

        string nextLevelName = GetComponent<MyLevelManager>().getNextLevelNameFromLevelName(GetComponent<MyLevelManager>().getPreviousLevelName());
        StartCoroutine(GetComponent<MyLevelManager>().goToLevelAfterXSeconds(nextLevelName, 5f));
        yield return new WaitUntil(() => GetComponent<MyLevelManager>().isLevelLoaded());

        SoundManager.instance.setMusicVolumeToStandar();
        HUD_Manager.Instance.fadeIn();
        HUD_Manager.Instance.hideNextLevelPanel();
        player.SetActive(true);
        player.GetComponent<Player_Controller>().animPlayDefault();
        player.GetComponent<Player_Controller>().enableMovement();
    }

    public void restorePlayerHealth(float healthRestored)
    {
        player.GetComponent<Player_Controller>().restoreHealth(healthRestored);
    }


}
