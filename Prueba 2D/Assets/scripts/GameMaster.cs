using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster Instance { get; set; }

    public float minYValue = -20f;
    public GameObject player;
    public GameObject HUD;
    public Vector2 currentRespawn = Vector2.zero;
    public int currentPrioritySpawn = 0;

    void Awake () {
        Instance = this;
	}
	
	
	void Update () {
        Vector2 playerPosition = player.transform.position;

        if (playerPosition.y < minYValue)
        {
            player.GetComponent<Player_Controller>().takeFallDamage(1);
            player.transform.position = currentRespawn;
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
        Destroy(player);
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
}
