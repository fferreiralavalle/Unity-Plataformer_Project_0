using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Spawn : MonoBehaviour {

    public GameObject enemy;                // The enemy prefab to be spawned.
    public AudioClip spawnSound;            //Spawning sound
    public AudioClip spawnDownSound;

    public float spawnTime = 4f;            // How long between each spawn.
    public float initialDelay = 0f;
    public int spawnCap = 6;
    //public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public List<GameObject> prefabSpawnedList;

    protected AudioSource audioSource;
    protected bool isDisabled = false;

    public virtual void Spawn()
    {
        // Find a random index between zero and one less than the number of spawn points.
        //int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        for (int i = 0; i < prefabSpawnedList.Count; i++)
        {
            if (prefabSpawnedList[i] == null)
                prefabSpawnedList.RemoveAt(i);
        }
        if (!isDisabled && prefabSpawnedList.Count < spawnCap)
        {
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.transform.position = transform.position;
            newEnemy.transform.Rotate(Vector3.forward, Mathf.PI);
            prefabSpawnedList.Add(newEnemy);
            afterSpawn(newEnemy);
        }

    }

    public virtual void afterSpawn(GameObject spawned)
    {

    }

    public virtual void killAllSpawns()
    {
        foreach (GameObject go in prefabSpawnedList)
        {
            Destroy(go);
        }
    }
}
