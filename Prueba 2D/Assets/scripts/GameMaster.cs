using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    public static GameMaster Instance { get; set; }

    public float minYValue = -20f;
    public GameObject playerPrefab;
    public GameObject HUD;
    public Vector2 initialSpawn = Vector2.zero; 
    public int currentPrioritySpawn = 0;

    private Vector2 currentRespawn;
    private GameObject player { get; set; }

    void Awake () {
        currentRespawn = initialSpawn;

        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
        
        if (player == null){
            player = Instantiate(playerPrefab, transform);
        }
            
    }
	
	
	void Update () {
        Vector2 playerPosition = player.transform.position;

        if (playerPosition.y < minYValue)
        {
            player.GetComponent<Player_Controller>().takeFallDamage(1);
            goToCurrentSpawn();
        }

	}

    private void HUDChecker()
    {
        Player_Controller pc = player.GetComponent<Player_Controller>();

    }

    public void handlePlayerDeath(float seconds)
    {
        Invoke ("destroyPlayerObject", seconds);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool updateSpawn(int prioritySpawn, Vector2 v)
    {
        if (prioritySpawn > currentPrioritySpawn){
            currentPrioritySpawn = prioritySpawn;
            currentRespawn = v;
            Debug.Log("NEW CHECKPOINT");
            return true;
        }
        return false;
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

}
